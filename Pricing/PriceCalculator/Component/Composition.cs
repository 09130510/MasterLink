using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriceCalculator.Utility;
using System.Data;

namespace PriceCalculator.Component
{
    /// <summary>
    /// ETF的組成成份父類, 不單指PCF
    /// </summary>
    public abstract class Composition
    {
        protected class Lock
        {
            public DateTime ExpiredDate { get; set; }
            public decimal YP { get; set; }
        }

        #region Variable
        public const string CLEARBYETFSQL = "DELETE FROM ETFForBrian..tblFixYstPrice WHERE ETFCode='{0}' ";
        public const string CNTBYETFSQL = "SELECT COUNT(*) FROM ETFForBrian..tblFixYstPrice WHERE ETFCode='{0}' ";
        private const string CNTBYPIDSQL = "SELECT COUNT(*) FROM ETFForBrian..tblFixYstPrice WHERE ETFCode='{0}' AND Type ='{1}' AND PID ='{2}'  ";
        private const string INSSQL = "INSERT INTO ETFForBrian..tblFixYstPrice(ExpiredDate, ETFCode, Type, PID, YP) Values('{0}','{1}','{2}','{3}',{4})";
        private const string UPDSQL = "UPDATE ETFForBrian..tblFixYstPrice SET ExpiredDate='{0}' WHERE ETFCode='{1}' AND Type ='{2}' AND PID ='{3}'  ";

        private decimal m_YP = PriceLib.MktPrice.NULLVALUE;
        private decimal m_MP = PriceLib.MktPrice.NULLVALUE;
        private CalculationType m_CType = CalculationType.None;
        #endregion

        #region Property
        public string ETFCode { get; protected set; }
        public virtual Exch Exch { get; protected set; }
        public virtual string BaseCurncy { get; protected set; }
        /// <summary>
        /// 取最新匯率
        /// </summary>
        public virtual decimal FXRate { get { return (decimal)Util.FXRates[ETFCode, BaseCurncy, "TWD"]; } }
        public virtual decimal CValue { get; protected set; }

        public virtual string Capital { get; protected set; }
        public virtual string PATS { get; protected set; }
        public virtual string iPush { get; protected set; }
        public virtual string Redis { get; protected set; }
        public virtual string xQuote { get; protected set; }
        public string PID { get; protected set; }
        public decimal Units { get; protected set; }
        public decimal Shares { get; protected set; }
        public decimal PCFUnits { get; protected set; }
        public decimal TotalUnits { get; protected set; }

        public decimal YP
        {
            get
            {
                if (AssignedYP != PriceLib.MktPrice.NULLVALUE)
                {
                    return AssignedYP;
                }
                return m_YP;
            }
            set
            {
                //20160613 試看看Capital清盤後會不會算對
                //啊 不對 會有下午盤的問題..
                //20160630 Brian說不管下午盤
                //if (m_YP != PriceLib.MktPrice.NULLVALUE || value == default(decimal) || value == m_YP)
                if (value == default(decimal) || value == m_YP)
                {
                    return;
                }
                m_YP = value;
            }
        }
        public decimal MP
        {
            get { return AssignedMP != PriceLib.MktPrice.NULLVALUE ? AssignedMP : m_MP; }
            set
            {
                if (value == m_MP) { return; }
                m_MP = value;
            }
        }
        public decimal AssignedMP { get; set; }
        public decimal AssignedYP { get; set; }
        public DateTime LockExpired { get; set; }
        #endregion

        public Composition(DataRow row)
        {
            ETFCode = row["ETFCODE"].ToString();
            Exch = row["EXCHANGE"].ToEnum<Exch>();
            PID = row["PID"].ToString();
            PCFUnits = Convert.ToDecimal(row["PCFUNITS"]);
            TotalUnits = Convert.ToDecimal(row["TOTALUNITS"]);
            AssignedMP = Convert.ToDecimal(row["ASSIGNEDMP"]);
            AssignedYP = Convert.ToDecimal(row["YP"]);
            string str = row["ExpiredDate"].ToString();
            if (!string.IsNullOrEmpty(str))
            {
                LockExpired = DateTime.Parse(str);
                AssignedYP = Convert.ToDecimal(row["LockYP"]);
            }
        }       

        public void Start(ETF etf)
        {
            if (etf.ETFCode != ETFCode) { return; }
            m_CType = etf.CType;
            Shares = (decimal)(m_CType == CalculationType.PCFUnits ? etf.PRShares : etf.PublicShares);
            Units = m_CType == CalculationType.PCFUnits ? PCFUnits : TotalUnits;
        }
        public void Stop()
        {
            m_CType = CalculationType.None;
            Shares = 0M;
            Units = 0M;            
            YP = MP = PriceLib.MktPrice.NULLVALUE;
        }
        public virtual string ToExcel()
        {
            return $"{PID}\r\n{TotalUnits}\r\n{YP}\r\n{MP}\r\n{CValue}\r\n{FXRate}\r\n1";
        }

        protected void LockYP(DateTime expired, string type)
        {
            string str2;
            string sql = $"SELECT COUNT(*) FROM ETFForBrian..tblFixYstPrice WHERE ETFCode='{ETFCode}' AND Type ='{type}' AND PID ='{PID}'  ";
            if (Util.SQL.Query<int>(sql, null).First<int>() > 0)
            {
                str2 = $"UPDATE ETFForBrian..tblFixYstPrice SET ExpiredDate='{expired.ToString("yyyy/MM/dd")}' WHERE ETFCode='{ETFCode}' AND Type ='{type}' AND PID ='{PID}'  ";
            }
            else
            {
                str2 = $"INSERT INTO ETFForBrian..tblFixYstPrice(ExpiredDate, ETFCode, Type, PID, YP) Values('{expired.ToString("yyyy/MM/dd")}','{ETFCode}','{type}','{PID}',{YP})";
            }
            Util.SQL.DoExecute(str2);
        }
    }
}