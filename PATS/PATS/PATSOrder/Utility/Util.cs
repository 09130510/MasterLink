using IniParser;
using IniParser.Model;
using PATSOrder.Class;
using PriceLib.PATS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PATSOrder.Utility
{
    public struct PQ
    {
        public double Price;
        public int Qty;
    }

    public static class Util
    {
        public const string LOG = "LOG";
        public const string PATS_BEFORERESET = "PATS_BeforeReset";
        public const string PATS_AFTERRESET = "PATS_AfterReset";
        public const string CONFIGFILE = @"./Config.ini";
        public const string DOCKLAYOUTFILE = @"./DockLayout.xml";


        public static frmLog LogForm { get; set; }
        public static frmSetting SettingForm { get; set; }
        public static frmOrderSetting OrderSettingForm { get; set; }
        public static frmOrderReport OrderRPTForm { get; set; }
        public static frmDealReport DealRPTForm { get; set; }
        public static frmCancelReport CancelRPTForm { get; set; }
        public static frmErrReport ErrRPTForm { get; set; }
        public static frmSummary SummaryForm { get; set; }
        private static frmProductFilter m_ProdFilterForm;
        public static frmProductFilter ProdFilterForm
        {
            get
            {
                if (m_ProdFilterForm == null || m_ProdFilterForm.IsDisposed)
                {
                    m_ProdFilterForm = new frmProductFilter();
                }
                return m_ProdFilterForm;
            }
        }

        public static PATSLib PATS { get; private set; }
        public static IniData INI { get; private set; }
        public static List<int> TickSeqNo { get; set; } = new List<int>();
        public static List<ProductInfo> ProductInfos { get; set; } = null;
        public static List<AccountInfo> AccountInfos { get; set; } = null;

        public static void Init()
        {
            //LogForm = new frmLog();
            if (INI == null)
            {
                var parser = new FileIniDataParser();
                INI = parser.ReadFile(CONFIGFILE);
            }
            _InitPATS();

            LogForm = new frmLog();
            SettingForm = new frmSetting();
            OrderSettingForm = new frmOrderSetting();
            OrderRPTForm = new frmOrderReport();
            DealRPTForm = new frmDealReport();
            CancelRPTForm = new frmCancelReport();
            ErrRPTForm = new frmErrReport();
            SummaryForm = new frmSummary();
        }
        public static string VersionInfo()
        {
            object[] attribute = typeof(frmMain).Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = typeof(frmMain).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            return $"[{title.Title} - {Process.GetCurrentProcess().Id}]  {desc.Description}  V{Application.ProductVersion}";
        }
        public static void LoadConfig(Control c)
        {
            if (c != null && c.Controls.Count > 0)
            {
                foreach (Control sub in c.Controls)
                {
                    LoadConfig(sub);
                }
            }
            _LoadConfig(c);
        }
        public static void SaveConfig(Control c)
        {
            if (c.Tag == null || string.IsNullOrEmpty(c.Tag.ToString())) { return; }

            string[] tags = c.Tag.ToString().Split(';');
            if (tags.Length < 3) { return; }
            switch (tags[2])
            {
                case "Text":
                    INI[tags[0]][tags[1]] = c.Text.Replace("\r\n", ";");
                    break;
                case "Value":
                    if (c is NumericUpDown)
                    {
                        INI[tags[0]][tags[1]] = (c as NumericUpDown).Value.ToString();
                    }
                    break;
                case "Checked":
                    INI[tags[0]][tags[1]] = (c as CheckBox).Checked.ToString();
                    break;
                default:
                    break;
            }
            SaveConfig();
        }
        public static void SaveConfig()
        {
            var parser = new FileIniDataParser();
            parser.WriteFile(CONFIGFILE, INI);
        }
        public static void ResetPATS()
        {
            Center.Instance.Post(PATS_BEFORERESET, "");
            _InitPATS();
            Center.Instance.Post(PATS_AFTERRESET, "");
        }
        public static void ReloadProductInfo()
        {
            ProductInfos = ProductInfo.Convert(PATS.Products());
            Center.Instance.Post(nameof(PATS), nameof(ProductInfos));
        }


        #region Tick
        //public static List<PQ> ExtendBidByTickNumber(PriceStruct price, double tickSize, int tickNumber)
        //{
        //    List<PQ> re = new List<PQ>();
        //    double initPrice;
        //    int plus;
        //    if (price.Last0.Price.ToDouble() == 0 || price.Last0.Price.ToDouble() <= price.BidDOM0.Price.ToDouble())
        //    {
        //        initPrice = price.BidDOM0.Price.ToDouble();
        //        plus = 0;
        //    }
        //    else
        //    {
        //        initPrice = price.Last0.Price.ToDouble();
        //        plus = 1;
        //    }
        //    for (int i = 0; i < tickNumber; i++)
        //    {
        //        decimal p =(decimal)( initPrice + (i + plus) * -1 * tickSize);
        //        int qty = 0;
        //        for (int idx = 0; idx < price.BidDOMs.Length; idx++)
        //        {
        //            if (price.BidDOMs[idx].Price == p.ToString())
        //            {
        //                qty = price.BidDOMs[idx].Volume;
        //                break;
        //            }
        //        }
        //        re.Add(new PQ() { Price =(double) p, Qty = qty });
        //    }
        //    return re;
        //}
        //public static List<PQ> ExtendAskByTickNumber(PriceStruct price, double tickSize, int tickNumber)
        //{
        //    List<PQ> re = new List<PQ>();
        //    double initPrice;
        //    int plus;
        //    if (price.Last0.Price.ToDouble() == 0 || price.Last0.Price.ToDouble() <= price.OfferDOM0.Price.ToDouble())
        //    {
        //        initPrice = price.OfferDOM0.Price.ToDouble();
        //        plus = 0;
        //    }
        //    else
        //    {
        //        initPrice = price.Last0.Price.ToDouble();
        //        plus = 1;
        //    }
        //    for (int i = 0; i < tickNumber; i++)
        //    {
        //        decimal p = (decimal)(initPrice + (i + plus) * tickSize);
        //        int qty = 0;
        //        for (int idx = 0; idx < price.OfferDOMs.Length; idx++)
        //        {
        //            if (price.OfferDOMs[idx].Price == p.ToString())
        //            {
        //                qty = price.OfferDOMs[idx].Volume;
        //                break;
        //            }
        //        }
        //        re.Add(new PQ() { Price = (double)p, Qty = qty });
        //    }
        //    return re;
        //}        
        //private static List<PQ> _Extend(int tickNumber, double initPrice, int plusCnt, double tickSize, int multiplier, PriceDetailStruct[] DOM)
        //{
        //    List<PQ> re = new List<PQ>();
        //    for (int i = 0; i < tickNumber; i++)
        //    {
        //        if (initPrice == 0)
        //        {
        //            re.Add(new PQ() { Price = 0, Qty = 0 });
        //            continue;
        //        }
        //        decimal p = (decimal)(initPrice + (i + plusCnt) * (multiplier) * tickSize);
        //        int qty = 0;
        //        for (int idx = 0; idx < DOM.Length; idx++)
        //        {
        //            if (DOM[idx].Price == p.ToString())
        //            {
        //                qty = DOM[idx].Volume;
        //                break;
        //            }
        //        }
        //        re.Add(new PQ() { Price = (double)p, Qty = qty });
        //    }
        //    return re;
        //}
        //public static void ExtendTick(PriceStruct price, double tickSize, int tickNumber, out List<PQ> bid, out List<PQ> offer)
        //{            
        //    //PriceDetailStruct[] DOM = buy ? price.BidDOMs : price.OfferDOMs;
        //    double lastPrice = price.Last0.Price.ToDouble();
        //    double bidInit = price.BidDOM0.Price.ToDouble();
        //    double offerInit = price.OfferDOM0.Price.ToDouble();
        //    int bidPlusCnt =0;
        //    int offerPlusCnt = 0;
        //    if (lastPrice!=0 )
        //    {
        //        if (lastPrice> bidInit)
        //        {
        //            bidInit = lastPrice;
        //            bidPlusCnt = 1;
        //        }
        //        if (lastPrice<offerInit)
        //        {
        //            offerInit = lastPrice;
        //            offerPlusCnt = 1;
        //        }                
        //    }
        //    bid = _Extend(tickNumber, bidInit, bidPlusCnt, tickSize, -1, price.BidDOMs);
        //    offer = _Extend(tickNumber, offerInit, offerPlusCnt, tickSize, 1, price.OfferDOMs);
        //    //if (price.Last0.Price.ToDouble() == 0 || price.Last0.Price.ToDouble() <= DOM[0].Price.ToDouble())
        //    //{
        //    //    initPrice = DOM[0].Price.ToDouble();
        //    //    plus = 0;
        //    //}
        //    //else
        //    //{
        //    //    initPrice = price.Last0.Price.ToDouble();
        //    //    plus = 1;
        //    //}
        //    //for (int i = 0; i < tickNumber; i++)
        //    //{
        //    //    if (initPrice==0)
        //    //    {
        //    //        re.Add(new PQ() { Price = 0, Qty = 0 });
        //    //        continue;
        //    //    }
        //    //    decimal p = (decimal)(initPrice + (i + plus) * (buy ? -1 : 1) * tickSize);
        //    //    int qty = 0;
        //    //    for (int idx = 0; idx < DOM.Length; idx++)
        //    //    {
        //    //        if (DOM[idx].Price == p.ToString())
        //    //        {
        //    //            qty = DOM[idx].Volume;
        //    //            break;
        //    //        }
        //    //    }
        //    //    re.Add(new PQ() { Price = (double)p, Qty = qty });
        //    //}
        //    //return re;
        //}

        #endregion

        #region Private
        private static void _LoadConfig(Control c)
        {
            if (c.Tag == null || string.IsNullOrEmpty(c.Tag.ToString())) { return; }
            string[] tags = c.Tag.ToString().Split(';');
            if (tags.Length < 3) { return; }
            PropertyInfo pi = c.GetType().GetProperty(tags[2]);
            if (pi == null) { return; }

            switch (tags[2])
            {
                case "Text":
                    pi.SetValue(c, INI[tags[0]][tags[1]].Replace(";", "\r\n"), null);
                    break;
                case "Checked":
                    bool chk = default(bool);
                    bool.TryParse(INI[tags[0]][tags[1]], out chk);
                    pi.SetValue(c, chk, null);
                    break;
                case "Value":
                    decimal dec = default(decimal);
                    decimal.TryParse(INI[tags[0]][tags[1]], out dec);
                    if (dec == default(decimal) && tags.Length == 4)
                    {
                        dec = decimal.Parse(tags[3]);
                    }
                    pi.SetValue(c, dec, null);
                    break;
                default:
                    break;
            }
        }
        private static void _InitPATS()
        {
            if (PATS?.isConnected ?? false)
            {
                PATS.Disconnect();
                PATS.OnConnectStateChanged -= PATS_OnConnectStateChanged;
            }
            PATS = new PATSLib(INI["PATS"]["HOSTIP"], INI["PATS"]["HOSTPORT"], INI["PATS"]["PRICEIP"], INI["PATS"]["PRICEPORT"], INI["PATS"]["USER"], INI["PATS"]["PWD"]);
            //ProductInfos = null;
            //AccountInfos = null;
            PATS.OnConnectStateChanged += PATS_OnConnectStateChanged;
        }

        private static void PATS_OnConnectStateChanged(object sender, ConnectStateEventArgs e)
        {
            if (e.DLComplete)
            {
                if (ProductInfos == null)
                {
                    ProductInfos = ProductInfo.Convert(PATS.Products());
                    Center.Instance.Post(nameof(PATS), nameof(ProductInfos));
                }
                if (AccountInfos == null)
                {
                    AccountInfos = AccountInfo.Convert(PATS.Traders());
                    Center.Instance.Post(nameof(PATS), nameof(AccountInfos));
                }
            }
            else
            {
                if (ProductInfos != null)
                {
                    ProductInfos.Clear();
                    Center.Instance.Post(nameof(PATS), nameof(ProductInfos));
                }
                if (AccountInfos != null)
                {
                    AccountInfos.Clear();
                    Center.Instance.Post(nameof(PATS), nameof(AccountInfos));
                }
            }
        }
        #endregion
    }
}
