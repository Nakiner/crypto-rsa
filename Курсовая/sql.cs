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
    /// <summary>
    /// Класс для конекта с базой и работы с данными
    /// </summary>
   class sql
    {
        private const string Address = "";//fill
        private const string Login = "";//fill
        private const string Password = "";//fill
        private const string Database = "";//fill
        public static string username = "";//keep clean
        private static MySqlConnection conn = null;
        private static MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
        public static MySqlCommand cmd = new MySqlCommand();
        public static MySqlDataReader rdr = null;
       /// <summary>
       /// Коннект к базе
       /// </summary>
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
       /// <summary>
       /// Сравнение нашей информации с информацией из базы
       /// </summary>
       /// <param name="prms"> Что выбираем</param>
       /// <param name="table">Из какой таблицы</param>
       /// <param name="by">параметр из базы для сравнение с who</param>
       /// <param name="who">наш параметр для сравнения с параметром из базы by</param>
       /// <returns></returns>
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
            string ret = rdr.GetString(0);
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
                string temp = prms[i];
                cmd.Parameters.AddWithValue("@p" + i, temp);
                if(cnt > i) names += "@p" + i + ",";
                else if (cnt == i) names += "@p" + i + ")";
            }
            cmd.CommandText = "INSERT INTO " + table + "(" + flds + ") values(" + names;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }
        public static string Reverse(string str)
        {
            str = str.Replace(":", "");
            string[] abc = str.Split(' ');
            string[] cba = abc[0].Split('.');
            abc[0] = cba[2] + cba[1] + cba[0];
            return abc[0] + abc[1];
        }
        public static string Ban(int minn = 0, int hrr = 0, int dayy = 0, int mnts = 0)
        {
            string[] asd = sql.Date().Split(' ');
            string[] lfa = asd[0].Split('.');
            string[] ffs = asd[1].Split(':');
            int min = Convert.ToInt32(ffs[1])+minn;
            int hr = Convert.ToInt32(ffs[0])+hrr;
            int day = Convert.ToInt32(lfa[0])+dayy;
            int mnth = Convert.ToInt32(lfa[1])+mnts;
            int yr = Convert.ToInt32(lfa[2]);
            if (min > 60) { hr+=min/60; min = min % 60; }
            if(hr > 24) { day+=hr/24; hr = hr % 24;  }
            if(day > 30) { mnth+=day/30; day = day % 30;  }
            if(mnth > 12) { yr+=mnth/12; mnth = mnth % 12;  }
            string result = yr + "-" + mnth + "-" + day + " " + hr + ":" + min + ":00";
            return result;
        }
    }
}
