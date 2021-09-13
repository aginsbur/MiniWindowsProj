using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using BO;
using DALAPI;
using DO;

namespace BL
{

    class BLImp1 : IBL
    {

        readonly IDAL dal = DLFactory.GetDL();

        #region Singleton 
        //make sure that data we can have only one copy of our data-using singleton patter!!!
        static readonly BLImp1 instance = new BLImp1();
        static BLImp1() { }
        BLImp1() { }
        /// <summary>
        /// private c-tor, creates a single DLObject
        /// </summary>
        public static BLImp1 Instance => instance;
        #endregion
        #region ***Bo2DoConverters***
        /// <summary>
        /// converts a DO BusStop to BO BusStop
        /// </summary>
        /// <param name="busStop"></param>the bus stop to convert
        /// <returns></returns>retuns a BO bus stop
        private BO.BusStop convertDOBusStop2BO(DO.BusStop busStop)
        {
            BO.BusStop busStopBO = new BO.BusStop();
            busStop.CopyPropertiesTo(busStopBO);
            return busStopBO;
        }
        /// <summary>
        ///converts a BO BusStop to DO BusStop
        /// </summary>
        /// <param name="busStop"></param>the bus stop to convert
        /// <returns></returns>returns a DO bus stop
        private DO.BusStop converBOBusStop2DO(BO.BusStop busStop)
        {
            DO.BusStop busStopDO = new DO.BusStop();
            busStop.CopyPropertiesTo(busStopDO);
            return busStopDO;
        }
        /// <summary>
        ///converts a DO BusLine to BO BusLine
        /// </summary>
        /// <param name="busStop"></param>the bus line to convert
        /// <returns></returns>returns a BO bus line
        private BO.BusLine convertDOBusLine2BO(DO.BusLine busLine)
        {
            BO.BusLine busLineBO = new BO.BusLine();
            busLine.CopyPropertiesTo(busLineBO);
            return busLineBO;
        }
        /// <summary>
        ///converts a BO BusLine to DO BusLine
        /// </summary>
        /// <param name="busStop"></param>the bus line to convert
        /// <returns></returns>returns a DO bus line
        private DO.BusLine convertBOBusLine2DO(BO.BusLine busLine)
        {
            DO.BusLine busLineDO = new DO.BusLine();
            busLine.CopyPropertiesTo(busLineDO);
            return busLineDO;
        }
        /// <summary>
        ///converts a DO adjacentStop to BO adjacentStop
        /// </summary>
        /// <param name="adjStop"></param>the adjacentStop to convert
        /// <returns></returns>returns a BO adjacent stop
        private BO.AdjacentStops convertDOAdjStop2BO(DO.AdjacentStops adjStop)
        {
            BO.AdjacentStops adjStopBO = new BO.AdjacentStops();
            adjStop.CopyPropertiesTo(adjStopBO);
            return adjStopBO;
        }
        ///<summary>
        ///converts a BO adjacentStop to DO adjacentStop
        /// </summary>
        /// <param name="adjStop"></param>the adjacentStop to convert
        /// <returns></returns>returns a DO adjacent stop
        private DO.AdjacentStops convertBOAdjStop2DO(BO.AdjacentStops adjStop)
        {
            DO.AdjacentStops adjStopDO = new DO.AdjacentStops();
            adjStop.CopyPropertiesTo(adjStopDO);
            return adjStopDO;
        }
        ///<summary>
        ///converts a BO LineStop to BO LineStop
        /// </summary>
        /// <param name="lineStop"></param>the adjacentStop to convert
        /// <returns></returns>returns a DO Linestop
        private DO.LineStop convertBOLineStop2DO(BO.LineStop lineStop)
        {
            DO.LineStop lineStopDO = new DO.LineStop();
            lineStop.CopyPropertiesTo(lineStopDO);
            return lineStopDO;
        }
        ///<summary>
        ///converts a DO Bus to DO Bus
        /// </summary>
        /// <param name="bus"></param>the Bus to convert
        /// <returns></returns>returns a BO Bus
        private BO.Bus convertDOBus2BO(DO.Bus bus)
        {
            BO.Bus busBO = new BO.Bus();
            bus.CopyPropertiesTo(busBO);
            return busBO;
        }
        ///<summary>
        ///converts BO Bus to DO Bus
        /// </summary>
        /// <param name="bus"></param>the Bus to convert
        /// <returns></returns>returns a BO Bus
        private DO.Bus convertBOBus2DO(BO.Bus bus)
        {
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            return busDO;
        }
        /// <summary>
        /// converts BO user to DO user
        /// </summary>
        /// <param name="user"></param>the user to convert
        /// <returns></returns>returns a BO user
        private BO.User convertDOUser2BO(DO.User user)
        {
            BO.User UserBO = new BO.User();
            user.CopyPropertiesTo(UserBO);
            return UserBO;
        }
        ///<summary>
        ///converts BO user to DO user
        /// </summary>
        /// <param name="bus"></param>the user to convert
        /// <returns></returns>returns a DO user
        private DO.User convertBOUser2DO(BO.User user)
        {
            DO.User UserDO = new DO.User();
            user.CopyPropertiesTo(UserDO);
            return UserDO;
        }
        #endregion
        #region***BusLine***
        /// <summary>
        /// returns all bus lines in the data source
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.BusLine> getAllBusLines()
        {
            return from busLine in dal.GetAllBusLines()
                   select convertDOBusLine2BO(busLine);
        }
        /// <summary>
        /// creats a list of BusStops in a busline
        /// </summary>
        /// <returns></returns>list of BusStops
        public IEnumerable<BO.BusStop> getBusStopsInLine(BO.BusLine myBusLine)
        {
            return (from lineStop in dal.GetAllLineStopBy(ls => ls.LineID == myBusLine.ID).ToList()
                    from busStop in dal.GetAllBusStopsBy(bs => bs.StationCode == lineStop.StationCode).ToList()
                    select (new BO.BusStop
                    {
                        StationCode = busStop.StationCode,
                        Address = busStop.Address,
                        Longitude = busStop.Longitude,
                        Latitude = busStop.Latitude,
                        IndexInLine = lineStop.StationIndex,
                    })).ToList();

        }
        public IEnumerable<BO.BusStop> getBusStopsNotInLine(BO.BusLine myBusLine)
        {                                                                                              
           var result= from b in dal.GetAllBusStops().ToList()//gets all bus stops
                   where dal.GetAllLineStopBy(x => myBusLine.ID == x.LineID).ToList().All(l => l.StationCode != b.StationCode)//gets all line stops that are in the line
                   select (new BO.BusStop                                                                                     //selects all bus stops which station codes do not have a 
                   {                                                                                                          //line stop in this bus line
                       StationCode = b.StationCode,
                       Address = b.Address,
                       Longitude = b.Longitude,
                       Latitude = b.Latitude,
                   });
           // var result = dal.GetAllBusStops().ToList().Where(b => ls.All(p2 => p2.StationCode != b.StationCode));
            return result;

        }
        /// <summary>
        /// returns a busline based on it's number and region
        /// </summary>
        /// <param name="myBusLine"></param>
        /// <returns></returns>
        public IEnumerable<BO.BusLine> getBusLinewithOutID(BO.BusLine myBusLine)
        {
            return from busLine in dal.GetAllBusLinessBy(bl => bl.LineNumber == myBusLine.LineNumber)//compares bus line number
                   let temp = convertDOBusLine2BO(busLine)
                   where temp.Region == myBusLine.Region//compare bus line region
                   select temp;

        }
        /// <summary>
        /// gets a busline from the data source
        /// </summary>
        /// <param name="myLineID"></param>line id of busline which we want to get
        /// <returns></returns>
        public BO.BusLine getBusLine(int myLineID)
        {
            BO.BusLine result = new BO.BusLine();
            try
            {
                result = convertDOBusLine2BO(dal.GetBusLine(myLineID));
                result.StopsInLine = getBusStopsInLine(result);//insert to "result" its bus Stops on the route
            }
            catch (DO.BadBusLineIdException DOex)
            {
                throw new BO.BadBusLineIdException("Bus stop with line number entered does not exists!", DOex);
            }
            return result;
        }
        ///<summary>
        /// this method removes a bus stop from a line
        /// </summary>
        /// <param name="myBusStop"></param> the bus stop we wish to remove
        /// <param name="myLineID"></param> the id of the line from where we wish to remove a bus stop
        public void deleteBusStopFromLine(BO.BusStop myBusStop, int myLineID)
        {
            //delete line stop in the data source
            try
            {
                if (getBusLine(myLineID).StopsInLine.Count() > 2)//if there are more than two stops in the line
                {
                    dal.DeleteLineStop(myLineID, myBusStop.StationCode);//deletes the line stop
                    dal.UpdateLineStopByLine(x => x.LineID == myLineID);//updates the stops in lines indices
                    BO.BusLine busLine = getBusLine(myLineID);
                    busLine.BegStopNum = busLine.StopsInLine.First().StationCode;
                    busLine.EndStopNum = busLine.StopsInLine.Last().StationCode;
                    dal.UpdateBusLine(convertBOBusLine2DO(busLine));//updates the begin and end stop of the busLine
                }
                else
                    throw new BO.BadBusLineToDelException("the bus line must have at least two stops!");

            }
            catch (DO.BadBusStopIdException DOex)
            {
                throw new BO.BadBusStopIdException("Bus stop doesn't exists!", DOex);
            }
            catch (DO.BadBusLineIdException DOex)
            {
                throw new BO.BadBusStopIdException("Bus line doesn't exists!", DOex);
            }
        }
        public void addBusLine(BO.BusLine myBusLine)
        {
            Random rnd = new Random();//the random generator will determine the frequency of the bus lines trips per hour
            try
            {
                dal.AddBusLine(convertBOBusLine2DO(myBusLine));
                addLinesOnTrip(getBusLinewithOutID(myBusLine).FirstOrDefault().ID,rnd.Next(1,5));
            }
            catch (DO.BadBusLineIdException DOex)
            {
                throw new BO.BadBusLineIdException($"Bus line with line number: {myBusLine.LineNumber} already exists in this region", DOex);
            }
            catch (DO.BadBusStopIdException DOex)
            {
                throw new BO.BadBusStopIdException($" Bus stop with station number: {myBusLine.BegStopNum} was selected as the first and last stop! \nplease select different begin and end stops", DOex);
            }
        }
        /// <summary>
        /// deletes a line stop
        /// </summary>
        /// <param name="myBusLine"></param>
        public void deleteBusLine(BO.BusLine myBusLine)
        {
            try
            {
                dal.DeleteBusLine(myBusLine.ID);//delete the bus line
                dal.DeleteLineStopBy(x => x.LineID == myBusLine.ID);//delete all line stops using the bus line
                IEnumerable<DO.LineOnTrip> lot = dal.GetAllLinesOnTripBy(x => x.LineId == myBusLine.ID);
                //delete all lines on trip using the bus line
                foreach (var item in lot)
                {
                    dal.DeleteLineOnTrip(item.LineId);
                }
            }
            catch (DO.BadBusLineIdException ex)
            {
                throw new BO.BadBusLineIdException("bus line to delete not found!", ex);
            }
        }
        #endregion***BusLine***
        #region***BusStop***

