using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisRTD
{
    /// <summary>
    /// RTD 訂閱資料
    /// </summary>
    public class RTDItem
    {
        #region Property
        /// <summary>
        /// 頻道
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// 項目
        /// </summary>
        public string Item { get; set; }
        /// <summary>
        ///  Excel给定
        /// </summary>
        public int TopicID { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        #endregion

        /// <summary>
        /// RTD 訂閱資料
        /// </summary>
        /// <param name="Strings"></param>
        public RTDItem(Array Strings)
        {
            Channel = Strings.GetValue(0).ToString().Trim();
            Item = Strings.GetValue(1).ToString().Trim();

            TopicID = -1;
        }
        /// <summary>
        /// 比對 Channel, Item
        /// </summary>
        /// <param name="Strings"></param>
        /// <returns></returns>
        public bool Equals(Array Strings)
        {
            if (Channel != Strings.GetValue(0).ToString().Trim()) { return false; }
            if (Item != Strings.GetValue(1).ToString().Trim()) { return false; }
            return true;
        }
    }
}
