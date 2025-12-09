using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR_One_One;
using LP_One_Two;
using OopMonitoringLab;

namespace OOPLab1
{
    internal class Program
    {
        static public void Main(string[] args)
        {

            Console.WriteLine("Choose which lab to test:\n1:Lab 1.1\n2:Lab 1.2\n3:Lab 1.3\n4:Lab 2");
            int x =Convert.ToInt32(Console.ReadLine());
            switch (x)
            {
                case 1:
                    LR_One_One.Program.MainOne(args);
                    break;
                case 2:
                    LP_One_Two.Program.MainTwo(args);
                    break;
                case 3:
                    LR_One_Three.Program.MainThree(args);
                    break;
                case 4:
                    OopMonitoringLab.Program.MainTwo(args);
                    break;
                default:
                    Console.Error.WriteLine("Unexpected value!");
                    break;
            }

        }
    }
}
