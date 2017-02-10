using Dapper;
using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATS.Utility
{
    public class SQLTool
    {
        #region Variable
        private ILog m_Log = LogManager.GetLogger(typeof(SQLTool));
        private SQLType m_SQLType;
        private string m_IP;
        private string m_DB;
        private string m_User = "sa";
        private string m_Pwd = "sa";
        #endregion
                
        private string ConnStr { get { return $"server={m_IP};database={m_DB};uid={m_User};pwd={m_Pwd}"; } }
        

        private SQLTool(SQLType sqlType, string ip, string db)
        {
            m_SQLType = sqlType;
            m_IP = ip;
            m_DB = db;
        }
        public SQLTool(SQLType sqlType, string ip, string db, string user , string pwd )
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
            conn.Open();
            try
            {
                int r = comm.ExecuteNonQuery();
                comm.Dispose();
                conn.Close();
                return r;
            }
            catch (Exception)
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
        public void Execute(string sql, object param, CommandType commandType = CommandType.Text, int timeout = 10)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                conn.Execute(sql, param, null, timeout, commandType);
            }
        }
    }
}
