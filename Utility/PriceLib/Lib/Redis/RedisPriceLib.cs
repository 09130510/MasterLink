using log4net;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceLib.Redis
{
    public class RedisPriceLib : RedisLib
    {
        public RedisPriceLib(string ip, int port) : base(ip, port)
        {
            m_Log = LogManager.GetLogger(typeof(RedisPriceLib));
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
                        subscription.OnMessage += _OnPriceReceive;
                        subscription.SubscribeToChannels(m_Subs.Keys.ToArray());
                    }
                }
            }
            catch (Exception ex) { }
        }
        private void _OnPriceReceive(string channel, string msg)
        {
            MD md = MD.Create(channel, msg);
            if (md != null)
            {
                RaiseMktPrice(md.ID, MktPrice.NULLVALUE, (decimal)md.MP, MktPrice.NULLVALUE, MktPrice.NULLVALUE, MktPrice.NULLINT, MktPrice.NULLINT);
            }
            else
            {
                MB mb = MB.Create(channel, msg);
                if (mb != null)
                {
                    RaiseMktPrice(mb.ID, m_Newest[mb.ID].YP, m_Newest[mb.ID].MP, (decimal)mb.Ask[0], (decimal)mb.Bid[0], mb.AVolume[0], mb.BVolume[0]);
                }
            }
        }
        #endregion

        #region Public
        public void GetYesterdayPrice(SubscribeType stype, params string[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (string.IsNullOrEmpty(items[i])) { continue; }

                m_Log.Info($"{nameof(GetYesterdayPrice)} - {items[i]}");
                double yp = -1D;
                double mp = -1D;
                double ap = -1D;
                double bp = -1D;
                int aq = MktPrice.NULLINT;
                int bq = MktPrice.NULLINT;
                byte[][] values = _HashGetAll((int)stype, items[i]);
                if ((values.Length == 0) || (values[0] == null)) { continue; }

                for (int j = 0; j < values.Length; j += 2)
                {
                    string channel = Encoding.UTF8.GetString(values[j]);
                    string msg = Encoding.UTF8.GetString(values[j + 1]);
                    MR mr = MR.Create(channel, msg);
                    if (mr != null)
                    {
                        yp = mr.Ref;
                    }
                    else
                    {
                        MD md = MD.Create(items[i], channel, msg);
                        if (md != null)
                        {
                            mp = md.MP;
                        }
                        else
                        {
                            MB mb = MB.Create(items[i], channel, msg);
                            if (mb != null)
                            {
                                ap = mb.Ask[0];
                                bp = mb.Bid[0];
                                aq = mb.AVolume[0];
                                bq = mb.BVolume[0];
                            }
                        }
                    }
                }
                RaiseMktPrice(items[i], yp, mp, ap, bp, aq, bq);
            }
        }
        public void Subscribe(SubscribeType stype, params string[] items)
        {
            bool hasAddItem = false;
            string mkt = stype.MktChannel();
            string bidoffer = stype.BidOfferChannel();

            GetYesterdayPrice(stype, items);
            lock (m_Subs)
            {
                foreach (var item in items)
                {
                    if (string.IsNullOrEmpty(item)) { continue; }
                    string mktchannel = $"{mkt}{item}";
                    string bochannel = $"{bidoffer}{item}";

                    if (!m_Subs.ContainsKey(mktchannel))
                    {
                        m_Subs.Add(mktchannel, 0);
                        hasAddItem = true;
                        m_Log.Info($"{nameof(Subscribe)} - Add New Market: {mktchannel}");
                    }
                    m_Subs[mktchannel]++;

                    if (!m_Subs.ContainsKey(bochannel))
                    {
                        m_Subs.Add(bochannel, 0);
                        hasAddItem = true;
                        m_Log.Info($"{nameof(Subscribe)} - Add New BidOffer: {bochannel}");
                    }
                    m_Subs[bochannel]++;
                }
                if (hasAddItem) { _ResetSubscribeThread(nameof(Subscribe)); }
            }
        }
        public void Unsubscribe(SubscribeType stype, params string[] items)
        {
            bool hasRemoveItem = false;
            string mkt = stype.MktChannel();
            string bidoffer = stype.BidOfferChannel();

            lock (m_Subs)
            {
                foreach (var item in items)
                {
                    if (string.IsNullOrEmpty(item)) { continue; }
                    string mktchannel = $"{mkt}{item}";
                    string bochannel = $"{bidoffer}{item}";

                    if (m_Subs.ContainsKey(mktchannel))
                    {
                        m_Subs[mktchannel]--;
                        if (m_Subs[mktchannel] <= 0)
                        {
                            m_Subs.Remove(mktchannel);
                            hasRemoveItem = true;
                            m_Log.Info($"{nameof(Unsubscribe)} - Delete Market: {mktchannel}");
                        }
                    }
                    if (m_Subs.ContainsKey(bochannel))
                    {
                        m_Subs[bochannel]--;
                        if (m_Subs[bochannel] <= 0)
                        {
                            m_Subs.Remove(bochannel);
                            hasRemoveItem = true;
                            m_Log.Info($"{nameof(Unsubscribe)} - Delete BidOffer: {bochannel}");
                        }
                    }
                }
                if (hasRemoveItem) { _ResetSubscribeThread(nameof(Unsubscribe)); }
            }
        }
        public void UnsubscribeAll()
        {
            if (m_Subs == null || m_Subs.Count <= 0) { return; }
            lock (m_Subs)
            {
                m_Subs.Clear();
                _ResetSubscribeThread(nameof(UnsubscribeAll));
            }
        }
        #endregion
    }
}