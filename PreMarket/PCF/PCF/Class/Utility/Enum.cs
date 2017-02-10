using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCF
{
    public enum Market
    {
        /// <summary>
        /// Tanwan
        /// </summary>
        TW,
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
        KR,
        /// <summary>
        /// HK
        /// </summary>
        HK
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
    public enum ETFType
    {
        /// <summary>
        /// ETF
        /// </summary>
        N,
        /// <summary>
        /// ETF 以外幣計價
        /// </summary>
        K,
        /// <summary>
        /// 槓桿型ETF
        /// </summary>
        L,
        /// <summary>
        /// 槓桿型ETF 以外幣計價
        /// </summary>
        M,
        /// <summary>
        /// 反向型 ETF
        /// </summary>
        R,
        /// <summary>
        /// 反向型 ETF 以外幣計價
        /// </summary>
        S,
        /// <summary>
        /// 指數股票型期貨信託基金
        /// </summary>
        U,
        /// <summary>
        /// 指數股票型期貨信託基金 以外幣計價
        /// </summary>
        V

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
        /// 歐洲交易所
        /// </summary>
        EUREX,
        /// <summary>
        /// 韓國交易所
        /// </summary>
        KRX,
        /// <summary>
        /// 芝加哥商品交易所
        /// </summary>
        CBT,
        /// <summary>
        /// 芝加哥期權交易所
        /// </summary>
        CBOE
    }
    public enum DataKind
    {
        HEAD,
        COMPOSITION,           
        STK,
        FUT,
        FUND,
        FX,
        CASH,
        MARGIN,
        FORWARD,
        FUNDSIZE,
        SETTLECURNCY
    }    
}
