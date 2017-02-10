using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using QuickFix;
using Util.Extension.Class;

namespace OrderProcessor.SinoPac
{
    public partial class SinoPacProcessor : Processor
    {
        #region Event
        public delegate void OrderReplyDelegate(SinoPacProcessor sender, SinoPacRPT reply);
        public delegate void MatchReplyDelegate(SinoPacProcessor sender, SinoPacRPT reply);
        public event OrderReplyDelegate OrderReplyEvent;
        public event MatchReplyDelegate MatchReplyEvent;
        #endregion

        #region Variable
        private SocketInitiator m_Initiator;
        private QuickFix.Application m_Application;
        private SessionSettings m_Settings = new SessionSettings(ININAME);
        private FileStoreFactory m_StoreFactory = null;
        private MessageFactory m_MessageFactory = new DefaultMessageFactory();
        private ScreenLogFactory m_LogFactory = null;
        private ArrayList m_SessionList = null;

        private bool m_isResetSeq;
        private bool m_isConnect = false;
        internal Dictionary<string, SinoPacORD> m_Orders = new Dictionary<string, SinoPacORD>();
        internal List<SinoPacRPT> m_RPTs = new List<SinoPacRPT>();
        internal List<SinoPacRPT> m_Matchs = new List<SinoPacRPT>();
        #endregion

        #region Property
        public bool isResetSeq
        {
            get { return m_isResetSeq; }
            set
            {
                if (value == m_isResetSeq) { return; }
                m_isResetSeq = value;
            }
        }
        public bool isConnect
        {
            get { return m_isConnect; }
            set
            {
                if (value == m_isConnect)
                {
                    return;
                }
                m_isConnect = value;

                Config Config = new Config(@"./", "Config.ini");
                DateTime lastTime = Config.GetSetting<DateTime>("ORDER", "LastConnectTime");
                if (lastTime.ToString("yyyy/MM/dd") != DateTime.Today.ToString("yyyy/MM/dd"))
                {
                    Config.SetSetting("ORDER", "Seqno", 0);
                }
                Config.SetSetting("ORDER", "LastConnectTime", DateTime.Now.ToString());
                NotificationCenter.Instance.Post(GetType().Name, new Notification("ISLOGIN", m_isConnect ? "FIX LOGIN" : "FIX LOGOUT"));
                if (m_isConnect) { Connected(); }
                else { Disconnected(); }
            }
        }
        public SessionID Session
        {
            get
            {
                if (m_SessionList != null && m_SessionList.Count > 0)
                {
                    return (SessionID)m_SessionList[0];
                }
                return null;
            }
        }
        public int Seqno
        {
            get
            {
                Config Config = new Config(@"./", "Config.ini");
                return Config.GetSetting<int>("ORDER", "Seqno");
            }
        }
        public Dictionary<string, SinoPacORD> Orders { get { return m_Orders; } }
        public List<SinoPacRPT> RPTs { get { return m_RPTs; } }
        public List<SinoPacRPT> Matchs { get { return m_Matchs; } }
        #endregion

        public SinoPacProcessor()
        {
            Extractor.ExtractResourceToFile("OrderProcessor.SinoPac.quickfix_net.dll", "quickfix_net.dll", true);
            Extractor.ExtractResourceToFile("OrderProcessor.SinoPac.quickfix_net_messages.dll", "quickfix_net_messages.dll", true);

            m_Application = new FixClient(this);
            m_StoreFactory = new FileStoreFactory(m_Settings);
            m_LogFactory = new ScreenLogFactory(m_Settings);
        }

        public void Start()
        {
            if (isConnect && m_Initiator != null) { return; }
            m_Initiator = new SocketInitiator(m_Application, m_StoreFactory, m_Settings, m_MessageFactory);
            m_Initiator.start();
            Thread t = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(3000);
                m_SessionList = m_Initiator.getSessions();
            }));
            t.Start();
        }
        public void Stop()
        {
            if (!isConnect) { return; }
            m_Initiator.stop();
            m_Initiator.Dispose();
            m_Initiator = null;
        }
        public int GetSeqno()
        {
            Config Config = new Config(@"./", "Config.ini");
            int no = Config.GetSetting<int>("ORDER", "Seqno") + 1;
            Config.SetSetting("ORDER", "Seqno", no);
            return no;
        }
        public void Order(SinoPacORD orderData)
        {
            if (isConnect)
            {
                m_Orders.Add(orderData.ClOrdID, orderData);
                QuickFix.Session.sendToTarget(orderData.Order, Session);
            }
        }
        public void Cancel(SinoPacORD cancelData)
        {
            if (isConnect)
            {
                m_Orders.Add(cancelData.ClOrdID, cancelData);
                QuickFix.Session.sendToTarget(cancelData.Cancel, Session);
            }
        }
        public void Replace(SinoPacORD replaceData)
        {
            if (isConnect)
            {
                m_Orders.Add(replaceData.ClOrdID, replaceData);
                QuickFix.Session.sendToTarget(replaceData.Replace, Session);
            }
        }

        internal void Connected()
        {
            base.Connected(this, EventArgs.Empty);
        }
        internal void Disconnected()
        {
            base.Disconnected(this, EventArgs.Empty);
        }
        internal void OrderReply(SinoPacRPT reply)
        {
            m_RPTs.Add(reply);
            if (OrderReplyEvent != null)
            {
                OrderReplyEvent(this, reply);
            }
        }
        internal void MatchReply(SinoPacRPT reply)
        {
            m_Matchs.Add(reply);
            if (MatchReplyEvent != null)
            {
                MatchReplyEvent(this, reply);
            }
        }
    }
}