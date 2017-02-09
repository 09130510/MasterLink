using System;
using System.Text;
using System.Threading;
using ZeroMQ;
using System.Collections;

namespace BLParser
{
    public class Req : BaseZMQ
    {
        public delegate void OnREPReceivedDelegate(string Head, object Msg);
        public event OnREPReceivedDelegate OnREPReceived;

        private bool m_isCanRequest = true;
        private System.Timers.Timer m_Timer;
        private Queue m_Queue = new Queue();

        public bool isCanRequest { get { return m_isCanRequest; } }

        public Req(ConnectType connectType, int port, string ip = "")
            : base(SocketType.REQ, connectType, port, ip)
        {

            m_Timer = new System.Timers.Timer(100);
            m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            m_Timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            if (m_Queue.Count <= 0)
            {
                return;
            }
            string[] msg = m_Queue.Dequeue().ToString().Split('|');
            Request(msg[0], msg[1]);
        }

        public void Request(string Head, string Msg)
        {
            if (!isCanRequest)
            {
                m_Queue.Enqueue(Head + "|" + Msg);
                return;
            }
            //while (!isCanRequest)
            //{
            //    //or (int i = 0; i < 5; i++)
            //    //{
            //    //    if (isCanRequest) { break; }
            //        ////Thread.Sleep(1);
            //    //}
            //    return;
            //}

            m_isCanRequest = false;
            var message = new ZmqMessage();
            message.Append(new Frame(Encoding.ASCII.GetBytes(Head)));
            message.Append(new Frame(Encoding.ASCII.GetBytes(Msg)));
            Socket.SendMessage(message);

            ThreadPool.QueueUserWorkItem((e) =>
            {
                ZmqMessage msg = Socket.ReceiveMessage(new TimeSpan(0, 0, 3));
                if (msg == null || msg.FrameCount == 0) { return; }

                m_isCanRequest = true;
                if (OnREPReceived != null)
                {
                    OnREPReceived(Encoding.ASCII.GetString(msg[0]), msg.FrameCount > 1 ? FromArray(msg[1]) : null);
                }
            });
        }
    }
}