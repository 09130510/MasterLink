using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Util.Extension.Class;
using ZeroMQ;
using System.Runtime.Serialization;
using System.Reflection;

namespace Util.Extension.ZMQ
{
    public enum ConnectType
    {
        Bind,
        Connect
    }
   
    public class BaseZMQ : DisposableClass
    {
        #region Variable
        private ZmqContext m_Context;
        private ZmqSocket m_Socket;
        #endregion

        #region Property
        protected ZmqContext Context
        {
            get
            {
                if (m_Context == null) { m_Context = ZmqContext.Create(); }
                return m_Context;
            }
        }
        protected ZmqSocket Socket
        {
            get { return m_Socket; }
            set { m_Socket = value; }
        }
        #endregion


        public BaseZMQ(SocketType sotcketType, ConnectType connectType, int port, string ip = "")
        {
            Socket = Context.CreateSocket(sotcketType);

            switch (connectType)
            {
                case ConnectType.Bind:
                    Socket.Bind("tcp://*:" + port);
                    break;
                case ConnectType.Connect:
                    if (String.IsNullOrEmpty(ip)) { ip = "localhost"; }
                    Socket.Connect("tcp://" + ip + ":" + port);
                    break;
            }
        }

        protected static byte[] ToArray(object obj, int bytesCount)
        {
            try
            {
                if (obj == null) { return null; }
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, obj);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);                
            }
            return null;
        }        
        public static object FromArray(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
        protected override void DoDispose() { }
    }
}
