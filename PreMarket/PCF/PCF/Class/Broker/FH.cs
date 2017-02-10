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
    public class FH : ETF
    {
        enum DataType
        {
            Stk,
            Fut,
            Cash
        }
        //搜尋字串
        private const string STKT = "證券代碼";
        private const string FUTT = "期貨代碼";
        private const string CASHT = "名稱";
        private const string CASHNAME = "現金(";
        private const string MARGINNAME = "期貨保證金(";

        private static string ROCYearString = "YYY";
        private static string MonthDateString = "MMdd";
        /// <summary>
        /// PCF Address
        /// </summary>
        private const string URLHeader = "http://www.fhtrust.com.tw/funds/fund_intro_ETF_pcf.asp?ShowDay=YYYMMdd&QueryFund=";
        /// <summary>
        /// FundAsset Address
        /// </summary>
        private const string PCFURL = "http://www.fhtrust.com.tw/funds/fund_intro_ETF_FundAsset.asp?ShowDay=YYYMMdd&QueryFund=";

        #region Variable
        private Dictionary<DataKind, Uri> m_URLs = new Dictionary<DataKind, Uri>();
        #endregion

        #region Property
        public override Dictionary<DataKind, Uri> URLs { get { return m_URLs; } }
        public override string Address
        {
            get { return base.Address; }
            set
            {
                base.Address = value;
                m_URLs.Clear();
                m_FutFXs.Clear();

                DateTime t = m_Today;
                //DateTime t = DateTime.Now;
                m_Url = new Uri(URLHeader.Replace(ROCYearString, (t.Year - 1911).ToString()).Replace(MonthDateString, t.ToString(MonthDateString)) + base.Address);
                MSG($"[{DataKind.HEAD}]  {m_Url.AbsoluteUri}");

                m_URLs.Add(DataKind.COMPOSITION, new Uri(PCFURL.Replace(ROCYearString, (t.Year - 1911).ToString()).Replace(MonthDateString, t.ToString(MonthDateString)) + base.Address));
                MSG($"[{DataKind.COMPOSITION}]  {m_URLs[DataKind.COMPOSITION].AbsoluteUri}");

                _DataKind();
                MSG(string.Empty);
            }
        }
        #endregion

        public FH()
        {
            m_Log = LogManager.GetLogger(typeof(FH));
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
                    if (kind == DataKind.HEAD)
                    {
                        Head(doc);
                        using (WebClient w = new WebClient())
                        {
                            w.Encoding = Encoding.UTF8;
                            w.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompleted);
                            w.DownloadStringAsync(URLs[DataKind.COMPOSITION], DataKind.COMPOSITION);
                        }
                    }
                    else if (kind == DataKind.COMPOSITION)
                    {
                        Stk(doc);
                        Fut(doc);
                        FxRate(doc);
                        CashMargin(doc);
                        CHANNEL();
                    }
                }
                catch (TargetInvocationException ex)
                {
                    MSG("[Error]    目標網站掛點! " + ex.Message);
                }
#if !NOTHREADING
            }, e);
