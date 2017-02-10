using PriceLib.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notifier.Class
{
    public class Monitor
    {
        private RedisPublishLib m_Publish;
        private ConcurrentDictionary<string, ChannelInfo> m_Channels = new ConcurrentDictionary<string, ChannelInfo>();
        ////private Dictionary<string, Dictionary<string, ChannelInfo>> m_Allow = new Dictionary<string, Dictionary<string, ChannelInfo>>();
        //private Dictionary<string, AllowInfo> m_Allow = new Dictionary<string, AllowInfo>();
        private List<AllowInfo> m_Allow
        {
            get
            {
                if (!frmStopNotify.Allows.ContainsKey(Server))
                {
                    return null;
                }
                return frmStopNotify.Allows[Server];
            }
        }
        public ServerInfo Server { get; private set; }

        //public Monitor(ServerInfo server, AllowInfo allow)
        //{
        //    Server = server;
        //    if (allow != null) { m_Allow.Add(allow.Key, allow); }
        //}
        public Monitor(ServerInfo server)
        {
            Server = server;            
        }

        public void Start()
        {
            m_Publish = new RedisPublishLib(Server.IP, Server.Port);
            m_Publish.OnValueUpdated += OnValueUpdated;
            m_Publish.SubscribeAllChannels();
            foreach (var channel in m_Channels)
            {
                channel.Value.Start();
            }
        }
        public void Stop()
        {
            m_Publish.OnValueUpdated -= OnValueUpdated;
            m_Publish.UnsubscribeAll();
            m_Publish.Close();
            foreach (var channel in m_Channels)
            {
                channel.Value.Stop();
            }            
        }
        public void Remove(AllowInfo ainfo)
        {
            m_Publish.OnValueUpdated -= OnValueUpdated;            
            foreach (var channel in m_Channels)
            {                

                if (channel.Value.Channel == ainfo.Channel &&
                    (ainfo.Item == string.Empty || channel.Value.Item == ainfo.Item))
                {
                    ChannelInfo cinfo;
                    m_Channels.TryRemove(channel.Key, out cinfo);
                    cinfo.Dispose();
                }
            }
            m_Publish.OnValueUpdated += OnValueUpdated;
        }
        //public void Allow(AllowInfo allow)
        //{
        //    if (allow == null) { return; }
        //    if (!m_Allow.ContainsKey(allow.Key))
        //    {
        //        m_Allow.Add(allow.Key, allow);
        //    }
        //    m_Allow[allow.Key] = allow;
        //}
        //public void Remove(AllowInfo allow)
        //{
        //    if (m_Allow.ContainsKey(allow.Key))
        //    {
        //        m_Allow.Remove(allow.Key);
        //    }
        //}
        private void OnValueUpdated(string channel, string item, string value)
        {
            string key = $"{channel}.{item}";
            AllowInfo allowinfo = null;

            //foreach (var allow in m_Allow.Values)
            foreach (var allow in m_Allow)
            {
                if (allow.isAllow(channel, item))
                {
                    allowinfo = allow;
                    break;
                }
            }
            if (allowinfo == null) { return; }

            if (!m_Channels.ContainsKey(key))
            {
                ChannelInfo cinfo = new ChannelInfo(Server, allowinfo.Style, allowinfo.Interval, channel, item, value);
                m_Channels.TryAdd(key, cinfo);
            }
            else
            {
                m_Channels[key].Value = value;
            }
        }
    }
}
