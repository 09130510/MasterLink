using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFPosition.Class
{
    public class STK : Position
    {
        public override int CValue
        {
            get
            {
                if (ID.Contains("L"))
                {
                    return 2;
                }
                else if (ID.Contains("R"))
                {
                    return -1;
                }
                return 1;
            }
        }
        public override int Lots
        {
            get
            {
                //借券帳號在製表時, 數量轉為負數;
                string[] lendingAccno = Util.INI["SYSTEM"]["STKLENDINGACCNO"].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                return base.Lots * (lendingAccno.Contains(AccNo) ? -1 : 1);
            }

            set
            {
                base.Lots = value;
            }
        }
    }
}
