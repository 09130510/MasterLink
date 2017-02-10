using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceCalculator.Utility
{
    /// <summary>
    /// 找尋Composition的方式
    /// </summary>
    public enum CollectionKey
    {
        ETFCode,
        PID,
        Redis,
        iPush,
        Capital,
        PATS,
        Currency
    }
    /// <summary>
    /// 找Asset的方式
    /// </summary>
    public enum AssetKey
    {
        /// <summary>
        /// 用ETFCode找
        /// </summary>
        ETFCode,
        /// <summary>
        /// 用幣別找
        /// </summary>
        BaseCrncy
    }
    public enum CollectionType
    {
        Stock,
        Future,
        Fund
    }
    /// <summary>
    /// 資產種類
    /// </summary>
    public enum AssetType
    {
        /// <summary>
        /// 現金
        /// </summary>
        Cash,
        /// <summary>
        /// 保證金
        /// </summary>
        Margin,
        /// <summary>
        /// 遠匯
        /// </summary>
        Forward
    }
    public enum CalculationType
    {
        None,
        /// <summary>
        /// 用基金資產來計算
        /// </summary>
        TotalUnits,
        /// <summary>
        /// 用PCF來計算
        /// </summary>
        PCFUnits
    }
    public enum Market
    {
        /// <summary>
        /// Tanwan
        /// </summary>
        TW,
        /// <summary>
        /// HK
        /// </summary>
        HK,
        /// <summary>
        /// China
        /// </summary>
        CN,
        /// <summary>
        /// Japan
        /// </summary>
        JP,
        /// <summary>
        /// US
        /// </summary>
        US,
        /// <summary>
        /// India
        /// </summary>
        IN,
        /// <summary>
        /// Europe
        /// </summary>
        ER,
        /// <summary>
        /// Korea
        /// </summary>
        KR
    }
    public enum Broker
    {
        /// <summary>
        /// Yuanta
        /// </summary>
        YT,
        /// <summary>
        /// FuBon
        /// </summary>
        FB,
        /// <summary>
        /// CathayHoldings
        /// </summary>
        CH,
        /// <summary>
        /// FuHwa
        /// </summary>
        FH,
        /// <summary>
        /// Capital
        /// </summary>
        CP,
        /// <summary>
        /// SinoPac
        /// </summary>
        SP
    }
    public enum Exch
    {
        /// <summary>
        /// 台灣證交所
        /// </summary>
        TWSE,
        /// <summary>
        /// 上海交易所
        /// </summary>
        SSE,
        /// <summary>
        /// 深圳交易所
        /// </summary>
        SZSE,
        /// <summary>
        /// 東京交易所
        /// </summary>
        TSE,
        /// <summary>
        /// 香港交易所
        /// </summary>
        HKEx,
        /// <summary>
        /// 紐約證券交易所
        /// </summary>
        NYSE,
        /// <summary>
        /// 那斯達克股票交易所
        /// </summary>        
        NASDAQ,
        /// <summary>
        /// 印度國家證券交易所
        /// </summary>
        NSE,
        /// <summary>
        /// 新加坡交易所
        /// </summary>
        SGX,
        /// <summary>
        /// 大阪交易所
        /// </summary>
        OSE,
        /// <summary>
        /// 台灣期貨交易所
        /// </summary>
        TAIFEX,
        /// <summary>
        /// 芝加哥商業交易所
        /// </summary>
        CME,
        /// <summary>
        /// 芝加哥商品交易所
        /// </summary>
        CBT,
        /// <summary>
        /// 紐約商業交易所
        /// </summary>
        NYM,
        /// <summary>
        /// 歐洲期貨交易所
        /// </summary>
        EUREX,
        /// <summary>
        /// 韓國交易所
        /// </summary>
        KRX
    }
    //public enum Currency
    //{ 
    //    TWD,
    //    CNY,
    //    HKD,
    //    USD,
    //    JPD
    //}
    public enum Source
    {
        None,
        iPush,
        //xQuote,
        Redis//,
        //Capital
    }
    public enum ForeignSource
    {
        None,
        Capital,
        PATS
    }
    public enum FXSource
    {
        None,
        Bloomberg,
        Redis
    }
    public enum Substitute
    {
        None,
        /// <summary>
        /// 西元年
        /// </summary>
        AD,
        /// <summary>
        /// 民國年
        /// </summary>
        TW,
        /// <summary>
        /// 數字月
        /// </summary>
        DG,
        /// <summary>
        /// 國外月; 1F,2G,3H,4J,5K,6M,7N,8Q,9U,10V,11X,12Z
        /// </summary>
        FL,
        /// <summary>
        /// 國外月; 1JAN,2FEB,3MAR,4APR,5MAY,6JUN,7JUL,8AUG,9SEP10OCT,11NOV,12DEC
        /// </summary>
        ML,
        /// <summary>
        /// 台灣月; 1A,2B,3C,4D,5E,6F,7G,8H,9I,10J,11K,12L
        /// </summary>
        TL
    }
    /// <summary>
	/// Alert Box Button Style
	/// </summary>
	public enum AlertBoxButton
    {
        /// <summary>
        /// 出現OK, Caption正常
        /// </summary>
        Msg_OK,
        /// <summary>
        /// 出現OK, Caption為紅字
        /// </summary>
        Error_OK,
        /// <summary>
        /// 出現OK/Cancel
        /// </summary>
        OKCancel,
        /// <summary>
        /// 出現Yes/No
        /// </summary>
        YesNo
    }
}