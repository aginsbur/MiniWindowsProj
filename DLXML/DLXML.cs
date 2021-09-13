using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using DO;
using DALAPI;

namespace DAL
{
	sealed class DLXml : IDAL
	{
		#region***singelton***
		static readonly DLXml instance = new DLXml();
		static DLXml() { }// static ctor to ensure instance init is done just before first usage
		DLXml() { } // default => private
		public static DLXml Instance { get => instance; }// The public Instance property to use
		#endregion
		#region***DS XML Files***

		string busStopPath = @"BusStopsXml.xml";//XMLSerializer
		string busLinePath = @"BusLinesXml.xml"; //XMLSerializer
		string adjacentStopPath = @"AdjacentStopsXml.xml"; //Xelement
		string lineStopPath = @"LineStopsXml.xml"; //XMLSerializer
		string busPath = @"BusesXml.xml";//XElement
		string lineOnTripPath = @"LineOnTripXml.xml"; //Xelement
		string runningNumberPath = @"runningNumber.xml";
		string userPath = @"UserXml.xml";//XMLSerializer
		#endregion
        #region ***CRUD BusStop***
        public IEnumerable<BusStop> GetAllBusStops()//gets a list of bus stops
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopPath);
            return from busStop in ListBusStops
                   select busStop; 
        }
        public IEnumerable<BusStop> GetAllBusStopsBy(Predicate<BusStop> predicate)//gets a list of bus stops according to a condition
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopPath);
            return from busStop in ListBusStops
                   where predicate(busStop)
                   select busStop;
        }
         public BusStop GetBusStop(int id)
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopPath);
            DO.BusStop bs = ListBusStops.Find(b => b.StationCode == id);
            if (bs != null)
                return bs; 
            else
                throw new DO.BadBusStopIdException(id, $"bad bus stop id: {id}");
        }
        public void AddBusStop(BusStop busStop)
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopPath);
            if (ListBusStops.FirstOrDefault(b => b.StationCode == busStop.StationCode) != null)
                throw new DO.BadBusStopIdException(busStop.StationCode, $"Bus Stop with station code { busStop.StationCode } already exists!");
            ListBusStops.Add(busStop);
            XMLTools.SaveListToXMLSerializer(ListBusStops,busStopPath);
        }
        public void UpdateBusStop(BusStop myBusStop, int id)
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopPath);
            //find the busStop
            DO.BusStop bs = ListBusStops.Find(b => b.StationCode == id);
            if (bs != null)
            {
                ListBusStops.Remove(bs);
                ListBusStops.Add(myBusStop);
                XMLTools.SaveListToXMLSerializer(ListBusStops, busStopPath);
            }
            else
                throw new DO.BadBusStopIdException(myBusStop.StationCode, "bus stop not found!");
        }
        public void DeleteBusStop(int stationCode)
        {
            List<BusStop> ListBusStops = XMLTools.LoadListFromXMLSerializer<BusStop>(busStopPath);
            //if bus stop does not exist
            if (!ListBusStops.Exists(item => item.StationCode == stationCode))
            {
                throw new BadBusStopIdException(stationCode);
            }
           ListBusStops.RemoveAll(item => item.StationCode == stationCode);//delete the bus stop from the data source         
            XMLTools.SaveListToXMLSerializer(ListBusStops, busStopPath);
        }
        #endregion
        #region***CRUD, BusLine***
        public IEnumerable<BusLine> GetAllBusLines()//gets a list of bus lines
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            return from busLine in ListBusLines
                   select busLine;
        }
        public IEnumerable<BusLine> GetAllBusLinessBy(Predicate<BusLine> predicate)//gets a list of bus lines according to a condition
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            return from busLine in ListBusLines
                   where predicate(busLine)
                   select busLine;
        }
        public BusLine GetBusLine(int id)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            DO.BusLine bl = ListBusLines.Find(b => b.ID == id);//bl holds the busline to get
            if (bl != null)
                return bl;
            else
                throw new DO.BadBusLineIdException(id, $"bad bus line id: {id}");
        }
        public void AddBusLine(BusLine busLine)
        {  List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            //check if a bus line with the line number already exists in the region
            if (ListBusLines.FirstOrDefault(b => b.LineNumber == busLine.LineNumber && b.Region == busLine.Region) != null)
                throw new DO.BadBusLineIdException(busLine.LineNumber, "Duplicate bus line number");
            if (busLine.BegStopNum == busLine.EndStopNum)//if the new bus lines begin and end stop have the same station number
                throw new DO.BadBusStopIdException(busLine.BegStopNum, "Duplicate begin and end bus stop number!");
            busLine.ID = XMLTools.getRuningNumber(runningNumberPath);
            ListBusLines.Add(busLine);
            XMLTools.SaveListToXMLSerializer(ListBusLines, busLinePath);
        }
        public void UpdateBusLine(BusLine newBusLine)//updates a busLine
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            DO.BusLine bl = ListBusLines.Find(b => b.ID == newBusLine.ID);
            if (bl != null)
            {
               ListBusLines.Remove(bl);
               ListBusLines.Add(newBusLine);
                XMLTools.SaveListToXMLSerializer(ListBusLines, busLinePath);
            }
            else
                throw new DO.BadBusLineIdException(newBusLine.ID, $"bad student id: {newBusLine.ID}");

        }
        public void DeleteBusLine(int id)
        {
            List<BusLine> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLine>(busLinePath);
            //find the busLine
            DO.BusLine bl =ListBusLines.Find(b => b.ID == id);
            if (bl != null)
            {
                ListBusLines.Remove(bl);
                XMLTools.SaveListToXMLSerializer(ListBusLines, busLinePath);
            }
            else
                throw new DO.BadBusLineIdException(id, "bus line not found!");
        }
        #endregion***BusLine***
        #region***CRUD, AdjacentStop***
        public IEnumerable<AdjacentStops> GetAllAdjacentStops()//gets a list of AdjacentStops
        {
            XElement adjStopsRootElem = XMLTools.LoadListFromXMLElement(adjacentStopPath);

            return (from adjS in adjStopsRootElem.Elements()
                    select new AdjacentStops()
                    {
                        StopCode1 = Int32.Parse(adjS.Element("StopCode1").Value),
                        StopCode2 = Int32.Parse(adjS.Element("StopCode2").Value),
                        Distance = Double.Parse(adjS.Element("Distance").Value),
                        AvgTravelTime = TimeSpan.Parse(adjS.Element("AvgTravelTime").Value)
                    }
                   );
        }
        public IEnumerable<AdjacentStops> GetAllAdjacentStopsBy(Predicate<AdjacentStops> predicate)//gets a list of AdjacentStops according to a condition
        {
            XElement adjStopsRootElem = XMLTools.LoadListFromXMLElement(adjacentStopPath);
            return (from adjS in adjStopsRootElem.Elements()
                    let adjS1= new AdjacentStops()
                    {
                        StopCode1 = Int32.Parse(adjS.Element("StopCode1").Value),
                        StopCode2 = Int32.Parse(adjS.Element("StopCode2").Value),
                        Distance = Double.Parse(adjS.Element("Distance").Value),
                        AvgTravelTime = TimeSpan.Parse(adjS.Element("AvgTravelTime").Value)
                        
                    }
                    where predicate(adjS1)
                    select adjS1
                   );
        }
        public AdjacentStops GetAdjacentStops(int id1, int id2)
        {
            XElement adjStopsRootElem = XMLTools.LoadListFromXMLElement(adjacentStopPath);

            AdjacentStops adjStop = (from adjS in adjStopsRootElem.Elements()
                                     where (Int32.Parse(adjS.Element("StopCode1").Value) == id1) && (Int32.Parse(adjS.Element("StopCode2").Value) == id2)
                                     select new AdjacentStops()
                                     {
                                         StopCode1 = Int32.Parse(adjS.Element("StopCode1").Value),
                                         StopCode2 = Int32.Parse(adjS.Element("StopCode2").Value),
                                         Distance = Double.Parse(adjS.Element("Distance").Value),
                                         AvgTravelTime = TimeSpan.Parse(adjS.Element("AvgTravelTime").Value)
                                     }
                        ).FirstOrDefault();

            if (adjStop == null)
                throw new DO.BadBusStopIdException(id1, $"bad bus stop id: {id1}");
            return adjStop;

        }
        public void AddAdjacentStops(AdjacentStops busStops)
        {
            XElement adjStopRootElem = XMLTools.LoadListFromXMLElement(adjacentStopPath);

            XElement adjStops1 = (from adj in adjStopRootElem.Elements()
                                where ((Int32.Parse(adj.Element("StopCode1").Value) ==busStops.StopCode1)&& (Int32.Parse(adj.Element("StopCode2").Value)==busStops.StopCode2))
                              select adj).FirstOrDefault();

            if (adjStops1 != null)
                throw new DO.BadBusStopIdException(busStops.StopCode1, "Duplicate bus stop ID for adjacent stops");
            XElement adjStopsElem = new XElement("AdjacentStops",
                                   new XElement("StopCode1",busStops.StopCode1),
                                   new XElement("StopCode2", busStops.StopCode2),
                                   new XElement("Distance",busStops.Distance),
                                   new XElement("AvgTravelTime",busStops.AvgTravelTime.ToString())
                                   );
            adjStopRootElem.Add(adjStopsElem);
            XMLTools.SaveListToXMLElement(adjStopRootElem,adjacentStopPath);
        }
        public void UpdateAdjacentStops(AdjacentStops adjStops)
        {
            XElement adjStopRootElem = XMLTools.LoadListFromXMLElement(adjacentStopPath);

            XElement adj = (from a in adjStopRootElem.Elements()
                            where ((Int32.Parse(a.Element("StopCode1").Value) == adjStops.StopCode1) || (Int32.Parse(a.Element("StopCode2").Value) == adjStops.StopCode2))
                            select a).FirstOrDefault();

            if (adj != null)
            {
                adj.Element("StopCode1").Value = adjStops.StopCode1.ToString();
                adj.Element("StopCode2").Value= adjStops.StopCode2.ToString();
                adj.Element("Distance").Value = adjStops.Distance.ToString();
                adj.Element("AvgTravelTime").Value= adjStops.AvgTravelTime.ToString();
                XMLTools.SaveListToXMLElement(adjStopRootElem,adjacentStopPath);
            }
            else
                throw new DO.BadBusStopIdException(adjStops.StopCode1, $"bad bus stop id: {adjStops.StopCode1}");
        }
        public void DeleteAdjacentStops(int id1, int id2)
        {
            XElement adjStopRootElem = XMLTools.LoadListFromXMLElement(adjacentStopPath);

            XElement adj = (from a in adjStopRootElem.Elements()
                            where ((Int32.Parse(a.Element("StopCode1").Value) ==id1) && (Int32.Parse(a.Element("StopCode2").Value) == id2))
                            select a).FirstOrDefault();

            if (adj != null)
            {
                adj.Remove(); 
                XMLTools.SaveListToXMLElement(adjStopRootElem, adjacentStopPath);
            }
            else
                throw new DO.BadBusStopIdException(id1, $"bad bus stop id: {id1}");
        }
        #endregion***AdjacentStops***
        #region***CRUD LineStop***
        public IEnumerable<LineStop> GetAllLineStop()//gets a list of stops on a line
        {
            List<LineStop> ListBusLineStops = XMLTools.LoadListFromXMLSerializer<LineStop>(lineStopPath);
            return from busLineStop in ListBusLineStops
                   select busLineStop;
        }
        /// <summary>
        /// gets a collection of bus lines stops with a condition
        /// </summary>
        /// <param name="predicate"></param>condition of which line stop to get
        /// <returns></returns>
        public IEnumerable<LineStop> GetAllLineStopBy(Predicate<LineStop> predicate)//gets a list of stops on a line according to a condition
        {
            List<LineStop> ListBusLineStops = XMLTools.LoadListFromXMLSerializer<LineStop>(lineStopPath);
            return from busLineStop in ListBusLineStops
                   where predicate(busLineStop)
                   select busLineStop;
        }
        public LineStop GetLineStop(int stopId, int lineId)
        {
            List<LineStop> ListBusLineStops = XMLTools.LoadListFromXMLSerializer<LineStop>(lineStopPath);
            DO.LineStop ls = ListBusLineStops.Find(l => (l.StationCode == stopId) && (l.LineID == lineId));//bl holds the busLineStop to get
            // try { Thread.Sleep(2000); } catch (ThreadInterruptedException ex) { }
            if (ls != null)
                return ls;
            else if (ListBusLineStops.Find(l => (l.LineID == lineId)) != null)//if the line stop with stopId dosen't exist
                throw new DO.BadBusStopIdException(stopId, $"bad bus stop id: {stopId}");
            else//if the line stop with LineId dosen't exist
                throw new DO.BadBusLineIdException(lineId, $"bad bus line id: {lineId}");
        }
        public void AddLineStop(LineStop lineStop)
        {
            List<LineStop> ListBusLineStops = XMLTools.LoadListFromXMLSerializer<LineStop>(lineStopPath);
            if (ListBusLineStops.FirstOrDefault(l => (l.LineID == lineStop.LineID) && (l.StationCode == lineStop.StationCode)) != null)
                throw new DO.BadBusStopIdException(lineStop.LineID, "Duplicate line stop ID");
            ListBusLineStops.Add(lineStop);
            XMLTools.SaveListToXMLSerializer(ListBusLineStops, lineStopPath);
        }
        public void UpdateLineStop(LineStop lineStop)//updates a single line stops index in a bus line
        {
            List<LineStop> ListBusLineStops = XMLTools.LoadListFromXMLSerializer<LineStop>(lineStopPath);
            DO.LineStop ls =ListBusLineStops.Find(l => (l.LineID == lineStop.LineID) && (l.StationCode == lineStop.StationCode));
            if (ls != null)
            {
                ListBusLineStops.Remove(ls);//remove the line stop from data source
                ListBusLineStops.Add(lineStop);//insert the updated line stop to data source
                XMLTools.SaveListToXMLSerializer(ListBusLineStops, lineStopPath);
            }
            else
                throw new DO.BadBusLineIdException(lineStop.LineID, $"bad line stop id: {lineStop.LineID}");
        }
      /// <summary>
      /// updates a line stop indices in a line
      /// </summary>
      /// <param name="predicate"></param>
      public void UpdateLineStopByLine(Predicate<LineStop> predicate)
        {
            List<LineStop> ListBusLineStops = XMLTools.LoadListFromXMLSerializer<LineStop>(lineStopPath);
            int index = 1;
            IEnumerable<DO.LineStop> ls = ListBusLineStops.FindAll(predicate);//finds all line stops that fill the condition
            if (ls.Count() != 0)
            {  
                foreach (var lineStop in ls)
                {
                    ListBusLineStops.Remove(lineStop);//remove the lineStop 
                    lineStop.StationIndex = index++;//update the index of the lineStop
                    ListBusLineStops.Add(lineStop);//add the updated line stop 
                }
                XMLTools.SaveListToXMLSerializer(ListBusLineStops, lineStopPath);
            }
        }
     /// <summary>
     /// updates line stop station code
     /// </summary>
     /// <param name="PrevStationCode"></param>
     /// <param name="NewStationCode"></param>
     public void UpdateLineStopByBusStop(int PrevStationCode, int NewStationCode)
        {
            List<LineStop> ListBusLineStops = XMLTools.LoadListFromXMLSerializer<LineStop>(lineStopPath);
            IEnumerable<DO.LineStop> ls =ListBusLineStops.FindAll(x => x.StationCode == PrevStationCode);//finds all bus stops that have the previous station code
            foreach (var lineStop in ls)
            {
                ListBusLineStops.Remove(lineStop);//remove the lineStop 
                lineStop.StationCode = NewStationCode;//update the station code of the lineStop
                ListBusLineStops.Add(lineStop);//add the updated line stop 
            }
            XMLTools.SaveListToXMLSerializer(ListBusLineStops, lineStopPath);
        }
        /// <summary>
        /// deletes a line Stop from the data source
        /// </summary>
        /// <param name="lineId"></param>line id of the linestop to delete
        /// <param name="stationCode"></param>station code the linestop to delete
        public void DeleteLineStop(int lineId, int stationCode)
        {
            List<LineStop> ListBusLineStops = XMLTools.LoadListFromXMLSerializer<LineStop>(lineStopPath);
            if (!ListBusLineStops.Exists(item => (item.LineID == lineId) && (item.StationCode == stationCode)))
            {
                if (!ListBusLineStops.Exists(item => (item.LineID == lineId)))//if the line id is incorrect
                    throw new BadBusLineIdException(lineId);
                else//if the station code is invalid
                    throw new BadBusStopIdException(stationCode);
            }
            ListBusLineStops.RemoveAll(item => (item.LineID == lineId) && (item.StationCode == stationCode));
            XMLTools.SaveListToXMLSerializer(ListBusLineStops, lineStopPath);
        }
        /// <summary>
        /// deletes all line stops with a condition
        /// </summary>
        /// <param name="func"></param>
        public void DeleteLineStopBy(Predicate<DO.LineStop> predicate)
        {
            List<LineStop> ListBusLineStops = XMLTools.LoadListFromXMLSerializer<LineStop>(lineStopPath);
            IEnumerable<DO.LineStop> ls = ListBusLineStops.FindAll(predicate);//finds all line stops that fill the condition
            if (ls.Count() != 0)
            {
                foreach (var lineStop in ls)
                {
                    ListBusLineStops.Remove(lineStop);//remove the lineStop  
                }
                XMLTools.SaveListToXMLSerializer(ListBusLineStops, lineStopPath);
            }
            else throw new DO.BadBusStopIdException(ls.First().StationIndex, "could not find line stop to delete");
        }
        #endregion***CRUD LineStop***
        #region***CRUD, LineOnTrip***
        public IEnumerable<LineOnTrip> GetAllLinesOnTrip()//gets a list of lines on trip
        {
            XElement lineOnTripRootElem = XMLTools.LoadListFromXMLElement(lineOnTripPath);

            return (from lot in lineOnTripRootElem.Elements()
                    select new LineOnTrip()
                    {LineId=Int32.Parse(lot.Element("LineId").Value),
                     StartTime= TimeSpan.Parse(lot.Element("StartTime").Value)
                    }
                   );
        }
        public IEnumerable<LineOnTrip> GetAllLinesOnTripBy(Predicate<LineOnTrip> predicate)//gets a list of lines on trip according to a condition
        {
            XElement lineOnTripRootElem = XMLTools.LoadListFromXMLElement(lineOnTripPath);
            return (from lot in lineOnTripRootElem.Elements()
                    let lot1 = new LineOnTrip()
                    {
                        LineId = Int32.Parse(lot.Element("LineId").Value),
                        StartTime = TimeSpan.Parse(lot.Element("StartTime").Value)
                    }
                    where predicate(lot1)
                    select lot1
                   );
        }
        public void AddLineOnTrip(LineOnTrip lineOnTrip)
        {
            XElement lineOnTripRootElem = XMLTools.LoadListFromXMLElement(lineOnTripPath);

            XElement lot1 = (from lot in lineOnTripRootElem.Elements()
                             where (Int32.Parse(lot.Element("LineId").Value) == lineOnTrip.LineId)&& (TimeSpan.Parse(lot.Element("StartTime").Value) == lineOnTrip.StartTime)
                             select lot).FirstOrDefault();

            if (lot1 != null)
                throw new DO.BadBusLineIdException(lineOnTrip.LineId, "Duplicate line on trip");
            XElement lotElem = new XElement("LineOnTrip",
                                   new XElement("LineId",lineOnTrip.LineId),
                                   new XElement("StartTime",lineOnTrip.StartTime.ToString())
                                   );
            lineOnTripRootElem.Add(lotElem);
            XMLTools.SaveListToXMLElement(lineOnTripRootElem,lineOnTripPath);
        }
        public void UpdateLineOnTrip(LineOnTrip lineOnTrip)
        {
            //List<LineOnTrip> ListLineOnTrip = XMLTools.LoadListFromXMLSerializer<LineOnTrip>(lineOnTripPath);
            //IEnumerable<DO.LineOnTrip> lot =ListLineOnTrip.FindAll(x => x.LineId==lineOnTrip.LineId);
            //foreach (var lineOt in lot)
            //{
            //   ListLineOnTrip.Remove(lineOt);//remove the line on trip 
            //   //update the lineOt
            //   ListLineOnTrip.Add(lineOt);//add the updated line stop 
            //}
            //XMLTools.SaveListToXMLSerializer(ListLineOnTrip,lineOnTripPath);
        }
        public void DeleteLineOnTrip(int lineId)
        {
            XElement lineOnTripRootElem = XMLTools.LoadListFromXMLElement(lineOnTripPath);

            IEnumerable<XElement> lot = (from l in lineOnTripRootElem.Elements()
                            where (Int32.Parse(l.Element("LineId").Value) == lineId)
                            select l);

            if (lot != null)
            {  
                lot.Remove();
                XMLTools.SaveListToXMLElement(lineOnTripRootElem,lineOnTripPath);
            }
            else
                throw new BadBusLineIdException(lineId);
        }
        #endregion***LineOnTrip***
        #region***CRUD, Bus***
        public IEnumerable<Bus> GetAllBus()//gets a list of buses
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            return (from bus in busRootElem.Elements()
                    select new Bus()
                    {
                        LicenseNum = Int32.Parse(bus.Element("LicenseNum").Value),
                        StartDate = DateTime.Parse(bus.Element("StartDate").Value),
                        Date = DateTime.Parse(bus.Element("Date").Value),
                        TotalTravel = Double.Parse(bus.Element("TotalTravel").Value),
                        Travel = Double.Parse(bus.Element("Travel").Value),
                        Currstate = (BusStatus)Enum.Parse(typeof(BusStatus), bus.Element("CurrState").Value),
                    }
                   );
        }
         public IEnumerable<Bus> GetAllBusBy(Predicate<Bus> predicate)//gets a list buses according to a condition
         {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            return from bus in busRootElem.Elements()
                   let b = new Bus()
                   {
                       LicenseNum = Int32.Parse(bus.Element("LicenseNum").Value),
                       StartDate = DateTime.Parse(bus.Element("StartDate").Value),
                       Date = DateTime.Parse(bus.Element("Date").Value),
                       TotalTravel = Double.Parse(bus.Element("TotalTravel").Value),
                       Travel = Double.Parse(bus.Element("Travel").Value),
                       Currstate = (BusStatus)Enum.Parse(typeof(BusStatus), bus.Element("CurrState").Value),
                   }
                   where predicate(b)
                   select b;
        }
        public Bus GetBus(int license)
        {

            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            Bus b = (from bus in busRootElem.Elements()
                        where int.Parse(bus.Element("LicenseNum").Value) == license
                        select new Bus()
                        {
                            LicenseNum = Int32.Parse(bus.Element("LicenseNum").Value),
                            StartDate=DateTime.Parse(bus.Element("StartDate").Value),
                            Date=DateTime.Parse(bus.Element("Date").Value),
                            TotalTravel=Double.Parse(bus.Element("TotalTravel").Value),
                            Travel=Double.Parse(bus.Element("Travel").Value),
                            Currstate=(BusStatus)Enum.Parse(typeof(BusStatus), bus.Element("CurrState").Value)
                        }
                        ).FirstOrDefault();

            if (b == null)
                throw new DO.BadBusIdException(license, $"bad person id: {license}");
            return b;
        }
        public void AddBus()
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            //XElement bus1 = (from b in busRootElem.Elements()
            //                 where Int32.Parse(b.Element("LicenseNum").Value) ==bus.LicenseNum
            //                 select b).FirstOrDefault();

            //if (bus1 != null)
            //   throw new DO.BadBusIdException(bus.LicenseNum, "Duplicate bus license number");
            XElement busElem = new XElement("Bus",
                                   new XElement("LicenseNum", XMLTools.getRuningNumber(runningNumberPath)),
                                   new XElement("StartDate", DateTime.Now),
                                   new XElement("Date", DateTime.Now),
                                   new XElement("TotalTravel", 0),
                                   new XElement("Travel",0),
                                   new XElement("CurrState",0));
            busRootElem.Add(busElem);
            XMLTools.SaveListToXMLElement(busRootElem, busPath);
        }
        public void UpdateBus(Bus bus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            XElement bus1 = (from b in busRootElem.Elements()
                             where Int32.Parse(b.Element("LicenseNum").Value) == bus.LicenseNum
                             select b).FirstOrDefault();

            if (bus != null)
            {
                bus1.Element("LicenseNum").Value = bus.LicenseNum.ToString();
                bus1.Element("StartDate").Value = bus.StartDate.ToString();
                bus1.Element("Date").Value = bus.Date.ToString();
                bus1.Element("TotalTravel").Value = bus.TotalTravel.ToString();
                bus1.Element("Travel").Value = bus.Travel.ToString();
                bus1.Element("CurrState").Value = bus.Currstate.ToString();
                XMLTools.SaveListToXMLElement(busRootElem,busPath);
            }
            else
                throw new DO.BadBusIdException(bus.LicenseNum, $"bad bus license: {bus.LicenseNum}");
        }
        public void DeleteBus(int license)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            XElement bus = (from b in busRootElem.Elements()
                            where Int32.Parse(b.Element("LicenseNum").Value) == license
                            select b).FirstOrDefault();

			if (bus != null)
			{
				bus.Remove();
				XMLTools.SaveListToXMLElement(busRootElem, busPath);
			}
			else
				throw new DO.BadBusIdException(license, $"bad bus license: {license}");
		}
		#endregion***Bus***
		#region***User***
		public IEnumerable<User> GetAllUser()//gets a list of users
		{
			List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(userPath);
			return from User in ListUser
				   select User;
		}
		public IEnumerable<User> GetAllUserBy(Predicate<User> predicate)//gets a list of users according to a condition
		{
			List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(userPath);
			return from user in ListUser
				   where predicate(user)
				   select user;
		}
		public void AddUser(User user)
		{
			List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(userPath);
			if (ListUser.FirstOrDefault(b => b.Username == user.Username) != null)
				throw new DO.BadUserNameException(user.Username, $"user with username { user.Username} already exists!");
			ListUser.Add(user);
			XMLTools.SaveListToXMLSerializer(ListUser,userPath);
		}
		public void UpdateUser(User myUser, string username)
		{
			List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(userPath);
			//find the User
			DO.User bs = ListUser.Find(b => b.Username == username);
			if (bs != null)
			{
				ListUser.Remove(bs);
				ListUser.Add(myUser);
				XMLTools.SaveListToXMLSerializer(ListUser, userPath);
			}
			else
				throw new DO.BadUserNameException(myUser.Username, "username not found!");
		}
		public void DeleteUser(string username)
		{
			List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(userPath);
			//if user does not exist
			if (!ListUser.Exists(item => item.Username == username))
			{
				throw new BadUserNameException(username);
			}
			ListUser.RemoveAll(item => item.Username == username);//delete the user from the data source
			XMLTools.SaveListToXMLSerializer(ListUser, userPath);
		}
		public User GetUser(string name)
		{
			List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(userPath);
			DO.User bs = ListUser.Find(b => b.Username == name);
			if (bs != null)
				return bs;
			else
				throw new DO.BadUserNameException(name, $"bad username: {name}");
		}
		#endregion***User***
	}
}
