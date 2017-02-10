using PATS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATS.Class
{
    /// <summary>
    /// 交易所資料
    /// 其實是存下單種類
    /// </summary>
    public class ExchInfo
    {
        #region Variable
        private Dictionary<string, List<string>> m_OrderTypes;
        private Dictionary<string, string> m_DefultType;
        #endregion

        #region Property
        /// <summary>
        /// 列出交易所可以下的下單種類
        /// </summary>
        /// <param name="Exch"></param>
        /// <returns></returns>
        public List<string> this[string Exch]
        {
            get
            {
                if (!m_OrderTypes.ContainsKey(Exch))
                {
                    m_OrderTypes.Add(Exch, new List<string>());
                }
                return m_OrderTypes[Exch];
            }
            set
            {
                if (!m_OrderTypes.ContainsKey(Exch))
                {
                    m_OrderTypes.Add(Exch, new List<string>());
                }
                m_OrderTypes[Exch] = value;
            }
        }        
        public string[] Exchs { get { return m_OrderTypes.Keys.ToArray(); } }
        #endregion

        public ExchInfo(  )
        {
            m_DefultType = new Dictionary<string, string>();
            m_OrderTypes = Util.PATS.OrderTypes();            
        }
        private void _LoadDefaultOrderType()
        {
            m_DefultType.Clear();
            string[] dts = Util.INI["SYS"]["DEFAULTORDERTYPE"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in dts)
            {
                string[] dt = item.Split(',');
                if (!m_DefultType.ContainsKey(dt[0]))
                {
                    m_DefultType.Add(dt[0], dt[1]);
                }
            }
        }
        /// <summary>
        /// 取出預設的下單種類
        /// </summary>
        /// <param name="exch"></param>
        /// <returns></returns>
        public string DefaultType(string exch)
        {
            _LoadDefaultOrderType();
            if (m_DefultType.ContainsKey(exch))
            {
                return m_DefultType[exch];
            }
            return string.Empty;
        }

        public void SaveDefaultType(string exch, string orderType)
        {
            _LoadDefaultOrderType();
            if (!m_DefultType.ContainsKey(exch))
            {
                m_DefultType.Add(exch, orderType);
            }
            m_DefultType[exch] = orderType;

            string re = string.Empty;
            foreach (var item in m_DefultType)
            {
                re += $"{item.Key},{item.Value};";
            }
            Util.INI["SYS"]["DEFAULTORDERTYPE"] = re;
            Util.SaveConfig();
        }
    }
}
