using System;
using System.Collections.Generic;
using Bloomberglp.Blpapi;

namespace BLParser
{
    [Serializable]
    public class Subscribe : Parser
    {
        #region Variable        
        private List<string> m_Errors;
        #endregion

        #region Property        
        public override bool HasError
        {
            get { return m_Errors != null && m_Errors.Count > 0; }
        }
        public List<string> Errors
        {
            get
            {
                if (m_Errors == null) { m_Errors = new List<string>(); }
                return m_Errors;
            }
        }

        public object this[object CorrelationID, string Field]
        {
            get
            {
                if (!Collection.Securities.ContainsKey(CorrelationID.ToString())) { return null; }
                Security s = Collection[CorrelationID.ToString()];
                if (!s.Values.ContainsKey(Field)) { return null; }
                return s[Field];
            }
        }
        #endregion

        public static Subscribe Create(Event e)
        {
            if (e.Type != Event.EventType.SUBSCRIPTION_DATA)
            {
                return null;
            }
            return new Subscribe(e);
        }
        private Subscribe(Event e)
            : base(e)
        {
            Collection = new SecurityCollection(e);
        }

        public override string ToString()
        {
            return m_MsgString;
        }
    }
}