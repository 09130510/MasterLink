using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;

namespace NuDotNet.THDS
{
    /// <summary>
    /// Thread Pool 執行的callback fucntion
    /// </summary>
    /// <param name="argu"></param>
    public delegate void dlgThdCB(object argu);

    internal class NuThreadArgu : EventArgs, IDisposable
    {
        public object objArgu;
        public dlgThdCB dlgHdl;
        public void Dispose() { }
        public void Work()
        {
            dlgHdl(objArgu);
        }
    }

    /// <summary>
    /// 自訂Thread Pool, 若thread數不夠用, 會自動新增
    /// </summary>
    public class NuThreadPool : IDisposable
    {
        #region --- private variable --- 
        private bool m_quit = false;
        private List<Thread> m_lst_thds;
//        private Stack<NuThreadArgu> m_stack_argus;
        private NuList<NuThreadArgu> m_argus;
        private NuQueue<NuThreadArgu> m_work_queue;
        private object m_lock = new object();
        private int m_work_thd_cnt = 0;
        #endregion

        #region construct / disconstruct
        private bool m_bDioposed = false;

        /// <summary>
        /// 建構式
        /// </summary>
        public NuThreadPool()
        {
            m_lst_thds = new List<Thread>();
            m_work_queue = new NuQueue<NuThreadArgu>();
//            m_stack_argus = new Stack<NuThreadArgu>();
            m_argus = new NuList<NuThreadArgu>();
            CreateThread(1);
        }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="iThdCnt">預設的Thread數量</param>
        public NuThreadPool(int iThdCnt)
        {
            m_lst_thds = new List<Thread>();
            m_work_queue = new NuQueue<NuThreadArgu>();
//            m_stack_argus = new Stack<NuThreadArgu>();
            m_argus = new NuList<NuThreadArgu>();
            CreateThread(iThdCnt);
        }

        /// <summary>
        /// 解構式
        /// </summary>
        ~NuThreadPool() { Dispose(); }

        /// <summary>
        /// Dispose function
        /// </summary>
        /// <param name="bDisposed"></param>
        protected virtual void Dispose(bool bDisposed)
        {
            if (m_bDioposed)
                return;
            // dispose all resource 
            m_quit = true;
            m_work_queue.CancelAllBlocking();
            m_lst_thds.Clear();
            m_work_queue.Dispose();
//            m_stack_argus.Clear();
            m_argus.Dispose();
            // ----------------------------------------
            m_bDioposed = true;

            
        }

        /// <summary>
        /// 清除所有資源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// thread 個數
        /// </summary>
        public int ThreadCount { get { return m_lst_thds.Count; } }

        #region --- private function ---
        //private NuThreadArgu _ArguPop()
        //{
        //    lock (m_lock)
        //    {
        //        return m_stack_argus.Pop();
        //    }
        //}
        //private void _ArguPush(NuThreadArgu oArgu)
        //{
        //    //oArgu.Event -= oArgu.dlgHdl;
        //    oArgu.dlgHdl = null;
        //    oArgu.objArgu = null;
        //    lock (m_lock)
        //    {
        //         m_stack_argus.Push(oArgu);
        //    }
        //}
        private void _doWork(object oObj)
        {
            NuThreadArgu Argu = null;
            try
            {
                while (!m_quit)
                {
                    Argu = m_work_queue.Dequeue();
                    if (Argu == null)
                        continue;
                    Argu.Work();

                    m_work_thd_cnt--;
                    lock (m_lock)
                        m_argus.Push(ref Argu);
//                        m_stack_argus.Push(Argu);
                }
                return;
            }
            catch (ThreadAbortException)
            {
                return;
            }
            catch (ThreadInterruptedException)
            {
                return;
            }
            finally
            {
                lock (m_lock)
                    m_lst_thds.Remove(Thread.CurrentThread);
            }
        }
        #endregion

        #region --- public methods ---
        /// <summary>
        /// 產生Thread
        /// </summary>
        /// <param name="iThdCnt">個數</param>
        public void CreateThread(int iThdCnt)
        {
            Thread thd = null;
            NuThreadArgu argu = null;
            for (int i = 0; i < iThdCnt; i++)
            {
                thd = new Thread(_doWork);
                argu = new NuThreadArgu();
                thd.IsBackground = true;

                lock (m_lock)
                {
                    m_lst_thds.Add(thd);
                    m_argus.Push(ref argu);
//                    m_stack_argus.Push(argu);
                }

                thd.Start();
            }

        }

        /// <summary>
        /// 將資料送到背景處理
        /// </summary>
        /// <param name="oObj">參數</param>
        /// <param name="WorkFn">Callback function</param>
        public void Invoke(object oObj, dlgThdCB WorkFn)
        {
            NuThreadArgu Argu = null;

            //System.Diagnostics.Debug.WriteLine(string.Format("[{0} <=> {1}", m_lst_thds.Count, m_work_thd_cnt));
            m_work_thd_cnt++;
            if (m_lst_thds.Count == m_work_thd_cnt)
            {   // thread not enough
                CreateThread(1);
            }

            lock (m_lock)
                Argu = m_argus.Pop();
//                Argu = m_stack_argus.Pop();

            Argu.objArgu = oObj;
            //Argu.Event += WorkFn;
            Argu.dlgHdl = WorkFn;
            //Argu.Event += Argu.dlgHdl;
            m_work_queue.Enqueue(Argu);
        }

        #endregion
    }
}
