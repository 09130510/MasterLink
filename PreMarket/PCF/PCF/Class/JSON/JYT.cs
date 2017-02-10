using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCF.Class.JSON
{
    public class JYT_ETF
    {
        public string fundid { get; set; }
        public string fundname { get; set; }
        public string fullname { get; set; }
        public string ename { get; set; }
        public string markcd { get; set; }
        public string trandate { get; set; }
        public string totalav { get; set; }
        public string osunit { get; set; }
        public string nav { get; set; }
        public string baseunit { get; set; }
        public string estcvalue { get; set; }
        public string estdvalue { get; set; }
        public string issuesdiff { get; set; }
        public string cashdiff { get; set; }
        public string allot { get; set; }
        public string preallot { get; set; }
        public string anndate { get; set; }
        public string upddate { get; set; }
        public string preunit { get; set; }
        public string predate { get; set; }
    }
    public class JYT_ForeignSTK
    {
        public string code { get; set; }
        public string ym { get; set; }
        public string name { get; set; }
        public string ename { get; set; }
        public string weights { get; set; }
        public string qty { get; set; }
    }
    public class JYT_FUND
    {
        public string code { get; set; }
        public string ym { get; set; }
        public string name { get; set; }
        public string ename { get; set; }
        public string weights { get; set; }
        public string qty { get; set; }
    }
    public class JYT_STK
    {
        public string code { get; set; }
        public string stkcd { get; set; }
        public string name { get; set; }
        public string ename { get; set; }
        public string qty { get; set; }
        public string cashinlieu { get; set; }
        public string minimum { get; set; }
    }
    public class JYT_Currency
    {
        public string fundId { get; set; }
        public string cDate { get; set; }
        public string crncy { get; set; }
        public string endRate { get; set; }
    }
    public class JYT_Cash
    {
        public string code { get; set; }
        public string name { get; set; }
        public string ename { get; set; }
        public string crncy { get; set; }
        public string exrate { get; set; }
        public string rto { get; set; }
        public string amt { get; set; }
    }
    /// <summary>
    /// PCF期貨保證金資料
    /// </summary>
    public class JYT_Margin
    {
        /// <summary>
        /// 商品代碼
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 商品名稱
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 商品英文名稱
        /// </summary>
        public string ename { get; set; }
        /// <summary>
        /// 商品幣別
        /// </summary>
        public string crncy { get; set; }
        /// <summary>
        /// 對台幣匯率
        /// </summary>
        public string exrate { get; set; }
        /// <summary>
        /// 權重
        /// </summary>
        public string rto { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public string amt { get; set; }
    }
    /// <summary>
    /// PCF遠匯資料
    /// </summary>
    public class JYT_FX
    {
        /// <summary>
        /// 中文名稱
        /// </summary>
        public string Cname { get; set; }
        /// <summary>
        /// 英文名稱
        /// </summary>
        public string Ename { get; set; }
        /// <summary>
        /// 幣別
        /// </summary>
        public string SAL_CRNCY { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public string SAL_AMT { get; set; }
        /// <summary>
        /// 結算匯率
        /// </summary>
        public string End_Rate { get; set; }
        /// <summary>
        /// 避險比率
        /// </summary>
        public string HedgeRatio { get; set; }
    }
    /// <summary>
    /// 基金各項部位權重資訊
    /// </summary>
    public class JYT_FundSize
    {
        /// <summary>
        /// 商品代碼
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 商品名稱
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 股票金額
        /// </summary>
        public string stkvalues { get; set; }
        /// <summary>
        /// 期貨金額
        /// </summary>
        public string futvalues { get; set; }
        /// <summary>
        /// ETF金額
        /// </summary>
        public string etfvalues { get; set; }
        /// <summary>
        /// 基金資產
        /// </summary>
        public string fundsize { get; set; }
    }
    /// <summary>
    /// 結算匯率
    /// </summary>
    public class JYT_SettleCurncy
    {
        /// <summary>
        /// 基金代碼
        /// </summary>
        public string fundId { get; set; }
        /// <summary>
        /// 匯率日期
        /// </summary>
        public string cDate { get; set; }
        /// <summary>
        /// 幣別
        /// </summary>
        public string crncy { get; set; }
        /// <summary>
        /// 收盤匯率
        /// </summary>
        public string endRate { get; set; }
    }
}
