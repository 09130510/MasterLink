using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Extension.Class;

namespace OrderProcessor
{
    
    public abstract class Processor
    {
        #region Event
        public event EventHandler ConnectedEvent;
        public event EventHandler DisconnectedEvent;
        #endregion

        #region Variable
        public const string ININAME = @"./Config.ini";
        private NuLog m_Log = new NuLog(@"./OrderLog/", DateTime.Now.ToString("yyyyMMdd"));        
        #endregion

        protected void Connected(Processor sender, EventArgs e)
        {
            ConnectedEvent?.Invoke(sender, e);
        }
        protected void Disconnected(Processor sender, EventArgs e)
        {
            DisconnectedEvent?.Invoke(sender, e);
        }

        protected void MsgLog(string msg)
        {
            lock (m_Log)
            {
                m_Log.WrtLogWithFlush(msg);
            }            
        }
        protected void ErrLog(string err)
        {
            lock (m_Log)
            {
                m_Log.WrtErrWithFlush(err);
            }            
        }
        
    }
}