        /// <summary>
        /// gets the bus stop with inputed station code number 
        /// </summary>
        /// <param name="myStationCode"></param>the station code number of requested bus stop
        /// <returns></returns>bus stop with inputed station code
        public BO.BusStop getBusStop(int myStationCode)
        {
            BO.BusStop result = new BO.BusStop();
            try
            {
                result = convertDOBusStop2BO(dal.GetBusStop(myStationCode));
            }
            catch (DO.BadBusStopIdException DOex)
            {
                throw new BO.BadBusStopIdException("Bus stop with station number entered does not exists!", DOex);
            }
            return result;
        }

        /// <summary>
        /// gets the all of the busStops in the data source
        /// </summary>
        /// <returns></returns>returns collection of busStops in the data source
        public IEnumerable<BO.BusStop> getAllBusStops()
        {
            return from busStop in dal.GetAllBusStops()
                   select convertDOBusStop2BO(busStop);
        }
        /// <summary>
        /// gets all of the busStops with a condition in the data source
        /// </summary>
       /// <returns></returns>returns collection of busStops in the data source
        public IEnumerable<BO.BusStop> getAllBusStopsBy(Func<BO.BusStop, bool> func)
        {
            return from busStop in dal.GetAllBusStops()
                   where func(convertDOBusStop2BO(busStop))
                   select convertDOBusStop2BO(busStop);
        }


