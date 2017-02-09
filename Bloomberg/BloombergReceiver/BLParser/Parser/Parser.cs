using System;
using Bloomberglp.Blpapi;

namespace BLParser
{
    [Serializable]
    public abstract class Parser
    {
        #region Const / Static
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
        public static readonly Name MARKETDATAEVENTS = Name.GetName("MarketDataEvents");
        #endregion

        #region Variable        
        protected string m_MsgString;
        protected SecurityCollection m_Collection;
        #endregion

        #region Peoperty        
        public SecurityCollection Collection
        {
            get { return m_Collection; }
            set { m_Collection = value; }
        }
        public abstract bool HasError { get; }
        #endregion

        public Parser(Event e)
        {
            if (e != null)
            {
                foreach (Message msg in e)
                {
                    m_MsgString += msg.CorrelationID + "   " + msg.ToString() + "\r\n";
                }
            }
        }
        public static string ErrorElement(object sender, string leadingStr, Element err, string security = "")
        {
            string errStr = string.Format("[{0}]{5}   Code:{1}    Category:{2}    Subcategory:{3} Message:{4}", leadingStr, err.GetElementAsString(CODE), err.GetElementAsString(CATEGORY), err.GetElementAsString(SUBCATEGORY), err.GetElementAsString(MESSAGE), security == "" ? "" : "    Security:" + security);
            //NC.Instance.Error(sender, errStr);
            return errStr;
        }
    }
}
