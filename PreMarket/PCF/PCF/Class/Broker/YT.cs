//#define NOTHREADING

using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCF.Class.JSON;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using Util.Extension;

namespace PCF.Class
{
    public class YT : ETF
    {
        private const string YTD = "@D";
        /// <summary>
        /// Header
        /// </summary>
        private const string URLHeader = "http://www.yuantaetfs.com/";
        /// <summary>
        /// ETF 基本資料
        /// </summary>
        private static string ETFSubURL = URLHeader + "api/Orders?fundid={0}";
        /// <summary>
        /// ETF 成份資料
        /// </summary>
        private static string TWURL = URLHeader + "api/Composition?fundid={0}";
        //2017/02/08 元大問題把日期拿掉
        /// <summary>
        /// Fut Asset
        /// </summary>
        private static string FUTURL = URLHeader + "api/FutWeights?fundid={0}&date=" + YTD;//+ m_Today.ToString(DATE);
        /// <summary>
        /// Stk Asset
        /// </summary>
        private static string STKURL = URLHeader + "api/stkweights?fundid={0}&date=" + YTD;// + m_Today.ToString(DATE);
        /// <summary>
        /// Fund Asset
        /// </summary>
        private static string FUNDURL = URLHeader + "api/EtfWeights?fundid={0}&date=" + YTD;//+ m_Today.ToString(DATE);
        /// <summary>
        /// FXRate
        /// 2016/08/24 這個匯率才是對的, 不然跨天的國外ETF算起來會有問題(EX:00647L,00648R)
        /// </summary>
        /// //private static string FXURL = URLHeader + "api/CrncyRate?fundid={0}";
        private static string FXURL = URLHeader + "api/PcfCrncy?fundid={0}&date=" + YTD;//+ m_Today.ToString(DATE);
        /// <summary>
        /// Cash
        /// </summary>
        private static string CASHURL = URLHeader + "api/PcfCash?fundid={0}&date=" + YTD;// + m_Today.ToString(DATE);
        /// <summary>
        /// Margin
        /// </summary>
        private static string MARGINURL = URLHeader + "api/Margin?fundid={0}&date=" + YTD;// + m_Today.ToString(DATE);
        /// <summary>
        /// Forward
        /// </summary>
        private static string FORWARDURL = URLHeader + "api/FX?fundid={0}&date=" + YTD;// + m_Today.ToString(DATE);
        /// <summary>
        /// ETF Asset Value
        /// </summary>
        private static string FUNDSIZEURL = URLHeader + "api/FundWeights?fundid={0}&date=" + YTD;//+ m_Today.ToString(DATE);
        private static string SETTLECRNCYURL = URLHeader + "api/PcfCrncy?fundid={0}&date=" + YTD;//+ m_Today.ToString(DATE);

        #region Variable
        /// <summary>
        /// Address List
        /// </summary>
        private Dictionary<DataKind, Uri> m_URLs = new Dictionary<DataKind, Uri>();
        /// <summary>
        /// Stk PCF 
        /// </summary>
        private Dictionary<string, PCF_STK> m_Stocks = new Dictionary<string, PCF_STK>();
        /// <summary>
        /// 結算匯率
        /// </summary>
        private Dictionary<string, PCF_FxRate> m_SettleCurncies = new Dictionary<string, PCF_FxRate>();
        /// <summary>
        /// Fund Size Info
        /// </summary>
        private JYT_FundSize m_Size;
        private DataKind m_Stage2;
        #endregion

