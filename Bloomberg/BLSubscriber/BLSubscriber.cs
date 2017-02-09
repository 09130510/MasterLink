using System;
using System.Collections.Generic;
using Bloomberglp.Blpapi;
using EventHandler = Bloomberglp.Blpapi.EventHandler;

namespace BL
{
    public delegate void ErrorHandler();
    public class BLSubscriber
    {
        #region Const / Static
        private const string LOCAL = "localhost";
        private const string REFSERVICE = "//blp/refdata";
        private const string MKTSERVICE = "//blp/mktdata";
        private const string REQUEST = "ReferenceDataRequest";
        private const int PORT = 8194;

        public static readonly Name SECURITIES = new Name("securities");
        public static readonly Name FIELDS = new Name("fields");
        public static readonly Name OVERRIDES = new Name("overrides");
        public static readonly Name RESPONSE_ERROR = new Name("responseError");
        public static readonly Name SECURITY_ERROR = new Name("securityError");
        public static readonly Name SECURITY_DATA = new Name("securityData");
        public static readonly Name FIELD_DATA = new Name("fieldData");
        public static readonly Name FIELD_EXCEPTIONS = new Name("fieldExceptions");
        public static readonly Name FIELD_ID = new Name("fieldId");
        public static readonly Name ERROR_INFO = new Name("errorInfo");
        public static readonly Name SECURITY = new Name("security");
        public static readonly Name CODE = new Name("code");
        public static readonly Name SUBCATEGORY = new Name("subcategory");
        public static readonly Name CATEGORY = new Name("category");
        public static readonly Name MESSAGE = new Name("message");

        public static readonly Name SUBSCRIPTION_TERMINATED = Name.GetName("SubscriptionTerminated");
        public static readonly Name SLOW_CONSUMER_WARNING = Name.GetName("SlowConsumerWarning");
        public static readonly Name SLOW_CONSUMER_WARNING_CLEARED = Name.GetName("SlowConsumerWarningCleared");
        public static readonly Name DATA_LOSS = Name.GetName("DataLoss");
        public static readonly Name SOURCE = Name.GetName("source");
        #endregion

        #region Event
        public event EventHandler OnRequestReply;
        public event EventHandler OnSubscriptionReply;
        public event EventHandler OnStatusReceived;
        public event EventHandler OnOtherReceived;
        #endregion

        #region Variable
        private SessionOptions m_Options;
        private Session m_Session;
        private Service m_REFService;
        private Service m_MKTService;

        private bool m_isSlow = false;
        private List<Subscription> m_Subscriptions = new List<Subscription>();
        private List<Subscription> m_PendingSubscriptions = new List<Subscription>();
        private Dictionary<CorrelationID, object> m_PendingUnsubscribe = new Dictionary<CorrelationID, object>();
        #endregion

        public static BLSubscriber Create()
        {
            bool create, open;
            BLSubscriber r = new BLSubscriber(out create, out open);
            return create && open ? r : null;
        }
        private BLSubscriber(out bool create, out bool open)
        {
            create = _CreateSession();
            if (!create)
            {
                open = false;
                return;
            }
            open = _OpenService();
        }

        #region Public
        public void SendRequest(object ID, List<string> securities, List<string> fields)
        {
            Request request = m_REFService.CreateRequest(REQUEST);
            foreach (var item in securities)
            {
                request.Append(SECURITIES, item);
            }
            foreach (var item in fields)
            {
                request.Append(FIELDS, item);
            }
            CorrelationID CID = new CorrelationID(ID);
            m_Session.SendRequest(request, CID);
        }
        public void SendSubscription(List<string> securities, List<string> fields)
        {
            List<Subscription> sub = new List<Subscription>();
            foreach (var security in securities)
            {
                sub.Add(new Subscription(security, fields, new CorrelationID(security)));
            }
            m_Session.Subscribe(sub);
            m_Subscriptions.AddRange(sub);
        }
        public void SendUnsubscription(List<string> securities)
        {
            foreach (string security in securities)
            {
                CorrelationID cid = new CorrelationID(security);
                m_Session.Cancel(cid);

                Subscription s = _GetSubscription(cid);
                m_Subscriptions.Remove(s);
            }
        }
        public void Close()
        {
            m_Session.Stop();
        }
        #endregion

