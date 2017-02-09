using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ZeroMQ;

namespace BLParser
{
    public enum ConnectType
    {
        Bind,
        Connect
    }

    sealed class CustomizedBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type returntype = null;
            assemblyName = Assembly.GetExecutingAssembly().FullName;
            returntype = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
            if (returntype == null) { returntype = Type.GetType(typeName); }
            return returntype;
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            base.BindToName(serializedType, out assemblyName, out typeName);
            //assemblyName = "BLParser, Version=1.0.0.1, Culture=neutral, PublicKeyToken=0ff2ae38acecaed0";
        }
    }
    public class BaseZMQ : IDisposable
    {
        #region Variable
        private bool m_disposed = false;
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
                //throw;
            }
            return null;
        }
        public static object FromArray(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                binForm.Binder = new CustomizedBinder();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        /// <summary>
        /// 可釋放類別
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="IsDisposing"></param>
        protected void Dispose(bool IsDisposing)
        {
            if (m_disposed) return;

            if (IsDisposing)
            {
                DoDispose();
            }
            m_disposed = true;
        }
        /// <summary>
        /// Do something when disposing
        /// </summary>
        protected virtual void DoDispose() { }
    }
}