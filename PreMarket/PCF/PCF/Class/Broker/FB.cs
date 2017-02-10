//#define NOTHREADING

using HtmlAgilityPack;
using log4net;
using Newtonsoft.Json;
using PCF.Class.JSON;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using Util.Extension;

namespace PCF.Class
{
    public class FB : ETF
    {
        enum DataType
        {
            Fut,
            Fund
        }
        //搜尋字串
        private const string STKT = "股票代碼";
        private const string FUTT = "期貨代碼";
        private const string FUNDT = "基金代碼";

        /// <summary>
        /// Header
        /// </summary>
        private const string URLHeader = "http://websys.fsit.com.tw/FubonETF/Trade/";
        /// <summary>
        /// PCF Address
        /// </summary>
        private static string PCFURL = URLHeader + "Pcf.aspx?stock={0}&date=" + m_Today.ToString(DATE);
        /// <summary>
        /// Fund Asset JSON Address
        /// </summary>
        private static string HoldingURL = URLHeader + "FundInvestJson.aspx?stkId={0}&checkSum=37815";

        #region Variable
        /// <summary>
        /// Address List
        /// </summary>
        private Dictionary<DataKind, Uri> m_URLs = new Dictionary<DataKind, Uri>();
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

                m_Url = new Uri(string.Format(HoldingURL, m_Address));
                MSG($"[{DataKind.HEAD} (FundAsset JSON)]  {m_Url.AbsoluteUri}");

                m_URLs.Add(DataKind.COMPOSITION, new Uri(string.Format(PCFURL, m_Address)));
                MSG($"[{DataKind.COMPOSITION}]  {m_URLs[DataKind.COMPOSITION].AbsoluteUri}");

