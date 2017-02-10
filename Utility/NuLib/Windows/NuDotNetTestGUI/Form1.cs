using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using NuDotNet;
using NuDotNet.THDS;
using NuDotNet.Net;
using NuDotNet.IPC;
using NuDotNet.MMF;
using NuDotNet.SocketUtil;

namespace NuDotNetTestGUI
{
    public partial class Form1 : Form
    {
        #region private variable 
        NuTukanBusSvr m_tukan_bus = null;
        //List<TukanBusClnt> m_tukan_bus_clnts = new List<TukanBusClnt>();
        int m_bus_cnt = 0;
        int m_bus_svr = 0;
        TestServer m_svr = null;
        List<TestClient> m_clients = new List<TestClient>(10);
        //TestClient m_test_client = null;
        string TestIP = "127.0.0.1";
        int TestPort = 33333;

        NuIniFile m_ini = new NuIniFile();
        NuThreadPool m_thd_pool = null;

        NuSocketBox m_socket_box = new NuSocketBox();
        #endregion

        public Form1()
        {
            InitializeComponent();
            m_thd_pool = new NuThreadPool(10);
        }

        private void btnSvrUp_Click(object sender, EventArgs e)
        {
            if (m_svr == null)
            {
                m_svr = new TestServer(TestIP, TestPort);
                m_svr.LogEv += new TestServer.dlgLog(_SvrShowMsg);
                m_svr.Start();
            }
        }

        private void btnSvrDown_Click(object sender, EventArgs e)
        {
            if (m_svr != null)
            {
                m_svr.Stop();
                m_svr = null;
            }

        }


