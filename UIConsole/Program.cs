using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BO;

namespace UIConsole
{
    class Program
    {
        //static
        IBL bl1 = BLFactory.GetBL();
        static void Main(string[] args)
        {
            IBL bl1 = BLFactory.GetBL();
            bl1.getAllBusStops();
            Console.WriteLine(bl1.getBusStop(1234).ToString());
            Console.WriteLine( bl1.getBusStop(100).ToString());

            // bl1.
           // Console.Write("Please enter how many days back: ");
           // int days = int.Parse(Console.ReadLine());
           // for (int d = days; d >= 0; --d)
           // {
           //     Weather w = bl1.GetWeather(d);
           //     Console.WriteLine($"{d} days before - Feeling was: {w.Feeling} Celsius degrees");
           // }
           // try
           // {
           //     //Console.WriteLine(bl1.getBusStop(123));
           //     BusStop bs = new BusStop
           //     {
           //         StationCode = 45,
           //         Address = "nachal luz",
           //         Availability = Status.NOTINUSE,
           //         Longitude = 43.1,
           //         Latitude = 21.2,
           //         BusLinesUsingStation = new List<BusLine>(),
           //     };
           //     BusLine bll = new BusLine
           //     { ID = 22,
           //         LineNumber = 345,
           //         BegStopNum = 123,
           //         EndStopNum = 456,
           //         Region = Region.GENERAL,
           //         StopsInLine = new List<BusStop>(),
           //     };
           //     bl1.addBusStop(bs);
           //     bl1.addBusStop2Line(bs, 1234,1);
           //     bl1.getBusStopsInLine(1234);
           //     //bl1.removeBusStopFromLine(bs,1234);
           //     //bl1.getBusStopsInLine(1234);
           //     //bl1.addBusLine(bll);
           //     //bl1.addBusStop2Line(bs, 22,1);
           //     //bl1.getBusLine(22);
           //     //  Console.WriteLine(bl1.getBusStop(45));
           //     // Console.WriteLine(bl1.getBusStop(0));
           //     //bl1.addBusStop2Line(bs, 23);
           //     //bl1.getBusLine(12);
           //     //bl1.getBusStopsInLine(12);
           // }
           // catch (BO.BadBusStopIdException ex)
           // {
           //     Console.WriteLine(ex.Message);
           // }
           // catch (BO.BadBusLineIdException ex)
           // {
           //     Console.WriteLine(ex.Message);
           // }
           // List<BO.BusStop> lbs = bl1.getAllBusStops().ToList();
           // foreach(var item in lbs)
           //     Console.WriteLine(item.ToString());
           // Console.WriteLine(bl1.getBusLine(1234));
           //// lbs = bl1.getBusStopsInLine(1234).ToList();
           //// Console.WriteLine(lbs);
           //// Console.WriteLine(bl1.getBusStopsInLine(1234));
           // BusLine bl = new BusLine();
          
        }
        
    }
}
