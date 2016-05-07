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

namespace Курсовая
{
    public partial class Form7 : Form
    {
        string enc = "";
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            else
            {
                openFileDialog1.OpenFile();
                enc = File.ReadAllText(openFileDialog1.FileName);
                if (enc == "") MessageBox.Show("Укажите файл с шифрограммой");
                else
                {
                    textBox1.Text = enc;
                    textBox1.ReadOnly = true;
                    button1.Enabled = false;
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
            enc = textBox2.Text;
            if (enc == "") MessageBox.Show("Поле не должно быть пустым");
            else
            {
                textBox2.ReadOnly = true;
                button1.Enabled = false;
                button2.Enabled = false;
                if (sql.username != "")
                {
                    textBox3.ReadOnly = false;
                    button3.Enabled = true;
                }
                button4.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string temp = Convert.ToString(Convert.ToInt32(textBox3.Text)*2145);
            if (!sql.HasRows("value", "private", "keyid", temp))
            {
                MessageBox.Show("Ключа с таким ID не найдено.");
                return;
            }
            if (sql.Select("login", "private", "keyid", temp, 0) != sql.username)
            {
                MessageBox.Show("Вы не можете воспользоваться этим ключом.");
                return;
            }
            rs.privkey = sql.Select("value", "private", "keyid", temp, 0);
            MessageBox.Show("Ключ " + textBox3.Text + " успешно импортирован.");
            textBox3.ReadOnly = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = true;
            button6.Enabled = true;
            textBox5.Text = rs.Decrypt(enc);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog2.FileName = "";
            if (openFileDialog2.ShowDialog() != DialogResult.OK) return;
            else
            {
                openFileDialog2.OpenFile();
                rs.privkey = File.ReadAllText(openFileDialog2.FileName);
                textBox4.Text = openFileDialog2.FileName;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = true;
                button6.Enabled = true;
                textBox5.Text = rs.Decrypt(enc);
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

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
