using PriceLib.PATS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATSOrder.Class
{
    public class AccountInfo
    {
        public int Index { get; set; }
        public string BackOfficeID { get; set; }
        public string TraderAccount { get; set; }

        public AccountInfo(int index, TraderAcctStruct acct)
        {
            Index = index;
            BackOfficeID = acct.BackOfficeID;
            TraderAccount = acct.TraderAccount;
        }

        public static List<AccountInfo> Convert(Dictionary<int, TraderAcctStruct> source)
        {
            List<AccountInfo> re = new List<AccountInfo>();
            foreach (var item in source)
            {
                re.Add(new AccountInfo(item.Key, item.Value));
            }
            return re;
        }
    }
}
