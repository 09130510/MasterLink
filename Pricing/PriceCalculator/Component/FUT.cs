using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PriceCalculator.Utility;
using PriceCalculator.Component;
using log4net;
using System.Reflection;

namespace PriceCalculator.Component
{
    public class FUT : Composition
    {
        #region Variable
        private ILog m_Log = LogManager.GetLogger(typeof(FUT));
        private string m_CapitalFormat;
        private string m_PATSFormat;
        private string m_iPushFormat;
        private string m_RedisFormat;
        private string m_xQuoteFormat;
        #endregion

        #region Property
        public string Head { get; private set; }
        public string YM { get; private set; }
        public int Y { get; private set; }
        public int M { get; private set; }

        private string CapitalFormat
        {
            get { return m_CapitalFormat; }
            set
            {
                m_CapitalFormat = value;
                Capital = _PIDConvert(value);
            }
        }
        private string PATSFormat
        {
            get { return m_PATSFormat; }
            set
            {
                m_PATSFormat = value;
                PATS = _PIDConvert(value);
            }
        }
        private string iPushFormat
        {
            get { return m_iPushFormat; }
            set
            {
                m_iPushFormat = value;
                iPush = _PIDConvert(value);
            }
        }
        private string RedisFormat
        {
            get { return m_RedisFormat; }
            set
            {
                m_RedisFormat = value;
                Redis = _PIDConvert(value);
            }
        }
        private string xQuoteFormat
        {
            get { return m_xQuoteFormat; }
            set
            {
                m_xQuoteFormat = value;
                xQuote = _PIDConvert(value);
            }
        }
        #endregion

        public FUT(DataRow row)
            : base(row)
        {
            Head = row["HEAD"].ToString();
            YM = row["YM"].ToString();
            Y = Convert.ToInt32(row["Y"]);
            M = Convert.ToInt32(row["M"]);
            BaseCurncy = row["CURRENCY"].ToString();
            CValue = Convert.ToDecimal(row["CVALUE"]);
            CapitalFormat = row["CAPITALFORMAT"].ToString();
            PATSFormat = row["PATSFORMAT"].ToString();
            iPushFormat = row["IPUSHFORMAT"].ToString();
            RedisFormat = row["REDISFORMAT"].ToString();
            xQuoteFormat = row["XQUOTEFORMAT"].ToString();
        }

        public void LockYP(DateTime expired)
        {
            base.LockYP(expired, nameof(FUT));
        }

        #region Private
        private string _PIDConvert(string format)
        {
            string re = string.Empty;

            if (string.IsNullOrEmpty(format)) { return format; }
            string[] substitute = format.Split('|');
            //if (substitute.Length < 2) { return format; }
            foreach (var item in substitute)
            {
                string[] subformat = item.Split('.');
                if (subformat.Length < 2)
                {
                    re += item;
                    continue;
                }
                Substitute t = subformat[0].ToEnum<Substitute>();
                int num = Convert.ToInt32(subformat[1]);
                switch (t)
                {
                    case Substitute.AD:
                        re += Y.ToString().PadLeft(4, '0').Substring(4 - num, num);
                        break;
                    case Substitute.TW:
                        re += (Y - 1911).ToString().PadLeft(4, '0').Substring(4 - num, num);
                        break;
                    case Substitute.DG:
                        re += M.ToString().PadLeft(num, '0');
                        break;
                    case Substitute.FL:
                        re += _ForeignMonthLetter(M);
                        break;
                    case Substitute.ML:
                        re += _ForeignLongMonthLetter(M);
                        break;
                    case Substitute.TL:
                        re += _TWMonthLetter(M);
                        break;
                    case Substitute.None:
                    default:
                        break;
                }
            }
            Util.Info(m_Log, nameof(FUT._PIDConvert), $"{PID} Format:{format} Result:{re}");
            return re;
        }
        private string _ForeignMonthLetter(int month)
        {
            switch (month)
            {
                case 1:
                    return "F";
                case 2:
                    return "G";
                case 3:
                    return "H";
                case 4:
                    return "J";
                case 5:
                    return "K";
                case 6:
                    return "M";
                case 7:
                    return "N";
                case 8:
                    return "Q";
                case 9:
                    return "U";
                case 10:
                    return "V";
                case 11:
                    return "X";
                case 12:
                    return "Z";
            }
            return string.Empty;
        }
        private string _ForeignLongMonthLetter(int month)
        {
            switch (month)
            {
                case 1:
                    return "JAN";
                case 2:
                    return "FEB";
                case 3:
                    return "MAR";
                case 4:
                    return "APR";
                case 5:
                    return "MAY";
                case 6:
                    return "JUN";
                case 7:
                    return "JUL";
                case 8:
                    return "AUG";
                case 9:
                    return "SEP";
                case 10:
                    return "OCT";
                case 11:
                    return "NOV";
                case 12:
                    return "DEC";
            }
            return string.Empty;
        }
        private string _TWMonthLetter(int month)
        {
            return Convert.ToChar(64 + month).ToString();
        }
        #endregion
    }
}