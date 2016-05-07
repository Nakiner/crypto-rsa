using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Курсовая
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sql.Init();
            ip.GetIp();
            if(ip.Banned(ip.Ip))
            {
                string bandate = sql.Select("date", "bans", "ip", ip.Ip, 0);
                string res = bandate.Trim(new Char[] { ' ', ';', '.' });
                MessageBox.Show("Вы заблокированы в системе.\nДата разблокировки: " + res);
                this.Close();
            }
            if(sql.username == "")
            {
                авторизацияToolStripMenuItem.Enabled = true;
                регистрацияToolStripMenuItem.Enabled = true;
                сменаПароляToolStripMenuItem.Enabled = false;
                генерированиеКлючейToolStripMenuItem.Enabled = false;
                toolStripButton1.Enabled = false;
            }
            else
            {
                авторизацияToolStripMenuItem.Enabled = false;
                регистрацияToolStripMenuItem.Enabled = false;
                сменаПароляToolStripMenuItem.Enabled = true;
                генерированиеКлючейToolStripMenuItem.Enabled = true;
                toolStripButton1.Enabled = true;
            }
        }
        private void авторизацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            this.Hide();
        }
        private void регистрацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Hide();
        }

        private void сменаПароляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
            this.Hide();
        }

        private void генерированиеКлючейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
            this.Hide();
        }

        private void зашифроватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.ShowDialog();
            this.Hide();
        }

        private void расшифроватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.ShowDialog();
            this.Hide();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.ShowDialog();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://nakiner.ru");
        }
    }
}