        #region Property   
        public override Dictionary<DataKind, Uri> URLs { get { return m_URLs; } }
        public override string Address
        {
            get { return m_Address; }
            set
            {
                m_URLs.Clear();
                m_FutFXs.Clear();
                m_SettleCurncies.Clear();

                m_Address = value;
                m_Url = new Uri(string.Format(ETFSubURL, m_Address));
                MSG($"[{DataKind.HEAD}]  {m_Url.AbsoluteUri}");

                var kinds = Enum.GetValues(typeof(DataKind));

                foreach (DataKind kind in kinds)
                {
                    //string k = kind.ToString();
                    string url = string.Empty;

                    switch (kind)
                    {
                        case DataKind.COMPOSITION:
                            url = string.Format(TWURL, m_Address);
                            //m_URLs.Add(kind, new Uri(string.Format(TWURL, m_Address)));
                            break;
                        case DataKind.STK:
                            url = string.Format(STKURL, m_Address);
                            //m_URLs.Add(kind, new Uri(string.Format(STKURL, m_Address)));
                            break;
                        case DataKind.FUT:
                            url = string.Format(FUTURL, m_Address);
                            //m_URLs.Add(kind, new Uri(string.Format(FUTURL, m_Address)));
                            break;
                        case DataKind.FUND:
                            url = string.Format(FUNDURL, m_Address);
                            //m_URLs.Add(kind, new Uri(string.Format(FUNDURL, m_Address)));
                            break;
                        case DataKind.FX:
                            url = string.Format(FXURL, m_Address);
                            //m_URLs.Add(kind, new Uri(string.Format(FXURL, m_Address)));
                            break;
                        case DataKind.CASH:
                            url = string.Format(CASHURL, m_Address);
                            //m_URLs.Add(kind, new Uri(string.Format(CASHURL, m_Address)));
                            break;
                        case DataKind.MARGIN:
                            url = string.Format(MARGINURL, m_Address);
                            //m_URLs.Add(kind, new Uri(string.Format(MARGINURL, m_Address)));
                            break;
                        case DataKind.FORWARD:
                            url = string.Format(FORWARDURL, m_Address);
                            //m_URLs.Add(kind, new Uri(string.Format(FORWARDURL, m_Address)));
                            break;
                        case DataKind.FUNDSIZE:
                            url = string.Format(FUNDSIZEURL, m_Address);
                            //m_URLs.Add(kind, new Uri(string.Format(FUNDSIZEURL, m_Address)));
                            break;
                        case DataKind.SETTLECURNCY:
                            url = string.Format(SETTLECRNCYURL, m_Address);
                            //m_URLs.Add(kind, new Uri(string.Format(SETTLECRNCYURL, m_Address)));
                            break;
                        default:
                            continue;
                    }
                    url = url.Replace(YTD, Utility.YuantaNotInputDate ? string.Empty : m_Today.ToString(DATE));
                    m_URLs.Add(kind, new Uri(url));
                    MSG($"[{kind} (JSON)]   {m_URLs[kind].AbsoluteUri}");
                }
                _DataKind();
                MSG(string.Empty);
            }
        }
        #endregion

        public YT()
        {
            m_Log = LogManager.GetLogger(typeof(YT));
        }
        /// <summary>
        /// 順序
        /// 1. ETF基本資料
        /// 2. Composition, FundSize, SettleCurrency 
        /// 3. STK, FUT, FUND, Forward, FX 
        /// 4. Cash, Margin
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
                    DataKind kind = evt.UserState.ToEnum<DataKind>();
                    switch (kind)
                    {
                        case DataKind.HEAD: //順序1, 最先呼叫
                            Head(evt.Result);
                            //呼叫順序2
                            //Composition, FundSize, SettleCurncy
                            foreach (var url in URLs)
                            {
                                if (url.Key != DataKind.COMPOSITION &&
                                url.Key != DataKind.FUNDSIZE &&
                                url.Key != DataKind.SETTLECURNCY) { continue; }
                                using (WebClient w = new WebClient())
                                {
                                    w.Encoding = Encoding.UTF8;
                                    w.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompleted);
                                    w.DownloadStringAsync(URLs[url.Key], url.Key);
                                }
                            }
                            break;
                        case DataKind.COMPOSITION:  //順序2, 順序2全都處理完才呼叫順序3
                            Composition(evt.Result);
                            m_Stage2 |= kind;
                            _Stage2Complete();
                            ////呼叫順序3
                            ////STK, FUT, FUND, Forward, FX
                            //foreach (var url in URLs)
                            //{
                            //    //if (url.Key == DataKind.COMPOSITION) { continue; }
                            //    if (url.Key == DataKind.COMPOSITION ||
                            //    url.Key == DataKind.CASH ||
                            //    url.Key == DataKind.MARGIN ||
                            //    url.Key == DataKind.FUNDSIZE ||
                            //    url.Key == DataKind.SETTLECURNCY) { continue; }
                            //    using (WebClient w = new WebClient())
                            //    {
                            //        w.Encoding = Encoding.UTF8;
                            //        w.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompleted);
                            //        w.DownloadStringAsync(url.Value, url.Key);
                            //    }
                            //}
                            break;
                        case DataKind.FUNDSIZE: //順序2
                            FundSize(evt.Result);
                            m_Stage2 |= kind;
                            _Stage2Complete();
                            break;
                        case DataKind.SETTLECURNCY: //順序2
                            SettleCurncy(evt.Result);
                            m_Stage2 |= kind;
                            _Stage2Complete();
                            break;
                        case DataKind.STK:  //順序3
                            Stk(evt.Result);
                            break;
                        case DataKind.FUT:  //順序3
                            Fut(evt.Result);
                            break;
                        case DataKind.FUND: //順序3
                            Fund(evt.Result);
                            break;
                        case DataKind.FORWARD:  //順序3
                            Forward(evt.Result);
                            break;
                        case DataKind.FX:   //順序3, 處理完匯率才呼叫順序4
                            FxRate(evt.Result);
                            //呼叫順序4
                            //Cash, Margin
                            foreach (var url in URLs)
                            {
                                if (url.Key != DataKind.CASH &&
                                url.Key != DataKind.MARGIN) { continue; }
                                using (WebClient w = new WebClient())
                                {
                                    w.Encoding = Encoding.UTF8;
                                    w.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompleted);
                                    w.DownloadStringAsync(url.Value, url.Key);
                                }
                            }
                            break;
                        case DataKind.CASH: //順序4
                            Cash(evt.Result);
                            break;
                        case DataKind.MARGIN:   //順序4
                            Margin(evt.Result);
                            CHANNEL();
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

