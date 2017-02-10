using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using ServiceStack.Redis;
using System.Threading;

namespace RedisRTD
{
    /// <summary>
    /// 資料取得
    /// </summary>
    public class RTDItemGetter
    {
        #region Variable
        private const string IP = "10.14.105.85";
        private const int PORT = 26386;        
        //private const string IP = "10.14.106.122";
        //private const int PORT = 6379;        

        private RTDSvr m_RTDSvr;
        private PooledRedisClientManager m_Redis;
        //private RedisClient m_RedisCli;
        private RedisSubscription m_Subscribe;
        private Dictionary<string, string> m_Values;                
        #endregion
               

        /// <summary>
        /// 資料取得
        /// </summary>
        /// <param name="rtds"></param>
        public RTDItemGetter(RTDSvr rtds)
        {
            m_RTDSvr = rtds;
            m_Values = new Dictionary<string, string>();
            m_Redis = new PooledRedisClientManager(IP + ":" + PORT);
            //m_RedisCli = new RedisClient(IP, PORT);
            //m_Subscribe = new RedisSubscription(m_RedisCli);
            //m_Subscribe.OnMessage += OnMessage;
        }
        private void Subscribe(string channel)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                using (RedisClient client = (RedisClient)m_Redis.GetClient())
                using (RedisSubscription subscription = new RedisSubscription(client))
                {
                    //subscription = new RedisSubscription(client);
                    subscription.OnSubscribe += msg =>
                    {

                    };
                    subscription.OnUnSubscribe += msg =>
                    {

                    };
                    subscription.OnMessage += OnMessage;
                    subscription.SubscribeToChannels(channel);
                }
            },channel);
            
        }
        private void OnMessage(string channel, string msg)
        {           
            if (m_Values.ContainsKey(channel))
            {
                m_Values[channel] = msg;
                m_RTDSvr.UpdateNotify();
            }
        }
        /// <summary>
        /// 更新所有RTDItem資料
        /// </summary>
        /// <param name="datas"></param>
        public void GetRTDItem(List<RTDItem> datas)
        {
            foreach (var item in datas)
            {
                GetRTDItem(item);
            }
        }
        /// <summary>
        /// 更新單一RTDItem資料
        /// </summary>
        /// <param name="data"></param>
        public void GetRTDItem(RTDItem data)
        {
            //if (m_Values == null)
            //{
            //    Connect();
            //}
            string key = string.Format("{0}.{1}", data.Channel, data.Item);
            if (m_Values.ContainsKey(key))
            {
                data.Value = m_Values[key];
            }
            else
            {
                m_Values.Add(key, "");
                Subscribe(key);
                //m_Subscribe.SubscribeToChannels(key);
                //m_Redis.SubscribeChannel(data.Channel, data.Item);
            }
        }
        ///// <summary>
        ///// 連線至Capital
        ///// </summary>
        //public void Connect()
        //{
        //    m_Values = new Dictionary<string, string>();
        //    m_Redis = new RedisLib(IP, PORT, Process.GetCurrentProcess().Id);
        //    m_Redis.OnChannelUpdate += new RedisLib.OnChannelUpdateDelegate(m_Redis_OnChannelUpdate);
        //    m_Redis.OnUnsubscribed += new EventHandler(m_Redis_OnUnsubscribed);
        //}

        //private void m_Redis_OnUnsubscribed(object sender, EventArgs e)
        //{
        //    if (sender == null) { return; }
        //    string key = sender.ToString();
        //    if (m_Values.ContainsKey(key))
        //    {
        //        string[] items = key.Split('.');
        //        m_Redis.SubscribeChannel(items[0], items[1]);
        //    }
        //}

        private void m_Redis_OnChannelUpdate(string channel, string item, string value)
        {
            string key = string.Format("{0}.{1}", channel, item);
            if (m_Values.ContainsKey(key))
            {
                m_Values[key] = value;
                m_RTDSvr.UpdateNotify();
            }
        }
        /// <summary>
        /// 與Capital斷線
        /// </summary>
        public void Disconnect()
        {
            m_Values.Clear();
            m_Redis.Dispose();
            //m_Subscribe.Dispose();
            //m_RedisCli.Dispose();
            //m_Redis.OnChannelUpdate -= new RedisLib.OnChannelUpdateDelegate(m_Redis_OnChannelUpdate);
            //m_Redis.Close();
        }                
    }
}