using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capital.RTD
{
    /// <summary>
    /// RTD 訂閱資料
    /// </summary>
    public class RTDItem
    {
        #region Property
        /// <summary>
        /// RTD 訂閱種類
        /// Summary | OpenInterest
        /// </summary>
        public string RTDType { get; set; }
        /// <summary>
        /// 交易
        /// </summary>
        public string CustNo { get; set; }
        ///// <summary>
        ///// 帳號
        ///// </summary>
        //public string Account { get; set; }
        ///// <summary>
        ///// 密碼
        ///// </summary>
        //public string Password { get; set; }
        /// <summary>
        /// 商品
        /// </summary>
        public string ComID { get; set; }
        /// <summary>
        /// RTD訂閱項目
        /// Summary:  ALOT, BLOT, AAMT. BAMT
        /// OpenInterest: LOTS, MARKETPRICE, AVGPRICE, CLOSEPRICE, PL, UPDATETIME
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
            for (int i = 0; i < Strings.Length; i++)
            {
                if (i == 0) { RTDType = Strings.GetValue(i).ToString().Trim().ToUpper(); }
                //if (i == 1) { Account = Strings.GetValue(i).ToString().Trim(); }
                //if (i == 2) { Password = Strings.GetValue(i).ToString().Trim(); }
                if (i == 1) { CustNo = Strings.GetValue(i).ToString().Trim(); }
                if (i == 2) { ComID = Strings.GetValue(i).ToString().Trim().ToUpper(); }
                if (i == 3) { Item = Strings.GetValue(i).ToString().Trim().ToUpper(); }
            }

            //RTDType = Strings.GetValue(0).ToString().Trim().ToUpper();
            //CustNo = Strings.GetValue(1).ToString().Trim();
            //ComID = Strings.GetValue(2).ToString().Trim().ToUpper();
            //Item = Strings.GetValue(3).ToString().Trim().ToUpper();
            TopicID = -1;
        }
        /// <summary>
        /// 比對 RTDType, CustNo, ComID, Item
        /// </summary>
        /// <param name="Strings"></param>
        /// <returns></returns>
        public bool Equals(Array Strings)
        {
            if (Strings.Length != 4) { return false; }
            for (int i = 0; i < Strings.Length; i++)
            {
                if (i == 0 && RTDType != Strings.GetValue(i).ToString().Trim().ToUpper()) { return false; }
                //if (i == 1 && Account != Strings.GetValue(i).ToString().Trim()) { return false; }
                //if (i == 2 && Password != Strings.GetValue(i).ToString().Trim()) { return false; }
                if (i == 1 && CustNo != Strings.GetValue(i).ToString().Trim().ToUpper()) { return false; }
                if (i == 2 && ComID != Strings.GetValue(i).ToString().Trim().ToUpper()) { return false; }
                if (i == 3 && Item != Strings.GetValue(i).ToString().Trim().ToUpper()) { return false; }
            }
            //if (RTDType != Strings.GetValue(0).ToString().Trim().ToUpper()) { return false; }
            //if (CustNo != Strings.GetValue(1).ToString().Trim()) { return false; }
            //if (ComID != Strings.GetValue(2).ToString().Trim().ToUpper()) { return false; }
            //if (Item != Strings.GetValue(3).ToString().Trim().ToUpper()) { return false; }
            return true;
        }
    }
}
