using log4net;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceLib.Redis
{
    public class RedisPublishLib : RedisLib
    {
        public delegate void OnValueUpdateDelegate(string channel, string item, string value);
        public event OnValueUpdateDelegate OnValueUpdated;

        public RedisPublishLib(string ip, int port) : base(ip, port)
        {
            m_Log = LogManager.GetLogger(typeof(RedisPublishLib));
            _InitManager();
        }

        #region Delegate
        protected override void _ThreadProcess()
        {
            try
            {
                using (IRedisClient client = m_Manager.GetReadOnlyClient())
                {
                    using (IRedisSubscription subscription = client.CreateSubscription())
                    {
                        subscription.OnSubscribe += OnSubscribe;
                        subscription.OnUnSubscribe += OnUnsubscribe;
                        subscription.OnMessage += _OnMsg;
                        subscription.SubscribeToChannelsMatching(m_Subs.Keys.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void _OnMsg(string channel, string msg)
        {
            string[] items = channel.Split('.');
            if (items.Length != 2) { return; }

            OnValueUpdated?.Invoke(items[0], items[1], msg);
        }
        #endregion

        #region Public
        public void Flush(int db)
        {
            base._Flush(db);
        }
        public byte[] HashGet(int db, string hashid, string key)
        {
            return _HashGet(db, hashid, key);
        }
        public void HashSet(int db, string hashid, string key, string value)
        {
            _HashSet(db, hashid, key, value);
        }
        public void Publish(string channel, string msg)
        {
            _Publish(channel, msg);
        }
        public void Subscribe(string subchannel, string item)
        {
            string key = $"{subchannel}.{item}";
            lock (m_Subs)
            {
                if (!m_Subs.ContainsKey(key))
                {
                    m_Subs.Add(key, 1);
                    _ResetSubscribeThread(nameof(Subscribe));
                    m_Log.Info($"{nameof(Subscribe)} - Add New: {key}");
                }
                else
                {
                    m_Subs[key]++;
                }
            }
        }
        public void SubscribeAllChannels()
        {
            string key = "*";
            lock (m_Subs)
            {
                if (!m_Subs.ContainsKey(key))
                {
                    m_Subs.Add(key, 1);
                    _ResetSubscribeThread(nameof(SubscribeAllChannels));
                    m_Log.Info($"{nameof(SubscribeAllChannels)} - Add New: {key}");
                }
                else
                {
                    m_Subs[key]++;
                }
            }
        }
        public void Unsubscribe(string subchannel, string item)
        {
            string key = $"{subchannel}.{item}";
            lock (m_Subs)
            {
                if (!m_Subs.ContainsKey(key)) { return; }

                m_Subs[key]--;
                if (m_Subs[key] <= 0)
                {
                    m_Subs.Remove(key);
                    _ResetSubscribeThread(nameof(Unsubscribe));
                    m_Log.Info($"{nameof(Unsubscribe)} - Delete: {key}");
                }
            }
        }
        public void UnsubscribeAll()
        {
            lock (m_Subs)
            {
                if (m_Subs.Count > 0)
                {
                    m_Subs.Clear();
                    _ResetSubscribeThread(nameof(UnsubscribeAll));
                }
            }
        }
        #endregion
    }
}