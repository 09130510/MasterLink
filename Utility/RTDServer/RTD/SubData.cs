using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTDServer
{
    public enum SubItem
    { 
        BLot,
        ALot,
        BAmt,
        AAmt
    }
    class SubData
    {
        public string Account { get; set; }
        public string ComID { get; set; }
        public SubItem Item { get; set; }
        public int TopicID { get; set; }
        public object Value { get; set; }

        public SubData()
        {
            TopicID = -1;
        }
        public bool Equals(string account, string comId, SubItem item)
        {
            if (Account != account) { return false; }
            if (ComID != comId) { return false; }
            if (Item != item) { return false; }
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
