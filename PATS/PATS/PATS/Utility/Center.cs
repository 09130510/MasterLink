using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PATS.Utility
{
    /// <summary>
    /// 訊息中心訊息
    /// </summary>
    public class Msg
    {
        /// <summary>
        /// 傳送者
        /// </summary>
        public object Sender { get; }
        //內容
        public object Message { get; }
        /// <summary>
        /// 訊息中心訊息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public Msg(object sender, object message)
        {
            Sender = sender;
            Message = message;
        }
    }
    /// <summary>
    /// 訊息中心(同步)
    /// 要非同步請用zmq pub/sub
    /// </summary>
    public class Center
    {
        //public const string ERR = "ERR";
        //public const string ALL = "*";
        /// <summary>
        /// Delegate
        /// </summary>
        /// <param name="Msg"></param>
        public delegate void MsgDelegate(Observer MsgName, Msg Msg);

        private static Center m_Instance;
        //private readonly Hashtable m_Hashtable;
        private readonly Dictionary<Observer, List<MsgDelegate>> m_Hashtable;

        private Center()
        {
            // m_Hashtable = new Hashtable();
            m_Hashtable = new Dictionary<Observer, List<MsgDelegate>>();
        }
        /// <summary>
        /// 訊息中心
        /// </summary>
        public static Center Instance { get { return m_Instance ?? (m_Instance = new Center()); } }
        /// <summary>
        /// 訂閱
        /// </summary>
        /// <param name="MsgDelegate"></param>
        /// <param name="msgName"></param>
        public void AddObserver(MsgDelegate MsgDelegate, Observer MsgName = default(Observer))
        {
            if (MsgName == default(Observer))
            {
                throw new ArgumentNullException(@"Null Msg Name");
            }
            if (MsgDelegate == null)
            {
                throw new ArgumentNullException(@"Null Msg Delegate");
            }


            //var delegatesCollection = (List<MsgDelegate>)m_Hashtable[MsgName];
            //if (delegatesCollection == null)
            //{
            //    delegatesCollection = new List<MsgDelegate>();
            //    m_Hashtable.Add(MsgName, delegatesCollection);
            //}
            //delegatesCollection.Add(MsgDelegate);
            if (!m_Hashtable.ContainsKey(MsgName))
            {
                m_Hashtable.Add(MsgName, new List<MsgDelegate>());
            }
            m_Hashtable[MsgName].Add(MsgDelegate);
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="MsgDelegate"></param>
        /// <param name="MsgName"></param>
        public void RemoveObserver(MsgDelegate MsgDelegate, Observer MsgName = default(Observer))
        {
            if (MsgName == default(Observer))
            {
                throw new ArgumentNullException(@"Null Msg Name");
            }
            if (MsgDelegate == null)
            {
                throw new ArgumentNullException(@"Null Msg Delegate");
            }

            //var delegatesCollection = (List<MsgDelegate>)m_Hashtable[MsgName];
            //if (delegatesCollection != null)
            //{
            //    delegatesCollection.Remove(MsgDelegate);
            //}
            if (m_Hashtable.ContainsKey(MsgName))
            {
                m_Hashtable[MsgName].Remove(MsgDelegate);
            }
            //m_Hashtable?[MsgName]?.Remove(MsgDelegate);
        }
        /// <summary>
        /// 發送
        /// </summary>
        /// <param name="MsgName"></param>
        /// <param name="Msg"></param>
        public void Post(Observer MsgName, string Msg, [CallerMemberName] string Sender = null)
        {
            if (MsgName == default(Observer))
            {
                throw new ArgumentNullException(@"Null Msg Name");
            }
            //if (notification==null)
            //{
            //	throw new ArgumentNullException("Null Nofification Content");
            //}
            lock (m_Hashtable)
            {
                //var delegatesCollection = (List<MsgDelegate>)m_Hashtable[MsgName];
                //if (delegatesCollection != null)
                //{
                //    foreach (var notificationDelegate in delegatesCollection)
                //    {
                //        notificationDelegate(new Msg(Sender, Msg));
                //    }
                //}
                _Publish(MsgName, Msg, Sender);
            }
        }
        public void AsyncPost(Observer MsgName, string Msg, [CallerMemberName] string Sender = null)
        {
            if (MsgName == default(Observer))
            {
                throw new ArgumentNullException(@"Null Msg Name");
            }

            //var delegatesCollection = (List<MsgDelegate>)m_Hashtable[MsgName];
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            //    if (delegatesCollection != null)
            //    {
            //        foreach (var notificationDelegate in delegatesCollection)
            //        {
            //            notificationDelegate((Msg)e);
            //        }
            //    }
            //}, Msg);
            _AsyncPublish(MsgName, Msg, Sender);
        }
        public void Error(object sender, string Msg, [CallerMemberName] string Sender = null)
        {
            Post(Observer.Error, Msg, Sender);
        }
        public void AddErrObserver(MsgDelegate MsgDelegate)
        {
            if (MsgDelegate == null)
            {
                throw new ArgumentNullException(@"Null Msg Delegate");
            }

            //var delegatesCollection = (List<MsgDelegate>)m_Hashtable[ERR];
            //if (delegatesCollection == null)
            //{
            //    delegatesCollection = new List<MsgDelegate>();
            //    m_Hashtable.Add(ERR, delegatesCollection);
            //}
            //delegatesCollection.Add(MsgDelegate);
            if (!m_Hashtable.ContainsKey(Observer.Error))
            {
                m_Hashtable.Add(Observer.Error, new List<MsgDelegate>());
            }
            m_Hashtable[Observer.Error].Add(MsgDelegate);
        }
        public void RemoveErrObserver(MsgDelegate MsgDelegate)
        {
            if (MsgDelegate == null)
            {
                throw new ArgumentNullException(@"Null Notification Delegate");
            }

            //var delegatesCollection = (List<MsgDelegate>)m_Hashtable[ERR];
            //if (delegatesCollection != null)
            //{
            //    delegatesCollection.Remove(MsgDelegate);
            //}
            if (m_Hashtable.ContainsKey(Observer.Error))
            {
                m_Hashtable[Observer.Error].Remove(MsgDelegate);
            }
            //m_Hashtable?[ERR]?.Remove(MsgDelegate);
        }

        #region Private
        private void _Publish(Observer MsgName, string Msg, string Sender)
        {
            //var delegatesCollection = (List<MsgDelegate>)m_Hashtable[MsgName];
            //if (delegatesCollection != null)
            //{
            //    foreach (var notificationDelegate in delegatesCollection)
            //    {
            //        notificationDelegate(new Msg(Sender, Msg));
            //    }
            //}
            var Collections = m_Hashtable.Where(e => e.Key == MsgName || e.Key == Observer.All).Select(e => e.Value);
            if (Collections != null)
            {
                foreach (var Collection in Collections)
                {
                    foreach (var dele in Collection)
                    {
                        dele(MsgName, new Msg(Sender, Msg));
                    }
                }
            }
        }
        private void _AsyncPublish(Observer MsgName, string Msg, string Sender)
        {
            //var delegatesCollection = (List<MsgDelegate>)m_Hashtable[MsgName];
            //Msg m = new Utility.Msg(Sender, Msg);
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            //    if (delegatesCollection != null)
            //    {
            //        foreach (var notificationDelegate in delegatesCollection)
            //        {
            //            notificationDelegate((Msg)e);
            //        }
            //    }
            //}, m);
            var Collections = m_Hashtable.Where(e => e.Key == MsgName || e.Key == Observer.All).Select(e => e.Value);
            Msg m = new Msg(Sender, Msg);
            ThreadPool.QueueUserWorkItem((e) =>
            {
                if (Collections != null)
                {
                    foreach (var Collection in Collections)
                    {
                        foreach (var dele in Collection)
                        {
                            dele(MsgName, (Msg)e);
                        }
                    }
                }
            }, m);
        }
        #endregion
    }
}