using System;
using System.Data;
using Util.Extension;

namespace PCF.Class
{
    public class PCF_FUT : IPCF
    {
        private string DEL = "DELETE FROM tblFutureOfPCF WHERE DataDate='{0}' AND ETFCode='{1}' AND PID='{2}' ";
        private string INS = "INSERT INTO tblFutureOfPCF (DataDate,ETFCode,PID,Head,YM,Y,M,Name,PCFUnits,TotalUnits,Weights,YP) VALUES('{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}',{8},{9},{10},{11}) ";
        private string CVALUE = "SELECT Currency,CValue FROM tblFuture WHERE Head='{0}' ";        

        #region Variable
        private DateTime m_IdentifyDate;
        private string m_Head;
        #endregion

        #region Property
        public DateTime DataDate { get; private set; }
        public string ETFCode { get; private set; }
        public string PID { get; set; }
        public string Head
        {
            get { return m_Head; }
            set
            {
                m_Head = value;
                if (Utility.SQL.Length <= 0) { return; }
                DataTable dt = Utility.SQL[0].DoQuery(string.Format(CVALUE, m_Head));
                if (dt == null || dt.Rows.Count <= 0) { return; }
                Currency = dt.Rows[0]["Currency"].ToString();
                CValue =dt.Rows[0]["CValue"].ToDouble();
            }
        }
        public string YM { get; set; }
        public int Y { get; set; }
        public int M { get; set; }
        public string Name { get; set; }
        public double PCFUnits { get; set; }
        public double TotalUnits { get; set; }
        public decimal Weights { get; set; }
        public double YP { get; set; }

        public string Currency { get; private set; }
        public double CValue { get; private set; }
        #endregion

        public PCF_FUT(string etfcode, DateTime identifyDate, DateTime dataDate)
        {
            ETFCode = etfcode;
            m_IdentifyDate = identifyDate;
            DataDate = dataDate;
            YP = -1;
        }

        #region Public        
        public string Insert()
        {
            //if (!Utility.IgnoreDate && DataDate.ToString(ETF.DATE) != m_IdentifyDate.ToString(ETF.DATE))
            if (!Utility.IgnoreDate && Utility.DayCompare(DataDate, m_IdentifyDate) <0)
            {
                return PID + " 日期不對";
            }
            string del = string.Format(DEL, DateTime.Now.ToString(ETF.DATE), ETFCode, PID);
            string ins = string.Format(INS, DateTime.Now.ToString(ETF.DATE), ETFCode, PID, Head, YM, Y, M, Name, PCFUnits, TotalUnits, Weights, YP);
            foreach (var sql in Utility.SQL)
            {
                sql.DoExecute(del);
                sql.DoExecute(ins);
            }
            return string.Empty;
        }
        #endregion
    }
}
