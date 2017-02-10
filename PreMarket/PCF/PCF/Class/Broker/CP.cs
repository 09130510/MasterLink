//#define NOTHREADING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Reflection;
using HtmlAgilityPack;
using System.Globalization;
using System.Threading;
using Util.Extension;
using log4net;

namespace PCF.Class
{
    public class CP : ETF
    {
        /// <summary>
        /// Header
        /// </summary>
        private const string URLHeader = "https://www.capitalfund.com.tw/ETF/";
        /// <summary>
        /// PCF Address
        /// </summary>
        private static string PCFURL = URLHeader + "pcf.aspx?fid={0}";

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

                m_Url = new Uri(string.Format(PCFURL, m_Address));
                MSG($"[{DataKind.HEAD}]  {m_Url.AbsoluteUri}");

                _DataKind();
                MSG(string.Empty);
            }
        }
        #endregion

        public CP()
        {
            m_Log = LogManager.GetLogger(typeof(CP));
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
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(evt.Result);
                DataKind kind = evt.UserState.ToEnum<DataKind>();
                try
                {
                    Head(doc);
                    Stk(doc);
                    Fut(doc);
                    FxRate(doc);
                    CashMargin(doc);
                    CHANNEL();
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
        private void Head(HtmlDocument doc)
        {
            MSG($"[{DataKind.HEAD}]");
            string[] xPath = INI(nameof(CP), SettingItem).Split('|');
            ETFDaily daily = new ETFDaily(ETFCode, m_Today);
            var properties = typeof(ETFDaily).GetProperties();
            for (int i = 0; i < properties.Count(); i++)
            {
                PropertyInfo p = properties.ElementAt(i);
                if (i > xPath.Length - 1) { break; }
                string value;
                if (xPath[i].Contains(';'))
                {
                    var path = xPath[i].Split(';');
                    double v = 0;
                    foreach (var item in path)
                    {
                        v += double.Parse(NODE(doc, item), NumberStyles.Currency);
                    }
                    value = v.ToString();
                }
                else
                {
                    value = NODE(doc, xPath[i]);
                    if (string.IsNullOrEmpty(value) || value == "Label")
                    {
                        if (p.Name == "ETFCode") { value = ETFCode; }
                        else
                        {
                            MSG("[Error]   沒有取得欄位值:" + p.Name);
                            continue;
                        }
                    }
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
            string msg = daily.Insert();
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.HEAD.ToString()];
            new Action(() =>
            {
                if (!(node.Checked = string.IsNullOrEmpty(msg)))
                {
                    MSG($"[{DataKind.HEAD}]   {msg}");
                }
            }).Invoke(node.TreeView);
            m_DataDate = daily.DataDate;
        }
        private void Stk(HtmlDocument doc)
        {
            MSG($"[{DataKind.STK}]");
            int cnt = 0;
            string xPath = INI(nameof(CP), DataKind.STK.ToString());
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.STK.ToString()];
            if (doc.DocumentNode.SelectSingleNode(xPath) == null)
            {
                MSG($"[{DataKind.STK}]   找不到網頁資料");
                return;
            }
            var Baskets = doc.DocumentNode.SelectSingleNode(xPath).Descendants(TR)
                    .Where(tr => tr.Elements(TD).Count() > 1)
                    .Select(tr => tr.Elements(TD).Select(td => td.InnerText.Trim()).ToList())
                    .ToList();
            if (Baskets == null || Baskets.Count <= 0)
            {
                MSG($"[{DataKind.STK}]  資料筆數為0 ");
                return;
            }
            foreach (var item in Baskets)
            {
                PCF_STK stk = new PCF_STK(ETFCode, m_Today, m_DataDate);
                if (item.Count < 3) { continue; }
                stk.PID = item[0];
                stk.Name = item[1];
                stk.TotalUnits = int.Parse(item[3], NumberStyles.AllowThousands);
                stk.Weights = decimal.Parse(item[2]) / 100M;
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
                        cnt++;
                    }
                }).Invoke(node.TreeView);
            }
            MSG($"[{DataKind.STK}]  筆數:{cnt} ");
        }
        private void Fut(HtmlDocument doc)
        {
            MSG($"[{DataKind.FUT}]");
            int cnt = 0;
            string xPath = INI(nameof(CP), DataKind.FUT.ToString());
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.FUT.ToString()];
            if (doc.DocumentNode.SelectSingleNode(xPath) == null)
            {
                MSG($"[{DataKind.FUT}]   找不到網頁資料");
                return;
            }
            var Baskets = doc.DocumentNode.SelectSingleNode(xPath).Descendants(TR)
                    .Where(tr => tr.Elements(TD).Count() > 1)
                    .Select(tr => tr.Elements(TD).Select(td => td.InnerText.Trim()).ToList())
                    .ToList();
            if (Baskets == null || Baskets.Count <= 0)
            {
                MSG($"[{DataKind.FUT}]  資料筆數為0 ");
                return;
            }
            foreach (var item in Baskets)
            {
                PCF_FUT fut = new PCF_FUT(ETFCode, m_Today, m_DataDate)
                {
                    YM = item[3],
                    Y = item[3].Substring(0, 4).ToInt(),
                    M = item[3].Substring(4, 2).ToInt(),
                    Head = item[0].Contains("富時中國A50指數期貨") ? "CN" : "",
                    Name = item[0],
                    TotalUnits = item[2].ToInt(),
                    Weights = item[1].ToDecimal() / 100M
                };
                fut.PID = fut.Head + fut.YM;
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
            MSG($"[{DataKind.FUT}]   筆數:{cnt}");
        }
        private void CashMargin(HtmlDocument doc)
        {
            MSG($"[{DataKind.CASH},{DataKind.MARGIN}]");
            int cntCash = 0, cntMargin = 0;
            string xPath = INI(nameof(CP), DataKind.CASH.ToString());
            System.Windows.Forms.TreeNode cashnode = Node.Nodes[DataKind.CASH.ToString()];
            System.Windows.Forms.TreeNode marginnode = Node.Nodes[DataKind.MARGIN.ToString()];
            PCF_Cash.Delete(m_DataDate, ETFCode);
            PCF_Margin.Delete(m_DataDate, ETFCode);

            if (doc.DocumentNode.SelectSingleNode(xPath) == null)
            {
                MSG($"[{DataKind.CASH},{DataKind.MARGIN}]   找不到網頁資料");
                return;
            }
            var Baskets = doc.DocumentNode.SelectSingleNode(xPath).Descendants(TR)
                    .Where(tr => tr.Elements(TD).Count() > 1)
                    .Select(tr => tr.Elements(TD).Select(td => td.InnerText.Trim()).ToList())
                    .ToList();
            if (Baskets == null || Baskets.Count <= 0)
            {
                MSG($"[{DataKind.CASH},{DataKind.MARGIN}]  資料筆數為0 ");
                return;
            }

            foreach (var item in Baskets)
            {
                var values = item[1].Split(' ');
                switch (item[0])
                {
                    case "保證金":
                        PCF_Margin margin = new PCF_Margin(ETFCode, m_DataDate) { Currency = values[0], Amount = values[1].ToDouble() };
                        margin.Rate = Rate(m_FutFXs, margin.Currency, "TWD");
                        string msg = margin.Insert();
                        new Action(() =>
                        {
                            if (!(marginnode.Checked = string.IsNullOrEmpty(msg)))
                            {
                                MSG($"[{DataKind.MARGIN}]   {msg}");
                            }
                            else
                            {
                                cntMargin++;
                            }
                        }).Invoke(marginnode.TreeView);
                        break;
                    case "現金":
                        PCF_Cash cash = new PCF_Cash(ETFCode, m_DataDate) { Currency = values[0], Amount = values[1].ToDouble() };
                        cash.Rate = Rate(m_FutFXs, cash.Currency, "TWD");
                        string cmsg = cash.Insert();
                        new Action(() =>
                        {
                            if (!(cashnode.Checked = string.IsNullOrEmpty(cmsg)))
                            {
                                MSG($"[{DataKind.CASH}]   {cmsg}");
                            }
                            else
                            {
                                cntCash++;
                            }
                        }).Invoke(cashnode.TreeView);
                        break;
                }
            }
            MSG($"[{DataKind.CASH},{DataKind.MARGIN}]  Cash:{cntCash}   Margin:{cntMargin}");
        }

        private void FxRate(HtmlDocument doc)
        {
            MSG($"[{DataKind.FX}]");
            int cnt = 0;
            string rateValue = string.Empty;
            string value = NODE(doc, INI(nameof(CP), DataKind.FX.ToString()));
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.FX.ToString()];
            if (string.IsNullOrEmpty(value))
            {
                MSG($"[{DataKind.FX}]   找不到網頁資料");
                return;
            }
            string[] currencies = value.Split(',');
            foreach (var currency in currencies)
            {
                string[] curritems = currency.Split('=');
                if (curritems[0] == "--") { continue; }
                string[] detail = curritems[0].Split(' ');
                if (detail.Length <= 1) { continue; }
                string baseCurrency = detail[1].Trim();
                string[] quoted = curritems[1].Trim().Split(' ');
                string quotedCurrency = quoted[1];
                double v = double.Parse(quoted[0]);
                PCF_FxRate fx = new PCF_FxRate(ETFCode, m_DataDate) { Base = baseCurrency, Quoted = quotedCurrency, Value = v };

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
                rateValue = !string.IsNullOrEmpty(rateValue) ? string.Join("|", rateValue, fx.ToString()) : fx.ToString();
                //給現金保證金用的
                m_FutFXs.Add(fx.Quoted, fx);
            }
            MSG($"[{DataKind.FX}]   筆數:{cnt}");
        }
        #endregion

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
                    case DataKind.FX:
                    case DataKind.FUT:
                    case DataKind.CASH:
                    case DataKind.MARGIN:
                        Node.Nodes.Add(new System.Windows.Forms.TreeNode(kind.ToString()) { Name = kind.ToString(), Tag = this });
                        break;
                    case DataKind.HEAD:
                    case DataKind.COMPOSITION:
                    case DataKind.FUND:
                    default:
                        continue;
                }
            }
        }
    }
}