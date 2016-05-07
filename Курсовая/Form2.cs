using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace Курсовая
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 4 || textBox2.Text.Length < 4)
            {
                MessageBox.Show("Вы не заполнили поля."); 
                return;
            }
            if (textBox3.Text.Length < 6 && textBox4.Text.Length < 6)
            {
                MessageBox.Show("Длина пароля составляет меньше 6 символов.");
                return;
            }
            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }
            if(sql.HasRows("login","users","login",textBox1.Text))
            {
                MessageBox.Show("Аккаунт с таким именем зарегистрирован, укажите другое имя.");
                return;
            }
            if (ip.Banned(ip.Ip)) sql.Delete("bans", "ip", ip.Ip);
            string[] parms = { textBox1.Text, rs.hashcode(textBox3.Text), textBox2.Text };
            sql.Insert("users", "login,password,email", parms, 3);
            MessageBox.Show("Регистрация успешно завершена.");
            sql.username = textBox1.Text;
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
    }
}
