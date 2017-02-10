//#define NOTHREADING

using HtmlAgilityPack;
using log4net;
using Newtonsoft.Json;
using PCF.Class.JSON;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Util.Extension;

namespace PCF.Class
{
    public class CH : ETF
    {
        //參考匯率搜尋字串
        private const string USDSTR = "台幣兌美元匯率";
        private const string CNYSTR = "台幣兌人民幣匯率";
        private const string JPYSTR = "台幣兌日幣匯率";

        /// <summary>
        /// JSON Header
        /// </summary>
        private const string URLHeader = "https://www.cathaysite.com.tw/funds/etf/etf_ws.asmx/";
        /// <summary>
        /// PCF JSON Address
        /// </summary>
        private static string PCFURL = URLHeader + "GetPCFByStockDate?_stock_code={0}&_pcf_date=" + m_Today.ToString(DATE);
        /// <summary>
        /// Fund Asset JSON Address
        /// </summary>
        private static string HoldingURL = URLHeader + "GetFundAssetsByStock?_stock_code={0}";
        /// <summary>
        /// 參考匯率跟JSON提供的匯率可能不同, 以參考匯率為準, 所以要另外抓 
        /// </summary>
        private static string FXURL = "https://www.cathaysite.com.tw/funds/etf/pcf.aspx?t=02&fc={0}";


        #region Variable
        /// <summary>
        /// Address List
        /// </summary>
        private Dictionary<DataKind, Uri> m_URLs = new Dictionary<DataKind, Uri>();
        /// <summary>
        /// Composition List
        /// </summary>
        private Dictionary<string, IPCF> m_Composition = new Dictionary<string, IPCF>();
        /// <summary>
        /// FX List
        /// </summary>
        protected Dictionary<string, PCF_FxRate> m_CashFXs = new Dictionary<string, PCF_FxRate>();
        #endregion

        #region Property     
        public override Dictionary<DataKind, Uri> URLs { get { return m_URLs; } }
        public override string Address
        {
            get { return m_Address; }
            set
            {
                m_Address = value;

                m_URLs.Clear();
                m_FutFXs.Clear();
                m_CashFXs.Clear();

                m_Url = new Uri(string.Format(PCFURL, ETFCode));
                MSG($"[{DataKind.HEAD} (PCF JSON)]  {m_Url.AbsoluteUri}");

                m_URLs.Add(DataKind.COMPOSITION, new Uri(string.Format(HoldingURL, ETFCode)));
                MSG($"[{DataKind.COMPOSITION} (Fund Asset JSON)]  {m_URLs[DataKind.COMPOSITION].AbsoluteUri}");
                m_URLs.Add(DataKind.FX, new Uri(string.Format(FXURL, m_Address)));
                MSG($"[{DataKind.FX}]  (Reference FX){m_URLs[DataKind.FX].AbsoluteUri}");

                _DataKind();
                MSG(string.Empty);
            }
        }
        #endregion

        public CH()
        {
            m_Log = LogManager.GetLogger(typeof(CH));
        }