        private void btnAddClient_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 10; i++)
            {
                TestClient client = new TestClient();
                client.LogEv += new TestClient.dlgLog(_CliShowMsg);
                client.Connect(TestIP, TestPort.ToString());
                //client.Connect("10.220.34.46", "45678");
                m_clients.Add(client);
            }
        }

        #region Invoke to write 


        public delegate void dlgShowMsg(string sMsg);
        public void _SvrShowMsg(string sMsg)
        {
            if (txtSvr.InvokeRequired)
            {
                if (!txtSvr.IsDisposed)
                {
                    dlgShowMsg dg = new dlgShowMsg(_SvrShowMsg);
                    txtSvr.BeginInvoke(dg, sMsg);
                }
            }
            else
            {
                if (chkSvr.Checked == false)
                {
                    if (txtSvr.Lines.Length > 1000)
                    {
                        txtSvr.Text.Remove(0, 200);
                        txtSvr.Clear();
                    }
                    string sTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff") + " - ";
                    txtSvr.AppendText(sTime + sMsg + Environment.NewLine);
                }
            }
        }
        public void _CliShowMsg(string sMsg)
        {
            if (txtCli.InvokeRequired)
            {
                if (!txtCli.IsDisposed)
                {
                    dlgShowMsg dg = new dlgShowMsg(_CliShowMsg);
                    txtCli.BeginInvoke(dg, sMsg);
                }
            }
            else
            {

                if (txtCli.Lines.Length > 1000)
                {
                    txtCli.Text.Remove(0, 200);
                    txtCli.Clear();
                }
                string sTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff") + " - ";
                txtCli.AppendText(sTime + sMsg + Environment.NewLine);
            }
            
        }

        #endregion

        private void btnBroadcast_Click(object sender, EventArgs e)
        {
            m_svr.Broadcase("Send data from server!");
        }

        private void btnCliBroadcast_Click(object sender, EventArgs e)
        {
            if (m_clients.Count > 0)
            {
                int i = 0;
                //for (int i = 0; i < m_clients.Count / 2; i++)
                //for (int i = 0; i < 2; i++)
                {
                    TestClient client = m_clients[i];
                    _CliShowMsg("send " + i.ToString());
                    client.SendBroadcase("send from client " + i.ToString());
                }
            }
        }

        private void btnIniSave_Click(object sender, EventArgs e)
        {
            m_ini.SaveToStream("./RptServer.bini");
        }

        private void btnIniRead_Click(object sender, EventArgs e)
        {
            //m_ini.Open("./RptServer.ini");
            //m_ini.Load();
            m_ini.LoadFromStream("./RptServer.bini");
            MessageBox.Show(m_ini["IVAN", "AAA"]);
            MessageBox.Show(m_ini["IVAN", "BBB"]);

            MessageBox.Show(m_ini["SYS", "IP"]);
        }

        private void btnIniSave2_Click(object sender, EventArgs e)
        {
            m_ini.SaveToTextFile("./ivan.ini");
        }

        /* ================================================================== */

        NuIpcServer m_ipc_svr = null;
        NuIpcClient m_ipc_cli = null;
        NuIpcClient m_ipc_cli2 = null;
        
        private void btnIpcSvrUp_Click(object sender, EventArgs e)
        {
            if (m_ipc_svr == null)
            {
                m_ipc_svr = new NuIpcServer("test", 50);
                m_ipc_svr.OnDataArrivedEv += new NuIpcServer.dlgOnDataArrived(m_ipc_svr_OnDataArrivedEv);
            }
            m_ipc_svr.Start();
        }

        void m_ipc_svr_OnDataArrivedEv(byte[] bData)
        {
            string s = Encoding.Default.GetString(bData);
            try
            {
                _SvrShowMsg(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnIpcSvrDown_Click(object sender, EventArgs e)
        {
            if (m_ipc_svr != null)
            {
                m_ipc_svr.Stop();
            }
        }

        private void btnIpcCliUp1_Click(object sender, EventArgs e)
        {
            m_ipc_cli = new NuIpcClient("test");
            m_ipc_cli.Connect(1, null);
        }

        private void btnIpcCliDown1_Click(object sender, EventArgs e)
        {
            m_ipc_cli.Disconnect();
        }

        private void btnIpcCli1Send_Click(object sender, EventArgs e)
        {
            string str = "[1234567890ABC]";

            MemoryStream mStream = new MemoryStream();
            BinaryWriter bin = new BinaryWriter(mStream);
            
            
            for (int i = 0; i < 50; i++)
            {
                mStream.SetLength(0);
                str = "[1234567890ABC]" + i.ToString();
                bin.Write(str);
                m_ipc_cli.Write(mStream.ToArray());
            }

        }

        private void btnIpcCliUp2_Click(object sender, EventArgs e)
        {
            if (m_ipc_cli2 == null)
                m_ipc_cli2 = new NuIpcClient("test");
            m_ipc_cli2.AutoReconnect = true;
            m_ipc_cli2.Connect(0, null);
        }

        private void btnIpcCli2Rcv_Click(object sender, EventArgs e)
        {
            string str = "123456789GGGGGGGGGGGG0   ABC" + Environment.NewLine;

            MemoryStream mStream = new MemoryStream();
            BinaryWriter bin = new BinaryWriter(mStream);
            bin.Write(str);
            m_ipc_cli2.Write(mStream.ToArray());
        }

        private void btnIpcCliDown2_Click(object sender, EventArgs e)
        {
            m_ipc_cli2.Disconnect();
        }

        /* ================================================================= */
        NuMMFSeqNo m_mmf_seqno = null;
        NuMMFSeqNoItem m_item_1 = null;
        NuMMFSeqNoItem m_item_2 = null;
        NuMMFSeqNoItem m_item_3 = null;
        NuMMFSeqNoItem m_item_4 = null;
        //NuSeqNo m_seqno = null;
        //NuMMap m_mmap = null;
        private void btnMMapOpen_Click(object sender, EventArgs e)
        {
            m_mmf_seqno = new NuMMFSeqNo("./test.txt", "IVAN", 10, 10);
            m_mmf_seqno.Open();

            m_item_1 = m_mmf_seqno.GetSeqNoItem("AA");
            if (m_item_1 == null)
            {
                m_mmf_seqno.AddSeqNoItem("AA", "00000000", "99999999");
                m_item_1 = m_mmf_seqno.GetSeqNoItem("AA");
            }

            m_item_2 = m_mmf_seqno.GetSeqNoItem("BB");
            if (m_item_2 == null)
            {
                m_mmf_seqno.AddSeqNoItem("BB", "00000000", "99999999");
                m_item_2 = m_mmf_seqno.GetSeqNoItem("BB");
            }

            m_item_3 = m_mmf_seqno.GetSeqNoItem();
            if (m_item_3 == null)
            {
                m_mmf_seqno.AddSeqNoItem();
                m_item_3 = m_mmf_seqno.GetSeqNoItem();
            }

            m_item_4 = m_mmf_seqno.GetSeqNoItem();

            //m_seqno = new NuSeqNo(10, "./seqno.txt");
        }


        private void btnMMapClose_Click(object sender, EventArgs e)
        {
            m_mmf_seqno.Close();
        }

        private void btnMMapWriter_Click(object sender, EventArgs e)
        {
            //int i;
            //m_mmap.GetInt32(out i);
            //i++;
            //m_mmap.WriteInt32(i);

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();//引用stopwatch物件
            sw.Reset();//碼表歸零
            sw.Start();//碼表開始計時

            string sSeqNo = "";
            for (int i = 0; i < 500000; i++)
                m_item_2.GetNext(out sSeqNo);

            for (int i = 0; i < 500000; i++)
                m_item_1.GetNext(out sSeqNo);

            sw.Stop();//碼錶停止
            //印出所花費的總豪秒數
            string result1 = sw.Elapsed.TotalMilliseconds.ToString();
            MessageBox.Show(result1);

            //sw.Reset();//碼表歸零
            //sw.Start();//碼表開始計時

            //for (int i = 0; i < 1000000; i++)
            //    m_seqno.GetNext(out sSeqNo);

            //sw.Stop();//碼錶停止
            ////印出所花費的總豪秒數
            //result1 = sw.Elapsed.TotalMilliseconds.ToString();
            //MessageBox.Show(result1);

        }

        private void btnMMapRead_Click(object sender, EventArgs e)
        {
            //int i;
            //m_mmap.GetInt32(out i);
            //MessageBox.Show(i.ToString());

            //string sSeqNo = "";
            
            //sSeqNo = m_mmf_seqno.GetCurrentSeqNo(iIndex);
            //if (sSeqNo[0] == 0x00)
            //{
            //    sSeqNo = "0000000000";
            //    m_mmf_seqno.SetSeqNo(iIndex, ref sSeqNo);

            //    sSeqNo = m_mmf_seqno.GetCurrentSeqNo(iIndex);
            //}
            //MessageBox.Show(sSeqNo);

            //MessageBox.Show(m_item_1.GetCurrentSeqNo());

            //List<Thread> thds = new List<Thread>(10);
            //Thread thd = null;
            //for (int i = 0; i < 10; i++)
            //{
            //    thd = new Thread(m_work);
            //    thd.IsBackground = true;
            //    thds.Add(thd);

            //    thd = new Thread(m_work1);
            //    thd.IsBackground = true;
            //    thds.Add(thd);
            //}

            //foreach (Thread t in thds)
            //    t.Start();

            //foreach (Thread t in thds)
            //    t.Join();
            MessageBox.Show(
                String.Format("{0}-{1}-{2}", m_item_1.GetCurrentSeqNo(),
                                             m_item_2.GetCurrentSeqNo(),
                                             m_item_3.GetCurrentSeqNo()));
                
        }

        private void m_work()
        {
            string sSeqNo = "";
            for (int i = 0; i < 100000; i++)
                m_item_3.GetNext(out sSeqNo);
        }

        private void m_work1()
        {
            string sSeqNo = "";
            for (int i = 0; i < 100000; i++)
                m_item_4.GetNext(out sSeqNo);
        }

        private void btnMMFSet_Click(object sender, EventArgs e)
        {
            m_item_3.SetSeqNo("0000001123");
            m_item_1.SetSeqNo("AA00001123");
        }

        /* ------------------------------------------------------ */
        //NuQueue<string> m_que = new NuQueue<string>(10);
        NuQueue<string> m_que = new NuQueue<string>(100);
        Thread m_que_thd = null;

        private void que_thd_work()
        {
            bool bRtn = false;
            string str = "";
            try
            {
                bRtn = true;
                while (true)
                {
                    //bRtn = m_que.Dequeue(out str, 1000);
                    str = m_que.Dequeue();
                    if (!bRtn)
                    {
                        //MessageBox.Show("Que Take Fail!");
                    }
                    else
                    {
                        //_SvrShowMsg(str);
                        _SvrShowMsg(String.Format("{0}, {1}", str,
                            DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fff")
                            ));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnQueTake_Click(object sender, EventArgs e)
        {
            if (m_que_thd == null)
            {
                m_que_thd = new Thread(que_thd_work);
                m_que_thd.IsBackground = true;
                m_que_thd.Start();
            }
            else
            {
                m_que_thd.Abort();
                m_que_thd.Join();
                m_que_thd = null;
            }
        }

        private void btnQueAdd_Click(object sender, EventArgs e)
        {
            bool bRtn = false;
            string str = "";

            for (int i = 0; i < 20; i++)
            {
                str = string.Format("test : {0}", DateTime.Now.ToString("yyyyMMdd hh:mm:ss.fff"));
                bRtn = m_que.TryEnqueue(str);
                if (!bRtn)
                {
                    MessageBox.Show("Que Add Fail!");
                }
            }
                                                      
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (TestClient cli in m_clients)
            {
                cli.Stop();
            }

            m_thd_pool.Dispose();
        }

        public class _argu_t
        {
            public string Name;
            public DateTime Time;
        }

        private void Work(object oObj)
        {
            _argu_t argu = (_argu_t)oObj;
            _CliShowMsg(String.Format("{0} | {1} | {2}", argu.Time.ToString("HH:mm:ss.fff"), argu.Name,
                DateTime.Now.ToString("HH:mm:ss.fff")));
        }
        private void btnThdPool_Click(object sender, EventArgs e)
        {
            _argu_t argu = null;

            for (int i = 0; i < 100; i++)
            {
                argu = new _argu_t();
                argu.Name = "TEST";
                argu.Time = DateTime.Now;
                m_thd_pool.Invoke(argu, Work);
            }

            _CliShowMsg("Thread : " + m_thd_pool.ThreadCount.ToString());
        }

        private void btnTukanBusUp_Click(object sender, EventArgs e)
        {
            if (m_tukan_bus != null)
            {
                return;
            }

            m_tukan_bus = new NuTukanBusSvr(TestIP, TestPort);
            m_tukan_bus.Start();
        }

        private void btnTukanBusDown_Click(object sender, EventArgs e)
        {
            if (m_tukan_bus != null)
            {
                m_tukan_bus.Stop();
                m_tukan_bus = null;
            }
        }

        private void btnBusCliAdd_Click(object sender, EventArgs e)
        {
            TukanBusClntTest frm = new TukanBusClntTest();
            m_bus_cnt++;
            //m_tukan_bus_clnts.Add(frm);
            //frm.LoginInfo = String.Format("IVAN-{0}@{1}:{2}", m_tukan_bus_clnts.Count, TestIP, TestPort.ToString());

            //frm.LoginInfo = String.Format("IVAN-{0}@{1}:{2}", m_bus_cnt, TestIP, TestPort.ToString());
            frm.LoginInfo = "IVAN2@10.220.34.46:23457";
            frm.Show();
        }

        private void btnBusClAdd2_Click(object sender, EventArgs e)
        {
            TukanBusClient frm = new TukanBusClient(m_socket_box);
            m_bus_cnt++;
//            frm.LoginInfo = String.Format("IVAN-{0}@{1}:{2}", m_bus_cnt, "10.220.35.49", "23457");
            frm.LoginInfo = String.Format("IVAN-{0}@{1}:{2}", m_bus_cnt, "127.0.0.1", "23457");
            frm.Show();

        }

        private void btnBusSvr_Click(object sender, EventArgs e)
        {
            TukanBusServer frm = new TukanBusServer(m_socket_box);
            m_bus_svr++;
            frm.ServiceInfo = String.Format("BUSSVR-{0}@{1}:{2}", m_bus_svr, "127.0.0.1", "23457");
            frm.Show();
        }


        #region NuList testing
        private void btnList_Click(object sender, EventArgs e)
        {
            //NuList<MemoryStream> mList = new NuList<MemoryStream>(() => { return new MemoryStream(); },
            //                                                     (object obj) => { ((MemoryStream)obj).Dispose(); });
            NuList<MemoryStream> mList = new NuList<MemoryStream>();
            List<MemoryStream> lst = new List<MemoryStream>();
            MemoryStream mStream = null;
            byte[] bData = null;
            String sData = "";
            int i, iCnt;
            for (i = 0; i < 100; i++)
            {
                sData = String.Format("IVAN-{0}", i);
                mStream = mList.Pop();
                bData = Encoding.Default.GetBytes(sData);
                mStream.Write(bData, 0, bData.Length);
                lst.Add(mStream);
            }

            iCnt = lst.Count;

            for (i = 0; i < iCnt; i++)
            {
                mStream = lst[i];
                sData = Encoding.Default.GetString(mStream.ToArray());
                _SvrShowMsg(sData);
                mList.Push(ref mStream);
            }

            _SvrShowMsg(mList.Count.ToString());

            mStream = mList.Pop();
            sData = Encoding.Default.GetString(mStream.ToArray());
            _SvrShowMsg(sData);
            _SvrShowMsg(mList.Count.ToString());

            mList.Dispose();
        }
        #endregion
        public void _TimerWork(object oObj)
        {
            // 1 tick = 10 ns
            // 1 sec = 10^9 ns = 10^8 tick
            DateTime Now = DateTime.Now;
            DateTime LastNow = DateTime.Now;
            int sleep_ms = 0;
            int i = 0;

            while (++i < 100)
            {
                Now = DateTime.Now;
                sleep_ms = 1000 - (Now - LastNow).Milliseconds;
                Thread.Sleep(sleep_ms);
                Thread.Sleep(10);
                _SvrShowMsg(String.Format("[{0}]{1}", sleep_ms.ToString(), i.ToString()));
                LastNow = Now;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread thd = new Thread(_TimerWork);
            thd.Start();           

        }
    }
}
