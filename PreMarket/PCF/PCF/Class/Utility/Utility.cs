using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IniParser.Model;
using IniParser;
using Util.Extension.Class;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using PriceLib.Redis;
using System.Diagnostics;
using Dapper;
using System.Data.Common;
using MySql.Data.MySqlClient;
using static PCF.SQLTool;
using Util.Extension;
using log4net.Config;
using log4net;
using System.Reflection;

namespace PCF
{
    public static class Utility
    {
        //private static string[] _ConnStr;
        /// <summary>
        /// 要不要判斷日期
        /// </summary>
        public static bool IgnoreDate = false;
        /// <summary>
        /// 元大網址要不要輸入日期
        /// </summary>
        public static bool YuantaNotInputDate = false;

        public static IniData INI;
        //public static SQLTool SQL;
        public static SQLTool[] SQL;
        public static RedisPublishLib[] Redis;

        public static void Init(CheckBox[] CheckBoxs)
        {
            if (INI == null)
            {
                var parser = new FileIniDataParser();
                INI = parser.ReadFile("Config.ini");
            }
            Extractor.ExtractResourceToFile("PCF.LogConfig.xml", @"./LogConfig.xml", false);
            XmlConfigurator.Configure(new System.IO.FileInfo(@"./LogConfig.xml"));

            string[] sqls = INI["SQL"]["IP"].Split(';');
            SQLType sqltype = INI["SQL"]["SQLTYPE"].ToEnum<SQLType>();
            string db = INI["SQL"]["DB"];
            string usr = INI["SQL"]["USR"];
            string pwd = INI["SQL"]["PWD"];
            //_ConnStr = new string[sqls.Length];
            SQL = new SQLTool[sqls.Length];
            for (int i = 0; i < sqls.Length; i++)
            {
                SQL[i] = new SQLTool(sqltype, sqls[i], db, usr, pwd);
                //_ConnStr[i] = string.Format("server={0};database={1};uid=sa;pwd=sa", sqls[i], db);
            }
            //SQL = new SQLTool(INI["SQL"]["IP"], INI["SQL"]["DB"]);
            string[] enable = INI["SYS"]["ENABLE"].Split(';');
            for (int i = 0; i < CheckBoxs.Length; i++)
            {
                if (i < enable.Length) { CheckBoxs[i].Checked = enable[i] == "Y"; }
            }
            string[][] rediss = INI["SYS"]["REDISIP"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Split(':')).ToArray();
            Redis = new RedisPublishLib[rediss.Length];
            for (int i = 0; i < rediss.Length; i++)
            {
                Redis[i] = new RedisPublishLib(rediss[i][0], int.Parse(rediss[i][1]));
            }
            //Redis = new RedisLib(INI["SYS"]["REDISIP"], int.Parse(INI["SYS"]["REDISPORT"]), Process.GetCurrentProcess().Id);
#if !DEBUG

            //Redis.Flush(int.Parse(INI["SYS"]["CHANNELDB"]));            
            //Redis.Flush(int.Parse(INI["SYS"]["FXDB"]));
            foreach (var redis in Redis)
            {
                redis.Flush(int.Parse(INI["SYS"]["CHANNELDB"]));
                //redis.Flush(int.Parse(INI["SYS"]["FXDB"]));
            }
#endif
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
        public static int DayCompare(DateTime date, DateTime compare)
        {
            int year = date.Year - compare.Year;
            int month = date.Month - compare.Month;
            int day = date.Day - compare.Day;
            if (year > 0) { return 1; }
            else if (year < 0) { return -1; }
            if (month > 0) { return 1; }
            else if (month < 0) { return -1; }
            if (day > 0) { return 1; }
            else if (day < 0) { return -1; }
            return 0;
        }
    }
    public class SQLTool
    {
        public enum SQLType
        {
            MSSQL,
            MYSQL
        }

        #region Variable
        private ILog m_Log = LogManager.GetLogger(typeof(SQLTool));
        private SQLType m_SQLType;
        private string m_IP;
        private string m_DB;
        private string m_User = "sa";
        private string m_Pwd = "sa";
        #endregion

        #region Property
        private string ConnStr { get { return $"server={m_IP};database={m_DB};uid={m_User};pwd={m_Pwd}"; } }
        #endregion

        private SQLTool(SQLType sqlType, string ip, string db)
        {
            m_SQLType = sqlType;
            m_IP = ip;
            m_DB = db;
        }
        public SQLTool(SQLType sqlType, string ip, string db, string user = "", string pwd = "")
            : this(sqlType, ip, db)
        {
            if (!string.IsNullOrEmpty(user)) { m_User = user; }
            if (!string.IsNullOrEmpty(pwd)) { m_Pwd = pwd; }
        }

        public DataTable DoQuery(string SQL)
        {
            DbConnection conn;
            DbDataAdapter adapter;
            switch (m_SQLType)
            {
                case SQLType.MSSQL:
                    conn = new SqlConnection(ConnStr);
                    adapter = new SqlDataAdapter(SQL, ConnStr);
                    break;
                case SQLType.MYSQL:
                    conn = new MySqlConnection(ConnStr);
                    adapter = new MySqlDataAdapter(SQL, ConnStr);

                    break;
                default:
                    return null;
            }
            return _Query(conn, adapter);
            //DataTable dt = new DataTable();
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(ConnStr))
            //    {
            //        conn.Open();
            //        SqlCommand comm = new SqlCommand(SQL, conn);

            //        SqlDataAdapter adapter = new SqlDataAdapter();
            //        adapter.SelectCommand = comm;
            //        adapter.Fill(dt);
            //        comm.Dispose();
            //        conn.Close();
            //        return dt;
            //    }
            //}
            //catch (SqlException) { }
            //return null;
        }
        public int DoExecute(string SQL)
        {
            DbConnection conn;
            DbCommand comm;
            switch (m_SQLType)
            {
                case SQLType.MSSQL:
                    conn = new SqlConnection(ConnStr);
                    comm = new SqlCommand(SQL, (SqlConnection)conn);                   
                    break;
                case SQLType.MYSQL:
                    conn = new MySqlConnection(ConnStr);
                    comm = new MySqlCommand(SQL, (MySqlConnection)conn);
                    break;
                default:
                    return -1;
            }
            return _Execute(conn, comm);
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(ConnStr))
            //    {
            //        conn.Open();
            //        SqlCommand comm = new SqlCommand(SQL, conn);
            //        int r = comm.ExecuteNonQuery();
            //        comm.Dispose();
            //        conn.Close();
            //        return r;
            //    }
            //}
            //catch (SqlException)
            //{
            //}
            //return -1;
        }
        private DataTable _Query(DbConnection conn, DbDataAdapter adapter)
        {
            DataTable dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }
        private int _Execute(DbConnection conn, DbCommand comm)
        {
            if (conn == null) { return -1; }            
            try
            {
                
                conn.Open();
                int r = comm.ExecuteNonQuery();
                comm.Dispose();
                conn.Close();
                return r;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                return conn.Query<T>(sql, param);
            }
        }
        public T QueryFirst<T>(string sql, object param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<T>(sql, param);
            }
        }
        public void Execute(string sql, object param, CommandType commandType = CommandType.Text, int timeout = 100)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                conn.Execute(sql, param, null, timeout, commandType);
            }
        }
        
    }
}