using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCF.Class
{
    public class PCF_Margin
    {
        //private string DEL = "DELETE FROM tblMarginOfPCF WHERE DataDate='{0}' AND ETFCode='{1}' AND Currency='{2}' ";
        private static string DEL = "DELETE FROM tblMarginOfPCF WHERE DataDate='{0}' AND ETFCode='{1}' ";
        private string MYINS = "INSERT INTO tblMarginOfPCF (Identity,DataDate,ETFCode,Currency,Amount,Rate) VALUES('{0}','{1}','{2}','{3}',{4},{5}) ";
        private string MSINS = "INSERT INTO tblMarginOfPCF ([Identity],DataDate,ETFCode,Currency,Amount,Rate) VALUES('{0}','{1}','{2}','{3}',{4},{5}) ";

        #region Property
        public DateTime DataDate { get; private set; }
        public string ETFCode { get; private set; }
        public string Currency { get; set; }        
        public double Amount { get; set; }
        public double Rate { get; set; } = 1;
        #endregion
                
        public PCF_Margin(string etfcode, DateTime dataDate)
        {
            ETFCode = etfcode;            
            DataDate = dataDate;
        }

        public string Insert()
        {
            //// string del = string.Format(DEL, DataDate.ToString(ETF.DATE), ETFCode, Currency);
            //string ins = string.Format(Utility.INI["SQL"]["SQLTYPE"] == "MSSQL" ? MSINS : MYINS, Guid.NewGuid(), DataDate.ToString(ETF.DATE), ETFCode, Currency,  Amount, Rate);
            string ins = string.Format(Utility.INI["SQL"]["SQLTYPE"] == "MSSQL" ? MSINS : MYINS, Guid.NewGuid(), DateTime.Now.ToString(ETF.DATE), ETFCode, Currency, Amount, Rate);
            foreach (var sql in Utility.SQL)
            {
                //sql.DoExecute(del);
                sql.DoExecute(ins);
            }
            return string.Empty;
        }
        public static void Delete(DateTime datadate, string etfcode)
        {
            string del = string.Format(DEL, datadate.ToString(ETF.DATE), etfcode);
            foreach (var sql in Utility.SQL)
            {
                sql.DoExecute(del);
            }
        }
        public new string ToString()
        {
            return string.Format("{0},{1},{2}", Currency, Amount,Rate);
        }
    }
}
