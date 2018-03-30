using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PRO_LAB1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Converter(string text, int [,] arr )
        {
            int k = 0;
            int n = Convert.ToInt32(textBox1.Text);
            int[] a = text.Split(' ','\n').Select(int.Parse).ToArray();
            if (n == 1)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        arr[i, j] = a[k];
                        k++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        arr[i, j] = a[k];
                        k++;
                    }
                }
            }
        }

        private void Write_Matrix(double[,] arr, string text)
        {
            int n = Convert.ToInt32(textBox1.Text);
            string writePath = @"C:\Users\фокс\Documents\visual studio 2013\Projects\PRO_LAB1\PRO_LAB1\result.txt";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(text);
                        sw.WriteLine(arr[i, j]);
                        sw.Close();
                    }
                }
            }
        }
        private void Write_Matrix1(double[] arr, string text)
        {
            int n = Convert.ToInt32(textBox1.Text);
            string writePath = @"C:\Users\фокс\Documents\visual studio 2013\Projects\PRO_LAB1\PRO_LAB1\result.txt";
            for (int i = 0; i < n; i++)
            {
                 using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                 {
                     sw.WriteLine(text);
                     sw.WriteLine(arr[i]);
                     sw.Close();
                 }  
            }
        }
        private void Write_Const(double arr, string text)
        {
            int n = Convert.ToInt32(textBox1.Text);
            string writePath = @"C:\Users\фокс\Documents\visual studio 2013\Projects\PRO_LAB1\PRO_LAB1\result.txt";
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                    sw.WriteLine(arr);
                    sw.Close();
                }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            int[,] arrA = new int[n, n];
            int[,] arrA1 = new int[n, n];
            int[,] arrA2 = new int[n, n];
            int[,] arrB2 = new int[n, n];
            double[,] arrC = new double[n, n]; 
            int[] b1 = new int[n];
            int[] c1 = new int[n];
            double[] y1 = new double[n*2];
            double[] y_1 = new double[n];
            double[] y2 = new double[n];
            double[,] Y3 = new double[n, n];
            double[] X = new double[n];
            double [] y1y2 = new double[n];
            double [] Y3y1y2 = new double[n];
            double[] Y3y2 = new double[n];

            if (textBox1.Text == "")
            {
                MessageBox.Show(this, "Введіть порядок матриць");
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show(this, "Введіть матрицю А");
            }
            else
            {
                Converter(textBox2.Text, arrA);
            }

            if (textBox3.Text == "")
            {
                MessageBox.Show(this, "Введіть матрицю А1");
            }
            else
            {
               Converter(textBox3.Text, arrA1); 
            }

            if (textBox4.Text == "")
            {
                MessageBox.Show(this, "Введіть матрицю А2");
            }
            else
            {
                Converter(textBox4.Text, arrA2);
            }

            if (textBox5.Text == "")
            {
                MessageBox.Show(this, "Введіть матрицю B2");
            }
            else
            {
                Converter(textBox5.Text, arrB2);
            }

            if (textBox6.Text == "")
            {
                MessageBox.Show(this, "Введіть вектор-стовпець b1");
            }
            else
            {
                b1 = textBox6.Text.Split(' ', '\n').Select(int.Parse).ToArray();
            }

            if (textBox7.Text == "")
            {
                MessageBox.Show(this, "Введіть вектор-стовпець c1");
            }
            else
            {
                c1 = textBox7.Text.Split(' ', '\n').Select(int.Parse).ToArray();
            }

            // Find matrix C 
            double k = 1;
            double f = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    arrC[i, j] = 1 / (k + f*f);
                    f++;   
                }
                k++;
            }
            Write_Matrix(arrC, "Matrix C:");

            // Find matrix y1
            int size = 0;
            double _i = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i % 2 == 0)
                    {
                        _i = i;
                        y1[size] = arrA[i, j]*(1/((_i*_i)+2));
                        size++;
                        Write_Const(1 / ((_i * _i) + 2), "b" + _i + ":");
                    }
                    else
                    {
                        _i = i;
                        y1[size] = arrA[i, j]*(1/_i);
                        size++;
                        Write_Const(1/_i, "b" + _i + ":");
                    }
                }
            }
            int size_y1 = 0;
            for (int i = 0; i < size; i = i+2)
            {
                y_1[size_y1] = y1[i] + y1[i+1];
                textBox8.Text += y_1[size_y1] + "\r" + "\n";
                size_y1++;
            }
            Write_Matrix1(y_1, "Matrix y1:");

            // Find matrix y2
            int size_y2 = 0;
            for (int i = 0; i < n; i++)
            {
                int size_y_2 = 0;
                for (int j = 0; j < n; j = j + 2)
                {
                    if (n == 1)
                    {
                        y2[size_y2] = arrA1[i, j] * ( b1[size_y_2] + c1[size_y_2]);
                        textBox11.Text += y2[size_y2] + " ";
                    }
                    else
                    {
                        y2[size_y2] = arrA1[i, j] * (b1[size_y_2] + c1[size_y_2]) + arrA1[i, j + 1] * (b1[size_y_2 + 1] + c1[size_y_2 + 1]);
                        size_y_2 = size_y_2 + 2;
                        textBox11.Text += y2[size_y2] + " ";
                        size_y2++;
                    }
                }
            }
            Write_Matrix1(y2, "Matrix y2:");

            // Find matrix Y3
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Y3[i, j] = arrA2[i, j] * (arrB2[i, j] - arrC[i, j]);
                    textBox12.Text += Y3[i, j] + " ";
                }
                textBox12.Text += "\r" + "\n";
            }
            Write_Matrix(Y3, "Matrix Y3:");

            // Find y1+y2
            for (int i = 0; i < n; i++)
            {
                y1y2[i] = y_1[i] + y2[i];
            }
            Write_Matrix1(y1y2, "y2+y2':");

            // Find Y3(y1+y2)
            int size_Y3 = 0;
            for (int i = 0; i < n; i = i + 2)
            {
                for (int j = 0; j < n; j++)
                {
                    if (n == 1)
                    {
                        Y3y1y2[size_Y3] = Y3[i, j] * y1y2[size_Y3];
                    }
                    else
                    {
                        Y3y1y2[size_Y3] = Y3[i, j] * y1y2[size_Y3] + Y3[i, j + 1] * y1y2[size_Y3 + 1];
                    }
                }
            }
            Write_Matrix1(Y3y1y2, "A(y1+y2):");

            // Find Y3^2*y2
            int size_Y = 0;
            for (int i = 0; i < n; i = i + 2)
            {
                for (int j = 0; j < n; j++)
                {
                    if (n == 1)
                    {
                        Y3y2[size_Y] = Y3[i, j] * Y3[i, j] * y2[size_Y];
                    }
                    else
                    {
                        Y3y2[size_Y] = Y3[i, j] * Y3[i, j] * y2[size_Y] + Y3[i, j + 1] * Y3[i, j+1] * y2[size_Y+1];
                    }
                }
            }
            Write_Matrix1(Y3y2, "Y3^2*y2:");

            // Find X
            for (int i = 0; i < n; i++)
            {
                X[i] = Y3y2[i] + Y3y1y2[i];
                textBox13.Text += X[i] + " ";
                
            }
            Write_Matrix1(X, "RESULT Matrix X:");
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            string writePath = @"C:\Users\фокс\Documents\visual studio 2013\Projects\PRO_LAB1\PRO_LAB1\result.txt";
            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
            {
                sw.Close();
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
