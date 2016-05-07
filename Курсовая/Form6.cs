using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Курсовая
{
    public partial class Form6 : Form
    {
        string msg = "";
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            else
            {
                openFileDialog1.OpenFile();
                msg = File.ReadAllText(openFileDialog1.FileName);
                textBox1.Text = msg;
                if (msg == "") MessageBox.Show("Пожалуйста укажите файл с текстом");
                else
                {
                    button2.Enabled = false;
                    textBox2.ReadOnly = true;
                    if (sql.username != "")
                    {
                        textBox3.ReadOnly = false;
                        button3.Enabled = true;
                    }
                    button4.Enabled = true;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            msg = textBox2.Text;
            if (msg == "") MessageBox.Show("Поле не должно быть пустым");
            else
            {
                button1.Enabled = false;
                textBox3.ReadOnly = false;
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!sql.HasRows("value","public","keyid",textBox3.Text))
            {
                MessageBox.Show("Ключа с таким ID не найдено.");
                return;
            }
            rs.pubkey = sql.Select("value","public","keyid",textBox3.Text,0);
            MessageBox.Show("Ключ " + textBox3.Text + " успешно импортирован.");
            textBox3.ReadOnly = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = true;
            button6.Enabled = true;
            textBox5.Text = rs.Encrypt(msg);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog2.FileName = "";
            if (openFileDialog2.ShowDialog() != DialogResult.OK) return;
            else
            {
                openFileDialog2.OpenFile();
                rs.pubkey = File.ReadAllText(openFileDialog2.FileName);
                textBox4.Text = openFileDialog2.FileName;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = true;
                button6.Enabled = true;
                textBox5.Text = rs.Encrypt(msg);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Txt Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            else File.WriteAllText(saveFileDialog1.FileName, textBox5.Text);
            MessageBox.Show("Шифрограмма успешно сохранена.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
    }
}
