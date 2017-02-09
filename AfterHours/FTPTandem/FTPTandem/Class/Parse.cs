using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DownloadJPX
{
    public class Parse
    {
        private static  Dictionary<string, string> m_ParsePID;
        private static SQLTool m_SQL;

        private static Dictionary<string, string> ParsePID
        {
            get
            {
                if (m_ParsePID == null)
                {
                    m_ParsePID = new Dictionary<string, string>();
                    m_ParsePID.Add("TOPIXM", "JMT");
                    m_ParsePID.Add("TOPIX", "JTI");                    
                }
                return m_ParsePID;
            }
        }
        public DateTime Date { get; private set; }
        public string PID { get; private set; }
        public string YM { get; private set; }
        public double SettlePrice { get; private set; }

        

        public Parse(DateTime date, string[] data)
        {
            Date = date;
            foreach (var name in ParsePID.Keys)
            {
                if (data[1].Contains(name))
                {
                    PID = m_ParsePID[name];
                    break;
                }
            }
            YM = data[3];
            SettlePrice = double.Parse(data[5]);
        }

        public static void Init(string ip, string db, string user = "", string pwd = "")
        {
            m_SQL = new SQLTool(ip, db, user, pwd);            
        }
        public void Write()
        {
            m_SQL.DoExecute(string.Format("DELETE FROM MM..SETTLEPRICE WHERE DATE ='{0}' AND  PID='{1}' AND  YM ='{2}'", Date.ToString("yyyy/MM/dd"), PID, YM));
            m_SQL.DoExecute(string.Format("INSERT INTO MM..SETTLEPRICE (DATE, PID, YM, SETTLEPRICE) VALUES ('{0}','{1}','{2}',{3}) ", Date.ToString("yyyy/MM/dd"), PID, YM, SettlePrice));
        }
        public class SQLTool
        {
            #region Variable
            private string m_IP;
            private string m_DB;
            private string m_User = "sa";
            private string m_Pwd = "sa";

            private static SqlConnection m_Connection;
            #endregion

            #region Property
            private string ConnStr { get { return string.Format("server={0};database={1};uid={2};pwd={3}", m_IP, m_DB, m_User, m_Pwd); } }
            #endregion

            private SQLTool(string ip, string db)
            {
                m_IP = ip;
                m_DB = db;
            }
            public SQLTool(string ip, string db, string user = "", string pwd = "")
                : this(ip, db)
            {
                if (!String.IsNullOrEmpty(user)) { m_User = user; }
                if (!String.IsNullOrEmpty(pwd)) { m_Pwd = pwd; }
                Open();
            }

            private void Open()
            {
                if (m_Connection != null && m_Connection.State != System.Data.ConnectionState.Closed)
                {
                    return;
                }
                m_Connection = new SqlConnection(ConnStr);
                m_Connection.Open();
            }
            public int DoExecute(string SQL)
            {
                try
                {
                    SqlCommand comm = new SqlCommand(SQL, m_Connection);
                    return comm.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    //AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "SQL Exception", ex.Message);
                }
                return -1;
            }
        }
    }
}
