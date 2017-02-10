using System;

namespace PCF.Class
{
    public class PCF_FxRate
    {
        private string DEL = "DELETE FROM tblFXRate WHERE DataDate='{0}' AND ETFCode='{1}' AND Base='{2}' AND Quoted='{3}' ";
        private string INS = "INSERT INTO tblFXRate (DataDate,ETFCode,Base,Quoted,FxRate) VALUES('{0}','{1}','{2}','{3}',{4}) ";
               

        #region Property
        public DateTime DataDate { get; private set; }
        public string ETFCode { get; private set; }
        public string Base { get; set; }
        public string Quoted { get; set; }
        public double Value { get; set; }
        #endregion
                
        public PCF_FxRate(string etfcode, DateTime dataDate)
        {
            ETFCode = etfcode;            
            DataDate = dataDate;
        }

        public string Insert()
        {
            //string del = string.Format(DEL, DataDate.ToString(ETF.DATE), ETFCode, Base, Quoted);
            //string ins = string.Format(INS, DataDate.ToString(ETF.DATE), ETFCode, Base, Quoted, Value);
            string del = string.Format(DEL, DateTime.Now.ToString(ETF.DATE), ETFCode, Base, Quoted);
            string ins = string.Format(INS, DateTime.Now.ToString(ETF.DATE), ETFCode, Base, Quoted, Value);
            foreach (var sql in Utility.SQL)
            {
                sql.DoExecute(del);
                sql.DoExecute(ins);
            }
            return string.Empty;
        }
        public new  string ToString()
        {
            return $"{Quoted},{Value}" ;
        }
    }
}
