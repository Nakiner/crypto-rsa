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
                MessageBox.Show("Длина логина или пароля слишком минимальна.");
                return;
            }
            if (!sql.HasRows("login","users","login",textBox1.Text))
            {
                MessageBox.Show("Такого аккаунта не существует.");
                return;
            }
            if (rs.hashcode(textBox2.Text) != sql.Select("password", "users", "login", textBox1.Text, 0))
            {
                MessageBox.Show("Вы указали неверный пароль.");
                return;
            }
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
