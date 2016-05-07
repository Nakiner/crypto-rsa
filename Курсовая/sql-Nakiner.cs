using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Курсовая
{
    class sql
    {
        private const string Address = "nakiner.ru";
        private const string Login = "you553are228allshit553";
        private const string Password = "WeAllLovePunkbuster6347821173";
        private const string Database = "crypt";
        public static string username = "";
        public static MySqlConnection conn = null;
        private static MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
        public static MySqlCommand cmd = new MySqlCommand();
        public static MySqlDataReader rdr = null;
        public static void Init()
        {
            conn_string.Server = Address;
            conn_string.UserID = Login;
            conn_string.Password = Password;
            conn_string.Database = Database;
            conn_string.SslMode = MySqlSslMode.Required;
            conn = new MySqlConnection(conn_string.ToString());
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
            }
            catch (MySqlException err)
            {
                MessageBox.Show("Error: " + Convert.ToString(err.Number));
                Application.Exit();
            }
        }
        public static bool HasRows(string prms,string table,string by, string who)
        {
            cmd.Parameters.AddWithValue("@" + by, who);
            cmd.CommandText = "SELECT " + prms + " FROM " + table + " WHERE " + by + " = @" + by;
            rdr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            bool res = rdr.HasRows;
            rdr.Close();
            return res;
        }
        public static string Select(string prms, string table, string by, string who, int retparam)
        {
            cmd.Parameters.AddWithValue("@" + by, who);
            cmd.CommandText = "SELECT " + prms + " FROM " + table + " WHERE " + by + " = @" + by;
            rdr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            rdr.Read();
            string ret = rdr.GetString(retparam);
            rdr.Close();
            return ret;
        }
        public static string Date()
        {
            cmd.CommandText = "SELECT NOW()";
            rdr = cmd.ExecuteReader();
            rdr.Read();
            string ret = rdr.GetString(0);
            rdr.Close();
            return ret;
        }
        public static void Delete(string table,string by,string who)
        {
            cmd.Parameters.AddWithValue("@" + by, who);
            cmd.CommandText = "DELETE FROM " + table + " WHERE " + by + " = @" + by;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }
        public static void Update(string table,string field,string val,string by,string who)
        {
            cmd.Parameters.AddWithValue("@" + field, val);
            cmd.Parameters.AddWithValue("@" + by, who);
            cmd.CommandText = "UPDATE " + table + " SET " + field + " = @" + field + " WHERE " + by + " = @" + by;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }
        public static void Insert(string table, string flds, string[] prms, int cnt)
        {
            string names = "";
            cnt = cnt - 1;
            for(int i = 0; i <= cnt; i++)
            {
                //string param = string.Join(", ", prms);
                //param = param.Substring(0, param.Length - 2);
                // MessageBox.Show(param);
                string temp = prms[i];
                cmd.Parameters.AddWithValue("@p" + i, temp);
                if(cnt > i) names += "@p" + i + ",";
                else if (cnt == i) names += "@p" + i + ")";
            }
            cmd.CommandText = "INSERT INTO " + table + "(" + flds + ") values(" + names;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }
    }
}
