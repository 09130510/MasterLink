using System.Text;
using ZeroMQ;

namespace BLParser
{
    public class Pub : BaseZMQ
    {
        public Pub(ConnectType connectType, int port, string ip = "")
            : base(SocketType.PUB, connectType, port, ip) { }

        public void Send(string Head, object Msg)
        {
            var message = new ZmqMessage();
            message.Append(new Frame(Encoding.ASCII.GetBytes(Head)));
            message.Append(new Frame(ToArray(Msg, 819200)));
            Socket.SendMessage(message);
        }
        public void Send(string Msg)
        {
            Send("MSG", Msg);
        }
        public void Send(string Head, string Msg)
        {
            var message = new ZmqMessage();
            message.Append(new Frame(Encoding.ASCII.GetBytes(Head)));
            message.Append(new Frame(Encoding.ASCII.GetBytes(Msg)));
            Socket.SendMessage(message);
        }
    }
}