     /// <summary>
     /// adds a bus stop
     /// </summary>
     /// <param name="myBusStop"></param>bus stop to be added
     public void addBusStop(BO.BusStop myBusStop)
        {
            try
            {
                dal.AddBusStop(converBOBusStop2DO(myBusStop));
            }
            catch (DO.BadBusStopIdException ex)
            {
                throw new BO.BadBusStopIdException($"Bus Stop with station code {myBusStop.StationCode} already exists!", ex);
                throw new BO.BadBusStopIdException("error",ex);
            }

        }
        /// <summary>
        /// will get the bus lines which pass through the specified bus stop
        /// </summary>
        /// <param name="myBusStop"></param>the bus stop which lines we want to get
        /// <returns></returns>a busStops collection of busLines
        public IEnumerable<BO.BusLine> getBusLinesInStation(BO.BusStop myBusStop)
        {
            return from lineStop in dal.GetAllLineStopBy(ls => ls.StationCode == myBusStop.StationCode)
                   from busLine in dal.GetAllBusLinessBy(bl => bl.ID == lineStop.LineID)
                   select convertDOBusLine2BO(busLine);
        }
        /// <summary>
        /// updates data of a bus stop
        /// </summary>
        /// <param name="newBusStop"></param>bus stop with updated data
        /// <param name="curBusStop"></param>bus stop with previous data
        public void updateBusStop(BO.BusStop newBusStop,BO.BusStop curBusStop) //int id-instead of curBS
        {
            try
            {
                dal.UpdateBusStop(converBOBusStop2DO(newBusStop), curBusStop.StationCode);
                if (newBusStop.StationCode != curBusStop.StationCode)//if the station code was changed
                {
                    dal.UpdateLineStopByBusStop(curBusStop.StationCode, newBusStop.StationCode);//update the line stops passing through the bus Stop station codes 
                    IEnumerable<DO.BusLine> l1= dal.GetAllBusLines().Where(x => x.BegStopNum == curBusStop.StationCode || x.EndStopNum == curBusStop.StationCode);//finds all bus lines which use the updated bus stop
                    foreach (var busLine in l1)
                    {
                        updateBegAndEndStops(busLine.ID);//resets the begin and end stop numbers of the bus line
                    }
                    List<DO.AdjacentStops> l2=dal.GetAllAdjacentStopsBy(x => x.StopCode1 == curBusStop.StationCode || x.StopCode2 == curBusStop.StationCode).ToList();
                    foreach(var adjStop in l2)//update all adjacent stops that included this bus stop
                    {
                        if (adjStop.StopCode1 == curBusStop.StationCode)//if the begin stop number has to be updated
                        {
                            adjStop.StopCode1 = newBusStop.StationCode;
                            dal.UpdateAdjacentStops(adjStop);
                        }
                        else
                        {
                            adjStop.StopCode2 = newBusStop.StationCode;
                            dal.UpdateAdjacentStops(adjStop);
                        }
                    }
                }
                //if the address is changed, it's ok- because the street has the same location-just the name was changed
            }
            catch (DO.BadBusStopIdException ex)
            {
                throw new BO.BadBusStopIdException("error in  updating the bus stop", ex);
            }
            catch (DO.BadBusIdException ex)
            {
                throw new BO.BadBusLineIdException("error updating the bus lines using the updated stop", ex);
            }

        }
       /// <summary>
       /// delete a bus stop 
       /// </summary>
       /// <param name="myBusStop"></param>the bus stop to delete
       public void deleteBusStop(BO.BusStop myBusStop)
        {
            try
            {
                IEnumerable<DO.LineStop> ls = dal.GetAllLineStopBy(item => item.StationCode == myBusStop.StationCode).ToList();
                foreach(var linestop in ls)
                {
                    deleteBusStopFromLine(myBusStop, linestop.LineID);
                }
                dal.DeleteBusStop(myBusStop.StationCode);
                List<DO.AdjacentStops> l = dal.GetAllAdjacentStopsBy(x => x.StopCode1 == myBusStop.StationCode || x.StopCode2 == myBusStop.StationCode).ToList();
                foreach (var adjStop in l)//remove all adjacent stops that included this bus stop
                {
                    dal.DeleteAdjacentStops(adjStop.StopCode1, adjStop.StopCode2);
                }
            }
            catch (DO.BadBusStopIdException ex)
            { 
                throw new BO.BadBusStopIdException("Bus stop to delete was not found in the data source!", ex); 
            }
            catch (BO.BadBusLineToDelException ex)
            {
                throw new BO.BadBusLineToDelException("Bus stop to delete will  reduce the bus line to fewer than 2 stops!");
            }

        }
        #endregion***BusStops***
        #region***LineStop***
        /// <summary>
        /// updates a line stop
        /// </summary>
        /// <param name="newLineStop"></param>
        public void updateLineStop(BO.LineStop newLineStop)
        {
            try
            {
                dal.UpdateLineStop(convertBOLineStop2DO(newLineStop));
            }
            catch (DO.BadBusIdException ex)
            {
                throw new BO.BadBusStopIdException("error", ex);
            }
        }
        public void addLineStop(int lineId, int stationCode, int index)
        {
            try
            {
                dal.AddLineStop(new DO.LineStop
                {
                    LineID = lineId,
                    StationCode = stationCode,
                    StationIndex = index,
                });
            }
            catch (DO.BadBusStopIdException ex)
            {
                throw new BO.BadBusStopIdException("a line stop with this bus stop id already exists", ex);
            }

        }

