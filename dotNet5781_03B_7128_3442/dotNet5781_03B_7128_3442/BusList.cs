using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace dotNet5781_03B_7128_3442
{
    public class BusList : IEnumerable
    {
        public List<Bus> buses = new List<Bus>();

        public List<Bus> B
        {
            get { return buses; }
            set { buses = value; }
        }

        /// <summary>
        /// this method adds a bus to the bus list
        /// </summary>
        /// <param name="license"></param> the users license number
        /// <param name="userDate"></param> the users inputted date 
        public void AddBus(Bus bus)
        {
            int year = bus.D.Year;
            if (year < 2018)//checks if we should recieve a 7 digit license or 8 digit
            {
                if (bus.L.Length >= 10)
                {
                    throw new FormatException ("invalid license number, must be 7 digits");
                }
            }
            else
            {
                if (bus.L.Length < 10)
                {
                    throw new FormatException ("invalid license number, must be 8 digits");
                }
            }
            //Bus temp = new Bus(license, userDate, 0, 0);
            buses.Add(bus);//adds new bus with users data to the list of buses
        }

        public IEnumerator GetEnumerator()
        {
            return buses.GetEnumerator();
        }
        /// <summary>
        /// this method searches for a bus that has a certain license number
        /// </summary>
        /// <param name="license"></param> the license number the user inputted
        /// <returns></returns> returns the bus with the same license (if the bus exists)
        /// from the list by reference
        public Bus FindBus(string license)
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
        /// this method repairs the bus by refueling or maintaining the bus
        /// </summary>
        /// <param name="license"></param> the license number the user inputted
        /// <param name="choice"></param> which action the user wants to do
        public void Repair(string license, int choice)
        {
            Bus temp = new Bus();
            temp = FindBus(license);//temp contained the bus requested by the users inputed license number
            if (temp != null)//if license number is  found in the list
            {
                if (choice == 1)//if the user chose to refuel
                {
                    temp.ST = status.REFUELING;
                    Console.WriteLine("Bus is refueling");
                    temp.T = 0;
                    Console.WriteLine("Bus is ready to go");
                    temp.ST = status.READY2GO;
                }
                else if (choice == 2)//if the user chose maintenance
                {
                    temp.ST = status.BEINGSERV;
                    Console.WriteLine("Bus is being serviced");
                    temp.TT = 0;
                    temp.D = DateTime.Now;
                    Console.WriteLine("Bus is ready to go");
                    temp.ST = status.READY2GO;
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
        public void BusTravel(string license)
        {
            Bus temp = new Bus();
            temp = FindBus(license);//finds bus in bus list to see if the bus exists
            if (temp != null)//checks if the bus exists
            {
                var rand = new Random();
               temp.KM= rand.Next(1201);//chooses a random distance for the bus to travel
                bool check = temp.checkTrip();
                if (check == true)//checks if the bus can travel the distance that was requested
                {
                    temp.ST = status.MIDOFRIDE;
                    temp.T = temp.T + temp.KM;
                    temp.TT = temp.TT + temp.KM;
                    temp.ST = status.READY2GO;
                }
                else
                    Console.WriteLine("cannot travel distance entered");
            }
            else
                Console.WriteLine("license entered is not valid");
        }

    }
}