                _DataKind();
                MSG(string.Empty);
            }
        }
        /// <summary>
        /// JSON Result
        /// </summary>
        private JFB JSON { get; set; }
        #endregion

        public FB()
        {
            m_Log = LogManager.GetLogger(typeof(FB));
        }

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
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(evt.Result);
                    DataKind kind = evt.UserState.ToEnum<DataKind>();
                    //先抓FundAsset再抓PCF
                    if (kind == DataKind.HEAD)
                    {

                        //建JSON
                        JSON = JsonConvert.DeserializeObject<JFB>(evt.Result);
                        using (WebClient w = new WebClient())
                        {
                            w.Encoding = Encoding.UTF8;
                            w.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompleted);
                            w.DownloadStringAsync(URLs[DataKind.COMPOSITION], DataKind.COMPOSITION);
                        }
                    }
                    else if (kind == DataKind.COMPOSITION)
                    {
                        //處理PCF
                        Head(doc);
                        //if (Market == Market.TW)
                        //{
                        Stk(doc);
                        //}
                        //else
                        //{
                        //    ForeignStk(doc);
                        //}
                        Fut(doc);
                        Fund(doc);
                        FxRate();
                        Forward();
                        Cash();
                        Margin();
                        CHANNEL();
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

        #region Parse HTML
        private string _TableTitle(HtmlDocument doc, string xPath)
        {
            if (doc.DocumentNode.SelectSingleNode(xPath) == null) { return string.Empty; }
            var node = doc.DocumentNode.SelectSingleNode(xPath).Descendants(TR)
                .Where(tr => tr.Elements(TH).Count() > 1)
                .Select(tr => tr.Elements(TH)).FirstOrDefault();
            if (node == null || node.FirstOrDefault() == null) { return string.Empty; }
            return node.First().InnerText.Trim();
        }
        private List<List<string>> _Basket(HtmlDocument doc, string xPath, DataType dt)
        {
            if (doc.DocumentNode.SelectSingleNode(xPath) == null) { return null; }
            var nodes = doc.DocumentNode.SelectSingleNode(xPath).Descendants(TR)
                .Where(tr => tr.Elements(TH).Count() == 3 || tr.Elements(TD).Count() == 3).ToList();
            List<List<string>> re = new List<List<string>>();
            string title = string.Empty;
            foreach (var node in nodes)
            {
                HtmlNode th = node.Element(TH);
                IEnumerable<HtmlNode> td = node.Elements(TD);
                if (th != null)
                {
                    title = th.InnerText.Trim();
                    continue;
                }
                if (td != null)
                {
                    if ((dt == DataType.Fut && title == FUTT) || (dt == DataType.Fund && title == FUNDT))
                    {
                        List<string> data = new List<string>();
                        foreach (var item in td)
                        {
                            data.Add(item.InnerText.Trim());
                        }
                        re.Add(data);
                    }
                }
            }
            return re;
        }
        private List<List<string>> _Basket(HtmlDocument doc, string xPath)
        {
            return doc.DocumentNode.SelectSingleNode(xPath).Descendants(TR)
                    .Where(tr => tr.Elements(TD).Count() > 1)
                    .Select(tr => tr.Elements(TD).Select(td => td.InnerText.Trim()).ToList())
                    .ToList();
        }
        #endregion

        #region Process
        private void Head(HtmlDocument doc)
        {
            MSG($"[{DataKind.HEAD}]");
            if (string.IsNullOrEmpty(SettingItem))
            {
                MSG($"[{DataKind.HEAD} Error]   沒有設定讀取方式");
                return;
            }
            string[] xPath = INI(nameof(FB), SettingItem).Split(new char[] { '|' });
            ETFDaily daily = new ETFDaily(ETFCode, m_Today);
            var properties = typeof(ETFDaily).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo p = properties.ElementAt(i);
                if (i > xPath.Length - 1) { break; }
                string value = NODE(doc, xPath[i]);
                if (string.IsNullOrEmpty(value))
                {
                    MSG($"[{DataKind.HEAD}]   沒有取得欄位值:{p.Name}");
                    continue;
                }
                switch (p.PropertyType.ToString())
                {
                    case "System.Int32":
                        p.SetValue(daily, int.Parse(value, NumberStyles.Currency), null);
                        break;
                    case "System.Double":
                        p.SetValue(daily, double.Parse(value, NumberStyles.Currency), null);
                        break;
                    default:
                        p.SetValue(daily, Convert.ChangeType(value, p.PropertyType), null);
                        break;
                }
            }
            daily.PublicShares = JSON.Fund.Missue.ToDouble();
            string msg = daily.Insert();
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.HEAD.ToString()];
            new Action(() =>
            {
                if (!(node.Checked = string.IsNullOrEmpty(msg))) { MSG($"[{DataKind.HEAD}]   {msg}"); }
            }).Invoke(node.TreeView);
            m_DataDate = daily.DataDate;
        }
        private void Stk(HtmlDocument doc)
        {
            MSG($"[{DataKind.STK}]");
            int cnt = 0;
            string xPath = INI(nameof(FB), CONTENT);
            string first = _TableTitle(doc, xPath);
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.STK.ToString()];

            //有的商品網頁沒有PCF資料 
            if (string.IsNullOrEmpty(first) && first != STKT)
            {
                //網頁沒有資料的用FundAsset另外處理
                if (JSON == null || JSON.Stk == null)
                {
                    MSG($"[{DataKind.STK}]   資料筆數為0 ");
                    return;
                }

                foreach (var json in JSON.Stk)
                {
                    PCF_STK stk = new PCF_STK(ETFCode, m_Today, m_DataDate)
                    {
                        PID = json.Id,
                        Name = json.Name,
                        Exch = ExchParse(json.Id),
                        TotalUnits = json.Units.ToDouble(),
                        Weights = (decimal)json.RateAuri.ToDouble(),
                        YP = json.Price.ToDouble()
                    };
                    string msg = stk.Insert();

                    new Action(() =>
                    {
                        if (!(node.Checked = string.IsNullOrEmpty(msg)))
                        {
                            MSG($"[{DataKind.STK} ]   {msg}");
                        }
                        else
                        {
                            cnt++;
                        }
                    }).Invoke(node.TreeView);
                }
            }
            else
            {
                //網頁有資料理網頁
                List<List<string>> Baskets = _Basket(doc, xPath);
                foreach (var item in Baskets)
                {
                    PCF_STK stk = new PCF_STK(ETFCode, m_Today, m_DataDate);
                    if (item.Count < 3) { continue; }
                    stk.PID = item[0];
                    stk.Name = item[1];
                    stk.PCFUnits = int.Parse(item[2], NumberStyles.AllowThousands);
                    if (item.Count > 3)
                    {
                        stk.ReplaceWithCash = item[3] == "是" ? "Y" : "N";
                    }
                    else
                    {

                    }
                    stk.Exch = ExchParse(stk.PID);
                    string msg = string.Empty;
                    //有Fund Asset資料, 塞進去
                    if (JSON != null && JSON.Stk != null)
                    {
                        JFB_Stk json = JSON.Stk.FirstOrDefault(e => e.Id == stk.PID);
                        if (json != null)
                        {
                            stk.TotalUnits = json.Units.ToDouble();
                            stk.Weights = (decimal)json.RateAuri.ToDouble();
                            stk.YP = json.Price.ToDouble();
                        }
                    }
                    msg = stk.Insert();
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
                }
            }
            MSG($"[{DataKind.STK}]   筆數:{cnt}");
        }
        //private void ForeignStk(HtmlDocument doc)
        //{
        //    MSG($"[{DataKind.STK} Foreign]");
        //    int cnt = 0;
        //    string xPath = INI(nameof(FB), CONTENT);
        //    string first = _TableTitle(doc, xPath);
        //    System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.STK.ToString()];

        //    //有的商品網頁沒有PCF資料 
        //    if (string.IsNullOrEmpty(first) && first != STKT)
        //    {
        //        //網頁沒有資料的用FundAsset另外處理
        //        if (JSON.Stk == null) { MSG($"[{DataKind.STK} Foreign]   資料筆數為0 "); }


        //        foreach (var json in JSON.Stk)
        //        {
        //            PCF_STK stk = new PCF_STK(ETFCode, m_Today, m_DataDate)
        //            {
        //                PID = json.Id,
        //                Name = json.Name,
        //                Exch = ExchParse(json.Id),
        //                TotalUnits = json.Units.ToDouble(),
        //                Weights = (decimal)json.RateAuri.ToDouble(),
        //                YP = json.Price.ToDouble()
        //            };
        //            string msg = stk.Insert();

        //            new Action(() =>
        //            {
        //                if (!(node.Checked = string.IsNullOrEmpty(msg)))
        //                {
        //                    MSG($"[{DataKind.STK} Foreign]   {msg}");
        //                }
        //                else
        //                {
        //                    cnt++;
        //                }
        //            }).Invoke(node.TreeView);
        //        }
        //    }
        //    else
        //    {
        //        //網頁有資料理網頁
        //        List<List<string>> Baskets = _Basket(doc, xPath);
        //        foreach (var item in Baskets)
        //        {
        //            PCF_STK stk = new PCF_STK(ETFCode, m_Today, m_DataDate);
        //            if (item.Count < 3) { continue; }
        //            stk.PID = item[0];
        //            stk.Name = item[1];
        //            stk.PCFUnits = int.Parse(item[2], NumberStyles.AllowThousands);
        //            stk.Exch = ExchParse(stk.PID);
        //            string msg = string.Empty;                    
        //            //有Fund Asset資料, 塞進去
        //            if (JSON != null && JSON.Stk!= null)
        //            {
        //                JFB_Stk json = JSON.Stk.FirstOrDefault(e => e.Id == stk.PID);
        //                if (json!= null)
        //                {
        //                    stk.TotalUnits = json.Units.ToDouble();
        //                    stk.Weights = (decimal)json.RateAuri.ToDouble();
        //                    stk.YP = json.Price.ToDouble();
        //                }                        
        //            }

        //            msg = stk.Insert();                    
        //            new Action(() =>
        //            {
        //                if (!(node.Checked = string.IsNullOrEmpty(msg)))
        //                {
        //                    MSG($"[{DataKind.STK} Foreign]   " + msg);
        //                }
        //                else
        //                {
        //                    cnt++;
        //                }
        //            }).Invoke(node.TreeView);
        //        }
        //    }
        //    MSG($"[{DataKind.STK}]   筆數:{cnt}");
        //}
        private void Fut(HtmlDocument doc)
        {
            MSG($"[{DataKind.FUT}]");
            if (JSON == null || JSON.Drfts == null)
            {
                MSG($"[{DataKind.FUT}] JSON null ");
                return;
            }
            string xPath = INI(nameof(FB), CONTENT);
            int cnt = 0;
            string msg = string.Empty;
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.FUT.ToString()];

            List<List<string>> Baskets = _Basket(doc, xPath, DataType.Fut);
            if (Baskets == null || Baskets.Count == 0)
            {
                //網頁沒資料 JSON有
                foreach (var item in JSON.Drfts)
                {
                    msg = _CreateFut(item.Id, item.Name, 0, item.Units.ToDouble(), (decimal)item.RateAuri.ToDouble(), item.Price.ToDouble());

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
            }
            else
            {
                //網頁跟JSON都有資料
                foreach (var item in Baskets)
                {
                    JFB_Drfts json = JSON.Drfts.FirstOrDefault(e => e.Id == item[0]);
                    if (json == null) { continue; }
                    msg = _CreateFut(item[0], item[1], item[2].ToDouble(), json.Units.ToDouble(), (decimal)json.RateAuri.ToDouble(), json.Price.ToDouble());
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
            }
            MSG($"[{DataKind.FUT}]  筆數:{cnt}");
        }
        private string _CreateFut(string pid, string name, double pcfunit, double totalunit, decimal weight, double price)
        {
            PCF_FUT fut = new PCF_FUT(ETFCode, m_Today, m_DataDate);
            fut.PID = pid;
            string temp = fut.PID;
            if (temp.Substring(temp.Length - 1, 1) == "F")
            {
                temp = temp.Substring(0, temp.Length - 1);
            }
            fut.Head = fut.PID.Substring(0, temp.Length - 2);
            fut.YM = (2010 + int.Parse(temp.Substring(temp.Length - 1, 1))) + ForeignFUTMonth(temp.Substring(temp.Length - 2, 1));
            fut.Y = 2010 + int.Parse(temp.Substring(temp.Length - 1, 1));
            fut.M = int.Parse(ForeignFUTMonth(temp.Substring(temp.Length - 2, 1)));
            fut.Name = name;
            fut.PCFUnits = pcfunit;
            fut.TotalUnits = totalunit;
            fut.Weights = weight;
            fut.YP = price;
            return fut.Insert();
        }
        private void Fund(HtmlDocument doc)
        {
            MSG($"[{DataKind.FUND}]");
            if (JSON == null || JSON.Fd == null)
            {
                MSG($"[{DataKind.FUND}] JSON null ");
                return;
            }
            string xPath = INI(nameof(FB), CONTENT);
            int cnt = 0;
            string msg = string.Empty;
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.FUND.ToString()];

            List<List<string>> Baskets = _Basket(doc, xPath, DataType.Fund);
            if (Baskets == null || Baskets.Count <= 0)
            {
                //網頁沒資料 JSON有
                foreach (var json in JSON.Fd)
                {
                    PCF_FUND fund = new PCF_FUND(ETFCode, m_Today, m_DataDate)
                    {
                        PID = json.Id,
                        Name = json.Name,
                        Exch = ExchParse(json.Id),
                        TotalUnits = json.Units.ToDouble(),
                        Weights = (decimal)json.RateAuri.ToDouble(),
                        YP = json.Price.ToDouble()
                    };
                    msg = fund.Insert();

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
            }
            else
            {
                //網頁跟JSON都有資料
                foreach (var item in Baskets)
                {
                    PCF_FUND fund = new PCF_FUND(ETFCode, m_Today, m_DataDate);
                    fund.PID = item[0];
                    fund.Name = item[1];
                    fund.PCFUnits = int.Parse(item[2], NumberStyles.AllowThousands);
                    fund.Exch = ExchParse(fund.PID);

                    JFB_Fd fd = JSON.Fd.FirstOrDefault(e => e.Id == fund.PID);
                    fund.TotalUnits = fd.Units.ToDouble();
                    fund.Weights = (decimal)fd.RateAuri.ToDouble();
                    fund.YP = fd.Price.ToDouble();
                    msg = fund.Insert();
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
            }
            MSG($"[{DataKind.FUND}]  筆數:{cnt}");
        }
        private void FxRate()
        {
            MSG($"[{DataKind.FX}]");
            if (JSON == null || JSON.Currency == null)
            {
                MSG($"[{DataKind.FX}] JSON null");
                return;
            }
            string rateValue = string.Empty;
            string baseCurrency = "USD";
            int cnt = 0;
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.FX.ToString()];

            foreach (var fx in JSON.Currency)
            {
                string quotedCurrency = fx.Id;
                PCF_FxRate f = new PCF_FxRate(ETFCode, m_DataDate) { Base = baseCurrency, Quoted = quotedCurrency, Value = fx.Exchange.ToDouble() };

                string msg = f.Insert();

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
                rateValue = !string.IsNullOrEmpty(rateValue) ? string.Join("|", rateValue, f.ToString()) : f.ToString();
                m_FutFXs.Add(f.Quoted, f);
            }
            MSG($"[{DataKind.FX}] 筆數:{cnt}");
        }
        private void Forward()
        {
            MSG($"[{DataKind.FORWARD}]");
            if (JSON == null || JSON.Drfwd == null)
            {
                MSG($"[{DataKind.FORWARD}] JSON null");
                return;
            }
            PCF_Forward.Delete(m_DataDate, ETFCode);
            int cnt = 0;
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.FORWARD.ToString()];

            foreach (var forward in JSON.Drfwd)
            {
                PCF_Forward f = new PCF_Forward(ETFCode, m_DataDate);

                switch (ETFCode)
                {
                    case "00645":
                    case "00640L":
                    case "00641R":
                        f.Currency = forward.CurrencyIdChange == "TWD" ? "JPY" : forward.CurrencyIdChange;
                        break;
                    case "00652":
                    case "00653L":
                    case "00654R":
                        f.Currency = forward.CurrencyIdChange == "TWD" ? "USD" : forward.CurrencyIdChange;
                        break;
                    default:
                        break;
                }
                f.Rate = Rate(m_FutFXs, f.Currency, "TWD");
                f.Amount = forward.TotalAmount.ToDouble() / f.Rate;
                if (f.Amount <= 0) { return; }
                string msg = f.Insert();

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
            MSG($"[{DataKind.FORWARD}]  筆數:{cnt}");
        }
        private void Cash()
        {
            MSG($"[{DataKind.CASH}]");
            if (JSON == null || JSON.Cash == null)
            {
                MSG($"[{DataKind.CASH}] JSON null");
                return;
            }
            PCF_Cash.Delete(m_DataDate, ETFCode);
            int cnt = 0;
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.CASH.ToString()];

            foreach (var cash in JSON.Cash)
            {
                PCF_Cash c = new PCF_Cash(ETFCode, m_DataDate) { Currency = cash.Id, };
                c.Rate = Rate(m_FutFXs, c.Currency, "TWD");
                c.Amount = cash.AmountAuri.ToDouble() / c.Rate;
                string msg = c.Insert();

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
            MSG($"[{DataKind.CASH}]  筆數:{cnt}");
        }
        private void Margin()
        {
            MSG($"[{DataKind.MARGIN}]");
            if (JSON == null || JSON.Drbroker == null)
            {
                MSG($"[{DataKind.MARGIN}] JSON null");
                return;
            }
            PCF_Margin.Delete(m_DataDate, ETFCode);
            int cnt = 0;
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.MARGIN.ToString()];

            foreach (var margin in JSON.Drbroker)
            {
                PCF_Margin m = new PCF_Margin(ETFCode, m_DataDate) { Currency = margin.Id };
                m.Rate = Rate(m_FutFXs, m.Currency, "TWD");
                m.Amount = margin.AmountAuri.ToDouble() / m.Rate;
                string msg = m.Insert();

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
            MSG($"[{DataKind.MARGIN}]  筆數:{cnt}");
        }
        #endregion

        #region Private
        protected override void _DataKind()
        {
            if (Node.Nodes != null) { Node.Nodes.Clear(); }
            Node.Nodes.Add(new System.Windows.Forms.TreeNode(DataKind.HEAD.ToString()) { Name = DataKind.HEAD.ToString(), Tag = this });
            var kinds = Enum.GetValues(typeof(DataKind));
            foreach (DataKind kind in kinds)
            {
                switch (kind)
                {
                    case DataKind.STK:
                    case DataKind.FUT:
                    case DataKind.FUND:
                    case DataKind.FX:
                    case DataKind.CASH:
                    case DataKind.MARGIN:
                    case DataKind.FORWARD:
                        Node.Nodes.Add(new System.Windows.Forms.TreeNode(kind.ToString()) { Name = kind.ToString(), Tag = this });
                        break;
                    case DataKind.HEAD:
                    case DataKind.COMPOSITION:
                    default:
                        continue;
                }
            }
        }
        #endregion
    }
}