using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7128_3442
{
    class Program
    {
      
        public enum choices { INSERT = 1, DELETE, SEARCH, PRINT, EXIT }
        static void Main(string[] args)
        {
            try
            {
                BusCompany BC = new BusCompany();
                for (int i = 0; i < 10; i++)//initialize 10 bus lines into a bus company
                { try { BC.addLine(new BusLine()); }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
                foreach (BusLine item in BC)
                { for (int p = 0; p < 2; p++)//inserts two random busstop into each bus line
                    {
                        try { item.Add(new Route_Bus_Stop()); }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                    }
                    try
                    {
                        Route_Bus_Stop temp = new Route_Bus_Stop();
                        Random i = new Random();
                        for (int k = i.Next(0, 10); k < 10; k++)//inserts temp bus stop into a randome number of buslines
                            BC.BL[k].Add(temp);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
                    Console.WriteLine("Menu:\npress 1 to add a bus line\npress 2 to delete a bus line");
                    Console.WriteLine("press 3 search for a bus line\npress 4 to print the bus lines in the company");
                    Console.WriteLine("press 5 to exit\n");
                choices choice;
                bool b;
                    string s;
                    int num = 0,num1=0;
                    do
                    {
                        Console.WriteLine("enter a number between 1-5");
                        s = Console.ReadLine();//user inputs a number
                        b = int.TryParse(s, out int error);
                        if (b)
                            num = int.Parse(s);
                        else
                            num = -1;
                        choice = (choices)num;
                        switch (choice)
                        {
                            case choices.INSERT://if the user would like to add a bus stop or to add a bus line
                            {
                                try
                                {
                                    Console.WriteLine("press 1 if you would like to add a bus stop, press 2 to add a bus line");
                                    num1 = int.Parse(Console.ReadLine());
                                    if ((num1 == 1) || (num1 == 2))//checks which action the user wanted to do
                                    {
                                        if (num1 == 1)//add bus stop
                                        {
                                            Console.WriteLine("enter bus line you would like to add a stop ");
                                            num1 = int.Parse(Console.ReadLine());
                                            BusLine temp = BC.indexer(num1);//calls indexer to find the bus line in the bus company
                                            Console.WriteLine("enter bus stop you would like to add ");
                                            num1 = int.Parse(Console.ReadLine());
                                            Route_Bus_Stop bustemp = new Route_Bus_Stop(num1);
                                            temp.Add(bustemp);//adds the bus stop to the bus line
                                        }
                                        else//add bus line
                                        {
                                            Console.WriteLine("enter bus line you would like to add");
                                            num1 = int.Parse(Console.ReadLine());
                                            BusLine Linetemp = new BusLine(num1);//creates new bus line
                                            BC.addLine(Linetemp);//adds bus line to the bus company
                                        }
                                    }
                                    else
                                        Console.WriteLine("invalid input");
                                }catch(Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;
                            }
                            case choices.DELETE://if the user would like to delete a bus line or to delete a station from a bus route
                            {
                                try { 
                                    Console.WriteLine("press 1 if you would like to delete a bus line, press 2 to delete a station from a bus route");
                                    num1 = int.Parse(Console.ReadLine());
                                    if ((num1 == 1) || (num1 == 2))//checks which action the user wanted to do
                                    {
                                        if (num1 == 1)//delete a bus line
                                        {
                                            Console.WriteLine("enter bus line you wish to delete");
                                            num1 = int.Parse(Console.ReadLine());
                                            BusLine temp = BC.indexer(num1);//calls indexer to find the bus line in the bus company
                                            BC.removeLine(temp);
                                        }
                                        else//delete a station from a bus route
                                        {
                                            Console.WriteLine("enter from which bus line you wish to delete a stop");
                                            num1 = int.Parse(Console.ReadLine());
                                            BusLine temp = BC.indexer(num1);//calls indexer to find the bus line in the bus company
                                            Console.WriteLine("enter which bus stop you wish to delete");
                                            num1 = int.Parse(Console.ReadLine());
                                            Route_Bus_Stop temp1 = temp.searchStop(num1);
                                            temp.remove(temp1);//removes bus stop from bus line
                                        }
                                    }
                                    else
                                        Console.WriteLine("invalid input");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;
                            }
                            case choices.SEARCH://if the user would like to search bus lines passing through a station or for travel options between two stops.
                            {
                                try { 
                                    Console.WriteLine("press 1 if you would like to search bus lines passing through a station, press 2 for travel options between two stops.");
                                    num1 = int.Parse(Console.ReadLine());
                                    if ((num1 == 1) || (num1 == 2))//checks which action the user wanted to do
                                    {
                                        if (num1 == 1)//search bus lines passing through a specific station
                                        {
                                            Console.WriteLine("enter the bus station");
                                            num1 = int.Parse(Console.ReadLine());
                                            foreach (BusLine item in BC)
                                                if (item.search(num1))//searches for bus stop and returns true if exists in that bus line
                                                    Console.WriteLine("bus stop is in the bus line" +item.LN);

                                        }
                                        else//Print the options for traveling between 2 stations, without changing a bus
                                        {
                                            Console.WriteLine("enter a starting station and a destination station code");
                                            num1 = int.Parse(Console.ReadLine());
                                            int num2 = int.Parse(Console.ReadLine());
                                            foreach (BusLine item in BC)
                                            {
                                                if (item.search(num1) && item.search(num2))//searches for bus stops and returns true if exists in that bus line
                                                    Console.WriteLine((item.subRoute(item.searchStop(num1), item.searchStop(num2))).ToString());
                                            }
                                        }
                                    }
                                    else
                                        Console.WriteLine("invalid input");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;

                            }
                            case choices.PRINT:
                                {
                                try { 
                                    foreach (BusLine item in BC)
                                        Console.WriteLine(item.ToString());//prints the bus line info
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;
                            }
                            case choices.EXIT:
                                break;
                            default:
                                Console.WriteLine("invalid input");
                                break;
                        }
                    
                } while (choice != choices.EXIT);       
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
            }
    }
}
