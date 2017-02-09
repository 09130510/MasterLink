﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BL
{
    /// <summary>
	/// 訊息中心(同步)
	/// 要非同步請用zmq pub/sub
	/// </summary>
	public class NotificationCenter
    {
        private const string ERR = "ERR";
        /// <summary>
        /// Delegate
        /// </summary>
        /// <param name="notification"></param>
        public delegate void NotificationDelegate(Notification notification);

        private static NotificationCenter m_Instance;
        private readonly Hashtable m_Hashtable;

        private NotificationCenter()
        {
            m_Hashtable = new Hashtable();
        }
        /// <summary>
        /// 訊息中心
        /// </summary>
        public static NotificationCenter Instance { get { return m_Instance ?? (m_Instance = new NotificationCenter()); } }
        /// <summary>
        /// 訂閱
        /// </summary>
        /// <param name="notificationDelegate"></param>
        /// <param name="notificationName"></param>
        public void AddObserver(NotificationDelegate notificationDelegate, string notificationName)
        {
            if (String.IsNullOrEmpty(notificationName))
            {
                throw new ArgumentNullException(@"Null Notification Name");
            }
            if (notificationDelegate == null)
            {
                throw new ArgumentNullException(@"Null Notification Delegate");
            }

            var delegatesCollection = (List<NotificationDelegate>)m_Hashtable[notificationName];
            if (delegatesCollection == null)
            {
                delegatesCollection = new List<NotificationDelegate>();
                m_Hashtable.Add(notificationName, delegatesCollection);
            }
            delegatesCollection.Add(notificationDelegate);
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="notificationDelegate"></param>
        /// <param name="notificationName"></param>
        public void RemoveObserver(NotificationDelegate notificationDelegate, string notificationName)
        {
            if (String.IsNullOrEmpty(notificationName))
            {
                throw new ArgumentNullException(@"Null Notification Name");
            }
            if (notificationDelegate == null)
            {
                throw new ArgumentNullException(@"Null Notification Delegate");
            }

            var delegatesCollection = (List<NotificationDelegate>)m_Hashtable[notificationName];
            if (delegatesCollection != null)
            {
                delegatesCollection.Remove(notificationDelegate);
            }
        }
        /// <summary>
        /// 發送
        /// </summary>
        /// <param name="notificationName"></param>
        /// <param name="notification"></param>
        public void Post(string notificationName, Notification notification)
        {
            if (String.IsNullOrEmpty(notificationName))
            {
                throw new ArgumentNullException(@"Null Notification Name");
            }
            //if (notification==null)
            //{
            //	throw new ArgumentNullException("Null Nofification Content");
            //}
            lock (m_Hashtable)
            {
                var delegatesCollection = (List<NotificationDelegate>)m_Hashtable[notificationName];
                if (delegatesCollection != null)
                {
                    foreach (var notificationDelegate in delegatesCollection)
                    {
                        notificationDelegate(notification);
                    }
                }
            }
        }
        public void AsyncPost(string notificationName, Notification notification)
        {
            if (String.IsNullOrEmpty(notificationName))
            {
                throw new ArgumentNullException(@"Null Notification Name");
            }

            var delegatesCollection = (List<NotificationDelegate>)m_Hashtable[notificationName];
            ThreadPool.QueueUserWorkItem((e) =>
            {
                if (delegatesCollection != null)
                {
                    foreach (var notificationDelegate in delegatesCollection)
                    {
                        notificationDelegate(notification);
                    }
                }
            });
        }
        /// <summary>
        /// 發送空訊息
        /// </summary>
        /// <param name="notificationName"></param>
        public void Post(string notificationName)
        {
            Post(notificationName, Notification.Empty);
        }
        public void Error(object sender, string Msg)
        {
            Post(ERR, new Notification(sender, Msg));
        }
        public void AddErrObserver(NotificationDelegate notificationDelegate)
        {
            if (notificationDelegate == null)
            {
                throw new ArgumentNullException(@"Null Notification Delegate");
            }

            var delegatesCollection = (List<NotificationDelegate>)m_Hashtable[ERR];
            if (delegatesCollection == null)
            {
                delegatesCollection = new List<NotificationDelegate>();
                m_Hashtable.Add(ERR, delegatesCollection);
            }
            delegatesCollection.Add(notificationDelegate);
        }
        public void RemoveErrObserver(NotificationDelegate notificationDelegate)
        {
            if (notificationDelegate == null)
            {
                throw new ArgumentNullException(@"Null Notification Delegate");
            }

            var delegatesCollection = (List<NotificationDelegate>)m_Hashtable[ERR];
            if (delegatesCollection != null)
            {
                delegatesCollection.Remove(notificationDelegate);
            }
        }
    }

    /// <summary>
	/// 訊息中心訊息
	/// </summary>
	public class Notification
    {
        private readonly object m_Sender;
        private readonly object m_Message;
        /// <summary>
        /// 傳送者
        /// </summary>
        public object Sender { get { return m_Sender; } }
        //內容
        public object Message { get { return m_Message; } }
        /// <summary>
        /// 訊息中心訊息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public Notification(object sender, object message)
        {
            m_Sender = sender;
            m_Message = message;
        }
        /// <summary>
        /// 空訊息
        /// </summary>
        public static Notification Empty
        {
            get
            {
                return new Notification(null, null);
            }
        }
    }
}
