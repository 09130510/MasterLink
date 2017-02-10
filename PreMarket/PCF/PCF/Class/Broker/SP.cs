//#define NOTHREADING

using HtmlAgilityPack;
using log4net;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using Util.Extension;

namespace PCF.Class
{
    public class SP : ETF
    {
        /// <summary>
        /// PCF Address
        /// </summary>
        private const string URLHeader = "http://sitc.sinopac.com.tw/web/etf/Ajax/GetTradeinfo_buylist.aspx?fundcode=";

        #region Property        
        public override string Address
        {
            get { return base.Address; }
            set
            {
                base.Address = value;
                m_FutFXs.Clear();
                m_Url = new Uri(URLHeader + base.Address);
                MSG($"[{DataKind.HEAD}]  {m_Url.AbsoluteUri}");

                _DataKind();
                MSG(string.Empty);
            }
        }
        #endregion

        public SP()
        {
            m_Log = LogManager.GetLogger(typeof(SP));
        }

        protected override void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MSG("[Error]   " + e.Error.Message);
                m_Log.Error(string.Empty, e.Error);
                return;
            }
#if !NOTHREADING
            ThreadPool.QueueUserWorkItem((state) =>
            {
                DownloadStringCompletedEventArgs evt = (DownloadStringCompletedEventArgs)state;
#else
                DownloadStringCompletedEventArgs evt = (DownloadStringCompletedEventArgs)e;
#endif
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(evt.Result);
                try
                {
                    Head(doc);
                    Stk(doc);
                    FxRate();
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
            string[] xPath = INI(nameof(SP), SettingItem).Split(new char[] { '|' });
            //ETFDaily daily = new ETFDaily(ETFCode, DateTime.Now);
            ETFDaily daily = new ETFDaily(ETFCode, m_Today);
            var properties = typeof(ETFDaily).GetProperties();
            for (int i = 0; i < properties.Count(); i++)
            {
                PropertyInfo p = properties.ElementAt(i);
                if (i > xPath.Length - 1) { break; }
                object value = NODE(doc, xPath[i]);
                if (value == null || value.ToString() == string.Empty)
                {
                    MSG($"[{DataKind.HEAD}]   沒有取得欄位值:{p.Name}");
                    continue;
                }
                switch (p.Name)
                {
                    case "DataDate":
                        value = DateTime.Parse(value.ToString().Replace("永豐臺灣加權ETF證券投資信託基金（證劵代碼：", "").Split('）')[1].Split(new string[] { "&nbsp;&nbsp;&nbsp;&nbsp;" }, StringSplitOptions.None)[0]);
                        break;
                    case "ETFCode":
                        value = value.ToString().Replace("永豐臺灣加權ETF證券投資信託基金（證劵代碼：", "").Split('）')[0];
                        break;
                    case "FundAssetValue":
                    case "NAV":
                    case "PreAllot":
                    case "Allot":
                    case "EstCValue":
                    case "EstDValue":
                    case "EstPublicShares":
                        value = double.Parse(value.ToString(), NumberStyles.Currency);
                        break;
                    case "PublicShares":
                    case "PublicSharesDiff":
                    case "CashDiff":
                        value = int.Parse(value.ToString(), NumberStyles.Currency);
                        break;
                }
                p.SetValue(daily, value, null);
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
            string xPath = INI(nameof(SP), CONTENT);
            int cnt = 0;
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.STK.ToString()];

            var Baskets = doc.DocumentNode.SelectSingleNode(xPath).Descendants(TR)
                    .Where(tr => tr.Elements(TD).Count() > 1)
                    .Select(tr => tr.Elements(TD).Select(td => td.InnerText.Trim()).ToList())
                    .ToList();
            if (Baskets == null || Baskets.Count <= 0)
            {
                MSG($"[{DataKind.STK}]  沒有資料");
                return;
            }

            foreach (var item in Baskets)
            {
                PCF_STK stk = new PCF_STK(ETFCode, m_Today, m_DataDate);
                if (item.Count < 3) { continue; }
                stk.PID = item[0];
                stk.Name = item[1];
                stk.PCFUnits = int.Parse(item[2], NumberStyles.AllowThousands);
                stk.ReplaceWithCash = item[3];
                stk.PhysicalPurchase = item[4];
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
            MSG($"[{DataKind.STK}]  筆數:{cnt}");
        }
        private void FxRate()
        {
            MSG($"[{DataKind.FX}]");
            PCF_FxRate fx = new PCF_FxRate(ETFCode, m_DataDate) { Base = "USD", Quoted = "USD", Value = 1 };

            string msg = fx.Insert();
            System.Windows.Forms.TreeNode node = Node.Nodes[DataKind.FX.ToString()];
            new Action(() =>
            {
                if (!(node.Checked = string.IsNullOrEmpty(msg)))
                {
                    MSG($"[{DataKind.FX}]   {msg}");
                }
                else
                {
                    MSG($"[{DataKind.FX}]   筆數:1");
                }
            }).Invoke(node.TreeView);
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
                        Node.Nodes.Add(new System.Windows.Forms.TreeNode(kind.ToString()) { Name = kind.ToString(), Tag = this });
                        break;
                    case DataKind.HEAD:
                    case DataKind.COMPOSITION:
                    case DataKind.FUT:
                    case DataKind.FUND:
                    case DataKind.CASH:
                    case DataKind.MARGIN:
                    default:
                        continue;
                }
            }
        }
    }
}