        #region Public        
        public override string ToString()
        {
            string tostring = base.ToString();
            if (!string.IsNullOrEmpty(Address))
            {
                tostring = tostring.Replace(Address, "");
            }
            foreach (var item in URLs)
            {
                tostring += item.ToString() + ";";// +"\r\n";
            }
            return tostring;
        }
        #endregion

        #region Process
        private void Head(string jsonStr)
        {
            MSG($"[{DataKind.HEAD}]");
            JArray array;
            int cnt = 0;
            TreeNode node = Node.Nodes[DataKind.HEAD.ToString()];

            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "null")
            {
                MSG($"[{DataKind.HEAD}]   沒有資料");
                return;
            }
            else
            {
                array = JArray.Parse(jsonStr);
                if (array.Count <= 0)
                {
                    MSG($"[{DataKind.HEAD}]   有JSON但筆數為0 ");
                    return;
                }
            }

            foreach (var JObj in array)
            {
                JYT_ETF json = JsonConvert.DeserializeObject<JYT_ETF>(JObj.ToString());
                ETFDaily daily = new ETFDaily(ETFCode, m_Today);
                daily.ValueFromJSON(json);
                string msg = daily.Insert();

                new Action(() =>
                {
                    if (!(node.Checked = string.IsNullOrEmpty(msg)))
                    {
                        MSG($"[{DataKind.STK}]   {msg}");
                    }
                    else
                    {
                        cnt++;
                    }
                }).Invoke(node.TreeView);
                m_DataDate = daily.DataDate;
            }
            MSG($"[{DataKind.HEAD}]   筆數:{cnt} ");
        }
        /// <summary>
        /// 成份資料
        /// </summary>
        /// <param name="jsonStr"></param>
        private void Composition(string jsonStr)
        {
            MSG($"[{DataKind.COMPOSITION}]");
            int cnt = 0;
            JArray array;
            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "null")
            {
                MSG($"[{DataKind.COMPOSITION}]   沒有資料 ");
                return;
            }
            else
            {
                array = JArray.Parse(jsonStr);
                if (array.Count <= 0)
                {
                    MSG($"[{DataKind.COMPOSITION}]   有JSON但筆數為0 ");
                    return;
                }
            }
            foreach (var JObj in array)
            {
                PCF_STK stk = new PCF_STK(ETFCode, m_Today, m_DataDate);
                JYT_STK json = JsonConvert.DeserializeObject<JYT_STK>(JObj.ToString());
                stk.PID = json.stkcd ?? json.code;
                stk.Name = json.name;
                stk.PCFUnits = json.qty.ToInt();
                stk.ReplaceWithCash = json.cashinlieu;
                stk.PhysicalPurchase = json.minimum;
                stk.Exch = ExchParse(stk.PID);
                m_Stocks.Add(stk.PID, stk);
                cnt++;
            }
            TreeNode node = Node.Nodes[DataKind.COMPOSITION.ToString()];
            new Action(() => { node.Checked = true; }).Invoke(node.TreeView);
            MSG($"[{DataKind.COMPOSITION}]   筆數:{cnt}");
        }
        /// <summary>
        /// 取期貨總值, 算期貨昨收用
        /// </summary>
        /// <param name="jsonStr"></param>
        private void FundSize(string jsonStr)
        {
            MSG($"[{DataKind.FUNDSIZE}]");
            JArray array;
            string rateValue = string.Empty;
            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "null")
            {
                MSG($"[{DataKind.FUNDSIZE}]   沒有資訊");
                return;
            }
            else
            {
                array = JArray.Parse(jsonStr);
                if (array.Count <= 0)
                {
                    MSG($"[{DataKind.FUNDSIZE}]   有JSON但筆數為0 ");
                    return;
                }
            }

            foreach (var JObj in array)
            {
                m_Size = JsonConvert.DeserializeObject<JYT_FundSize>(JObj.ToString());
                MSG($"[{DataKind.FUNDSIZE}]   筆數:1");
                break;
            }
        }
        private void SettleCurncy(string jsonStr)
        {
            MSG($"[{DataKind.SETTLECURNCY}]");
            int cnt = 0;
            JArray array;
            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "null")
            {
                MSG($"[{DataKind.SETTLECURNCY}]   沒有結算匯率");
                return;
            }
            else
            {
                array = JArray.Parse(jsonStr);
                if (array.Count <= 0)
                {
                    MSG($"[{DataKind.SETTLECURNCY}]   有JSON但筆數為0 ");
                    return;
                }
            }
            foreach (var JObj in array)
            {
                JYT_SettleCurncy json = JsonConvert.DeserializeObject<JYT_SettleCurncy>(JObj.ToString());
                PCF_FxRate fx = new PCF_FxRate(ETFCode, m_DataDate) { Base = "USD", Quoted = json.crncy == "NTD" ? "TWD" : json.crncy, Value = json.endRate.ToDouble() };

                m_SettleCurncies.Add(fx.Quoted, fx);
                cnt++;
            }
            MSG($"[{DataKind.SETTLECURNCY}]   筆數:{cnt}");
        }
        /// <summary>
        /// FundAsset - STK
        /// </summary>
        /// <param name="jsonStr"></param>
        private void Stk(string jsonStr)
        {
            MSG($"[{DataKind.STK}]");
            JArray array;
            int cntFundAsset = 0, cntPCF = 0;
            TreeNode node = Node.Nodes[DataKind.STK.ToString()];

            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "null")
            {
                MSG($"[{DataKind.STK}]   沒有資料");
                return;
            }
            else
            {
                array = JArray.Parse(jsonStr);
                if (array.Count <= 0)
                {
                    //沒有FundAsset有PCF
                    foreach (var stk in m_Stocks.Values)
                    {
                        string msg = stk.Insert();
                        new Action(() =>
                        {
                            if (!(node.Checked = string.IsNullOrEmpty(msg)))
                            {
                                MSG($"[{DataKind.STK}]   {msg}");
                            }
                            else
                            {
                                cntPCF++;
                            }
                        }).Invoke(node.TreeView);
                    }
                    MSG($"[{DataKind.STK}]   FundAsset筆數為0, 只有PCF的筆數:{cntPCF} ");
                    return;
                }
            }

            foreach (var JObj in array)
            {
                JYT_ForeignSTK json = JsonConvert.DeserializeObject<JYT_ForeignSTK>(JObj.ToString());
                PCF_STK stk = m_Stocks.ContainsKey(json.code) ? m_Stocks[json.code] : new PCF_STK(ETFCode, m_Today, m_DataDate);
                //有FundAsset, 補資料
                stk.PID = json.code;
                stk.Name = json.name;
                stk.TotalUnits = json.qty.ToDouble();
                stk.Weights = json.weights.ToDecimal() / 100M;
                stk.Exch = ExchParse(stk.PID);

                string msg = stk.Insert();
                new Action(() =>
                {
                    if (!(node.Checked = string.IsNullOrEmpty(msg)))
                    {
                        MSG($"[{DataKind.STK}]   {msg}");
                    }
                    else
                    {
                        cntFundAsset++;
                    }
                }).Invoke(node.TreeView);
                //寫入好的移掉, 有PCF沒FundAsset的需要再處理一下
                m_Stocks.Remove(json.code);
            }
            //有PCF沒FundAsset的
            foreach (var stk in m_Stocks.Values)
            {
                string msg = stk.Insert();

                new Action(() =>
                {
                    if (!(node.Checked = string.IsNullOrEmpty(msg)))
                    {
                        MSG($"[{DataKind.STK}]   {msg}");
                    }
                    else
                    {
                        cntPCF++;
                    }
                }).Invoke(node.TreeView);
            }
            MSG($"[{DataKind.STK}]   FundAsset筆數:{cntFundAsset}, 只有PCF的筆數:{cntPCF} ");
        }
        /// <summary>
        /// FundAsset - FUT
        /// 結算價, Fund Size處理完才能跑, 算價格會用到
        /// </summary>
        /// <param name="jsonStr"></param>
        private void Fut(string jsonStr)
        {
            MSG($"[{DataKind.FUT}]");
            JArray array;
            int cnt = 0;
            TreeNode node = Node.Nodes[DataKind.FUT.ToString()];

            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "null")
            {
                MSG($"[{DataKind.FUT}]   沒有資料");
                return;
            }
            else
            {
                array = JArray.Parse(jsonStr);
                if (array.Count <= 0)
                {
                    MSG($"[{DataKind.FUT}]   有JSON但筆數為0 ");
                    return;
                }
            }

            foreach (var JObj in array)
            {
                JYT_FUND json = JsonConvert.DeserializeObject<JYT_FUND>(JObj.ToString());
                PCF_FUT fut = new PCF_FUT(ETFCode, m_Today, m_DataDate);
                fut.Head = json.code;
                fut.YM = json.ym;
                fut.Y = fut.YM.ToInt() / 100;
                fut.M = fut.YM.ToInt() % (fut.Y * 100);
                fut.PID = fut.Head + fut.YM;
                fut.Name = json.name;
                fut.TotalUnits = json.qty.ToDouble();
                fut.Weights = json.weights.ToDecimal() / 100M;
                //if (fut.Currency!="TWD")
                //有沒有需要特別處理昨收註記(寫在tblETF裡面)
                if (YstFutPrice)
                {
                    //取Size&結算價計算昨收
                    fut.YP = m_Size.futvalues.ToDouble() / fut.CValue / Math.Abs(fut.TotalUnits) / Rate(m_SettleCurncies, fut.Currency, "TWD");
                }
                string msg = fut.Insert();

                new Action(() =>
                {
                    if (!(node.Checked = string.IsNullOrEmpty(msg)))
                    {
                        MSG($"[{DataKind.FUT}]   {msg}");
                    }
                    else
                    {
                        cnt++;
                    }
                }).Invoke(node.TreeView);
            }
            MSG($"[{DataKind.FUT}]   筆數:{cnt} ");
        }
        private void Fund(string jsonStr)
        {
            MSG($"[{DataKind.FUND}]");
            JArray array;
            int cnt = 0;
            TreeNode node = Node.Nodes[DataKind.FUND.ToString()];

            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "null")
            {
                MSG($"[{DataKind.FUND}]   沒有資料");
                return;
            }
            else
            {
                array = JArray.Parse(jsonStr);
                if (array.Count <= 0)
                {
                    MSG($"[{DataKind.FUND}]   有JSON但筆數為0 ");
                    return;
                }
            }

            foreach (var JObj in array)
            {
                JYT_FUND json = JsonConvert.DeserializeObject<JYT_FUND>(JObj.ToString());
                PCF_FUND fund = new PCF_FUND(ETFCode, m_Today, m_DataDate);
                fund.PID = json.code;
                fund.Name = json.name;
                fund.Exch = ExchParse(fund.PID);
                fund.TotalUnits = json.qty.ToDouble();
                fund.Weights = json.weights.ToDecimal() / 100M;
                string msg = fund.Insert();

                new Action(() =>
                {
                    if (!(node.Checked = string.IsNullOrEmpty(msg)))
                    {
                        MSG($"[{DataKind.FUND}]   {msg}");
                    }
                    else
                    {
                        cnt++;
                    }
                }).Invoke(node.TreeView);
            }
            MSG($"[{DataKind.FUND}]   筆數:{cnt}");
        }
        /// <summary>
        /// 不用等匯率, 資料裡面有匯率
        /// </summary>
        /// <param name="jsonStr"></param>
        private void Forward(string jsonStr)
        {
            MSG($"[{DataKind.FORWARD}]");
            JArray array;
            int cnt = 0;
            TreeNode node = Node.Nodes[DataKind.FORWARD.ToString()];

            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "null")
            {
                MSG($"[{DataKind.FORWARD}]   沒有遠匯");
                return;
            }
            else
            {
                array = JArray.Parse(jsonStr);
                if (array.Count <= 0)
                {
                    MSG($"[{DataKind.FORWARD}]   有JSON但筆數為0 ");
                    return;
                }
            }
            PCF_Forward.Delete(m_DataDate, ETFCode);
            foreach (var JObj in array)
            {
                JYT_FX json = JsonConvert.DeserializeObject<JYT_FX>(JObj.ToString());

                PCF_Forward forward = new PCF_Forward(ETFCode, m_DataDate) { Currency = json.SAL_CRNCY == "NTD" ? "TWD" : json.SAL_CRNCY, Amount = json.SAL_AMT.ToDouble(), Rate = json.End_Rate.ToDouble(), HedgeRatio = json.HedgeRatio.ToDouble() / 100D };

                string msg = forward.Insert();

                new Action(() =>
                {
                    if (!(node.Checked = string.IsNullOrEmpty(msg)))
                    {
                        MSG($"[{DataKind.FORWARD}]   {msg}");
                    }
                    else
                    {
                        cnt++;
                    }
                }).Invoke(node.TreeView);
            }
            MSG($"[{DataKind.FORWARD}]   筆數:{cnt} ");
        }
        private void FxRate(string jsonStr)
        {
            MSG($"[{DataKind.FX}]");
            JArray array;
            int cnt = 0;
            TreeNode node = Node.Nodes[DataKind.FX.ToString()];

            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "null")
            {
                MSG($"[{DataKind.FX}]   沒有匯率");
                return;
            }
            else
            {
                array = JArray.Parse(jsonStr);
                if (array.Count <= 0)
                {
                    MSG($"[{DataKind.FX}]   有JSON但筆數為0 ");
                    return;
                }
            }

            foreach (var JObj in array)
            {
                JYT_Currency json = JsonConvert.DeserializeObject<JYT_Currency>(JObj.ToString());
                PCF_FxRate fx = new PCF_FxRate(ETFCode, m_DataDate) { Base = "USD", Quoted = json.crncy == "NTD" ? "TWD" : json.crncy, Value = json.endRate.ToDouble() };
                string msg = fx.Insert();

                new Action(() =>
                {
                    if (!(node.Checked = string.IsNullOrEmpty(msg)))
                    {
                        MSG($"[{DataKind.FX}]   {msg}");
                    }
                    else
                    {
                        cnt++;
                    }
                }).Invoke(node.TreeView);
                if (!m_FutFXs.ContainsKey(fx.Quoted)) { m_FutFXs.Add(fx.Quoted, fx); }
            }
            MSG($"[{DataKind.FX}]   筆數:{cnt}");
        }
        /// <summary>
        /// 匯率抓完才能處理
        /// </summary>
        /// <param name="jsonStr"></param>
        private void Cash(string jsonStr)
        {
            MSG($"[{DataKind.CASH}]");
            JArray array;
            int cnt = 0;
            TreeNode node = Node.Nodes[DataKind.CASH.ToString()];

            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "null")
            {
                MSG($"[{DataKind.CASH}]   沒有現金");
                return;
            }
            else
            {
                array = JArray.Parse(jsonStr);
                if (array.Count <= 0)
                {
                    MSG($"[{DataKind.CASH}]   有JSON但筆數為0 ");
                    return;
                }
            }
            PCF_Cash.Delete(m_DataDate, ETFCode);
            foreach (var JObj in array)
            {
                JYT_Cash json = JsonConvert.DeserializeObject<JYT_Cash>(JObj.ToString());
                if (json.ename != DataKind.CASH.ToString()) { continue; }
                PCF_Cash cash = new PCF_Cash(ETFCode, m_DataDate) { Currency = json.crncy == "NTD" ? "TWD" : json.crncy, Amount = json.amt.ToDouble() };
                cash.Rate = Rate(m_FutFXs, cash.Currency, "TWD");
                string msg = cash.Insert();

                new Action(() =>
                {
                    if (!(node.Checked = string.IsNullOrEmpty(msg)))
                    {
                        MSG($"[{DataKind.CASH}]   {msg}");
                    }
                    else
                    {
                        cnt++;
                    }
                }).Invoke(node.TreeView);
            }
            MSG($"[{DataKind.CASH}]   筆數:{cnt} ");
        }
        /// <summary>
        /// 匯率抓完才能處理
        /// </summary>
        /// <param name="jsonStr"></param>
        private void Margin(string jsonStr)
        {
            MSG($"[{DataKind.MARGIN}]");
            JArray array;
            int cnt = 0;
            TreeNode node = Node.Nodes[DataKind.MARGIN.ToString()];

            if (string.IsNullOrEmpty(jsonStr) || jsonStr == "null")
            {
                MSG($"[{DataKind.MARGIN}]   沒有保證金");
                return;
            }
            else
            {
                array = JArray.Parse(jsonStr);
                if (array.Count <= 0)
                {
                    MSG($"[{DataKind.MARGIN}]   有JSON但筆數為0 ");
                    return;
                }
            }
            PCF_Margin.Delete(m_DataDate, ETFCode);
            foreach (var JObj in array)
            {
                JYT_Margin json = JsonConvert.DeserializeObject<JYT_Margin>(JObj.ToString());

                PCF_Margin margin = new PCF_Margin(ETFCode, m_DataDate) { Currency = json.crncy == "NTD" ? "TWD" : json.crncy, Amount = json.amt.ToDouble() };
                margin.Rate = Rate(m_FutFXs, margin.Currency, "TWD");
                string msg = margin.Insert();

                new Action(() =>
                {
                    if (!(node.Checked = string.IsNullOrEmpty(msg)))
                    {
                        MSG($"[{DataKind.MARGIN}]   {msg}");
                    }
                    else
                    {
                        cnt++;
                    }
                }).Invoke(node.TreeView);
            }
            MSG($"[{DataKind.MARGIN}]   筆數:{cnt} ");
        }
        #endregion

        #region Private
        private void _Stage2Complete()
        {
            if ((m_Stage2 & DataKind.COMPOSITION) != DataKind.COMPOSITION)
            {
                return;
            }
            if ((m_Stage2 & DataKind.FUNDSIZE) != DataKind.FUNDSIZE)
            {
                return;
            }
            if ((m_Stage2 & DataKind.SETTLECURNCY) != DataKind.SETTLECURNCY)
            {
                return;
            }
            //呼叫順序3
            //STK, FUT, FUND, Forward, FX
            foreach (var url in URLs)
            {
                //if (url.Key == DataKind.COMPOSITION) { continue; }
                if (url.Key == DataKind.COMPOSITION ||
                url.Key == DataKind.CASH ||
                url.Key == DataKind.MARGIN ||
                url.Key == DataKind.FUNDSIZE ||
                url.Key == DataKind.SETTLECURNCY) { continue; }
                using (WebClient w = new WebClient())
                {
                    w.Encoding = Encoding.UTF8;
                    w.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompleted);
                    w.DownloadStringAsync(url.Value, url.Key);
                }
            }
            m_Stage2 = DataKind.HEAD;
        }
        protected override void _DataKind()
        {
            if (Node.Nodes != null) { Node.Nodes.Clear(); }
            Node.Nodes.Add(new TreeNode(DataKind.HEAD.ToString()) { Name = DataKind.HEAD.ToString(), Tag = this });

            foreach (var kind in m_URLs.Keys)
            {
                string k = kind.ToString();
                if (!Node.Nodes.ContainsKey(k)) Node.Nodes.Add(new TreeNode(k) { Name = k, Tag = this });
            }
        }
        #endregion
    }
}