        #endregion***LineStop***
        #region***adjacentStops***
        public void addAdjacentStops(BO.AdjacentStops adjStop)
        {
            try
            {
                dal.AddAdjacentStops(new DO.AdjacentStops
                {
                    StopCode1 = adjStop.StopCode1,
                    StopCode2 = adjStop.StopCode2,
                    Distance = adjStop.Distance,
                    AvgTravelTime = adjStop.AvgTravelTime,
                });
            }
            catch (DO.BadBusStopIdException ex)
            {
                //if adjacent stop already exists, means the adjacent stop already exists
                throw new BO.BadBusStopIdException( "adjacent stop already in the system",ex);
            }
        }
        /// <summary>
        /// gets an adjacent stop from the data source
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public BO.AdjacentStops getAdjacentStops(int id1, int id2)
        {
            try
            {
                DO.AdjacentStops adjS = dal.GetAdjacentStops(id1, id2);
                return convertDOAdjStop2BO(adjS);
            }
            catch (DO.BadBusStopIdException)
            { return null; }


        }
         public void updateAdjacentStops(BO.AdjacentStops stops)
           {
               try
               {
                   dal.UpdateAdjacentStops(convertBOAdjStop2DO(stops));
               }
               catch(DO.BadBusIdException ex)
               {
                   throw new BO.BadBusStopIdException("could not find bus stop to update", ex);
               }
           }
        #endregion***adjacentStops***
        #region***Bus***
        public IEnumerable<BO.Bus> getAllBusses()//gets a collection of buses
        {
            return from bus in dal.GetAllBus()
                   select convertDOBus2BO(bus);
        }
        public IEnumerable<BO.Bus> GetAllBusBy(Predicate<BO.Bus> predicate)//gets a list buses according to a condition
        {
            return from bus in dal.GetAllBus().ToList()
                   let b= convertDOBus2BO(bus)
                   where predicate(b)
                   select b;
        }
        public BO.Bus GetBus(int id)
        {
            try
            {
                return convertDOBus2BO(dal.GetBus(id));
            }
            catch(DO.BadBusIdException ex)
            {
                throw new BO.BadBusIdException("bus does not exist!",ex);
            }
        }
        public void AddBus()
        {
            try
            {
                dal.AddBus();
            }
            catch(DO.BadBusIdException ex)
            {
                throw new BO.BadBusIdException("duplicate bus id", ex);
            }
        }
        public void UpdateBus(BO.Bus bus)
        {
            try 
            {  if(bus.KM!=0)
                checkTrip(bus);//check if the bus can cmplete the trip
                bus.Travel += bus.KM;
                bus.TotalTravel += bus.KM;
                bus.KM = 0;
                dal.UpdateBus(convertBOBus2DO(bus));//sende the bus stop with the new data
                updateBusStatus(bus);//updates the busses status
            }
            catch (DO.BadBusIdException ex)
            {
                throw new BO.BadBusIdException("could not find the bus to update", ex);
            }
            catch (BO.BadBusIdException ex)
            {
                updateBusStatus(bus);
                throw new BO.BadBusIdException(ex.Message);
            }

        }
      public void DeleteBus(int id)
        {
            try
            {
               dal.DeleteBus(id);
            }
            catch (DO.BadBusIdException ex)
            {
                throw new BO.BadBusIdException("could not find the bus to delete", ex);
            }
        }
        #endregion***Bus***
        #region***LineOnTrip***
       /// <summary>
       /// adds lines on trip for a new bus line
       /// </summary>
       /// <param name="frequencyInHour"></param>ammount of trips leaving per hour
       public void addLinesOnTrip(int lineId,int frequencyInHour)
        {
            try
            {
                int minOfDeparturePerHour = 60 / frequencyInHour;
                int i = 0;
                while (i < 24)//add lines on trip for 24 hours of the day
                {
                    int j = 0;
                    while (j != frequencyInHour)//for every hour of the day add lines on trip
                    {
                        string s = i.ToString() + ":" + (minOfDeparturePerHour * j).ToString() + ":" + "00";
                        dal.AddLineOnTrip(new DO.LineOnTrip
                        {
                            LineId = lineId,
                            StartTime = TimeSpan.Parse(s)
                        }
                                            );
                        j++;
                    }
                    i++;
                }
            }
            catch(DO.BadBusIdException ex)
            {
                throw new BO.BadBusIdException("Duplicate line on trip!", ex);
            }
        }
        /// <summary>
        ///gets the lines on trip which pass through this bus stop and Start runnin within the next hour
        /// </summary>
        /// <param name="stationCode"></param>bus stop that the scheadule is displaying
        /// <param name="time"></param>the time which hour of departure is the current hour 
        /// <returns></returns>a collection of lines on trip which pass through this bus stop, and depart during this hour
        public IEnumerable<BO.LineOnTrip> getAllLinesOntripByBusStopAndHour(int stationCode, TimeSpan time)
        {
            return from lot in getAllLinesOntripByBusStop(stationCode).ToList()
                   where (lot.TimeOfArrival.Hours > time.Hours)|| ((lot.TimeOfArrival.Hours >= time.Hours) && (lot.TimeOfArrival.Minutes > time.Minutes))||((lot.TimeOfArrival.Hours >= time.Hours)&& (lot.TimeOfArrival.Minutes >= time.Minutes)&&(lot.TimeOfArrival.Seconds>=time.Seconds))
                   select lot;
        }
        /// <summary>
        /// gets the lines on trip which pass through this bus stop
        /// </summary>
        /// <param name="stationCode"></param>bus stop that the scheadule is displaying
        /// <returns></returns>a collection of lines on trip which pass through this bus stop
        IEnumerable<BO.LineOnTrip> getAllLinesOntripByBusStop(int stationCode)
        { 
            try
            {
                IEnumerable<BO.LineOnTrip> linesOnTrip = from item1 in dal.GetAllLineStopBy(ls => ls.StationCode == stationCode).ToList()//list of line stops using station   
                                                         from item2 in dal.GetAllLinesOnTripBy(lot => lot.LineId == item1.LineID).ToList()//list of lines on trip using station
                                                         select (new BO.LineOnTrip
                                                         {
                                                             LineId = item2.LineId,
                                                             StartTime = item2.StartTime,
                                                         }
                                                         );
                return from lot in linesOnTrip
                       let lotReturn = new BO.LineOnTrip() 
                       {
                           LineId = lot.LineId,
                           LineNum = convertDOBusLine2BO(dal.GetBusLine(lot.LineId)).LineNumber,//gets a bus line running through the bus stop
                           Destination = dal.GetBusStop(convertDOBusLine2BO(dal.GetBusLine(lot.LineId)).EndStopNum).Address,//sets the address of the destination
                           StartTime=lot.StartTime,
                           TimeOfArrival = calcTimeOfArrival(stationCode,lot.LineId, lot.StartTime)
                       }
                       select lotReturn;
            }
            catch(DO.BadBusStopIdException ex)
            { 
                throw new BO.BadBusStopIdException("could not complete task for loading the bus stop scheadule!", ex); 
            }
            }

