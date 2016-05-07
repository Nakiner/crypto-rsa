using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Курсовая
{
    class ip
    {
        public static string Ip = "";
       /// <summary>
       /// Методы для отслеживания ip адреса клиента, обращающегося к нашему серверу
       /// </summary>
       /// <returns>возвращает ip адрес</returns>
        public static string GetIp()
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("http://www.ip-ping.ru/");
            StreamReader sr = new StreamReader(stream);
            string newLine;
            Regex regex = new Regex("<div class=\"hc2\">(.*)</div>");
            while ((newLine = sr.ReadLine()) != null)
            {
                Match match = regex.Match(newLine);
                string str = match.Groups[1].ToString();
                if (str != "")
                { 
                    Ip = str;
                    break;
                }
            }
            stream.Close();
            return Ip;
        }
        /// <summary>
        /// Заполнение таблицы банов, для того чтобы пароль не подбирали
        /// </summary>
        /// <param name="ip">ip адрес клиента</param>
        /// <returns>Возвращает ip адрес в таблицу на сервере</returns>
        public static bool Banned(string ip)
        {
            return sql.HasRows("ip", "bans", "ip", ip);
        }
    }
}
