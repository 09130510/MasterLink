using PCF.Class.JSON;
using System;
using System.Globalization;
using System.Linq;

namespace PCF.Class
{
    public class ETFDaily
    {
        private string DEL = "DELETE FROM tblETFDaily WHERE DataDate='{0}' AND ETFCode='{1}' ";
        private string INS = "INSERT INTO tblETFDaily  (DataDate,ETFCode,FundAssetValue,PublicShares,PublicSharesDiff,EstPublicShares,NAV,CashDiff,PreAllot,Allot,EstCValue,EstDValue) VALUES('{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}) ";

        #region Variable
        private string m_IdentifyCode;
        private string m_ETFCode;
        private DateTime m_IdentifyDate;
        #endregion

        #region Property
        public DateTime DataDate { get; set; }
        public string ETFCode
        {
            get { return m_ETFCode; }
            set
            {
                if (value.Trim().Contains(' '))
                {
                    m_ETFCode = value.Trim().Split(' ')[0];
                }
                else if (value.Contains("（股票代號："))
                {
                    m_ETFCode = value.Replace("（股票代號：", "").Split(new string[] { "&nbsp;&nbsp;" }, StringSplitOptions.RemoveEmptyEntries)[0];
                }
                else if (string.IsNullOrEmpty(value))
                {
                    m_ETFCode = m_IdentifyCode;
                }
                else if (value.Contains("(證券代碼："))
                {
                    m_ETFCode = value.Replace("(證券代碼：", "").Split(new string[] { ")" }, StringSplitOptions.RemoveEmptyEntries)[0];
                }
                else { m_ETFCode = value; }
            }
        }
        public double FundAssetValue { get; set; }
        public double PublicShares { get; set; }
        public double PublicSharesDiff { get; set; }
        public double NAV { get; set; }
        public double CashDiff { get; set; }

        public double PreAllot { get; set; }
        public double Allot { get; set; }
        public double EstCValue { get; set; }
        public double EstDValue { get; set; }
        public double EstPublicShares { get; set; }
        #endregion

        public ETFDaily(string etfcode, DateTime date)
        {
            m_IdentifyCode = etfcode;
            m_IdentifyDate = date;
        }

        public void ValueFromJSON(JYT_ETF JSON)
        {
            DataDate = DateTime.ParseExact(JSON.anndate.Trim(), "yyyyMMdd", new CultureInfo("zh-TW"));
            ETFCode = JSON.markcd;
            FundAssetValue = double.Parse(JSON.totalav);
            PublicShares = double.Parse(JSON.osunit);
            PublicSharesDiff = double.Parse(JSON.issuesdiff);
            EstPublicShares = double.Parse(JSON.preunit);

            NAV = double.Parse(JSON.nav);
            CashDiff = int.Parse(JSON.cashdiff);

            PreAllot = double.Parse(JSON.preallot);
            Allot = double.Parse(JSON.allot);
            EstCValue = double.Parse(JSON.estcvalue);
            EstDValue = double.Parse(JSON.estdvalue);
        }
        public string Insert()
        {
            if (this.ETFCode != m_IdentifyCode)
            {
                return ETFCode + " ETF不對";
            }

            if (!Utility.IgnoreDate && Utility.DayCompare( DataDate,m_IdentifyDate) <0)
            //if (!Utility.IgnoreDate && DataDate.ToString(ETF.DATE) != m_IdentifyDate.ToString(ETF.DATE))
            {
                return ETFCode + " 日期不對";
            }

            if (EstPublicShares == 0) { EstPublicShares = PublicShares; }            
            string del = string.Format(DEL, DateTime.Now.ToString(ETF.DATE), m_ETFCode);
            string ins = string.Format(INS, DateTime.Now.ToString(ETF.DATE), m_ETFCode, FundAssetValue, PublicShares, PublicSharesDiff, EstPublicShares, NAV, CashDiff, PreAllot, Allot, EstCValue, EstDValue);
            foreach (var sql in Utility.SQL)
            {
                sql.DoExecute(del);
                sql.DoExecute(ins);
            }
            return string.Empty;
        }
    }
}