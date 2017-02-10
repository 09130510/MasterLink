using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SorApi
{
    using TIndex = UInt32;

    public class SorObj
    {
        #region Variable
        private SorTable m_Table;
        private Dictionary<string, object> m_Fields = new Dictionary<string, object>();
        #endregion

        #region Property
        public string this[string FieldName]
        {
            get
            {
                if (!m_Fields.ContainsKey(FieldName)) { return null; }
                return m_Fields[FieldName] == null ? string.Empty : m_Fields[FieldName].ToString();
            }
        }
        public string this[int FieldIndex]
        {
            get
            {
                if (m_Fields.Count <= FieldIndex) { return null; }
                return m_Fields.ElementAt(FieldIndex).Value.ToString();
            }
        }

        public string TableName { get { return m_Table.Properties.Name; } }
        public string[] Fields { get { return m_Fields.Keys.ToArray(); } }
        #endregion

        public SorObj(SorTable table)
        {
            m_Table = table;
            SorFields fields = table.Fields;
            TIndex fcount = fields.Count;
            for (TIndex i = 0; i < fcount; i++)
            {
                SorProperties properities = fields.IndexField(i).Properties;
                string fldName = properities.Name;
                if (!m_Fields.ContainsKey(fldName))
                {
                    m_Fields.Add(fldName, null);
                }
            }
        }
        public SorObj(SorTable table, string[] fieldValues)
            : this(table)
        {
            for (int i = 0; i < fieldValues.Length; i++)
            {
                if (m_Fields.Count <= i) { break; }
                m_Fields[m_Fields.ElementAt(i).Key] = fieldValues[i];
            }
        }
        public SorObj(SorTable table, SorObj dataObj)
            : this(table)
        {
            string[] fields = dataObj.Fields;
            foreach (var field in fields)
            {
                if (m_Fields.ContainsKey(field))
                {
                    m_Fields[field] = dataObj[field];
                }
            }
        }

        public new string ToString()
        {
            return string.Join(XTLib.XTLib.FieldSplit.ToString(), m_Fields.Values.ToArray());
        }
    }
}