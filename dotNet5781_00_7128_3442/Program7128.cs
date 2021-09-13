using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_7128_3442
{
    partial class Program 
    {
        static void Main(string[] args)
        {
            Welcome7128();
            Welcome3442();
            Console.ReadKey();
        }
        static partial void Welcome3442();
        private static void Welcome7128()
        {
            string name;
            Console.WriteLine("Enter your name:");
            name = Console.ReadLine();
            Console.WriteLine($"{name},welcome to my first console application");
        }
    }
    
}


/*
output:
Enter your name:
tamar
tamar,welcome to my first console application
*/
