using System;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace BLParser
{
    public class Rep : BaseZMQ
    {
        public delegate void OnREQReceivedDelegate(string Head, string Msg);
        public event OnREQReceivedDelegate OnREQReceived;

        #region Variable
        private Thread m_Thread;
        #endregion

        public Rep(ConnectType connectType, int port, string ip = "")
            : base(SocketType.REP, connectType, port, ip) { }

        public void Start()
        {
            if (m_Thread == null)
            {
                m_Thread = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        ZmqMessage msg = Socket.ReceiveMessage(new TimeSpan(0, 0, 3));
                        if (msg == null || msg.FrameCount == 0)
                        {
                            Thread.Sleep(1);
                            continue;
                        }

                        if (OnREQReceived != null && msg.FrameCount > 1)
                        {
                            OnREQReceived(Encoding.ASCII.GetString(msg[0]), Encoding.ASCII.GetString(msg[1]));
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

        public void Response(string Head)
        {
            Socket.Send(Head, Encoding.UTF8, new TimeSpan(0, 0, 3));
        }
        public void Response(string Head, object Msg)
        {
            var message = new ZmqMessage();
            message.Append(new Frame(Encoding.ASCII.GetBytes(Head)));
            message.Append(new Frame(ToArray(Msg, 819200)));
            Socket.SendMessage(message);
        }

        protected override void DoDispose()
        {
            Stop();
            base.DoDispose();            
        }
    }
}
