using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCF.Class
{
    public class PCF_Forward
    {
        private static string DEL = "DELETE FROM tblForwardOfPCF WHERE DataDate='{0}' AND ETFCode='{1}'  ";
        private string MYINS = "INSERT INTO tblForwardOfPCF (Identity,DataDate,ETFCode,Currency,Amount,Rate,HedgeRatio) VALUES('{0}','{1}','{2}','{3}',{4},{5},{6}) ";
        private string MSINS = "INSERT INTO tblForwardOfPCF ([Identity],DataDate,ETFCode,Currency,Amount,Rate,HedgeRatio) VALUES('{0}','{1}','{2}','{3}',{4},{5},{6}) ";

        #region Property
        public DateTime DataDate { get; private set; }
        public string ETFCode { get; private set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
        public double Rate { get; set; } = 1;
        public double HedgeRatio { get; set; } = 1;
        #endregion

        public PCF_Forward(string etfcode, DateTime dataDate)
        {
            ETFCode = etfcode;
            DataDate = dataDate;
        }

        public string Insert()
        {
            ////string del = string.Format(DEL, DataDate.ToString(ETF.DATE), ETFCode, Currency);
            //string ins = string.Format(Utility.INI["SQL"]["SQLTYPE"] == "MSSQL" ? MSINS : MYINS, Guid.NewGuid(), DataDate.ToString(ETF.DATE), ETFCode, Currency, Amount, Rate, HedgeRatio);
            string ins = string.Format(Utility.INI["SQL"]["SQLTYPE"] == "MSSQL" ? MSINS : MYINS, Guid.NewGuid(), DateTime.Now.ToString(ETF.DATE), ETFCode, Currency, Amount, Rate, HedgeRatio);
            foreach (var sql in Utility.SQL)
            {
                //sql.DoExecute(del);
                sql.DoExecute(ins);
            }
            return string.Empty;
        }
        public static void Delete(DateTime datadate, string etfcode)
        {
            string del = string.Format(DEL, datadate.ToString(ETF.DATE),etfcode);
            foreach (var sql in Utility.SQL)
            {
                sql.DoExecute(del);                
            }
        }
        public new string ToString()
        {
            return $"{Currency},{Amount},{Rate},{HedgeRatio}";
        }
    }
}
