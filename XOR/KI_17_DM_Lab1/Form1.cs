using System;
using System.Windows.Forms;

namespace KI_17_DM_Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Введіть значення!");
            }

            string A = string.Empty;
            string B = string.Empty;

            string tmp = string.Empty;

            string tmpA = string.Empty;
            string tmpB = string.Empty;

            A = textBox1.Text;
            B = textBox2.Text;

            for (int i = 0; i < A.Length; i++)  
            {
                for (int j = 0; j < B.Length; j++)
                {
                    if (A[i] == B[j])
                    {
                        tmp += A[i];
                    }
                }
            }

            char[] tm = tmp.ToCharArray();

            tmpA = A.Trim(tm);
            tmpB = B.Trim(tm);

            textBox3.Text = tmpA + tmpB;
        }
    }
}
