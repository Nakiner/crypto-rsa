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

namespace Курсовая
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            sql.cmd.Parameters.AddWithValue("@login", sql.username);
            sql.cmd.CommandText = "SELECT keyid FROM private WHERE login = @login";
            sql.rdr = sql.cmd.ExecuteReader();
            sql.cmd.Parameters.Clear();
            while (sql.rdr.Read())
            {
                comboBox1.Items.Add(Convert.ToInt32(sql.rdr.GetString(0))/2145);
            }
            sql.rdr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "")
            {
                MessageBox.Show("Вы не выбрали ID ключа.");
                return;
            }
            switch(Convert.ToInt32(comboBox2.SelectedIndex.ToString()))
            {
                case 0:
                {
                    sql.Delete("public", "keyid", comboBox1.Text);
                    int ls = Convert.ToInt32(comboBox1.Text) * 2145;
                    sql.Delete("private", "keyid", Convert.ToString(ls));
                    comboBox1.Items.Remove(comboBox1.SelectedItem);
                    comboBox1.Text = "";
                    MessageBox.Show("Пара ключей успешно удалена.");
                    break;
                }
                case 1:
                {
                    saveFileDialog1.FileName = "";
                    saveFileDialog1.Filter = "Txt Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    saveFileDialog1.FilterIndex = 2;
                    if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                    else
                    {
                        string key = sql.Select("value", "public", "keyid", comboBox1.Text, 0);
                        File.WriteAllText(saveFileDialog1.FileName, key);
                    }
                    MessageBox.Show("Публичный ключ успешно сохранён.");
                    break;
                }
            }
        }
        private void Form8_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