        /// <summary>
        /// PCF -> 參考匯率 -> Fund Asset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
#if !NOTHREADING
            ThreadPool.QueueUserWorkItem((state) =>
            {
                DownloadStringCompletedEventArgs evt = (DownloadStringCompletedEventArgs)state;
#else
            DownloadStringCompletedEventArgs evt = (DownloadStringCompletedEventArgs)e;
#endif
                try
                {
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(evt.Result);
                    DataKind kind = evt.UserState.ToEnum<DataKind>();
                    switch (kind)
                    {
                        case DataKind.HEAD:
                            JCH json = JsonConvert.DeserializeObject<JCH>(doc.DocumentNode.InnerText.Replace("\r\n", ""));
                            Head(json);
                            //先抓匯率
                            using (WebClient w = new WebClient())
                            {
                                w.Encoding = Encoding.UTF8;
                                w.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompleted);
                                w.DownloadStringAsync(URLs[DataKind.FX], DataKind.FX);
                            }
                            break;
                        case DataKind.COMPOSITION:
                            JCH_Fund funds = JsonConvert.DeserializeObject<JCH_Fund>(doc.DocumentNode.InnerText.Replace("\r\n", ""));
                            Holding(funds);
                            //處理有PCF沒有持股權重的資料      
                            int cntSTK = 0, cntFUT = 0, cntFUND = 0;
                            foreach (var item in m_Composition.Values)
                            {
                                string msg = item.Insert();
                                DataKind k;
                                if (item is PCF_STK) { k = DataKind.STK; }
                                else if (item is PCF_FUT) { k = DataKind.FUT; }
                                else if (item is PCF_FUND) { k = DataKind.FUND; }
                                else { continue; }

                                TreeNode tn = Node.Nodes[k.ToString()];
                                new Action(() =>
                                {
                                    if (!(tn.Checked = string.IsNullOrEmpty(msg)))
                                    {
                                        MSG($"[{k}]   {msg}");
                                    }
                                    else
                                    {
                                        switch (k)
                                        {
                                            case DataKind.STK:
                                                cntSTK++;
                                                break;
                                            case DataKind.FUT:
                                                cntFUT++;
                                                break;
                                            case DataKind.FUND:
                                                cntFUND++;
                                                break;
                                        }
                                    }
                                }).Invoke(tn.TreeView);
                            }
                            MSG($"[In PCF Not In FundAsset] STK:{cntSTK}    FUT:{cntFUT}    FUND:{cntFUND}");
                            m_Composition.Clear();
                            CHANNEL();
                            break;
                        case DataKind.FX:
                            FxRate(doc);

                            //匯率抓完再處理成份
                            using (WebClient w = new WebClient())
                            {
                                w.Encoding = Encoding.UTF8;
                                w.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompleted);
                                w.DownloadStringAsync(URLs[DataKind.COMPOSITION], DataKind.COMPOSITION);
                            }
                            break;
                    }
                }
                catch (TargetInvocationException ex)
                {
                    MSG("[Error]    目標網站掛點! " + ex.Message);
                    m_Log.Error(string.Empty, ex);
                }
#if !NOTHREADING
            }, e);
