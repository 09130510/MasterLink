using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;
using System.Threading;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;

namespace PriceLib.Redis
{
    public abstract class RedisLib : PriceLib
    {
        #region Event                
        public delegate void OnSubscribeChangeDelegate(string channel);
        public event OnSubscribeChangeDelegate OnSubscribed;
        public event OnSubscribeChangeDelegate OnUnsubscribed;
        #endregion

        #region Variable        
        protected PooledRedisClientManager m_Manager;
        protected Dictionary<string, int> m_Subs = new Dictionary<string, int>();
        protected Thread m_SubscribeThread;
        #endregion

        #region Property
        public string IP { get; private set; }
        public int Port { get; private set; }
        #endregion

        protected RedisLib(string ip, int port)
        {
            m_Log = log4net.LogManager.GetLogger(typeof(RedisLib));

            IP = ip;
            Port = port;
        }
        public virtual void Close()
        {
            if ((m_SubscribeThread != null) && m_SubscribeThread.IsAlive)
            {
                m_SubscribeThread.Abort();
                m_SubscribeThread.Join();
                m_Log.Info($"{nameof(Close)} - Abort Thread");
            }
            m_Manager.Dispose();
        }

        protected abstract void _ThreadProcess();
        protected virtual void _ResetSubscribeThread(string caller)
        {
            if ((m_SubscribeThread != null) && m_SubscribeThread.IsAlive)
            {
                m_Log.Info($"{caller} - Abort Subscribe Thread");
                m_SubscribeThread.Abort();
                _InitManager();
            }
            if ((m_Subs != null) && (m_Subs.Count > 0))
            {
                m_Log.Info($"{nameof(_ResetSubscribeThread)} - {string.Join(",", m_Subs.Keys.ToArray())}");
                m_SubscribeThread = new Thread(new ThreadStart(_ThreadProcess));
                m_SubscribeThread.IsBackground = true;
                m_SubscribeThread.Start();
            }
        }
        protected virtual void _InitManager()
        {
            if (m_Manager != null)
            {
                m_Log.Info($"{nameof(_InitManager)} - Dispose Manager");
                m_Manager.Dispose();
            }
            m_Log.Info($"{nameof(_InitManager)} - {IP}:{Port}");
            string[] readWriteHosts = new string[] { $"{IP}:{Port}" };
            m_Manager = new PooledRedisClientManager(1000, 1, readWriteHosts);
        }
        protected virtual void OnSubscribe(string sub_channel)
        {
            OnSubscribed?.Invoke(sub_channel);
        }
        protected virtual void OnUnsubscribe(string unsub_channel)
        {
            OnUnsubscribed?.Invoke(unsub_channel);
        }

        protected virtual void _Del(int db, string key)
        {
            if (m_Manager == null) { return; }
            using (RedisClient client = (RedisClient)m_Manager.GetClient())
            {
                client.ChangeDb(db);
                client.Del(key);
            }
        }
        protected virtual void _Flush(int db)
        {
            if (m_Manager == null) { return; }
            using (RedisClient client = (RedisClient)m_Manager.GetClient())
            {
                client.ChangeDb(db);
                client.FlushDb();
            }

        }
        protected virtual byte[] _HashGet(int db, string hashid, string key)
        {
            if (m_Manager == null) { return null; }
            using (RedisClient client = (RedisClient)m_Manager.GetReadOnlyClient())
            {
                client.ChangeDb(db);
                return client.HGet(hashid, Encoding.ASCII.GetBytes(key));
            }
        }
        protected virtual byte[][] _HashGetAll(int db, string hashid)
        {
            if (m_Manager == null) { return null; }
            using (RedisClient client = (RedisClient)m_Manager.GetReadOnlyClient())
            {
                client.ChangeDb(db);
                return client.HGetAll(hashid);
            }
        }
        protected virtual void _HashSet(int db, string hashid, string key, string value)
        {
            if (m_Manager == null) { return; }
            using (RedisClient client = (RedisClient)m_Manager.GetClient())
            {
                client.ChangeDb(db);
                client.HSet(hashid, Encoding.ASCII.GetBytes(key), Encoding.ASCII.GetBytes(value));
                if ((client.HLen(hashid) > 1L) && (client.HExists(hashid, Encoding.ASCII.GetBytes(string.Empty)) == 1L))
                {
                    client.HDel(hashid, Encoding.ASCII.GetBytes(string.Empty));
                }
            }
        }
        protected virtual byte[][] _Keys(int db, string pattern = "*")
        {
            if (m_Manager == null) { return null; }
            using (RedisClient client = (RedisClient)m_Manager.GetReadOnlyClient())
            {
                client.ChangeDb(db);
                return client.Keys(pattern);
            }
        }
        protected virtual void _LPush(int db, string listid, string value)
        {
            if (m_Manager == null) { return; }

            using (RedisClient client = (RedisClient)m_Manager.GetClient())
            {
                client.ChangeDb(db);
                client.LPush(listid, Encoding.ASCII.GetBytes(value));
            }
        }
        protected virtual void _Publish(string channel, string msg)
        {
            if (m_Manager == null) { return; }

            using (IRedisClient client = m_Manager.GetReadOnlyClient())
            {
                client.PublishMessage(channel, msg);
            }
        }
        protected virtual byte[] _RPop(int db, string listid)
        {
            if (m_Manager == null) { return null; }
            using (RedisClient client = (RedisClient)m_Manager.GetClient())
            {
                client.ChangeDb(db);
                if (client.LLen(listid) <= 0L)
                {
                    return Encoding.ASCII.GetBytes("");
                }
                return client.RPop(listid);
            }
        }
        protected virtual void _Set(int db, string key, string value)
        {
            if (m_Manager == null) { return; }
            using (RedisClient client = (RedisClient)m_Manager.GetClient())
            {
                client.ChangeDb(db);
                client.Set(key, value);
            }
        }
    }
}