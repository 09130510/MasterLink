using System;
using System.Collections.Generic;
using System.Linq;
using Bloomberglp.Blpapi;

namespace BLParser
{
    [Serializable]
    public class Security
    {
        private static string[] LAST_PRICE = { "LAST_PRICE", "EVT_TRADE_PRICE_RT" };
        #region Variable
        private string m_SecurityName;
        private Dictionary<string, object> m_Values;        
        private List<string> m_FieldExceptions;
        #endregion

        #region Property
        public string Name { get { return m_SecurityName; } }
        public object this[string Field]
        {
            get
            {
                if (!Values.ContainsKey(Field)) { return null; }
                return Values[Field];
            }
        }
        public Dictionary<string, object> Values
        {
            get
            {
                if (m_Values == null) { m_Values = new Dictionary<string, object>(); }
                return m_Values;
            }
        }
        public bool HasException
        {
            get { return FieldEx != null && FieldEx.Count > 0; }
        }        
        public List<string> FieldEx
        {
            get
            {                
                if (m_FieldExceptions == null) { m_FieldExceptions = new List<string>(); }
                return m_FieldExceptions;
            }
        }
        #endregion
        
        public Security(Message m)
        {
            m_SecurityName = m.CorrelationID.IsObject ? m.CorrelationID.Object.ToString() : m.CorrelationID.Value.ToString();

            foreach (var item in m.Elements)
            {
                if (item.NumValues <= 0) { continue; }
                string key = item.Name.ToString();
                object value = item.GetValue();

                if (LAST_PRICE.Contains(key)) { key = "LAST_PRICE"; }
                Type t = value.GetType();
                //if (t.Namespace == "Bloomberglp.Blpapi")
                //{
                //    Console.WriteLine("~~~~~~~" + value.GetType() + " " + key);
                //}
                if (t.Namespace == "Bloomberglp.Blpapi" && value is Datetime)
                {
                    value = ((Datetime)value).ToSystemDateTime().ToString();
                }
                if ((int)(value.GetType().Attributes & System.Reflection.TypeAttributes.Serializable) == (int)System.Reflection.TypeAttributes.Serializable)
                {
                    if (!Values.ContainsKey(key))
                    {
                        Values.Add(key, value);
                    }
                    else
                    {
                        Values[key] = value;
                    }
                }
            }
        }
        public Security(Element e)
        {

            m_SecurityName = e.GetElementAsString(Parser.SECURITY);
            if (e.HasElement(Parser.SECURITY_ERROR))
            {
                Element securityErr = e.GetElement(Parser.SECURITY_ERROR);
                int sErrCnt = securityErr.NumValues;

                for (int k = 0; k < sErrCnt; ++k)
                {
                    FieldEx.Add(Parser.ErrorElement(this, "Security Error", securityErr.GetValueAsElement(k), m_SecurityName));
                }
            }

            if (e.HasElement(Parser.FIELD_DATA))
            {
                Element fields = e.GetElement(Parser.FIELD_DATA);

                int fieldsCnt = fields.NumElements;
                for (int j = 0; j < fieldsCnt; ++j)
                {
                    Element field = fields.GetElement(j);
                    if (field.NumValues <= 0) { continue; }
                    string key = field.Name.ToString();
                    object value = field.GetValue();
                    if (LAST_PRICE.Contains(key)) { key = "LAST_PRICE"; }
                    Type t = value.GetType();
                    //if (t.Namespace == "Bloomberglp.Blpapi")
                    //{
                    //    Console.WriteLine("~~~~~~~" + value.GetType() +" "+  key);
                    //}
                    if (t.Namespace == "Bloomberglp.Blpapi" && value is Datetime)
                    {
                        value = ((Datetime)value).ToSystemDateTime().ToString();
                    }
                    if ((int)(value.GetType().Attributes & System.Reflection.TypeAttributes.Serializable) == (int)System.Reflection.TypeAttributes.Serializable)
                    {
                        if (!Values.ContainsKey(key))
                        {
                            Values.Add(key, value);
                        }
                        else
                        {
                            Values[key] = value;
                        }
                        //else
                        //{
                        //    Console.WriteLine("^^^^^" + value.GetType().ToString() + "    " + value.GetType().Attributes);
                        //}
                        //Values.Add(key, field.GetValue());
                    }
                }
            }

            if (e.HasElement(Parser.FIELD_EXCEPTIONS))
            {
                Element fieldExceptions = e.GetElement(Parser.FIELD_EXCEPTIONS);

                int fExcepCnt = fieldExceptions.NumValues;

                for (int k = 0; k < fExcepCnt; ++k)
                {
                    FieldEx.Add(Parser.ErrorElement(this, "FieldException", fieldExceptions.GetValueAsElement(k), m_SecurityName));
                }
            }

        }

        #region Public
        public bool Join(Security s)
        {
            if (s.Name != Name) { return false; }

            foreach (var item in s.FieldEx)
            {
                FieldEx.Add(item);
            }

            foreach (var item in s.Values)
            {
                if (Values.ContainsKey(item.Key))
                {
                    Values[item.Key] = item.Value;
                }
                else
                {
                    Values.Add(item.Key, item.Value);
                }
            }
            return true;
        }
        #endregion
    }
}