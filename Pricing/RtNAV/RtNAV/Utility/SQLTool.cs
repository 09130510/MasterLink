using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtNAV.Util
{
    public class SQLTool
    {
        #region Variable
        //private ILog m_Log = LogManager.GetLogger(typeof(SQLTool));
        private string m_IP;
        private string m_DB;
        private string m_User = "sa";
        private string m_Pwd = "sa";
        #endregion

        #region Property
        private string ConnStr { get { return $"server={m_IP};database={m_DB};uid={m_User};pwd={m_Pwd}"; } }

        #endregion

        private SQLTool(string ip, string db)
        {
            m_IP = ip;
            m_DB = db;
        }
        public SQLTool(string ip, string db, string user = "", string pwd = "")
            : this(ip, db)
        {
            if (!string.IsNullOrEmpty(user)) { m_User = user; }
            if (!string.IsNullOrEmpty(pwd)) { m_Pwd = pwd; }
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
        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();
                return connection.Query<T>(sql, param, null, true, null, null);
            }
        }
    }
}