        #region Private
        private bool _CreateSession()
        {
            m_Options = new SessionOptions();
            m_Options.ServerHost = LOCAL;
            m_Options.ServerPort = PORT;
            m_Options.AutoRestartOnDisconnection = true;            

            m_Session = new Session(m_Options, new EventHandler(_ProcessEvent));            
            bool re = m_Session.Start();
            if (!re) { NotificationCenter.Instance.Error(this, "Failed to start session."); }
            return re;
        }
        private bool _OpenService()
        {
            if (!m_Session.OpenService(REFSERVICE))
            {
                NotificationCenter.Instance.Error(this, "Failed to open " + REFSERVICE);
                return false;
            }
            if (!m_Session.OpenService(MKTSERVICE))
            {
                NotificationCenter.Instance.Error(this, "Failed to open " + MKTSERVICE);
                return false;
            }
            m_REFService = m_Session.GetService(REFSERVICE);
            m_MKTService = m_Session.GetService(MKTSERVICE);
            return true;
        }
        private void _ProcessEvent(Event e, Session s)
        {
            switch (e.Type)
            {
                case Event.EventType.RESPONSE:
                case Event.EventType.PARTIAL_RESPONSE:
                    if (OnRequestReply != null) { OnRequestReply(e, s); }
                    break;
                case Event.EventType.SUBSCRIPTION_DATA:
                    if (OnSubscriptionReply != null) { OnSubscriptionReply(e, s); }
                    break;
                case Event.EventType.ADMIN:
                    _ProcessAdmin(e, s);
                    if (OnStatusReceived != null) { OnStatusReceived(e, s); }
                    break;
                case Event.EventType.SUBSCRIPTION_STATUS:
                    _ProcessSubscriptionStatus(e, s);
                    if (OnStatusReceived != null) { OnStatusReceived(e, s); }
                    break;
                case Event.EventType.AUTHORIZATION_STATUS:
                case Event.EventType.REQUEST_STATUS:
                case Event.EventType.RESOLUTION_STATUS:
                case Event.EventType.TOKEN_STATUS:
                case Event.EventType.TOPIC_STATUS:
                case Event.EventType.SERVICE_STATUS:
                case Event.EventType.SESSION_STATUS:
                    //foreach (var item in e)
                    //{
                    //    Console.WriteLine(e.Type + " " + item.ToString());
                    //}                    
                    if (OnStatusReceived != null) { OnStatusReceived(e, s); }
                    break;
                default:
                    if (OnOtherReceived != null) { OnOtherReceived(e, s); }
                    break;
            }
        }
        private void _ProcessAdmin(Event e, Session s)
        {
            List<CorrelationID> cidsToCancel = null;
            bool previouslySlow = m_isSlow;
            foreach (Message msg in e)
            {
                // An admin event can have more than one messages.
                if (msg.MessageType == SLOW_CONSUMER_WARNING) { m_isSlow = true; }
                if (msg.MessageType == SLOW_CONSUMER_WARNING_CLEARED) { m_isSlow = false; }
                if (msg.MessageType == DATA_LOSS)
                {
                    CorrelationID cid = msg.CorrelationID;
                    if (msg.HasElement(SOURCE))
                    {
                        string sourceStr = msg.GetElementAsString(SOURCE);
                        if (sourceStr.CompareTo("InProc") == 0 && !m_PendingUnsubscribe.ContainsKey(cid))
                        {
                            // DataLoss was generated "InProc". This can only
                            // happen if applications are processing events
                            // slowly and hence are not able to keep-up with
                            // the incoming events.
                            if (cidsToCancel == null)
                            {
                                cidsToCancel = new List<CorrelationID>();
                            }
                            cidsToCancel.Add(cid);
                            m_PendingUnsubscribe.Add(cid, null);
                        }
                    }
                }
            }

            if (cidsToCancel != null) { m_Session.Cancel(cidsToCancel); }
            if ((previouslySlow && !m_isSlow) && m_PendingSubscriptions.Count > 0)
            {
                // Session was slow but is no longer slow. subscribe to any
                // topics for which we have previously received
                // SUBSCRIPTION_TERMINATED                    
                m_Session.Subscribe(m_PendingSubscriptions);
                m_PendingSubscriptions.Clear();
            }
        }
        private void _ProcessSubscriptionStatus(Event e, Session s)
        {
            List<Subscription> subscriptionList = null;
            foreach (Message msg in e)
            {
                CorrelationID cid = msg.CorrelationID;
                string topic = (string)cid.Object;

                if (msg.MessageType == SUBSCRIPTION_TERMINATED && m_PendingUnsubscribe.Remove(cid))
                {
                    // If this message was due to a previous unsubscribe
                    Subscription subscription = _GetSubscription(cid);
                    if (m_isSlow)
                    {
                        m_PendingSubscriptions.Add(subscription);
                    }
                    else
                    {
                        if (subscriptionList == null)
                        {
                            subscriptionList = new List<Subscription>();
                        }
                        subscriptionList.Add(subscription);
                    }
                }
            }
        }

