using SourceGrid.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Notifier.Utility;

namespace Notifier.Class
{
    public abstract class NotifyInfo
    {
        #region Variable        
        protected ServerInfo m_Server;
        #endregion

        #region Cell
        public List<ICell> Cells { get; protected set; } = new List<ICell>();
        #endregion

        #region Property
        public virtual string Server
        {
            get
            {
                return m_Server == null ? string.Empty : m_Server.Key;
            }
        }
        public virtual string ServerName
        {
            get
            {
                return m_Server == null ? string.Empty : m_Server.Name;
            }
        }
        public virtual string Channel { get; protected set; }
        public virtual string Item { get; protected set; }
        public abstract Style Style { get; }
        public string Key { get { return $"{Channel}|{Item}"; } }
        #endregion

        protected NotifyInfo(ServerInfo server, string channel, string item)
        {
            m_Server = server;
            Channel = channel;
            Item = item;
        }

        public virtual bool isAllow(string channel, string item)
        {
            return channel == Channel && (string.IsNullOrEmpty(Item) || item == Item);
        }
    }
}
