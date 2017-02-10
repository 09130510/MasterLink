using PriceLib.PATS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATS.Class
{
    /// <summary>
    /// 帳號資料
    /// </summary>
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

        /// <summary>
        /// 從PATS TraderAcctStruct轉換成AccountInfo
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
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
