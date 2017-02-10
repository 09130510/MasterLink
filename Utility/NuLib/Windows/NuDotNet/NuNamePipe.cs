using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace NuDotNet.IPC
{
    /* *
     * 資料傳輸時, 內部格式為  
     * int + byte[]
     * 總長度 : 4 + byte.length
     * int 代表 byte array 長度
     * */

    public class NuIpcServer
    {
        private class internal_obj
        {
            public NamedPipeServerStream m_obj = null;
            public AutoResetEvent m_evt = null;
            public int m_readn = 0;

            public internal_obj(NamedPipeServerStream svr, AutoResetEvent evt)
                               
            {
                m_obj = svr;
                m_evt = evt;
            }
        }

        private int m_max_clients = 50;
        private bool m_work = false;
        private string m_pipe_name = "";
        
        // key : thread id, value : Thread
        private Dictionary<int, Thread> m_dic_thds = null;
        private List<NamedPipeServerStream> m_lst_svrs = null;

        #region --  construct/destruct  --
        /// <summary>
        /// IPC Server (Named Pipe) 建構式, 預設最大連線數 50
        /// </summary>
        /// <param name="sName">Named Pipe 名稱</param>
        public NuIpcServer(string sName)
        {
            m_pipe_name = sName;
        }

        /// <summary>
        /// IPC Server (Named Pipe) 建構式
        /// </summary>
        /// <param name="sName">Named Pipe 名稱</param>
        /// <param name="iMaxClients">最大連線數</param>
        public NuIpcServer(string sName, int iMaxClients)
        {
            m_pipe_name = sName;
            m_max_clients = iMaxClients;
            m_lst_svrs = new List<NamedPipeServerStream>(iMaxClients);
        }

        /// <summary>
        /// 解構式
        /// </summary>
        ~NuIpcServer()
        {
        }
        #endregion

        #region --  private functions  --
        private void _create_work_thd()
        {
            Thread thd = null;
            thd = new Thread(_work_flow);
            thd.IsBackground = true;
            thd.Start();
            lock (m_dic_thds)
            {
                m_dic_thds.Add(thd.ManagedThreadId, thd);
            }
        }

        private void _remove_work_thd(int thread_id)
        {
            lock (m_dic_thds)
            {
                m_dic_thds.Remove(thread_id);
            }
        }

        private void _connected_cb(IAsyncResult result)
        {
            internal_obj obj = (internal_obj)result.AsyncState;
            NamedPipeServerStream svr = (NamedPipeServerStream)obj.m_obj;
            svr.EndWaitForConnection(result);
            obj.m_evt.Set();
        }

        private void _read_cb(IAsyncResult result)
        {
            internal_obj obj = (internal_obj)result.AsyncState;
            NamedPipeServerStream svr = (NamedPipeServerStream)obj.m_obj;
            obj.m_readn= svr.EndRead(result);
            obj.m_evt.Set();
        }

        private void _work_flow()
        {
            int tid = Thread.CurrentThread.ManagedThreadId;
            NamedPipeServerStream pipe_svr = null;

            pipe_svr = new NamedPipeServerStream(m_pipe_name, PipeDirection.InOut, m_max_clients,
                PipeTransmissionMode.Byte, PipeOptions.Asynchronous);

            try
            {
                byte[] buffer = new byte[2048];
                int iDataSz = 0;
                MemoryStream mStream = new MemoryStream();

                AutoResetEvent evt = new AutoResetEvent(false);
                internal_obj cb_obj = new internal_obj(pipe_svr, evt);
                AsyncCallback read_cb = new AsyncCallback(_read_cb);

                int iReadN = 0;
                int iNeedN = 0;

                pipe_svr.BeginWaitForConnection(new AsyncCallback(_connected_cb), cb_obj);
                if (m_work)
                {
                    lock (m_lst_svrs)
                        m_lst_svrs.Add(pipe_svr);
                }
                evt.WaitOne();

                if (m_work)
                {
                    if (m_dic_thds.Count < m_max_clients)
                        _create_work_thd();
                }

                while (m_work)
                {
                    mStream.SetLength(0);

                    // get data size
                    iNeedN = 4;
                    iReadN = 0;
                    while (iReadN != 4)
                    {
                        pipe_svr.BeginRead(buffer, iReadN, iNeedN, read_cb, cb_obj);
                        evt.WaitOne();

                        if (cb_obj.m_readn == 0)
                            return;

                        iNeedN -= cb_obj.m_readn;
                        iReadN += cb_obj.m_readn;
                    }

                    iDataSz = BitConverter.ToInt32(buffer, 0);
                    if (iDataSz > 0)
                    {
                        iNeedN = iDataSz;
                        iReadN = 0;
                        while (iReadN != iDataSz)
                        {
                            pipe_svr.BeginRead(buffer, iReadN, iNeedN, read_cb, cb_obj);
                            evt.WaitOne();

                            mStream.Write(buffer, iReadN, cb_obj.m_readn);

                            iNeedN -= cb_obj.m_readn;
                            iReadN += cb_obj.m_readn;
                        }
                        
                        if (OnDataArrivedEv != null)
                            OnDataArrivedEv(mStream.ToArray());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (ThreadAbortException) {  }
            catch (ThreadInterruptedException) { }
            catch (IOException) { }
            finally
            {
                if (pipe_svr.IsConnected)
                {
                    pipe_svr.Disconnect();
                    pipe_svr.Close();
                }

                _remove_work_thd(tid);
                
                pipe_svr = null;
            }
            
            return;

        }
        #endregion

        /// <summary>
        /// 啟動 IPC Server Listen
        /// </summary>
        public void Start()
        {
            m_work = true;
            if (m_dic_thds == null)
                m_dic_thds = new Dictionary<int, Thread>(m_max_clients);

            _create_work_thd();
        }

        /// <summary>
        /// 停止 Listen
        /// </summary>
        public void Stop()
        {
            m_work = false;
            List<Thread> thds = new List<Thread>(m_dic_thds.Values);

            foreach (NamedPipeServerStream svr in m_lst_svrs)
            {
                if (!svr.IsConnected)
                {
                    NamedPipeClientStream cli = new NamedPipeClientStream(m_pipe_name);
                    cli.Connect(1000);
                }
            }

            foreach (Thread thd in thds)
            {
                if (thd.IsAlive)
                {
                    thd.Abort();
                    thd.Join();
                }
            }
            thds.Clear();
            m_lst_svrs.Clear();
        }

        public delegate void dlgOnDataArrived(byte[] bData);
        /// <summary>
        /// 收到資料的處理事件
        /// </summary>
        public event dlgOnDataArrived OnDataArrivedEv;

    }

    public class NuIpcClient
    {
        private string m_ipc_name = "";
        private NamedPipeClientStream m_ipc_cli;

        private Thread m_connect_thd = null;
        private object m_connect_thd_obj = null;
        private int m_timeout_sec = 0;

        private bool m_connected_flag = false;
        private bool m_auto_reconnect = false;

        #region --  construct/destruct  --
        /// <summary>
        /// IPC Client (Named Pipe) 建構式
        /// </summary>
        /// <param name="sName">Named Pipe 名稱</param>
        public NuIpcClient(string sName)
        {
            m_ipc_name = sName;
        }

        /// <summary>
        /// IPC Client (Named Pipe) 解構式
        /// </summary>
        ~NuIpcClient()
        {
        }
        #endregion

        #region --  public property  --
        /// <summary>
        /// 連線狀況
        /// </summary>
        public bool IsConnected { get { return m_connected_flag; } }
        /// <summary>
        /// 自動重連開關, 預設=false
        /// </summary>
        public bool AutoReconnect
        {
            get { return m_auto_reconnect; }
            set { m_auto_reconnect = value; }
        }
        #endregion

        #region --  private functions  --
        private void _connect_work()
        {
            int iTimeout = m_timeout_sec * 1000;
            try
            {
                if (!m_ipc_cli.IsConnected)
                {
                    if (m_timeout_sec == 0)
                        m_ipc_cli.Connect();
                    else
                        m_ipc_cli.Connect(iTimeout);

                    if (m_ipc_cli.IsConnected)
                    {
                        if (OnConnectEv != null)
                            OnConnectEv(m_connect_thd_obj);
                        m_connected_flag = true;
                    }
                }

            }
            catch (ThreadAbortException) { return; }
            catch (ThreadInterruptedException) { return; }
            catch (IOException)
            {
                if (OnConnectTimeoutEv != null)
                    OnConnectTimeoutEv(m_connect_thd_obj);
            }
            catch (TimeoutException)
            {
                if (OnConnectTimeoutEv != null)
                    OnConnectTimeoutEv(m_connect_thd_obj);
            }
            return;
        }

        private void _create_connect_thd()
        {
            if (m_connect_thd != null)
            {
                if (m_connect_thd.IsAlive)
                    m_connect_thd.Abort();
                m_connect_thd.Join();
                m_connect_thd = null;
            }
            m_connect_thd = new Thread(_connect_work);
            m_connect_thd.IsBackground = true;
            m_connect_thd.Start();
        }
        #endregion

        /// <summary>
        /// IPC Client 連線
        /// </summary>
        public void Connect()
        {
            this.Connect(0, null);
        }

        /// <summary>
        /// IPC Client 連線
        /// </summary>
        /// <param name="iTimeoutSec">timeout 時間, 0 代表 blocking</param>
        /// <param name="obj">event觸發時, 回傳的物件</param>
        public void Connect(int iTimeoutSec, object obj)
        {
            m_ipc_cli = new NamedPipeClientStream(m_ipc_name);

            m_connect_thd_obj = obj;
            m_timeout_sec = iTimeoutSec;
            _create_connect_thd();
        }

        /// <summary>
        /// IPC Client connection close 
        /// </summary>
        public void Disconnect()
        {
            m_ipc_cli.Close();
            m_ipc_cli.Dispose();
        }

        /// <summary>
        /// Send data to ipc server
        /// </summary>
        /// <param name="bData">data</param>
        /// <returns></returns>
        public bool Write(byte[] bData)
        {
            if (!m_connected_flag)
                return false;
            try
            {
                MemoryStream mSend = new MemoryStream();
                BinaryWriter bin = new BinaryWriter(mSend);
                int ilen = bData.Length + 4;
                
                bin.Write(bData.Length);
                bin.Write(bData);

                m_ipc_cli.Write(mSend.ToArray(), 0, ilen);
                return true;
            }
            catch (IOException)
            {
                if (OnDisconnectEv != null)
                    OnDisconnectEv(m_connect_thd_obj);

                m_connected_flag = false;
            }
            catch
            {
                m_connected_flag = false;
            }

            if (m_auto_reconnect)
                _create_connect_thd();

            return false;
        }

        public delegate void dlgAlert(object obj);
        public event dlgAlert OnConnectEv;
        public event dlgAlert OnConnectTimeoutEv;
        public event dlgAlert OnDisconnectEv;

    }
}
