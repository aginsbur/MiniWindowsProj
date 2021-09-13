using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALAPI;
using DO;
using DS;

namespace DAL
{
    /// <summary>
    /// implements IDAL
    /// </summary>
     class DLObject : IDAL
    { 
        #region Singleton 
        //make sure that data we can have only one copy of our data-using singleton patter!!!
        static readonly DLObject instance = new DLObject();
        static DLObject() { }
        DLObject() { }
        /// <summary>
        /// private c-tor, creates a single DLObject
        /// </summary>
        public static DLObject Instance => instance;
        #endregion
        #region***CRUD, Bus***
        public IEnumerable<Bus> GetAllBus()//gets a list of buses
        {
            return from bus in DataSource.ListBuses
                   select bus.Clone();
        }
         public IEnumerable<Bus> GetAllBusBy(Predicate<Bus> predicate)//gets a list buses according to a condition
         {
             IEnumerable<Bus> bl=new List<Bus>();
            return bl;
         }
        public Bus GetBus(int id)
        {
            DO.Bus bs = DataSource.ListBuses.Find(b => b.LicenseNum == id);//bs holds the bus  to get
                                                                           // try { Thread.Sleep(2000); } catch (ThreadInterruptedException ex) { }
            if (bs != null)
                return bs.Clone();
            else
                throw new DO.BadBusIdException(id, $"bad bus id: {id}");
        }
        public void AddBus()
        { 
        //{
        //    if (DataSource.ListBuses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum) != null)
        //        throw new DO.BadBusIdException(bus.LicenseNum, "Duplicate bus license number");
        //    DataSource.ListBuses.Add(bus.Clone());
        }
        public void UpdateBus(Bus bus)
        {

        }
        public void DeleteBus(int id)
        {

        }
        #endregion***CRUD Bus***
        #region ***CRUD, BusStop***
        public IEnumerable<DO.BusStop> GetAllBusStops()//gets a list of bus stops
        { 
                return from busStop in DataSource.ListBusStops
                       select busStop.Clone();
        }
        public IEnumerable<DO.BusStop> GetAllBusStopsBy(Predicate<BusStop> predicate)//gets a list of bus stops according to a condition
        {
            return from busStop in DataSource.ListBusStops
                   where predicate(busStop)
                   select busStop.Clone();
        }
        public BusStop GetBusStop(int id)
        {

            DO.BusStop bs = DataSource.ListBusStops.Find(b => b.StationCode == id);//bs holds the bus stop to get
           // try { Thread.Sleep(2000); } catch (ThreadInterruptedException ex) { }
            if (bs != null)
                return bs.Clone();
            else
                throw new DO.BadBusStopIdException(id, $"bad bus stop id: {id}");
        }
        public void AddBusStop(BusStop busStop)
        {
            if (DataSource.ListBusStops.FirstOrDefault(b => b.StationCode == busStop.StationCode) != null)
                throw new DO.BadBusStopIdException(busStop.StationCode, $"Bus Stop with station code { busStop.StationCode } already exists!");
            DataSource.ListBusStops.Add(busStop.Clone());
        }

