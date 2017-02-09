using System;
using System.Collections.Generic;
using Bloomberglp.Blpapi;

namespace BLParser
{
    [Serializable]
    public class Response : Parser
    {
        #region Variable        
        private List<string> m_Errors;        
        #endregion

        #region Peoperty
        public override bool HasError { get { return Errors != null && Errors.Count > 0; } }  
        public List<string> Errors
        {
            get
            {
                if (m_Errors == null) { m_Errors = new List<string>(); }
                return m_Errors;
            }
        }        
        public object this[string Name, string Field]
        {
            get
            {
                if (!Collection.Securities.ContainsKey(Name)) { return null; }
                Security s = Collection[Name];
                if (!s.Values.ContainsKey(Field)) { return null; }
                return s[Field];
            }
        }
        #endregion
        
        public Response(Event e)
            : base(e)
        {
            foreach (Message msg in e)
            {
                if (msg.HasElement(RESPONSE_ERROR))
                {
                    //Errors.Add(msg.GetElement(RESPONSE_ERROR));
                    Errors.Add(Parser.ErrorElement(this, "Response Error", msg.GetElement(RESPONSE_ERROR)));
                    continue;
                }
                //Collections.Add(new SecurityCollection(msg.GetElement(SECURITY_DATA)));
                Collection = new SecurityCollection(msg.GetElement(SECURITY_DATA));
            }
        }
    }
}