using System;

namespace Bloomberg.RTD
{
    /// <summary>
    /// RTD 訂閱資料
    /// </summary>
    public class RTDItem
    {
        #region Property
        /// <summary>
        /// 商品
        /// </summary>
        public string Security { get; set; }
        /// <summary>
        /// 欄位
        /// </summary>
        public string Field { get; set; }
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
            Security = Strings.GetValue(0).ToString().Trim();
            Field = Strings.GetValue(1).ToString().Trim();

            TopicID = -1;
        }
        /// <summary>
        /// 比對 
        /// </summary>
        /// <param name="Strings"></param>
        /// <returns></returns>
        public bool Equals(Array Strings)
        {
            if (Security != Strings.GetValue(0).ToString().Trim()) { return false; }
            if (Field != Strings.GetValue(1).ToString().Trim()) { return false; }
            return true;
        }
    }
}
