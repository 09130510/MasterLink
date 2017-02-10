using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTDSvr
{    
    class SubData
    {
        public string Account { get; set; }
        public string ComID { get; set; }
        public SummaryItem Item { get; set; }
        public int TopicID { get; set; }
        public object Value { get; set; }

        public SubData(string account , string comID, string summaryItem)
        {
            this.Account = account;
            this.ComID = comID;
            SummaryItem item = default(SummaryItem);
            Enum.TryParse<SummaryItem>(summaryItem, out item);
            this.Item = item;
            TopicID = -1;
        }
        public bool Equals(string account, string comId, string item)
        {
            if (Account != account) { return false; }
            if (ComID != comId) { return false; }
            if (Item.ToString() != item) { return false; }
            return true;
        }
        public bool Equals(SubData compare)
        {
            if (Account != compare.Account) { return false; }
            if (ComID != compare.ComID) { return false; }
            if (Item != compare.Item) { return false; }
            return true;
        }
    }
}
