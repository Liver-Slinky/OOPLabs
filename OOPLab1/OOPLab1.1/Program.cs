using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LR_One_One
{
    class Program
    {
        static private void variantSolution()
        {
            int x = 5;
            int y = 8;
            int z = 10;

            int solution = (2 * x) + (2 * y) - (4 * x * y) + z;
            Console.WriteLine("x={0}, y={1}, z={2}", x, y, z);

            Console.WriteLine("Решение уравнения f(x,y,z)=(2 * x) + (2 * y) - (4 * x * y) + z: {0}", solution);
        }
        static public void MainOne(string[] args)
        {
            string Lab = "Лабораторная работа 1: РАЗРАБОТКА КОНСОЛЬНОГО ПРИЛОЖЕНИЯ";
            string name = "Халиль Ибрахим";
            string group = "ИДБ-23-01";
            string speciality = "09.03.01";
            string residence = "Moscow";
            string fav = "средневековое искусство";
            string hobbies = "Готовим блинчики";

            Console.WriteLine(" {0} \n {1} \n {2} \n {3} \n {4} \n {5} \n {6}",Lab,name,group,speciality,residence,fav,hobbies);

            Console.WriteLine("================================");

            Program.variantSolution();

            Console.ReadKey();


        }
    }
}