#endif
        }

        #region Process        
        private void Head(JCH json)
        {
            MSG($"[{DataKind.HEAD}]");
            if (json.pcf_array.Length <= 0)
            {
                MSG($"[{DataKind.HEAD} Error]   沒有資料");
                return;
            }
            foreach (var pcf in json.pcf_array)
            {
                if (pcf.stock_code.Trim() != ETFCode) { continue; }
                ETFDaily daily = new ETFDaily(ETFCode, m_Today)
                {
                    ETFCode = pcf.stock_code.Trim(),
                    PreAllot = pcf.pre_amt.ToDouble(),
                    FundAssetValue = pcf.aum.ToDouble(),
                    PublicShares = pcf.tot_unit.ToDouble(),
                    PublicSharesDiff = pcf.diff_unit.ToDouble(),
                    NAV = pcf.nav.ToDouble(),
                    EstCValue = pcf.basket_val.ToDouble(),
                    CashDiff = pcf.basket_nav_val_diff.ToDouble(),
                    Allot = pcf.basket_act_amt.ToDouble(),
                    EstDValue = pcf.basket_act_amt_diff.ToDouble(),
                    DataDate = pcf.pcf_date.ToDateTime()
                };
                string msg = daily.Insert();
                TreeNode node = Node.Nodes[DataKind.HEAD.ToString()];
                new Action(() =>
                {
                    if (!(node.Checked = string.IsNullOrEmpty(daily.Insert()))) { MSG($"[{DataKind.HEAD} Error]   {msg}"); }
                }).Invoke(node.TreeView);
                m_DataDate = daily.DataDate;

                Stk(pcf.prod_array);
                Fut(pcf.prod_array);
                Fund(pcf.prod_array);
            }
        }
        private void Stk(JCH_Product[] json)
        {
            MSG($"[{DataKind.STK}] ");
            int cnt = 0;
            foreach (var prod in json)
            {
                //濾掉ETF
                //這應該是Cathay的Bug, 等改掉可以拿掉
                if (prod.prod_type != "SK" || (prod.prod_code.Substring(0, 2) == "00" && !prod.prod_code.Contains("."))) { continue; }
                PCF_STK stk = new PCF_STK(ETFCode, m_Today, m_DataDate)
                {
                    PID = prod.prod_code.Trim(),
                    Name = prod.prod_name.Trim(),
                    PCFUnits = prod.basket_shares.ToInt(),
                    Exch = ExchParse(prod.prod_code.Trim(), ".")
                };
                m_Composition.Add(stk.PID, stk);
                cnt++;
            }

            MSG($"[{DataKind.STK}]   成份筆數: {cnt}");
        }
        private void Fut(JCH_Product[] json)
        {
            MSG($"[{DataKind.FUT}] ");
            int cnt = 0;
            foreach (var prod in json)
            {
                if (prod.prod_type != "FT") { continue; }
                string[] ym = prod.futures_date.Split('/');
                PCF_FUT fut = new PCF_FUT(ETFCode, m_Today, m_DataDate)
                {
                    YM = prod.futures_date.Replace("/", ""),
                    Y = ym[0].ToInt(),
                    M = ym[1].ToInt(),
                    Head = prod.prod_code.Trim(),
                    Name = prod.prod_name.Trim(),
                    PCFUnits = prod.basket_shares.ToDouble() * (prod.futures_buysell == "B" ? 1 : -1)
                };
                fut.PID = fut.Head + fut.YM;
                m_Composition.Add(fut.PID, fut);
                cnt++;
            }
            MSG($"[{DataKind.FUT}]   成份筆數: {cnt}");
        }
        private void Fund(JCH_Product[] json)
        {
            MSG($"[{DataKind.FUND}] ");
            int cnt = 0;
            foreach (var prod in json)
            {
                //找ETF
                //這應該是Cathay的Bug, 等改掉可以拿掉
                if (prod.prod_type != "FN" && !(prod.prod_type == "SK" && prod.prod_code.Substring(0, 2) == "00" && !prod.prod_code.Contains("."))) { continue; }
                PCF_FUND fund = new PCF_FUND(ETFCode, m_Today, m_DataDate)
                {
                    PID = prod.prod_code.Trim(),
                    Name = prod.prod_name.Trim(),
                    PCFUnits = prod.basket_shares.ToInt(),
                    Exch = ExchParse(prod.prod_code.Trim(), ".")
                };
                m_Composition.Add(fund.PID, fund);
                cnt++;
            }

            MSG($"[{DataKind.FUND}]   成份筆數: {cnt}");
        }
        private void Holding(JCH_Fund json)
        {
            string msg = string.Empty;
            PCF_Cash.Delete(m_DataDate, ETFCode);
            PCF_Margin.Delete(m_DataDate, ETFCode);
            PCF_Forward.Delete(m_DataDate, ETFCode);

            if (json.fundassets_array.Length <= 0)
            {
                MSG($"[{DataKind.COMPOSITION}]   沒有資料");
                return;
            }

            foreach (var fund in json.fundassets_array)
            {
                if (fund.stock_code.Trim() != ETFCode) { continue; }

                #region FX Process
                if (fund.currency_array != null && fund.currency_array.Length > 0)
                {
                    MSG($"[{DataKind.FX}]");
                    int fxcnt = 0;
                    //找出美金台幣匯率
                    var twd = fund.currency_array.First(e => e.currency == "USD");
                    if (twd != null)
                    {
                        TreeNode fxNode = Node.Nodes[DataKind.FX.ToString()];
                        //建立美金台幣匯率, CH給的是各國對台幣匯率, 要轉成各國對美金匯率
                        PCF_FxRate twdfx = new PCF_FxRate(ETFCode, m_DataDate) { Base = "USD", Quoted = "TWD", Value = twd.rate.ToDouble() };
                        msg = twdfx.Insert();
                        m_CashFXs.Add(twdfx.Quoted, twdfx);
                        new Action(() =>
                        {
                            if (!(fxNode.Checked = string.IsNullOrEmpty(msg)))
                            {
                                MSG($"[{DataKind.FX} Error]   {msg}");
                            }
                            else
                            {
                                fxcnt++;
                            }
                        }).BeginInvoke(fxNode.TreeView);

                        foreach (var curr in fund.currency_array)
                        {
                            //美金處理過了, 跳過
                            if (curr.currency == "USD") { continue; }
                            msg = string.Empty;

                            PCF_FxRate fx = new PCF_FxRate(ETFCode, m_DataDate) { Base = "USD", Quoted = curr.currency, Value = curr.rate.ToDouble() };
                            //計算匯率
                            fx.Value = Math.Round(twdfx.Value / fx.Value, 4, MidpointRounding.AwayFromZero);
                            msg = fx.Insert();
                            m_CashFXs.Add(fx.Quoted, fx);

                            new Action(() =>
                            {
                                if (!(fxNode.Checked = string.IsNullOrEmpty(msg)))
                                {
                                    MSG($"[{DataKind.FX} Error]   {msg}");
                                }
                                else
                                {
                                    fxcnt++;
                                }
                            }).Invoke(fxNode.TreeView);
                        }
                        MSG($"[{DataKind.FX}]   筆數: {fxcnt}");
                    }
                }


                #endregion
                #region Cash Process
                if (fund.cash_array != null && fund.cash_array.Length > 0)
                {
                    MSG($"[{DataKind.CASH}]");
                    TreeNode cashNode = Node.Nodes[DataKind.CASH.ToString()];
                    int cashcnt = 0;
                    foreach (var cash in fund.cash_array)
                    {
                        msg = string.Empty;
                        PCF_Cash c = new PCF_Cash(ETFCode, m_DataDate) { Currency = cash.curr == "NTD" ? "TWD" : cash.curr, Amount = cash.val.ToDouble() };
                        c.Rate = Rate(m_CashFXs, c.Currency, "TWD");
                        msg = c.Insert();
                        new Action(() =>
                        {
                            if (!(cashNode.Checked = string.IsNullOrEmpty(msg)))
                            {
                                MSG($"[{DataKind.CASH} Error]   {msg}");
                            }
                            else
                            {
                                cashcnt++;
                            }
                        }).Invoke(cashNode.TreeView);
                    }
                    MSG($"[{DataKind.CASH}]     筆數:{cashcnt}");
                }


                #endregion
                #region Margin Process

                if (fund.margin_array != null && fund.margin_array.Length > 0)
                {
                    MSG($"[{DataKind.MARGIN}]");
                    TreeNode marginNode = Node.Nodes[DataKind.MARGIN.ToString()];
                    int margincnt = 0;
                    foreach (var margin in fund.margin_array)
                    {
                        msg = string.Empty;
                        PCF_Margin m = new PCF_Margin(ETFCode, m_DataDate) { Currency = margin.curr == "NTD" ? "TWD" : margin.curr, Amount = margin.val.ToDouble() };
                        m.Rate = Rate(m_CashFXs, m.Currency, "TWD");
                        msg = m.Insert();
                        new Action(() =>
                        {
                            if (!(marginNode.Checked = string.IsNullOrEmpty(msg)))
                            {
                                MSG($"[{DataKind.MARGIN} Error]   {msg}");
                            }
                            else
                            {
                                margincnt++;
                            }
                        }).Invoke(marginNode.TreeView);
                    }
                    MSG($"[{DataKind.MARGIN}]     筆數:{margincnt}");
                }
                #endregion
                #region Forward Process

                if (fund.fxa_array != null && fund.fxa_array.Length > 0)
                {
                    MSG($"[{DataKind.FORWARD}]");
                    TreeNode forwardNode = Node.Nodes[DataKind.FORWARD.ToString()];
                    int fxacnt = 0;
                    foreach (var forward in fund.fxa_array)
                    {
                        msg = string.Empty;
                        PCF_Forward f = new PCF_Forward(ETFCode, m_DataDate) { Currency = forward.curr_sell == "NTD" ? "TWD" : forward.curr_sell, Amount = forward.val.ToDouble() };
                        f.Rate = Rate(m_CashFXs, f.Currency, "TWD");
                        msg = f.Insert();
                        new Action(() =>
                        {
                            if (!(forwardNode.Checked = string.IsNullOrEmpty(msg)))
                            {
                                MSG($"[{DataKind.FORWARD}]   {msg}");
                            }
                            else
                            {
                                fxacnt++;
                            }
                        }).Invoke(forwardNode.TreeView);
                    }
                    MSG($"[{DataKind.FORWARD}]     筆數:{fxacnt}");
                }
                #endregion
                #region Holding Process
                MSG($"[FundAsset]");
                int cntSTK = 0, cntFUT = 0, cntFUND = 0;

                foreach (var comp in fund.composition_array)
                {
                    msg = string.Empty;
                    IPCF pcf = null;
                    DataKind k = DataKind.COMPOSITION;
                    switch (comp.type)
                    {
                        case "SK":
                            pcf = m_Composition.ContainsKey(comp.code.Trim()) ? m_Composition[comp.code.Trim()] : new PCF_STK(ETFCode, m_Today, m_DataDate)
                            {
                                PID = comp.code.Trim(),
                                Name = comp.name.Trim(),
                                Exch = ExchParse(comp.code.Trim(), ".")
                            };
                            pcf.TotalUnits = comp.volume.ToDouble();
                            pcf.Weights = decimal.Parse(comp.ratio) / 100M;
                            k = DataKind.STK;
                            break;
                        case "FT":
                            string key = comp.code.Trim() + comp.futures_date.Replace("/", "");
                            if (m_Composition.ContainsKey(key))
                            {
                                pcf = m_Composition[key];
                            }
                            else
                            {
                                string[] ym = comp.futures_date.Split('/');
                                pcf = new PCF_FUT(ETFCode, m_Today, m_DataDate)
                                {
                                    YM = comp.futures_date.Replace("/", ""),
                                    Y = ym[0].ToInt(),
                                    M = ym[1].ToInt(),
                                    Head = comp.code.Trim(),
                                    Name = comp.name.Trim()
                                };
                                pcf.PID = ((PCF_FUT)pcf).Head + ((PCF_FUT)pcf).YM;
                            }

                            pcf.TotalUnits = double.Parse(comp.volume, NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign) * (comp.futures_buysell == "B" ? 1 : -1);
                            pcf.Weights = decimal.Parse(comp.ratio) / 100M;
                            if (YstFutPrice)
                            {
                                ((PCF_FUT)pcf).YP = fund.futures_val.ToDouble() / ((PCF_FUT)pcf).CValue / Math.Abs(pcf.TotalUnits) / Rate(m_FutFXs, ((PCF_FUT)pcf).Currency, "TWD");
                            }
                            k = DataKind.FUT;
                            break;
                        case "FN":
                        case "ETF":
                            pcf = m_Composition.ContainsKey(comp.code.Trim()) ? m_Composition[comp.code.Trim()] : new PCF_FUND(ETFCode, m_Today, m_DataDate) { PID = comp.code.Trim(), Name = comp.name.Trim() };
                            pcf.TotalUnits = double.Parse(comp.volume, NumberStyles.AllowThousands);
                            pcf.Weights = decimal.Parse(comp.ratio) / 100M;
                            k = DataKind.FUND;
                            break;
                        default:
                            break;
                    }
                    if (pcf == null) { return; }
                    msg = pcf.Insert();
                    TreeNode node = Node.Nodes[k.ToString()];
                    new Action(() =>
                    {
                        if (!(node.Checked = string.IsNullOrEmpty(msg)))
                        {
                            MSG($"[{k} Error]   {msg}");
                        }
                        else
                        {
                            switch (k)
                            {
                                case DataKind.STK:
                                    cntSTK++;
                                    break;
                                case DataKind.FUT:
                                    cntFUT++;
                                    break;
                                case DataKind.FUND:
                                    cntFUND++;
                                    break;
                            }
                        }
                    }).Invoke(node.TreeView);
                    if (m_Composition.ContainsKey(pcf.PID)) { m_Composition.Remove(pcf.PID); }
                }

                MSG($"[FundAsset]   STK:{cntSTK}    FUT:{cntFUT}    FUND:{cntFUND}");
                #endregion
            }
        }
        private void FxRate(HtmlAgilityPack.HtmlDocument doc)
        {
            MSG($"[HTML {DataKind.FX}]");
            string rateValue = string.Empty;

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(INI(nameof(CH), DataKind.FX.ToString()));

            foreach (var node in nodes)
            {
                string value = node.InnerText.Trim();
                if (string.IsNullOrEmpty(value)) { continue; }
                value = value.Split('：')[1].Split('(')[0];
                string[] curritems = value.Split('；');
                double usd = 0;
                int cnt = 0;
                PCF_FxRate fx = null;
                foreach (var item in curritems)
                {
                    double v;
                    if (item.Contains(USDSTR))
                    {
                        usd = double.Parse(item.Replace(USDSTR, ""));
                        fx = new PCF_FxRate(ETFCode, m_DataDate) { Base = "USD", Quoted = "TWD", Value = usd };
                    }
                    else if (item.Contains(CNYSTR))
                    {
                        v = double.Parse(item.Replace(CNYSTR, ""));
                        fx = new PCF_FxRate(ETFCode, m_DataDate) { Base = "USD", Quoted = "CNY", Value = Math.Round(usd / v, 4, MidpointRounding.AwayFromZero) };
                    }
                    else if (item.Contains(JPYSTR))
                    {
                        v = double.Parse(item.Replace(JPYSTR, ""));
                        fx = new PCF_FxRate(ETFCode, m_DataDate) { Base = "USD", Quoted = "JPY", Value = Math.Round(usd / v, 4, MidpointRounding.AwayFromZero) };
                    }
                    if (fx == null) { continue; }
                    //string msg = fx.Insert();
                    //TreeNode tn = Node.Nodes[DataKind.FX.ToString()];
                    //new Action(() =>
                    //{
                    //    if (!(tn.Checked = string.IsNullOrEmpty(msg))) { MSG($"[{DataKind.FX}]   " + msg); }
                    //}).Invoke(tn.TreeView);
                    //rateValue = !string.IsNullOrEmpty(rateValue) ? string.Join("|", rateValue, fx.ToString()) : fx.ToString();
                    m_FutFXs.Add(fx.Quoted, fx);
                    cnt++;
                }
                MSG($"[HTML {DataKind.FX}]      筆數:{cnt}");
            }
            //foreach (var redis in Utility.Redis)
            //{
            //    redis.HashSet(int.Parse(Utility.INI["SYS"]["FXDB"]), ETFCode, "FXRate", rateValue);
            //}
        }
        #endregion

        protected override void _DataKind()
        {
            if (Node.Nodes != null) { Node.Nodes.Clear(); }
            Node.Nodes.Add(new TreeNode(DataKind.HEAD.ToString()) { Name = DataKind.HEAD.ToString(), Tag = this });
            var kinds = Enum.GetValues(typeof(DataKind));
            foreach (DataKind kind in kinds)
            {
                switch (kind)
                {
                    case DataKind.STK:
                    case DataKind.FUND:
                    case DataKind.FUT:
                    case DataKind.FX:
                    case DataKind.CASH:
                    case DataKind.MARGIN:
                    case DataKind.FORWARD:
                        Node.Nodes.Add(new TreeNode(kind.ToString()) { Name = kind.ToString(), Tag = this });
                        break;
                    case DataKind.HEAD:
                    case DataKind.COMPOSITION:
                    default:
                        continue;
                }
            }
        }
    }
}