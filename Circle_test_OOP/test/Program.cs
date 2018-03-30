using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            double largestCircle = 0;
            double largestCircle2 = 0;

            Circle c1 = new Circle(2,5,4);
            Circle c2 = new Circle(2,4,5);
            Circle c3 = new Circle(1,3,6);

            double L1 = c1.Lenth();
            double L2 = c2.Lenth();
            double L3 = c3.Lenth();

            largestCircle = c1.Compare(L1, L2);
            largestCircle2 = c2.Compare(largestCircle, L3);

            if (largestCircle > largestCircle2)
            {
                Console.WriteLine("The largesr circle is {0}", largestCircle);
            }
            else
            {
                Console.WriteLine("The largesr circle is {0}", largestCircle2);
            }       
        }
    }

    class Circle
    {
        private double X;
        private double Y;
        private double Radius;

        public Circle(double x, double y, double R)
        {
            this.X = x;
            this.Y = y;
            this.Radius = R;
        }

       public double Lenth (){
           double L = 2 * Math.PI * Radius;
           return L;
        }

        public double Compare(double com1, double com2)
        {
            double largest = 0;

            if (com1 > com2)
            {
                largest = com1;
            }

            else { largest = com2; }

            return largest;
        }
    }
}
