using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 5 && textBox2.Text.Length < 5)
            {
                MessageBox.Show("Длина логина или пароля слишком мала.");
                return;
            }
            if (!sql.HasRows("login","users","login",textBox1.Text) || rs.hashcode(textBox2.Text) != sql.Select("password", "users", "login", textBox1.Text, 0))
            {
                int attpt = 0;
                if(ip.Banned(ip.Ip))
                {
                    attpt = Convert.ToInt32(sql.Select("attpt", "bans", "ip", ip.Ip, 0));
                    attpt++;
                    sql.Update("bans", "attpt", attpt.ToString(), "ip", ip.Ip);
                }
                else
                {
                    string[] prms = { sql.Ban(), ip.Ip, "1" };
                    sql.Insert("bans", "date,ip,attpt", prms, 3);
                }
                MessageBox.Show("Вы указали неверный логин или пароль.");
                if (attpt == 3)
                {
                    sql.Update("bans", "date", sql.Ban(15), "ip", ip.Ip);
                    MessageBox.Show("Вы были заблокированы в системе на 15 минут.");
                    this.Close();
                }
                return;
            }
            if (ip.Banned(ip.Ip)) sql.Delete("bans", "ip", ip.Ip);
            MessageBox.Show("Вы успешно авторизовались в системе.");
            sql.username = textBox1.Text;
            this.Close();
        }
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
