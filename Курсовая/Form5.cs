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
using System.IO;
using MySql.Data.MySqlClient;

namespace Курсовая
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            rs.GenerateKeys();
            while(sql.HasRows("keyid","public","keyid",Convert.ToString(rs.keyid))) rs.GenerateKeys();
            textBox1.Text = Convert.ToString(rs.keyid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = rs.pubkey;
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            int keyidp = rs.keyid * 2145;
            string[] parms = { keyidp.ToString(), sql.username, rs.privkey };
            string[] parms1 = { rs.keyid.ToString(), rs.pubkey };
            sql.Insert("private", "keyid,login,value", parms, 3);
            sql.Insert("public", "keyid,value", parms1, 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Txt Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            else File.WriteAllText(saveFileDialog1.FileName, rs.pubkey);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ключи успешно сгенерированы и сохранены.");
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
    }
}
