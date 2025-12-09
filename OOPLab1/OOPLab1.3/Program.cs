using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR_One_Three
{

    class Program
    {


        public static void MainThree(string[] args)
        {
            TextWriter save_out = Console.Out;
            TextReader save_in = Console.In;
            var new_out = new StreamWriter(@"output2.txt");
            var new_in = new StreamReader(@"input2.txt");
            Console.SetOut(new_out);
            Console.SetIn(new_in);


            double t, N;
            double X, Y, p;
            t = Convert.ToInt32(Console.ReadLine());
            N = Convert.ToInt32(Console.ReadLine());
            X = Convert.ToDouble(Console.ReadLine());
            Y = Convert.ToDouble(Console.ReadLine());
            p = 0;

            if (t == 0)
            {
                for (int n = 1; n <= N; n++)
                {

                    p += Term(n, Y, X);
                }
            }
            else if (t == 1)
            {
                int n = 1;
                while (n <= N)
                {


                    p += Term(n, Y, X);
                    n++;
                }
            }
            else if (t == 2)
            {
                int n = 1;
                do
                {


                    p += Term(n, Y, X);


                    n++;
                } while (n <= N);
            }
            else
            {
                p = 0;
            }

            Console.WriteLine(String.Format("{0:0.0000000}", p));

            Console.SetOut(save_out); new_out.Flush(); new_out.Close();
            Console.SetIn(save_in); new_in.Close();
        }

        static double Term(int n, double Y, double X)
        {
            return ((Math.Pow(X, n) - Math.Pow(Y, n))/(n*(n+2)));
        }


    }


}
