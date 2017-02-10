using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace NuDotNet
{
    /// <summary>
    /// 自定義List物件, 記憶體自動增長不會送回給GC, 減少重覆跟Framwork要物件的次數 [Thread Safe]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NuList<T> : IDisposable where T: IDisposable, new()
    {
        #region private variable
        private bool flag = false;
        private bool bDisposed = false;
        private object m_lock = new object();
        private LinkedList<T> m_list = null;
        #endregion

        #region --  construct / destruct  --
        /// <summary>
        /// 建構式
        /// </summary>
        public NuList()
        {
            m_list = new LinkedList<T>();
        }
        ~NuList() 
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
        }

        protected virtual void Dispose(bool IsDisposing)
        {
            if (bDisposed)
                return;

            if (IsDisposing)
            {
                lock (m_lock)
                {
                    foreach (T obj in m_list)
                    {
                        obj.Dispose();
                    }
                    m_list.Clear();
                }
            }
        }
        #endregion

        #region property
        public int Count { get { return (m_list == null) ? 0 : m_list.Count; } }
        #endregion
        
        #region -- private function --
        private bool _CreateObject(int Cnt)
        {
            T oObj;
            try
            {
                for (int i = 0; i < Cnt; i++)
                {
                    oObj = new T();
                    m_list.AddLast(oObj);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Pop
        public T Pop()
        {
            T oObj;
            lock (m_lock)
            {
                if (m_list.Count == 0)
                {
                    _CreateObject(10);
                }
                oObj = m_list.First.Value;
                m_list.RemoveFirst();
            }
            return oObj;
        }
        #endregion

        #region Push
        public void Push(ref T oObj)
        {
            if (oObj == null)
                return;

            lock (m_lock)
            {
                m_list.AddFirst(oObj);
            }
            oObj = default(T);
        }

        #endregion


    }
}
