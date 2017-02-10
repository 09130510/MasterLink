using System;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace Util.Extension.ZMQ
{
    public class Req : BaseZMQ
    {
        public delegate void OnREPReceivedDelegate(string Head, object Msg);
        public event OnREPReceivedDelegate OnREPReceived;

        public Req(ConnectType connectType, int port, string ip = "")
            : base(SocketType.REQ, connectType, port, ip) { }

        public void Request(string Head, string Msg)
        {
            var message = new ZmqMessage();
            message.Append(new Frame(Encoding.ASCII.GetBytes(Head)));
            message.Append(new Frame(Encoding.ASCII.GetBytes(Msg)));
            Socket.SendMessage(message);

            ThreadPool.QueueUserWorkItem((e) =>
            {

                ZmqMessage msg = Socket.ReceiveMessage(new TimeSpan(0, 0, 3));
                if (msg == null || msg.FrameCount == 0) { return; }

                if (OnREPReceived != null)
                {
                    OnREPReceived(Encoding.ASCII.GetString(msg[0]), msg.FrameCount > 1 ? FromArray(msg[1]) : null);
                }
            });
        }
    }
}
