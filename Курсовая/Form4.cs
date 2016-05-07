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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 6 && textBox2.Text.Length < 6)
            {
                MessageBox.Show("Длина пароля составляет меньше 6 символов.");
                return;
            }
            if(textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }
            sql.Update("users", "password", rs.hashcode(textBox1.Text), "login", sql.username);
            MessageBox.Show("Пароль был изменён.");
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
    }
}
