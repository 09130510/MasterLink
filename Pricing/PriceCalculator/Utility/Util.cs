#define LOG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IniParser.Model;
using IniParser;
using PriceCalculator.Component;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using PriceLib.Redis;
using System.Diagnostics;
using PriceLib.iPush;
using PriceLib;
using log4net.Config;
using log4net;
using System.IO;
using System.Threading;

namespace PriceCalculator.Utility
{
    public static class Util
    {
        /// <summary>
        /// yyyy/MM/dd
        /// </summary>
        public const string DATESTR = "yyyy/MM/dd";

        internal static Calculator Calculator;
        public static string PublishChannel;
        public static IniData INI;
        public static SQLTool SQL;
        public static RedisPublishLib MiddleREDIS;
        public static ToolStripLabel StatusLabel = new ToolStripLabel();

        #region Component Collection
        public static Collection STK;
        public static Collection FUT;
        public static Collection FUND;
        public static AssetCollection CASH;
        public static AssetCollection FORWARD;
        public static AssetCollection MARGIN;
        public static FXCollection FXRates;
        #endregion

        #region Form        
        public static frmETFSelect ETFSelectForm;
        public static frmConnection ConnectForm;
        public static frmError ErrorForm;
        public static frmIIV IIVForm;
        public static frmMain2 MainForm;
        #endregion


        public static void Init(ToolStripStatusLabel[] statusLabels)
        {
            if (!File.Exists("Config.ini"))
            {
                //AlertBox 找不到設定檔
                Application.Exit();
            }
            if (INI == null)
            {
                var parser = new FileIniDataParser();
                INI = parser.ReadFile("Config.ini");
            }
            SQL = new SQLTool(INI["SQL"]["IP"], INI["SQL"]["DB"], INI["SQL"]["USR"], INI["SQL"]["PWD"]);
            XmlConfigurator.Configure(new FileInfo("./LogConfig.xml"));

            MiddleREDIS = new RedisPublishLib(INI["MIDDLEREDIS"]["IP"], int.Parse(INI["MIDDLEREDIS"]["PORT"]));
            PublishChannel = INI["MIDDLEREDIS"]["CHANNEL"];
            string dataDate = DateTime.Now.ToString(DATESTR);
            STK = Collection.Create(CollectionType.Stock, dataDate);
            FUT = Collection.Create(CollectionType.Future, dataDate);
            FUND = Collection.Create(CollectionType.Fund, dataDate);
            FXRates = new FXCollection();
            CASH = AssetCollection.Create(AssetType.Cash, dataDate);
            MARGIN = AssetCollection.Create(AssetType.Margin, dataDate);
            FORWARD = AssetCollection.Create(AssetType.Forward, dataDate);



            ETFSelectForm = new frmETFSelect();
            Calculator = new Calculator(statusLabels);
            ConnectForm = new frmConnection();
            ErrorForm = new frmError();
            IIVForm = new frmIIV();
        }
        public static void Status(string msg)
        {
            StatusLabel.InvokeIfRequired(() =>
            {
                StatusLabel.Text = msg;
            });
        }
        public static void WriteConfig()
        {
            var parser = new FileIniDataParser();
            parser.WriteFile("Config.ini", INI);
        }
        public static void PublishToMiddle(string etfcode, decimal iiv)
        {
            ThreadPool.QueueUserWorkItem((args) =>
            {
                string[] items = args.ToString().Split('|');
                string channel = $"{PublishChannel}.{items[0]}";
                MiddleREDIS.Publish(channel, items[1]);
                if (MiddleREDIS.HashGet(1, items[0], channel) == null)
                {
                    MiddleREDIS.Publish("ETFN.NAV", $"I|{items[0]}|{channel}");
                }
                MiddleREDIS.HashSet(1, items[0], channel, items[1]);

            }, $"{etfcode}|{iiv}");
        }
        public static string VersionInfo(Form form)
        {
            object[] attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            string ostype = Environment.Is64BitProcess ? "x64" : "x86";
#if DEBUG
            return $"[{title.Title} - {Process.GetCurrentProcess().Id}] { desc.Description} ({ostype}-D)  V{Application.ProductVersion}";
#else
            return $"[{title.Title} - {Process.GetCurrentProcess().Id}] {desc.Description} ({ostype}-R)  V{Application.ProductVersion}";
#endif
        }

        #region For Log
        public static void Info(ILog log, string mname, string msg)
        {
#if LOG
            log.Info($"[{mname}] {msg}");
#endif
        }
        public static void Error(ILog log, string mname, string msg)
        {
            string message = $"[{mname}] {msg}";
#if LOG
            if (log != null) { log.Error(message); }
#endif
            ErrorForm.Err(message);
        }
        public static void Debug(ILog log, string mname, string msg)
        {
#if LOG
            log.Debug($"[{mname}] {msg}");

#endif
        }
        #endregion
        #region For IIV Display
        public static void AddRow(ETF etf)
        {
            if (((IIVForm != null) && !IIVForm.IsDisposed) && (etf != null))
            {
                IIVForm.AddRow(etf);
            }
        }
        public static void RemoveRow(ETF etf)
        {
            if (((IIVForm != null) && !IIVForm.IsDisposed) && (etf != null))
            {
                IIVForm.RemoveRow(etf);
            }
        }
        public static void Refresh()
        {
            if ((IIVForm != null) && !IIVForm.IsDisposed)
            {
                IIVForm.RefreshGrid();
            }
        }
        #endregion
        #region For Component
        public static IEnumerable<Composition> Union(CollectionKey keyType, string key)
        {
            IEnumerable<Composition> union = null;
            var stks = STK[keyType, key];
            if (stks != null)
            {
                union = stks.Values;
            }
            var futs = FUT[keyType, key];
            if (futs != null)
            {
                if (union == null) { union = futs.Values; }
                else { union = union.Union(futs.Values); }
            }
            var funds = FUND[keyType, key];
            if (funds != null)
            {
                if (union == null) { union = funds.Values; }
                else { union = union.Union(funds.Values); }
            }
            return union;
        }
        public static IEnumerable<Asset> Union(AssetKey assetType, string key)
        {
            IEnumerable<Asset> union = null;
            //CASH
            var cash = CASH[assetType, key];
            if (cash != null)
            {
                foreach (var cashList in cash.Values)
                {
                    if (cashList == null) { continue; }
                    if (union == null)
                    {
                        union = cashList.AsEnumerable();
                    }
                    else
                    {
                        union = union.Union(cashList.AsEnumerable());
                    }
                }
            }
            //MARGIN
            var margin = MARGIN[assetType, key];
            if (margin != null)
            {
                foreach (var marginList in margin.Values)
                {
                    if (marginList == null) { continue; }
                    if (union == null)
                    {
                        union = marginList.AsEnumerable();
                    }
                    else
                    {
                        union = union.Union(marginList.AsEnumerable());
                    }
                }
            }
            //FORWARD
            var forward = FORWARD[assetType, key];
            if (forward != null)
            {
                foreach (var forwardList in forward.Values)
                {
                    if (forwardList == null) { continue; }
                    if (union == null)
                    {
                        union = forwardList.AsEnumerable();
                    }
                    else
                    {
                        union = union.Union(forwardList.AsEnumerable());
                    }
                }
            }
            return union;
        }
        #endregion
    }
}