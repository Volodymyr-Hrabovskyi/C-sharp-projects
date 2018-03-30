using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_Caesar_cipher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Caesar_Cipher_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string Str = textBox3.Text.Trim();
            int Num;
            bool isNum = int.TryParse(Str, out Num);
            if (isNum)
            {


                int key = 33;
                string alfa = " АБВГДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ АБВГДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";
                if (textBox3.Text == "")
                {
                    MessageBox.Show(this, "Введіть свій варіант від 0 до 32");
                }
                if (Convert.ToInt32(textBox3.Text) < 33)
                {
                    key = Convert.ToInt32(textBox3.Text);
                }
                if (Convert.ToInt32(textBox3.Text) >= 33)
                {
                    MessageBox.Show(this, "Введіть свій варіант від 0 до 32");
                }
                string text = textBox1.Text;
                text = text.ToUpper();
                int l = text.Length;
                char[] shifr = new char[l];
                if (key < 33)
                {

                    for (int y = 0; y < l; y++)
                    {
                        for (int i = 0; i < 33; i++)
                        {

                            if (text[y] == alfa[i])
                            {
                                char j = alfa[i + key];
                                shifr[y] = j;

                            }
                        }
                        if (shifr[y] == '\0')
                        {
                            MessageBox.Show(this, "Змініть мову на Українську");
                            goto Exit;
                        }

                    }

                    string res = new String(shifr);


                    if (textBox1.Text == "")
                    {
                        MessageBox.Show(this, "Введіть текст для шифрування");
                    }
                    else
                    {
                        MessageBox.Show(this, "Текст зашифровано!");
                    }

                    textBox2.Text = res;
                    Exit: { }


                }
            }
            else
            {
                MessageBox.Show(this, "Введіть число від 0 до 32");
            }       
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
