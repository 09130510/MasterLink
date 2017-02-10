using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Util.Extension.Class
{
    public class SQLTool
    {
        #region Variable
        private string m_IP;
        private string m_DB;
        private string m_User = "sa";
        private string m_Pwd = "sa";
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
        }

        public DataTable DoQuery(string SQL)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(SQL, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(dt);
                    comm.Dispose();
                    conn.Close();
                    return dt;
                }
            }
            catch (SqlException) { }
            return null;
        }

        public int DoExecute(string SQL)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(SQL, conn);
                    int r = comm.ExecuteNonQuery();
                    comm.Dispose();
                    conn.Close();
                    return r;
                }
            }
            catch (SqlException)
            {
            }
            return -1;
        }
    }
}
