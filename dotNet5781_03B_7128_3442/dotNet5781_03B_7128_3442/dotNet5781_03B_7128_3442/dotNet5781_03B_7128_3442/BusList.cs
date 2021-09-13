using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_7128_3442
{
    class BusList
    {
        public List<Bus> buses = new List<Bus>();

        /// <summary>
        /// this method adds a bus to the bus list
        /// </summary>
        /// <param name="license"></param> the users license number
        /// <param name="userDate"></param> the users inputted date 
        public void AddBus(int license, DateTime userDate)
        {
            int year = userDate.Year;
            if (year < 2018)//checks if we should recieve a 7 digit license or 8 digit
            {
                if (license >= 10000000)
                {
                    Console.WriteLine("invalid license number, must be 7 digits");
                    return;
                }
            }
            else
            {
                if (license < 10000000)
                {
                    Console.WriteLine("invalid license number, must be 8 digits");
                    return;
                }
            }
            Bus temp = new Bus(license, userDate, 0, 0);
            buses.Add(temp);//adds new bus with users data to the list of buses
        }

        /// <summary>
        /// this method searches for a bus that has a certain license number
        /// </summary>
        /// <param name="license"></param> the license number the user inputted
        /// <returns></returns> returns the bus with the same license (if the bus exists)
        /// from the list by reference
        public Bus FindBus(int license)
        {
            Bus temp = new Bus();
            foreach (Bus item in buses) // Loop through Bus with foreach
            {
                if (license == item.L)//checks if that bus has the same license number
                {
                    temp = item;
                    return temp;
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="license"></param> the license number the user inputted
        /// <param name="choice"></param> which action the user wants to do
        public void Repair(int license, int choice)
        {
            Bus temp = new Bus();
            temp = FindBus(license);//temp contained the bus requested by the users inputed license number
            if (temp != null)//if license number is  found in the list
            {
                if (choice == 1)//if the user chose to refuel
                    temp.T = 0;
                else if (choice == 2)//if the user chose maintenance
                {
                    temp.TT = 0;
                    temp.D = DateTime.Now;
                }
                else//users choice isn't valid
                    Console.WriteLine($"choice {choice} is not valid\n");
            }
            else//if license number is not found in the list
                Console.WriteLine($"license number {license} does not exist\n");
        }
        /// <summary>
        /// this method prints all the buses license and km since their last maintenance
        /// </summary>
        public void Print()
        {
            foreach (Bus bus in buses)// Loop through Bus with foreach
            {
                Console.WriteLine($"license number: {bus.L}, kilometrage since last maintenance: {bus.TT} ");
            }

        }
        /// <summary>
        /// this method chooses a bus to travel, and if possible adds the distance the bus traveled in the system
        /// </summary>
        /// <param name="license"></param> the license number the user inputted
        public void BusTravel(int license)
        {
            Bus temp = new Bus();
            temp = FindBus(license);
            if (temp != null)
            {
                var rand = new Random();
                int km = rand.Next(1201);
                bool check = temp.checkTrip(km);
                if (check == true)
                {
                    temp.T = temp.T + km;
                    temp.TT = temp.TT + km;
                }
                else
                    Console.WriteLine("cannot travel distance entered");
            }
            else
                Console.WriteLine("license entered is not valid");
        }
    }
}

