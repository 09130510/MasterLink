using PriceCalculator.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PriceCalculator.Component
{
    public abstract class Asset
    {
        private decimal m_YstRate;

        #region Property      

        public virtual int Direction { get { return 1; } }
        public virtual string Key { get; protected set; }
        public virtual string ETFCode { get; protected set; }
        public virtual string BaseCrncy { get; protected set; }
        public virtual decimal Amount { get; protected set; }
        public virtual decimal YstRate
        {
            get { return m_YstRate; }
            protected set
            {
                if ((value != 0M) && (value != m_YstRate))
                {
                    this.m_YstRate = value;
                }

            }
        }
        public virtual decimal FXRate
        {
            get
            {
                return (decimal)Util.FXRates[ETFCode, BaseCrncy, "TWD"];
            }
        }
        #endregion

        public Asset(DataRow row)
        {
            Key = row["Identity"].ToString();
            ETFCode = row["ETFCODE"].ToString();
            BaseCrncy = row["CURRENCY"].ToString();
            Amount = Convert.ToDecimal(row["AMOUNT"]);
            YstRate = Convert.ToDecimal(row["RATE"]);

        }

        public virtual string ToExcel()
        {
            return $"{BaseCrncy}\r\n{Amount}\r\n{YstRate}\r\n{FXRate}\r\n1\r\n1\r\n{Direction}";
        }
    }
}
