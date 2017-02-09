using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ZeroMQ;


namespace BLParser
{
    public class Sub : BaseZMQ
    {
        public delegate void OnSUBReceivedDelegate(string Head, object Msg);
        public event OnSUBReceivedDelegate OnSUBReceived;

        #region Variable
        private Thread m_Thread;
        private List<byte[]> m_Subscribes;
        #endregion

        public Sub(ConnectType connectType, int port, string ip = "")
            : base(SocketType.SUB, connectType, port, ip)
        {
            m_Subscribes = new List<byte[]>();
        }

        public void Start()
        {
            if (m_Subscribes.Count > 0)
            {
                foreach (var item in m_Subscribes)
                {
                    Socket.Subscribe(item);
                }
            }

            if (m_Thread == null)
            {
                m_Thread = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        ZmqMessage msg = Socket.ReceiveMessage(new TimeSpan(0, 0, 1));
                        if (msg == null || msg.FrameCount == 0)
                        {
                            Thread.Sleep(1);
                            continue;
                        }

                        if (OnSUBReceived != null && msg.FrameCount > 1)
                        {
                            OnSUBReceived(Encoding.ASCII.GetString(msg[0]), FromArray(msg[1]));
                        }
                    }
                }));
            }
            m_Thread.Start();
        }
        public void Stop()
        {
            if (m_Thread != null && m_Thread.IsAlive)
            {
                m_Thread.Abort();
                m_Thread.Join();
                m_Thread = null;
            }
        }
        public void SubscribeAll()
        {
            Socket.SubscribeAll();
        }
        public void Subscribe(string Head)
        {
            byte[] context = Encoding.ASCII.GetBytes(Head);
            if (m_Subscribes.Contains(context)) { return; }
            m_Subscribes.Add(context);
            Socket.Subscribe(context);
        }
        public void UnsubscribeAll()
        {
            Socket.UnsubscribeAll();
        }
        public void Unsubscribe(string Head)
        {
            byte[] context = Encoding.ASCII.GetBytes(Head);
            if (!m_Subscribes.Contains(context)) { return; }
            m_Subscribes.Remove(context);
            Socket.Unsubscribe(context);
        }

        protected override void DoDispose()
        {
            base.DoDispose();
            Stop();
        }
    }
}