        private Subscription _GetSubscription(CorrelationID cid)
        {
            foreach (Subscription subscription in m_Subscriptions)
            {
                if (subscription.CorrelationID.Equals(cid))
                {
                    return subscription;
                }
            }
            return null;
            //throw new KeyNotFoundException(
            //        "No subscription found corresponding to cid = "
            //            + cid.ToString());
        }
        //private void _ProcessEvent(Event e, Session s)
        //{
        //    switch (e.Type)
        //    {
        //        //case Event.EventType.ADMIN:
        //        //    break;
        //        //case Event.EventType.AUTHORIZATION_STATUS:
        //        //    break;
        //        case Event.EventType.REQUEST:
        //            break;
        //        case Event.EventType.REQUEST_STATUS:
        //            break;
        //        case Event.EventType.RESOLUTION_STATUS:
        //            break;
        //        case Event.EventType.SERVICE_STATUS:
        //            break;
        //        case Event.EventType.SESSION_STATUS:
        //            break;
        //        case Event.EventType.SUBSCRIPTION_DATA:
        //            break;
        //        case Event.EventType.SUBSCRIPTION_STATUS:
        //            break;
        //        case Event.EventType.TIMEOUT:
        //            break;
        //        case Event.EventType.TOKEN_STATUS:
        //            break;
        //        case Event.EventType.TOPIC_STATUS:
        //            break;
        //        case Event.EventType.RESPONSE:
        //        case Event.EventType.PARTIAL_RESPONSE:
        //            _ProcessResponseEvent(e);
        //            break;

        //        default:
        //            break;
        //    }
        //}

        //private void _ProcessResponseEvent(Event e)
        //{
        //    foreach (Message msg in e)
        //    {
        //        if (msg.HasElement(RESPONSE_ERROR))
        //        {
        //            _ErrorElement("REQUEST FAILED", msg.GetElement(RESPONSE_ERROR));
        //            continue;
        //        }
        //        Element securities = msg.GetElement(SECURITY_DATA);
        //        int securityCnt = securities.NumElements;
        //        for (int i = 0; i < securityCnt; ++i)
        //        {
        //            Element security = securities.GetValueAsElement(i);
        //            string SecutityName = security.GetElementAsString(SECURITY);
        //            if (security.HasElement(SECURITY_ERROR))
        //            {
        //                _ErrorElement("SECURITY FAILED", security.GetElement(SECURITY_ERROR), SecutityName);
        //                continue;
        //            }
        //            Element fields = security.GetElement(FIELD_DATA);
        //            int fieldsCnt = fields.NumElements;
        //            for (int j = 0; j < fieldsCnt; ++j)
        //            {
        //                Element field = fields.GetElement(j);
        //                //System.Console.WriteLine(field.Name + "\t\t" + field.GetValueAsString());
        //            }
        //            Element fieldExceptions = security.GetElement(FIELD_EXCEPTIONS);
        //            int fExcepCnt = fieldExceptions.NumValues;

        //            for (int k = 0; k < fExcepCnt; ++k)
        //            {
        //                Element fieldException = fieldExceptions.GetValueAsElement(k);
        //                _ErrorElement("FIELD EXCEPTION", fieldException, SecutityName);
        //            }

        //        }
        //    }
        //}
        //private void _ErrorElement(string leadingStr, Element err, string security = "")
        //{
        //    NC.Instance.Error(this, string.Format("[{0}]{5}   Code:{1}    Category:{2}    Subcategory:{3} Message:{4}", leadingStr, err.GetElementAsString(CODE), err.GetElementAsString(CATEGORY), err.GetElementAsString(SUBCATEGORY), err.GetElementAsString(MESSAGE), security == "" ? "" : "    Security:" + security));
        //}
        #endregion
    }
}