#endif
        }

        #region Parse HTML
        private string _TableTitle(HtmlDocument doc, string xPath)
        {
            return doc.DocumentNode.SelectSingleNode(xPath).Descendants(TR)
                .Where(tr => tr.Elements(TH).Count() >= 1)
                .Select(tr => tr.Elements(TH)).First().First().InnerText.Trim();
        }
        private List<List<string>> _Basket(HtmlDocument doc, string xPath, DataType dt)
        {
            if (doc.DocumentNode.SelectSingleNode(xPath) == null) { return null; }
            var nodes = doc.DocumentNode.SelectSingleNode(xPath).Descendants(TR)
                .Where(tr => tr.Elements(TD).Count() > 1).ToList();

            List<List<string>> re = new List<List<string>>();
            string title = string.Empty;
            foreach (var node in nodes)
            {
                HtmlNode th = node.Element(TH);
                IEnumerable<HtmlNode> td = node.Elements(TD);
                if (node.OuterHtml.Contains("style1"))
                {
                    title = node.Element(TD).InnerText.Trim();
                    continue;
                }
                else
                {
                    if ((dt == DataType.Fut && title == FUTT) ||
                        (dt == DataType.Stk && title == STKT) ||
                        (dt == DataType.Cash && title == CASHT))
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
        #endregion

        #region Process
        private void Head(HtmlDocument doc)
        {
            MSG($"[{DataKind.HEAD}]");
            string[] xPath = INI(nameof(FH), SettingItem).Split(new char[] { '|' });
            //ETFDaily daily = new ETFDaily(ETFCode, DateTime.Now);
            ETFDaily daily = new ETFDaily(ETFCode, m_Today);
            var properties = typeof(ETFDaily).GetProperties();
            for (int i = 0; i < properties.Count(); i++)
            {
                PropertyInfo p = properties.ElementAt(i);
                if (i > xPath.Length - 1) { break; }
                string value = NODE(doc, xPath[i]);
                if (string.IsNullOrEmpty(value) || value == "--")
                {
                    MSG($"[{ DataKind.HEAD}]   沒有取得欄位值:{p.Name}");
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
                    case "System.DateTime":
                        if (value.Contains("&nbsp"))
                        {
                            value = value.Split(new string[] { "&nbsp" }, StringSplitOptions.RemoveEmptyEntries)[0];

                        }
                        if (value.Length > 10)
                        {
                            MSG($"[{DataKind.HEAD}]   :{value}");
                            continue;
                        }
                        CultureInfo m_ciTaiwan = new CultureInfo("zh-TW");
                        m_ciTaiwan.DateTimeFormat.Calendar = m_ciTaiwan.OptionalCalendars[1];
                        p.SetValue(daily, DateTime.ParseExact(value, "yyyy/MM/dd", m_ciTaiwan), null);
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
                if (!(node.Checked = string.IsNullOrEmpty(msg))) { MSG($"[{DataKind.HEAD}]   {msg}"); }
            }).Invoke(node.TreeView);
            //if (Fail = !string.IsNullOrEmpty(msg)) { MSG("[Error]   " + msg); }
            m_DataDate = daily.DataDate;
        }
        private void Stk(HtmlDocument doc)
        {
            MSG($"[{DataKind.STK}]");
            string xPath = INI(nameof(FH), CONTENT);
            int cnt = 0;
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.STK.ToString()];

            List<List<string>> Baskets = _Basket(doc, xPath, DataType.Stk);
            if (Baskets == null || Baskets.Count <= 0)
            {
                MSG($"[{DataKind.STK}]   資料筆數為0 ");
                return;
            }
            foreach (var item in Baskets)
            {
                PCF_STK stk = new PCF_STK(ETFCode, m_Today, m_DataDate);

                stk.PID = item[0];
                stk.Name = item[1];
                stk.Exch = ExchParse(stk.PID);
                if (stk.Exch == Exch.HKEx) { stk.PID = stk.PID.PadLeft(7, '0'); }
                if (item.Count == 5)
                {
                    stk.TotalUnits = int.Parse(item[2], NumberStyles.AllowThousands);
                    stk.Weights = decimal.Parse(item[4].Replace("%", "")) / 100M;
                }
                else
                {
                    stk.PCFUnits = int.Parse(item[2], NumberStyles.AllowThousands);
                }
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
                //if (Fail = !string.IsNullOrEmpty(msg)) { MSG("[Error]   " + msg); }
            }
            MSG($"[{DataKind.STK}]   筆數:{cnt} ");
        }
        private void Fut(HtmlDocument doc)
        {
            MSG($"[{DataKind.FUT}]");
            string xPath = INI(nameof(FH), CONTENT);
            int cnt = 0;
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.FUT.ToString()];

            List<List<string>> Baskets = _Basket(doc, xPath, DataType.Fut);
            if (Baskets == null || Baskets.Count <= 0)
            {
                MSG($"[{DataKind.FUT}]   資料筆數為0 ");
                return;
            }
            foreach (var item in Baskets)
            {
                PCF_FUT fut = new PCF_FUT(ETFCode, m_Today, m_DataDate);
                fut.PID = item[0];
                fut.Head = fut.PID.Substring(0, fut.PID.Length - 6);
                fut.YM = fut.PID.Substring(fut.PID.Length - 6, 6);
                fut.Y = fut.YM.ToInt() / 100;
                fut.M = fut.YM.ToInt() % (fut.Y * 100);
                fut.Name = item[1];
                fut.TotalUnits = item[2].ToDouble();
                fut.Weights = decimal.Parse(item[4].Replace("%", "")) / 100M;
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
                //if (Fail = !string.IsNullOrEmpty(msg)) { MSG("[Error]   " + msg); }
            }
            MSG($"[{DataKind.FUT}]   筆數:{cnt} ");
        }
        private void CashMargin(HtmlDocument doc)
        {
            MSG($"[{DataKind.CASH},{DataKind.MARGIN}]");
            string xPath = INI(nameof(FH), DataKind.CASH.ToString());
            int cntCash = 0, cntMargin = 0;

            List<List<string>> Baskets = _Basket(doc, xPath, DataType.Cash);
            if (Baskets == null || Baskets.Count <= 0)
            {
                MSG($"[{DataKind.CASH},{DataKind.MARGIN}]   資料筆數為0 ");
                return;
            }
            PCF_Cash.Delete(m_DataDate, ETFCode);
            PCF_Margin.Delete(m_DataDate, ETFCode);
            foreach (var item in Baskets)
            {
                string msg = string.Empty;
                DataKind k = DataKind.COMPOSITION;
                if (item[0].Contains(CASHNAME))
                {
                    k = DataKind.CASH;
                    PCF_Cash c = new PCF_Cash(ETFCode, m_DataDate);
                    c.Currency = item[0].Replace(CASHNAME, "").Replace(")", "");
                    if (c.Currency == "NTD") { c.Currency = "TWD"; }
                    c.Amount = double.Parse(item[1], NumberStyles.Currency);
                    c.Rate = Rate(m_FutFXs, c.Currency, "TWD");
                    msg = c.Insert();
                }
                else if (item[0].Contains(MARGINNAME))
                {
                    k = DataKind.MARGIN;
                    PCF_Margin m = new PCF_Margin(ETFCode, m_DataDate);
                    m.Currency = item[0].Replace(MARGINNAME, "").Replace(")", "");
                    if (m.Currency == "NTD") { m.Currency = "TWD"; }
                    m.Amount = double.Parse(item[1], NumberStyles.Currency);
                    m.Rate = Rate(m_FutFXs, m.Currency, "TWD");
                    msg = m.Insert();
                }
                else
                {
                    continue;
                }
                System.Windows.Forms.TreeNode node = Node.Nodes[k.ToString()];
                new Action(() =>
                {
                    if (!(node.Checked = string.IsNullOrEmpty(msg)))
                    {
                        MSG($"[{k}]   {msg}");
                    }
                    else
                    {
                        switch (k)
                        {
                            case DataKind.CASH:
                                cntCash++;
                                break;
                            case DataKind.MARGIN:
                                cntMargin++;
                                break;
                        }
                    }
                }).Invoke(node.TreeView);
            }
            MSG($"[{DataKind.CASH},{DataKind.MARGIN}]   Cash:{cntCash}  Margin:{cntMargin}");
        }
        private void FxRate(HtmlDocument doc)
        {
            MSG($"[{DataKind.FX}]");
            string rateValue = string.Empty;
            string xPath = INI(nameof(FH), DataKind.FX.ToString());
            int cnt = 0;
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.FX.ToString()];

            string value = NODE(doc, xPath);
            if (string.IsNullOrEmpty(value))
            {
                MSG($"[{DataKind.FX}]   沒有匯率");
                return;
            }
            string[] Currencies = value.Split('：')[1].Split(new string[] { "， " }, StringSplitOptions.None);
            foreach (var currency in Currencies)
            {
                if (currency == "。") { continue; }
                string[] curritem = currency.Split('=');
                string baseCurrency = curritem[0].Replace("1", string.Empty).Trim();
                curritem[1] = curritem[1].Replace("。", string.Empty).Trim();
                string quotedCurrency = curritem[1].Substring(curritem[1].Length - 3, 3);
                double v = double.Parse(curritem[1].Replace(quotedCurrency, string.Empty));
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
                    case DataKind.FUT:
                    case DataKind.FX:
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