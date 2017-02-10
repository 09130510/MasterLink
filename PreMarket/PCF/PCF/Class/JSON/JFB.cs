using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCF.Class.JSON
{
    public class JFB
    {
        /// <summary>
        /// 現金
        /// </summary>
        public JFB_Cash[] Cash { get; set; }
        /// <summary>
        /// 匯率
        /// </summary>
        public JFB_Currency[] Currency { get; set; }
        /// <summary>
        /// 保證金
        /// </summary>
        public JFB_Drbroker[] Drbroker { get; set; }
        /// <summary>
        /// 期貨 持股
        /// </summary>
        public JFB_Drfts[] Drfts { get; set; }
        /// <summary>
        /// 匯率避險
        /// </summary>
        public JFB_Drfwd[] Drfwd { get; set; }
        /// <summary>
        /// Fund 持股
        /// </summary>
        public JFB_Fd[] Fd { get; set; }
        /// <summary>
        /// ETF 資料
        /// </summary>
        public JFB_Fund Fund { get; set; }
        /// <summary>
        /// 股票 持股
        /// </summary>
        public JFB_Stk[] Stk { get; set; }
    }
    /// <summary>
    /// 匯率
    /// </summary>
    public class JFB_Currency
    {
        /// <summary>
        /// 匯率
        /// </summary>
        public string Exchange { get; set; }
        /// <summary>
        /// 幣別
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 無
        /// </summary>
        public string Nav { get; set; }
    }
    /// <summary>
    /// 期貨 持股
    /// </summary>
    public class JFB_Drfts
    {
        /// <summary>
        /// 市價
        /// </summary>
        public string AmountAuri { get; set; }
        /// <summary>
        /// 代號
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 價格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 比率
        /// </summary>
        public string RateAuri { get; set; }
        /// <summary>
        /// 口數
        /// </summary>
        public string Units { get; set; }
    }
    /// <summary>
    /// 匯率避險
    /// </summary>
    public class JFB_Drfwd
    {
        /// <summary>
        /// 幣別 (不知道是Base還是Quoted)
        /// </summary>
        public string CurrencyIdChange { get; set; }
        /// <summary>
        /// 買賣
        /// </summary>
        public string CurrencyIdChangeTradeType { get; set; }
        /// <summary>
        /// 到期日
        /// </summary>
        public string DueDate { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public string TotalAmount { get; set; }
    }
    /// <summary>
    /// Fund 持股
    /// </summary>
    public class JFB_Fd
    {
        /// <summary>
        /// 市價
        /// </summary>
        public string AmountAuri { get; set; }
        /// <summary>
        /// 代號
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 價格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 比率
        /// </summary>
        public string RateAuri { get; set; }
        /// <summary>
        /// 股數
        /// </summary>
        public string Units { get; set; }
    }
    /// <summary>
    /// ETF資料
    /// </summary>
    public class JFB_Fund
    {
        public string DDate { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string DDateStr { get; set; }
        /// <summary>
        /// 公開發行股數
        /// </summary>
        public string Missue { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 淨資產價值
        /// </summary>
        public string Nav { get; set; }
        public string NavDate { get; set; }
        public string NavDateStr { get; set; }
        /// <summary>
        /// NAV
        /// </summary>
        public string PNav { get; set; }
    }
    /// <summary>
    /// 股票 持股
    /// </summary>
    public class JFB_Stk
    {
        /// <summary>
        /// 市價
        /// </summary>
        public string AmountAuri { get; set; }
        /// <summary>
        /// 代號
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 價格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 比率
        /// </summary>
        public string RateAuri { get; set; }
        /// <summary>
        /// 股數
        /// </summary>
        public string Units { get; set; }
    }
    /// <summary>
    /// 現金
    /// </summary>
    public class JFB_Cash
    {
        /// <summary>
        /// 金額
        /// </summary>
        public string AmountAuri { get; set; }
        /// <summary>
        /// 幣別
        /// </summary>
        public string Id { get; set; }
    }
    /// <summary>
    /// 保證金
    /// </summary>
    public class JFB_Drbroker
    {
        /// <summary>
        /// 金額
        /// </summary>
        public string AmountAuri { get; set; }
        /// <summary>
        /// 幣別
        /// </summary>
        public string Id { get; set; }
    }
}
