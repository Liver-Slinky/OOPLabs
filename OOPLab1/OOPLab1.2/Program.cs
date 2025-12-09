using System;
using System.IO;

namespace LP_One_Two
{
    class Program
    {
        public static void MainTwo(string[] args)
        {
            TextWriter save_out = Console.Out;
            TextReader save_in = Console.In;
            var new_out = new StreamWriter(@"output.txt");
            var new_in = new StreamReader(@"input.txt");
            Console.SetOut(new_out);
            Console.SetIn(new_in);


            double a=0, b=0, c=0, d=0, e=0;
            double s, k;
            a = Convert.ToDouble(Console.ReadLine());
            b = Convert.ToDouble(Console.ReadLine());
            c = Convert.ToDouble(Console.ReadLine());
            d = Convert.ToDouble(Console.ReadLine());
            e = Convert.ToDouble(Console.ReadLine());


            if ((a > 0 && a < Math.Pow(10, 5)) && (b > 0 && b < Math.Pow(10, 5)) && (c > 0 && c < Math.Pow(10, 5)) && (d > 0 && d < Math.Pow(10, 5)) && (e > 0 && e < Math.Pow(10, 5)))
            {
                s = (Math.Pow(a, 2)) / (Math.Pow(c, 2) - Math.Pow(e, 2));
                k = (Math.Pow(a - Math.Pow(c, 2), 0.5)) / (Math.Pow(Math.Pow(c, 2) - Math.Pow(d, 3), 0.5));

                if (double.IsNaN(s)||double.IsNaN(k))
                {
                    Console.Error.WriteLine("ERROR");
                }

                Console.WriteLine(String.Format("{0:0.000} \n {1:0.000}", s, k));
            }
            else
            {
                Console.WriteLine("ERROR");

            }

            Console.SetOut(save_out); new_out.Close();
            Console.SetIn(save_in); new_in.Close();
        }
    }
}