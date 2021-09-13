using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_7128_3442
{
    public enum MyEnum { ADDBUS=1, BUSTRAVEL, REPAIR, PRINT, EXIT }
    class Program
    {
        static void Main(string[] args)
        {
            int userL;
            BusList BL = new BusList() ;
            Console.WriteLine("Menu:");
            Console.WriteLine("press 1 to add a bus");
            Console.WriteLine("press 2 to choose a bus to travel with");
            Console.WriteLine("press 3 to repair a bus");
            Console.WriteLine("press 4 to display the mileage of each bus");
            Console.WriteLine("press 5 to exit");
            MyEnum choice;
            bool b;
            string s;
            int num=0,pick=0;
            do
            {
                Console.WriteLine("enter a number between 1-5");
                s=Console.ReadLine();
                b= int.TryParse(s,out int error);
               if (b)
                    num = int.Parse(s);
               else 
                  num=-1;
                choice = (MyEnum)num;
                switch (choice)
                {
                    case MyEnum.ADDBUS:
                        {
                            Console.WriteLine("enter a license number");
                            userL = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter month,day,yr _ _/_ _/_ _ _ _");
                            DateTime userD = new DateTime();
                            userD = DateTime.Parse(Console.ReadLine());
                            BL.AddBus(userL, userD);
                            break;
                        }
                    case MyEnum.BUSTRAVEL:
                        {
                            Console.WriteLine("enter a license number");
                            userL = int.Parse(Console.ReadLine());
                            BL.BusTravel(userL);
                            break;
                        }
                    case MyEnum.REPAIR:
                        {
                            Console.WriteLine("enter a license number");
                            userL = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter 1 for refueling, enter 2 for maintenance");
                            pick = int.Parse(Console.ReadLine());
                            BL.Repair(userL, pick);
                            break;
                        }
                    case MyEnum.PRINT:
                        {
                            BL.Print();
                            break;
                        }
                    case MyEnum.EXIT:
                        break;
                    default:
                        Console.WriteLine("invalid input");
                        break;
                }
            } while (choice !=MyEnum.EXIT);

            Console.ReadKey();
        }
    }
}
/*
 * output:
 * Menu:
press 1 to add a bus
press 2 to choose a bus to travel with
press 3 to repair a bus
press 4 to display the mileage of each bus
press 5 to exit
enter a number between 1-5
1
enter a license number
12345678
enter month,day,yr _ _/_ _/_ _ _ _
04/03/2020
enter a number between 1-5
1
enter a license number
2345678
enter month,day,yr _ _/_ _/_ _ _ _
12/12/2000
enter a number between 1-5
2
enter a license number
12345678
enter a number between 1-5
2
enter a license number
12345678
enter a number between 1-5
2
enter a license number
12345678
cannot travel distance entered
enter a number between 1-5
3
enter a license number
12345678
enter 1 for refueling, enter 2 for maintenance
1
enter a number between 1-5
4
license number: 12345678, kilometrage since last maintenance: 1136
license number: 2345678, kilometrage since last maintenance: 0
enter a number between 1-5
5
*/

