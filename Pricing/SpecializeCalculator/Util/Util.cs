#define SPREAD

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Reflection;
using System.Diagnostics;
using IniParser.Model;
using IniParser;
using System.Windows.Forms;
using PriceLib.PATS;
using PriceLib.Capital;
using PriceLib.Redis;
using PriceLib.iPush;
//using static PriceLib.Capital.OSCapitalLib;

namespace PriceCalculator
{
    public static class Util
    {
        private static string _ConnStr;
        public static frmMain MainForm;

        public static IniData INI { get; private set; }
        public static List<FutureBase> Calculators { get; private set; }
        public static RedisPriceLib REDIS { get; private set; }
#if SPREAD
        public static RedisPublishLib PUBLISH { get; private set; }
#else
        public static RedisPriceLib PUBLISH { get; private set; }
#endif
        public static PATSLib PATS { get; private set; }
        public static OSCapitalLib OSCAPITAL { get; private set; }
        public static iPushLib IPUSH { get; private set; }
        public static frmFutureBase FUTBaseForm { get; private set; }        
        public static Dictionary<string, Guid> CHANNEL { get; private set; } = new Dictionary<string, Guid>();

        public static void Init()
        {
            #region INI
            if (INI == null)
            {
                var parser = new FileIniDataParser();
                INI = parser.ReadFile("Config.ini");
            }
            #endregion
            #region SQL
            _ConnStr = $"server={INI["SQL"]["IP"]};database={INI["SQL"]["DB"]};uid={INI["SQL"]["USR"]};pwd={INI["SQL"]["PWD"]}";
            #endregion
            #region Calculators
            Calculators = new List<FutureBase>();
            #endregion
            #region Redis Price
            //REDIS = new RedisLib(INI["REDIS"]["IP"], int.Parse(INI["REDIS"]["PORT"]), Process.GetCurrentProcess().Id);
            REDIS = new RedisPriceLib(INI["REDIS"]["IP"], int.Parse(INI["REDIS"]["PORT"]));
            #endregion
            #region PATS
            PATS = new PATSLib(INI["PATS"]["HOSTIP"], INI["PATS"]["HOSTPORT"], INI["PATS"]["PRICEIP"], INI["PATS"]["PRICEPORT"], INI["PATS"]["USER"], INI["PATS"]["PWD"]);
            #endregion
            #region OSCAPITAL
            try
            {
                OSCAPITAL = new OSCapitalLib();
                OSCAPITAL.Connect(INI["CAPITAL"]["USR"], INI["CAPITAL"]["PWD"]);
            }
            catch (OSCapitalLib.CapitalException ex)
            {
                AlertBox.AlertWithoutReply(MainForm, AlertBoxButton.Error_OK, "Capital", ex.Message);
            }
            #endregion
            #region iPush
            IPUSH = new iPushLib();
            IPUSH.Connect(INI["IPUSH"]["IP"], INI["IPUSH"]["PORT"], INI["IPUSH"]["COMP"], INI["IPUSH"]["PROD"], INI["IPUSH"]["USER"], INI["IPUSH"]["PWD"]);
            #endregion
            #region Redis Publish
            //PUBLISH = new RedisLib(INI["PUBLISH"]["IP"], int.Parse(INI["PUBLISH"]["PORT"]), Rrocess.GetCurrentProcess().Id);
#if SPREAD
            PUBLISH = new RedisPublishLib(INI["PUBLISH"]["IP"], int.Parse(INI["PUBLISH"]["PORT"]));
#else
            PUBLISH = new RedisPriceLib(INI["PUBLISH"]["IP"], int.Parse(INI["PUBLISH"]["PORT"]) );
#endif
            #endregion
            FUTBaseForm = new frmFutureBase();            
        }

        internal static object QueryFirst<T>(object sELECTONE, object p)
        {
            throw new NotImplementedException();
        }

        public static string VersionInfo(Form form)
        {
            #region Version Info
            object[] attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            return $"[{title.Title} - {Process.GetCurrentProcess().Id}]  {desc.Description}  V{Application.ProductVersion}";
            #endregion
        }

        public static IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(_ConnStr))
            {
                conn.Open();
                return conn.Query<T>(sql, param);
            }
        }
        public static T QueryFirst<T>(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(_ConnStr))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<T>(sql, param);
            }
        }
        public static void Publish(string channel, string etfcode, string msg)
        {
            if (string.IsNullOrEmpty(channel) || string.IsNullOrEmpty(etfcode))
            {
                return;
            }
            string item =$"{channel}.{etfcode}";
            PUBLISH.Publish(item, msg);
            var v = PUBLISH.HashGet(01, etfcode, item);
            if (v == null)
            {
                PUBLISH.Publish("ETFN.NAV", "I|" + etfcode + "|" + item);
            }
            PUBLISH.HashSet(01, etfcode, item, msg);
        }
        public static void WriteConfig()
        {
            var parser = new FileIniDataParser();
            parser.WriteFile("Config.ini", INI);
        }
    }
}