        #endregion***LineOnTrip***
        #region***Enums***
        public IEnumerable<string> getAllRegions()
        {
            return Enum.GetValues(typeof(BO.Region))
                  .Cast<BO.Region>()
                 .Select(v => v.ToString())
                 .ToList();

        }
        #endregion***Enums***
        #region***Methods***
        /// <summary>
        /// calculates a bus lines time of arrival at a bus stop
        /// </summary>
        /// <param name="stationCode"></param>the bus stop for which we are calculating the time of arrival to
        /// <param name="lineId"></param>the bus line for which we are calculating the time of arrival
        /// <param name="startTime"></param>the departure time of the bus line
        /// <returns></returns>
        TimeSpan calcTimeOfArrival(int stationCode, int lineId, TimeSpan startTime)
        {
            try
            {

                TimeSpan timeToArrival = TimeSpan.FromHours((getDistanceToBusStop(stationCode, lineId) / 55));//travel time= travel distance/ travel speed
                TimeSpan TOA = startTime + timeToArrival;//time of arrival is: the time now + duration of the travel
                return TOA;
            }
            catch (DO.BadBusStopIdException ex)
            { throw ex; }
        }
        /// <summary>
        /// calculates distance from firts stop in a bus line to the current bus stop
        /// </summary>
        /// <param name="stationCode"></param>bus stop for which to calculate the distance to
        /// <param name="lineId"></param>bus line whos route the bus stop is on
        /// <returns></returns>
        Double getDistanceToBusStop(int stationCode, int lineId)
        {
            double distance = 0;
            int i = 1;
            DO.BusStop BS1 = new DO.BusStop();
            DO.BusStop BS2 = new DO.BusStop();
            do
            {
                try
                {
                    BS1 = dal.GetBusStop((dal.GetAllLineStopBy(x => x.LineID == lineId && x.StationIndex == i)).FirstOrDefault().StationCode);
                    BS2 = dal.GetBusStop((dal.GetAllLineStopBy(x => x.LineID == lineId && x.StationIndex == i + 1)).FirstOrDefault().StationCode);
                    if (BS1.StationCode == stationCode)//checks if the first bus stop in the line is the current stop
                        BS2.StationCode = stationCode;//sets the value to end the while loop
                    else if (BS2 != null)//if an adjacent stop from bs1 to bs2 exists
                    { IEnumerable<DO.AdjacentStops> adjS = dal.GetAllAdjacentStopsBy(x => x.StopCode1 == BS1.StationCode && x.StopCode2 == BS2.StationCode);
                       if(adjS.Count()!=0)//if there exists an adjacent stop
                            distance +=adjS.First().Distance;
                       else//if not-create a new adjacent stop and calc. distance
                        {
                            //var coor1 = new GeoCoordinate(BS1.Latitude, BS1.Longitude);
                            //var coor2 = new GeoCoordinate(BS2.Latitude, BS2.Longitude);
                            //var d =coor1.GetDistanceTo(coor2);    
                            
                            //because the program only requires a time span for the time of arrival, i created a randome generator for the distance
                            //between two bus stops-inorder to prevent a trip from taking more than a day.
                            //if the requirment was a date time type for the time of arrival i would use the code above
                            //for calculating the distance in between two bus stops
                            Random rnd=new Random();
                            Double d = rnd.Next(1, 30);
                            addAdjacentStops(new BO.AdjacentStops { StopCode1 = BS1.StationCode, StopCode2 = BS2.StationCode, Distance =d, AvgTravelTime= TimeSpan.FromHours(d/ 55) });
                            distance += d;
                        }
                        i++;
                    }
                }
                catch (DO.BadBusStopIdException ex)
                {
                    throw new BO.BadBusStopIdException("could not calc distance",ex);
                }
            } while (BS2.StationCode != stationCode);//iterate over the line stops until the current bus stop is reached

            return distance;
        }
        /// <summary>
        /// resets the first and last stop of a bus line
        /// </summary>
        /// <param name="lineId"></param>the bus line to update
        void updateBegAndEndStops(int lineId)
        {
            DO.BusLine bl = dal.GetBusLine(lineId);
            bl.BegStopNum=dal.GetAllLineStopBy(x=>x.LineID==lineId&&x.StationIndex==1).FirstOrDefault().StationCode;
            bl.EndStopNum = dal.GetAllLineStopBy(x => x.LineID == lineId).ToList().OrderBy(x=>x.StationIndex).Last().StationCode;
            dal.UpdateBusLine(bl);
        }
        /// <summary>
        /// this method returns true if requested travel is possible
        /// </summary>
        /// <param name="km"></param>distance that the user wants to travel
        /// <returns></returns>
         bool checkTrip(BO.Bus bus)
        {
            if (bus.Travel + bus.KM > 1200)
            { bus.KM = 0;
             throw new BO.BadBusIdException("Not enough gas to complete trip!"); }
            if (bus.TotalTravel + bus.KM > 20000)
            {
                bus.KM = 0;
                throw new BO.BadBusIdException("Distance requested Exceeds max allowed Km traveld since last maintenance!");
            }
                TimeSpan t = DateTime.Now - bus.Date;
            if (t.TotalDays >= 365)
            {
                bus.KM = 0;
                throw new BO.BadBusIdException("The Bus cannot complete the trip. Send for maintenance!");
            }
                return true;
        }
         public void updateBusStatus(BO.Bus bus)
        {
            if (bus.Travel == 1200 || bus.Travel > 1200)
                bus.Currstate = BO.BusStatus.NEEDREFUEL;
            else
            {
                TimeSpan t = DateTime.Now - bus.Date;
                if (t.TotalDays >= 365)//if bus  needs maintenance
                    bus.Currstate = BO.BusStatus.NEEDSERVICE;
                else
                    bus.Currstate = BO.BusStatus.READY2GO;
            }
            dal.UpdateBus(convertBOBus2DO(bus));
        }
        #endregion***Methods***
        #region***User***
        public IEnumerable<BO.User> GetAllUser()//gets a list of Users
        {
            return from user in dal.GetAllUser()
                   select convertDOUser2BO(user);
        }
        public IEnumerable<BO.User> GetAllUserBy(Predicate<BO.User> predicate)//gets a list of Users according to a condition
        {
            return from user in dal.GetAllUser()
                   let user1 = (convertDOUser2BO(user))
                   where predicate(user1)
                   select user1;
        }
        public BO.User GetUser(string name)
        {
            try
            {
                return convertDOUser2BO(dal.GetUser(name));
            }
            catch(DO.BadUserNameException ex)
            {
                throw new BO.BadUserNameException(ex.Message, "invalid username");
            }
        }
        public void AddUser(string name,string password)
        {
            dal.AddUser(new DO.User
            {
                Username = name,
                Password = password,
                Admin = false
            });
        }
        public void UpdateUser(BO.User user)
        {

        }
        public void DeleteUser(int id1)
        {

        }
        #endregion***User***
        #region***UserTrip***
       public IEnumerable<BO.BusLine> GetUserTrip(BO.BusStop bus1, BO.BusStop bus2)
        {
         return   from BusLine1 in getBusLinesInStation(bus1)
            from BusLine2 in getBusLinesInStation(bus2)
            where BusLine1.ID == BusLine2.ID
            select BusLine1;
        }
        #endregion***UserTrip***
    }
}