using System;


namespace PCF.Class
{
    public class PCF_FUND : IPCF
    {
        private string DEL = "DELETE FROM tblFundOfPCF WHERE DataDate='{0}' AND ETFCode='{1}' AND PID='{2}' ";
        private string INS = "INSERT INTO tblFundOfPCF (DataDate,ETFCode,Exchange,PID,Name,PCFUnits,TotalUnits,Weights,YP) VALUES('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8}) ";

        #region Variable
        private DateTime m_IdentifyDate;
        #endregion

        #region Property
        public DateTime DataDate { get; private set; }
        public string ETFCode { get; private set; }
        public string PID { get; set; }
        public string Name { get; set; }
        public double PCFUnits { get; set; }
        public double TotalUnits { get; set; }
        public decimal Weights { get; set; }
        public double YP { get; set; }
        public Exch Exch { get; set; }
        #endregion

        public PCF_FUND(string etfcode, DateTime identifyDate, DateTime dataDate)
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
            if (!Utility.IgnoreDate && Utility.DayCompare(DataDate, m_IdentifyDate)<0)
            {
                return PID + "日期不對";
            }
            string del = string.Format(DEL, DateTime.Now.ToString(ETF.DATE), ETFCode, PID);
            string ins = string.Format(INS, DateTime.Now.ToString(ETF.DATE), ETFCode, Exch, PID, Name, PCFUnits, TotalUnits, Weights, YP);
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
