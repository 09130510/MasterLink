using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCF.Class.JSON
{
    /// <summary>
    /// 申購買回清單
    /// </summary>
    public class JCH
    {
        /// <summary>
        /// PCF 資料
        /// </summary>
        public JCH_PCF[] pcf_array { get; set; }
        /// <summary>
        /// 回覆時間 yyyy/MM/dd HH:mm:ss
        /// </summary>
        public string rt_time { get; set; }
        /// <summary>
        /// 回覆訊息 “OK”表示正常
        /// </summary>
        public string rt_msg { get; set; }
        /// <summary>
        /// 狀態碼 “0000”表示正常
        /// </summary>
        public string rt_code { get; set; }
    }
    /// <summary>
    /// PCF 資料
    /// </summary>
    public class JCH_PCF
    {
        /// <summary>
        /// ETF代號
        /// </summary>
        public string stock_code { get; set; }
        /// <summary>
        /// ETF名稱
        /// </summary>
        public string stock_name { get; set; }
        /// <summary>
        /// 預收申購總價金
        /// </summary>
        public string pre_amt { get; set; }
        /// <summary>
        /// 基金淨資產價值
        /// </summary>
        public string aum { get; set; }
        /// <summary>
        /// 已發行受益權單位總數
        /// </summary>
        public string tot_unit { get; set; }
        /// <summary>
        /// 與前日已發行單位差異數
        /// </summary>
        public string diff_unit { get; set; }
        /// <summary>
        /// 每受益權單位淨資產價值
        /// </summary>
        public string nav { get; set; }
        /// <summary>
        /// 每申購/買回基數之受益權單位數
        /// </summary>
        public string basket_unit { get; set; }
        /// <summary>
        /// 每申購/買回基數約當淨值
        /// </summary>
        public string basket_nav { get; set; }
        /// <summary>
        /// 每申購/買回基數約當市值
        /// </summary>
        public string basket_val { get; set; }
        /// <summary>
        /// 每申購/買回基數估計現金差額
        /// </summary>
        public string basket_nav_val_diff { get; set; }
        /// <summary>
        /// 每申購基數實際申購總價金
        /// </summary>
        public string basket_act_amt { get; set; }
        /// <summary>
        /// 每申購基數總價金差異額
        /// </summary>
        public string basket_act_amt_diff { get; set; }
        /// <summary>
        /// PCF日期 yyyy/MM/dd
        /// </summary>
        public string pcf_date { get; set; }
        /// <summary>
        /// 淨值日期 yyyy/MM/dd
        /// </summary>
        public string nav_date { get; set; }
        /// <summary>
        /// 商品資料
        /// </summary>
        public JCH_Product[] prod_array { get; set; }
    }
    /// <summary>
    /// 商品資料
    /// </summary>
    public class JCH_Product
    {
        /// <summary>
        /// 商品代號
        /// </summary>
        public string prod_code { get; set; }
        /// <summary>
        /// 商品名稱
        /// </summary>
        public string prod_name { get; set; }
        /// <summary>
        /// 商品類別 BD：債券 FN：基金 FT：期貨 SK：股票
        /// </summary>
        public string prod_type { get; set; }
        /// <summary>
        /// 每基數的單位數
        /// </summary>
        public string basket_shares { get; set; }
        /// <summary>
        /// 期貨月份 yyyy/MM
        /// </summary>
        public string futures_date { get; set; }
        /// <summary>
        /// 期貨買賣別 B:買 S:賣
        /// </summary>
        public string futures_buysell { get; set; }
    }
    /// <summary>
    /// 基金持股權重
    /// </summary>
    public class JCH_Fund
    {
        /// <summary>
        /// PCF資料
        /// </summary>
        public JCH_FundAsset[] fundassets_array { get; set; }
        /// <summary>
        /// 回覆時間 yyyy/MM/dd HH:mm:ss
        /// </summary>
        public string rt_time { get; set; }
        /// <summary>
        /// 回覆訊息 “OK”表示正常
        /// </summary>
        public string rt_msg { get; set; }
        /// <summary>
        /// 狀態碼 “0000”表示正常
        /// </summary>
        public string rt_code { get; set; }
    }
    /// <summary>
    /// 基金資產
    /// </summary>
    public class JCH_FundAsset
    {
        /// <summary>
        /// ETF代號
        /// </summary>
        public string stock_code { get; set; }
        /// <summary>
        /// ETF名稱
        /// </summary>
        public string stock_name { get; set; }
        /// <summary>
        /// 基金淨資產價值
        /// </summary>
        public string aum { get; set; }
        /// <summary>
        /// 已發行受益權單位總數
        /// </summary>
        public string tot_unit { get; set; }
        /// <summary>
        /// 每受益權單位淨資產價值
        /// </summary>
        public string nav { get; set; }
        /// <summary>
        /// 股票金額
        /// </summary>
        public string stock_val { get; set; }
        /// <summary>
        /// 期貨金額
        /// </summary>
        public string futures_val { get; set; }
        /// <summary>
        /// ETF金額
        /// </summary>
        public string etf_val { get; set; }
        /// <summary>
        /// 基金金額
        /// </summary>
        public string fund_val { get; set; }
        /// <summary>
        /// 保證金
        /// </summary>
        public string margin { get; set; }
        /// <summary>
        /// 附買回債券
        /// </summary>
        public string repo_bonds { get; set; }
        /// <summary>
        /// 現金
        /// </summary>
        public string cash { get; set; }
        /// <summary>
        /// 申贖應付款
        /// </summary>
        public string payables { get; set; }
        /// <summary>
        /// 遠匯
        /// </summary>
        public string fxa { get; set; }
        /// <summary>
        /// 淨收付外匯款
        /// </summary>
        public string net_exchange { get; set; }
        /// <summary>
        /// 資料日期
        /// </summary>
        public string data_date { get; set; }
        /// <summary>
        /// 淨值日期
        /// </summary>
        public string nav_date { get; set; }
        /// <summary>
        /// 商品資料
        /// </summary>
        public JCH_Composition[] composition_array { get; set; }
        /// <summary>
        /// 幣別及匯率資料
        /// </summary>
        public JCH_Currency[] currency_array { get; set; }
        /// <summary>
        /// 保證金(by幣別)
        /// </summary>
        public JCH_Margin[] margin_array { get; set; }
        /// <summary>
        /// 附買回債券(by幣別)
        /// </summary>
        public JCH_Bonds[] repo_bonds_array { get; set; }
        /// <summary>
        /// 現金(by幣別)
        /// </summary>
        public JCH_Cash[] cash_array { get; set; }
        /// <summary>
        /// 申贖應付款(by幣別)
        /// </summary>
        public JCH_Payables[] payables_array { get; set; }
        /// <summary>
        /// 遠匯(by幣別)
        /// </summary>
        public JCH_FXA[] fxa_array { get; set; }
        /// <summary>
        /// 淨收付外匯款(by幣別)
        /// </summary>
        public JCH_NetExchange[] net_exchange_array { get; set; }
        /// <summary>
        /// 是否為預先結算 Y:預先結算資料 N:正式結算資料
        /// </summary>
        public string pre_settle { get; set; }
    }
    /// <summary>
    /// 商品資料 
    /// </summary>
    public class JCH_Composition
    {
        /// <summary>
        /// 代號
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 類別
        /// BD：債券 FN：基金 FT：期貨 SK：股票 ETF：ETF
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        public string volume { get; set; }
        /// <summary>
        /// 持股權重
        /// </summary>
        public string ratio { get; set; }
        /// <summary>
        /// 期貨月份 yyyy/MM
        /// </summary>
        public string futures_date { get; set; }
        /// <summary>
        /// 期貨買賣別 B:買 S:賣
        /// </summary>
        public string futures_buysell { get; set; }
    }
    /// <summary>
    /// 幣別及匯率資料
    /// </summary>
    public class JCH_Currency
    {
        /// <summary>
        /// 幣別
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 匯率
        /// </summary>
        public string rate { get; set; }
    }
    /// <summary>
    /// 保證金
    /// </summary>
    public class JCH_Margin
    {
        /// <summary>
        /// 幣別
        /// </summary>
        public string curr { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public string val { get; set; }
    }
    /// <summary>
    /// 附買回債券
    /// </summary>
    public class JCH_Bonds
    {
        /// <summary>
        /// 幣別
        /// </summary>
        public string curr { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public string val { get; set; }
    }
    /// <summary>
    /// 現金
    /// </summary>
    public class JCH_Cash
    {
        /// <summary>
        /// 幣別
        /// </summary>
        public string curr { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public string val { get; set; }
    }
    /// <summary>
    /// 申贖應付款
    /// </summary>
    public class JCH_Payables
    {
        /// <summary>
        /// 幣別
        /// </summary>
        public string curr { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public string val { get; set; }
    }
    /// <summary>
    /// 遠匯
    /// </summary>
    public class JCH_FXA
    {
        /// <summary>
        /// 買入幣別
        /// </summary>
        public string curr_buy { get; set; }
        /// <summary>
        /// 賣出幣別
        /// </summary>
        public string curr_sell { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public string val { get; set; }
    }
    /// <summary>
    /// 淨收付外匯款
    /// </summary>
    public class JCH_NetExchange
    {
        /// <summary>
        /// 幣別
        /// </summary>
        public string curr { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public string val { get; set; }
    }
}
