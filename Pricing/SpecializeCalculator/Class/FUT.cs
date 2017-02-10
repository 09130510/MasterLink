using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceCalculator
{
    public class FUT
    {
        public static string ListSQL = "SELECT DISTINCT EXCHANGE+','+HEAD FROM TBLFUTURE ";
        public static string SelSQL = "SELECT EXCHANGE+','+HEAD AS HEAD, @Y AS Y, @M AS M, CAPITALFORMAT, PATSFORMAT,REDISFORMAT,IPUSHFORMAT  FROM TBLFUTURE  WHERE EXCHANGE+','+HEAD =@HEAD ";

        #region Property
        public string Head { get; set; }
        public int Y { get; set; }
        public Month M { get; set; }
        private string CapitalFormat { get; set; }
        private string PATSFormat { get; set; }
        private string RedisFormat { get; set; }
        private string iPushFormat { get; set; }

        public string Capital { get { return _PIDConvert(CapitalFormat); } }
        public string PATS { get { return _PIDConvert(PATSFormat); } }
        public string Redis { get { return _PIDConvert(RedisFormat); } }
        public string iPush { get { return _PIDConvert(iPushFormat); } }
        #endregion

        public FUT() { }

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
                        re += ((int)M).ToString().PadLeft(num, '0');
                        break;
                    case Substitute.FL:
                        re += M.FL();
                        break;
                    case Substitute.ML:
                        re += M.ToString();
                        break;
                    case Substitute.TL:
                        re += Convert.ToChar(64 + (int)M).ToString();
                        break;
                    case Substitute.None:
                    default:
                        break;
                }
            }
            return re;
        }
        #endregion
    }
}