        /// <summary>
        /// removes a bus stop from the data source
        /// </summary>
        /// <param name="stationCode"></param>station code of bus stop to remove
        public void DeleteBusStop(int stationCode)
        {  
            //if bus stop does not exist
            if (!DataSource.ListBusStops.Exists(item => item.StationCode == stationCode))
            {
                 throw new BadBusStopIdException(stationCode);
            }
            DS.DataSource.ListBusStops.RemoveAll(item => item.StationCode == stationCode);//delete the bus stop from the data source
            DS.DataSource.ListAdjacentStops.RemoveAll(item => item.StopCode1 == stationCode || item.StopCode2 == stationCode);//delete adj stops using the station
        }
      /// <summary>
      /// updates the bus stop in the data source
      /// </summary>
      /// <param name="busStop"></param>
      public void UpdateBusStop(BusStop myBusStop,int id)
        {
            //find the busStop
            DO.BusStop bs = DataSource.ListBusStops.Find(b => b.StationCode == id);
             if (bs != null)
            {
                DataSource.ListBusStops.Remove(bs);
                DataSource.ListBusStops.Add(myBusStop.Clone());
            }
            else
                throw new DO.BadBusStopIdException(myBusStop.StationCode, "bus stop not found!");
        }
        #endregion ***BusStop***
        #region***CRUD, BusLine***
        public IEnumerable<BusLine> GetAllBusLines()//gets a list of bus lines
        {
            return from busLine in DataSource.ListBusLines
                   select busLine.Clone();
        }
       public IEnumerable<BusLine> GetAllBusLinessBy(Predicate<BusLine> predicate)//gets a list of bus lines according to a condition
        {
            return from busLine in DataSource.ListBusLines
                   where predicate(busLine)
                   select busLine.Clone();
        }
        public BusLine GetBusLine(int id)
        {
            DO.BusLine bl = DataSource.ListBusLines.Find(b => b.ID == id);//bl holds the busline to get
            // try { Thread.Sleep(2000); } catch (ThreadInterruptedException ex) { }
            if (bl != null)
                return bl.Clone();
            else
                throw new DO.BadBusLineIdException(id, $"bad bus line id: {id}");
        }
        public void AddBusLine(BusLine busLine)
        {   //check if a bus line with the line number already exists in the region
            if (DataSource.ListBusLines.FirstOrDefault(b => b.LineNumber == busLine.LineNumber && b.Region==busLine.Region) != null)
                throw new DO.BadBusLineIdException(busLine.LineNumber, "Duplicate bus line number");
            if (busLine.BegStopNum == busLine.EndStopNum)//if the new bus lines begin and end stop have the same station number
                throw new DO.BadBusStopIdException(busLine.BegStopNum, "Duplicate begin and end bus stop number!");
            busLine.ID = DataSource.BusLineID;//sets the new bus lines ID
            DataSource.ListBusLines.Add(busLine.Clone());
        }
        //public void UpdateBusLine(int LineID, int BusStopID1,int BusStopID2)//updates begin and end stop of bus line
        //{
        //   DO.BusLine bl= DataSource.ListBusLines.Find(b => b.ID == LineID);
        //    if (bl != null)
        //    {
        //        DataSource.ListBusLines.Remove(bl);
        //        bl.BegStopNum = BusStopID1;
        //        bl.EndStopNum = BusStopID2;
        //        DataSource.ListBusLines.Add(bl.Clone());
        //    }
        //    else
        //        throw new DO.BadBusLineIdException(LineID, $"bad student id: {LineID}");
        //}
        public void UpdateBusLine(BusLine newBusLine)//updates a busLine
        {
            DO.BusLine bl = DataSource.ListBusLines.Find(b => b.ID == newBusLine.ID);
            if (bl != null)
            {
                DataSource.ListBusLines.Remove(bl);
                DataSource.ListBusLines.Add(newBusLine.Clone());
            }
            else
                throw new DO.BadBusLineIdException(newBusLine.ID, $"bad student id: {newBusLine.ID}");
        }
        public void DeleteBusLine(int id)
        {
            //find the busLine
            DO.BusLine bl = DataSource.ListBusLines.Find(b => b.ID == id);
            if (bl != null)
            {
                DataSource.ListBusLines.Remove(bl);
            }
            else
                throw new DO.BadBusLineIdException( id,"bus line not found!");
        }
        #endregion***BusLine***
        #region***CRUD, LineOnTrip***
        public IEnumerable<LineOnTrip> GetAllLinesOnTrip()//gets a list of lines on trip
        {
            return new List<LineOnTrip>();
        }
        public IEnumerable<LineOnTrip> GetAllLinesOnTripBy(Predicate<LineOnTrip> predicate)//gets a list of lines on trip according to a condition
        {
            return new List<LineOnTrip>();
        }
        public void AddLineOnTrip(LineOnTrip busLine)
        {

        }
        public void UpdateLineOnTrip(LineOnTrip busLine)
        {

        }
        public void DeleteLineOnTrip(int id)
        {

        }
        #endregion
        #region***CRUD, AdjacentStops***
        public IEnumerable<AdjacentStops> GetAllAdjacentStops()//gets a list of AdjacentStops
        {
            return from AdjacentStop in DataSource.ListAdjacentStops
                   select AdjacentStop.Clone();
        }
        public IEnumerable<AdjacentStops> GetAllAdjacentStopsBy(Predicate<AdjacentStops> predicate)//gets a list of AdjacentStops according to a condition
        {
            return from adjStop in DataSource.ListAdjacentStops
                   where predicate(adjStop)
                   select adjStop.Clone();
        }
        public AdjacentStops GetAdjacentStops(int id1, int id2)
        {
            DO.AdjacentStops adjS = DataSource.ListAdjacentStops.Find(s =>( s.StopCode1 == id1)&& (s.StopCode2 == id2));//adjs holds the adjacentstop  to get
            // try { Thread.Sleep(2000); } catch (ThreadInterruptedException ex) { }
            if (adjS != null)
                return adjS.Clone();
            else if (DataSource.ListAdjacentStops.Find(s => (s.StopCode2 == id2))!=null)//if the adjacent bus stop with id1 dosen't exist
                throw new DO.BadBusStopIdException(id1, $"bad bus stop id: {id1}");
               else//if the adjacent bus stop with id2 dosen't exist
                throw new DO.BadBusStopIdException(id2, $"bad bus line id: {id2}");
                
        }
        public void AddAdjacentStops(AdjacentStops busStops)
        {
            if (DataSource.ListAdjacentStops.Exists(s => (s.StopCode1 == busStops.StopCode1) && (s.StopCode2 == busStops.StopCode2)))
                throw new DO.BadBusStopIdException(busStops.StopCode1, "Duplicate bus stop ID for adjacent stops");
            DataSource.ListAdjacentStops.Add(busStops.Clone());
        }
        public void UpdateAdjacentStops(AdjacentStops busStops)
        {
            AdjacentStops adjStop = DataSource.ListAdjacentStops.FirstOrDefault(s => (s.StopCode1 == busStops.StopCode1) && (s.StopCode2 == busStops.StopCode2));
            if(adjStop!=null)
            {
                DataSource.ListAdjacentStops.Remove(adjStop);
                adjStop.Distance = busStops.Distance;
                adjStop.AvgTravelTime = TimeSpan.Parse( (50 * busStops.Distance).ToString());
                DataSource.ListAdjacentStops.Add(adjStop.Clone());
            }
           else if (DataSource.ListAdjacentStops.Find(s => (s.StopCode2 == adjStop.StopCode1)) != null)//if the adjacent bus stop with id1 dosen't exist
                throw new DO.BadBusStopIdException(adjStop.StopCode1, $"bad bus stop id: {adjStop.StopCode1}");
            else//if the adjacent bus stop with id2 dosen't exist
                throw new DO.BadBusStopIdException(adjStop.StopCode2, $"bad bus line id: {adjStop.StopCode2}");
        }
        public void DeleteAdjacentStops(int id1, int id2)
        {

        }
        #endregion***AdjacentStops***
        #region***CRUD BusOnTrip-dont implement yet***
       /* public IEnumerable<BusOnTrip> GetAllBusOnTripBy()//gets a list of Buses On Trips
        {

        }
        public IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<BusOnTrip> predicate)//gets a list of Buses On Trips according to a condition
        {

        }
        public BusOnTrip GetBusOnTrip(int id1)
        {

        }
        public void AddBusOnTrip(BusOnTrip bus)
        {

        }
        public void UpdateBusOnTrip(BusOnTrip bus)
        {

        }
        public void DeleteBusOnTrip(int id1)
        {

        }
        */
        #endregion***CRUD BusOnTrip***
        #region***CRUD LineStop***
        public IEnumerable<LineStop> GetAllLineStop()//gets a list of stops on a line
        {
            return from busLineStop in DataSource.ListBusLineStops
                   select busLineStop.Clone();
        }
        /// <summary>
        /// gets a collection of bus lines stops with a condition
        /// </summary>
        /// <param name="predicate"></param>condition of which line stop to get
        /// <returns></returns>
        public IEnumerable<LineStop> GetAllLineStopBy(Predicate<LineStop> predicate)//gets a list of stops on a line according to a condition
        {
            return from busLineStop in DataSource.ListBusLineStops
                   where predicate(busLineStop)
                   select busLineStop.Clone();
        }
        public LineStop GetLineStop(int stopId, int lineId)
        {
            DO.LineStop ls = DataSource.ListBusLineStops.Find(l => (l.StationCode == stopId)&& (l.LineID == lineId));//bl holds the busLineStop to get
            // try { Thread.Sleep(2000); } catch (ThreadInterruptedException ex) { }
            if (ls!=null)
                return ls.Clone();
           else if (DataSource.ListBusLineStops.Find(l => (l.LineID == lineId))!=null)//if the line stop with stopId dosen't exist
                   throw new DO.BadBusStopIdException(stopId, $"bad bus stop id: {stopId}");
                else//if the line stop with LineId dosen't exist
                    throw new DO.BadBusLineIdException(lineId, $"bad bus line id: {lineId}");       
        }
        public void AddLineStop(LineStop lineStop)
        {
            if (DataSource.ListBusLineStops.FirstOrDefault(l => (l.LineID == lineStop.LineID)&&(l.StationCode==lineStop.StationCode))!=null)
                throw new DO.BadBusStopIdException(lineStop.LineID, "Duplicate line stop ID");
            DataSource.ListBusLineStops.Add(lineStop.Clone());
        }
        public void UpdateLineStop(LineStop lineStop)//updates a single line stops index in a bus line
        {
            DO.LineStop ls = DataSource.ListBusLineStops.Find(l => (l.LineID == lineStop.LineID) && (l.StationCode == lineStop.StationCode));
            if (ls != null)
            {
                DataSource.ListBusLineStops.Remove(ls);//remove the line stop from data source
                DataSource.ListBusLineStops.Add(lineStop.Clone());//insert the updated line stop to data source
            }
            else
                throw new DO.BadBusLineIdException(lineStop.LineID, $"bad line stop id: {lineStop.LineID}");
        }
        public void UpdateLineStopByLine(Predicate<LineStop> predicate)//updates line stops in a specific line
        {   int index = 1;
            IEnumerable<DO.LineStop> ls = DataSource.ListBusLineStops.FindAll(predicate);//finds all line stops that fill the condition
            if (ls.Count() != 0)
            {
                foreach (var lineStop in ls)
                {
                    DataSource.ListBusLineStops.Remove(lineStop);//remove the lineStop 
                    lineStop.StationIndex = index++;//update the index of the lineStop
                    DataSource.ListBusLineStops.Add(lineStop.Clone());//add the updated line stop 
                }
            }
        }
      public   void UpdateLineStopByBusStop(int PrevStationCode, int NewStationCode)////updates line stop of a spesific bus stop
        {
            IEnumerable<DO.LineStop> ls = DataSource.ListBusLineStops.FindAll(x=>x.StationCode==PrevStationCode);//finds all bus stops that have the previous station code
            foreach (var lineStop in ls)
            {
                DataSource.ListBusLineStops.Remove(lineStop);//remove the lineStop 
                lineStop.StationCode=NewStationCode;//update the station code of the lineStop
                DataSource.ListBusLineStops.Add(lineStop.Clone());//add the updated line stop 
            }
        }
        /// <summary>
        /// deletes a line Stop from the data source
        /// </summary>
        /// <param name="lineId"></param>line id of the linestop to delete
        /// <param name="stationCode"></param>station code the linestop to delete
        public void DeleteLineStop(int lineId,int stationCode)
        {
            if (!DataSource.ListBusLineStops.Exists(item => (item.LineID == lineId)&&(item.StationCode==stationCode)))
            {
                if (!DataSource.ListBusLineStops.Exists(item => (item.LineID == lineId)))//if the line id is incorrect
                    throw new BadBusLineIdException(lineId);
                else//if the station code is invalid
                    throw new BadBusStopIdException(stationCode);
            }
            DS.DataSource.ListBusLineStops.RemoveAll(item=>(item.LineID == lineId) && (item.StationCode == stationCode));
        }
      /// <summary>
      /// deletes all line stops with a condition
      /// </summary>
      /// <param name="func"></param>
      public void DeleteLineStopBy(Predicate<DO.LineStop> predicate)
        {
            try
            {
                DataSource.ListBusLineStops.RemoveAll(predicate);
            }
            catch(NullReferenceException)
            {
                
            }
        }
        #endregion***CRUD LineStop***
        #region***CRUD User***
        public IEnumerable<User> GetAllUser()//gets a list of Users
        {
            return from User in DataSource.ListUsers
                   select User.Clone();
        }
        public IEnumerable<User> GetAllUserBy(Predicate<User> predicate)//gets a list of Users according to a condition
        {
            IEnumerable<User> bl = new List<User>();
            return bl;
        }
        public User GetUser(string name)
        {
            DO.User bs = DataSource.ListUsers.Find(b => b.Username == name);//bs holds the bus  to get
                                                                           // try { Thread.Sleep(2000); } catch (ThreadInterruptedException ex) { }
            if (bs != null)
                return bs.Clone();
            else
                throw new DO.BadUserNameException(name, $"bad username: {name}");
        }
        public void AddUser(User user)
        {

        }
       public void UpdateUser(User user)
        {

        }
        public void DeleteUser(string username)
        {

        }
        #endregion***CRUD User***

    }
}
