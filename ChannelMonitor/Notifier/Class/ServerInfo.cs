using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notifier.Class
{
    /// <summary>
    /// 主機資訊    
    /// </summary>
    public class ServerInfo
    {
        #region Property
        public string IP { get; private set; }
        public int Port { get; private set; }
        public string Name { get; private set; }
        public string Key { get { return $"{IP}:{Port}"; } }
        #endregion

        private ServerInfo(string ip, int port, string name)
        {
            IP = ip;
            Port = port;
            Name = name;
        }
        public static ServerInfo Create(string[] info)
        {
            int port;
            if (info.Length < 3 || string.IsNullOrEmpty(info[0]) || !int.TryParse(info[1], out port) || string.IsNullOrEmpty(info[2]))
            {
                return null;
            }

            return new ServerInfo(info[0], port, info[2]);
        }
    }
}
