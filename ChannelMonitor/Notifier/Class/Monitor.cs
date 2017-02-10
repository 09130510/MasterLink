using Notifier.Utility;
using PriceLib.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Notifier.Class
{
    public class Monitor
    {
        #region Variable
        private RedisPublishLib m_Publish;
        private ConcurrentDictionary<string, TimerChannel> m_Channels = new ConcurrentDictionary<string, TimerChannel>();
        private ConcurrentDictionary<string, DisplayChannel> m_DisplayChannels = new ConcurrentDictionary<string, DisplayChannel>();
        #endregion

        #region Property
        private List<StopNotifyInfo> m_StopNotifies
        {
            get
            {
                if (!frmSetting.StopNotifies.ContainsKey(Server.Key))
                {
                    return null;
                }
                return frmSetting.StopNotifies[Server.Key];
            }
        }
        private List<InfoNotifyInfo> m_InfoNotifies
        {
            get
            {
                if (!frmSetting.InfoNotifies.ContainsKey(Server.Key))
                {
                    return null;
                }
                return frmSetting.InfoNotifies[Server.Key];
            }
        }
        public ServerInfo Server { get; private set; }
        #endregion

        private Monitor(ServerInfo server)
        {
            Server = server;

            //Channel, Item非空的, 直接先建ChannelInfo
            if (m_StopNotifies != null && m_StopNotifies.Count > 0)
            {
                foreach (var item in m_StopNotifies)
                {
                    if (string.IsNullOrEmpty(item.Channel) || string.IsNullOrEmpty(item.Item))
                    {
                        continue;
                    }
                    var key = $"{item.Channel}.{item.Item}";
                    if (!m_Channels.ContainsKey(key))
                    {
                        TimerChannel cinfo = new TimerChannel(Server, item.Style, item.Interval, item.Channel, item.Item, string.Empty);
                        m_Channels.TryAdd(key, cinfo);
                    }
                }
            }
        }
        public static Monitor Create(string server)
        {
            if (!Util.Servers.ContainsKey(server))
            {
                return null;
            }
            return new Monitor(Util.Servers[server]);
        }
        
        #region Delegate
        private void OnValueUpdated(string channel, string item, string value)
        {
            _StopNotify(channel, item, value);
            _InfoNotify(channel, item, value);
        }
        #endregion

        #region Public       
        public void MonitorStart()
        {
            m_Publish = new RedisPublishLib(Server.IP, Server.Port);
            m_Publish.OnValueUpdated += OnValueUpdated;
            m_Publish.SubscribeAllChannels();
        }
        public void MonitorStop()
        {
            m_Publish.OnValueUpdated -= OnValueUpdated;
            m_Publish.UnsubscribeAll();
            m_Publish.Close();
            foreach (var channel in m_Channels)
            {
                channel.Value.Stop();
            }
            m_Channels.Clear();
        }
        public void ChannelInterval(StopNotifyInfo info, int interval)
        {
            foreach (var item in m_Channels.Values)
            {
                if (info.isAllow(item.ChannelName, item.Item))
                {
                    item.Interval = new TimeSpan(0, 0, interval);
                }
            }
        }
        public void Publish(string channel, string msg)
        {
            m_Publish.Publish(channel, msg);
        }
        public void RemoveStopNotify(StopNotifyInfo info)
        {
            m_Publish.OnValueUpdated -= OnValueUpdated;
            foreach (var channel in m_Channels)
            {
                if (info.isAllow(channel.Value.ChannelName, channel.Value.Item))
                {
                    TimerChannel cinfo;
                    m_Channels.TryRemove(channel.Key, out cinfo);
                    cinfo.Dispose();
                }
            }
            m_Publish.OnValueUpdated += OnValueUpdated;
        }
        public void RemoveInfoNotify(InfoNotifyInfo info)
        {
            m_Publish.OnValueUpdated -= OnValueUpdated;
            foreach (var channel in m_DisplayChannels)
            {
                if (info.isAllow(channel.Value.ChannelName, channel.Value.Item))
                {
                    DisplayChannel dinfo;
                    m_DisplayChannels.TryRemove(channel.Key, out dinfo);
                    dinfo.Dispose();
                }
            }
            m_Publish.OnValueUpdated += OnValueUpdated;
        }
        #endregion

        #region Private
        private void _InfoNotify(string channel, string item, string value)
        {
            if (m_InfoNotifies == null)
            {
                return;
            }
            string key = $"{channel}.{item}";
            InfoNotifyInfo info = null;

            foreach (var allow in m_InfoNotifies)
            {
                if (allow.isAllow(channel, item))
                {
                    info = allow;
                    break;
                }
            }
            if (info == null) { return; }


            if (!m_DisplayChannels.ContainsKey(key))
            {
                DisplayChannel dinfo = new DisplayChannel(Server, info.Style, channel, item, value);
                m_DisplayChannels.TryAdd(key, dinfo);
            }
            else
            {
                m_DisplayChannels[key].Value = value;
            }
        }
        private void _StopNotify(string channel, string item, string value)
        {
            if (m_StopNotifies == null) { return; }
            string key = $"{channel}.{item}";
            StopNotifyInfo info = null;

            foreach (var allow in m_StopNotifies)
            {
                if (allow.isAllow(channel, item))
                {
                    info = allow;
                    break;
                }
            }
            if (info == null) { return; }

            if (!m_Channels.ContainsKey(key))
            {
                TimerChannel cinfo = new TimerChannel(Server, info.Style, info.Interval, channel, item, value);
                m_Channels.TryAdd(key, cinfo);
            }
            else
            {
                m_Channels[key].Value = value;
            }
        }
        #endregion
    }
}