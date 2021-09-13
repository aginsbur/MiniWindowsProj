using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BL
{
   public interface IBL
    {
        #region ***BusLine***
        IEnumerable<BO.BusStop> getBusStopsInLine(BO.BusLine myBusLine);
        IEnumerable<BO.BusStop> getBusStopsNotInLine(BO.BusLine myBusLine);
        IEnumerable<BO.BusLine> getAllBusLines();
        IEnumerable<BO.BusLine> getBusLinewithOutID(BO.BusLine myBusLine);
        BO.BusLine getBusLine(int myLineID);

        //void addBusStop2Line(BO.BusStop myBusStop, BO.AdjacentStops adjacentStop, int myLineID, int index);
        void deleteBusStopFromLine(BO.BusStop myBusStop, int myLineID);
        void addBusLine(BO.BusLine myBusLine);
        void deleteBusLine(BO.BusLine myBusLine);
        #endregion
        #region***BusStop***
        BO.BusStop getBusStop(int myStationCode);
        IEnumerable<BO.BusStop> getAllBusStops();
        IEnumerable<BO.BusStop> getAllBusStopsBy(Func<BO.BusStop, bool> func);
       IEnumerable<BO.BusLine> getBusLinesInStation(BO.BusStop myBusStop);
        void addBusStop(BO.BusStop myBusStop);
        void updateBusStop(BO.BusStop newBusStop,BO.BusStop currBusStop);
        void deleteBusStop(BO.BusStop myBusStop);
        #endregion
        #region***LineStop***
        void updateLineStop(BO.LineStop newLineStop);
        void addLineStop(int lineId, int stationNumber, int index);
        #endregion***LineStop***
        #region***adjacentStops***
        void addAdjacentStops(BO.AdjacentStops stops);
        BO.AdjacentStops getAdjacentStops(int id1, int id2);
        void updateAdjacentStops(BO.AdjacentStops stops);

        #endregion***adjacentStops***
        #region***Bus***
        IEnumerable<BO.Bus> getAllBusses();//gets a list of buses
        IEnumerable<BO.Bus> GetAllBusBy(Predicate<BO.Bus> predicate);//gets a list buses according to a condition
        BO.Bus GetBus(int id);
        void AddBus();
        void UpdateBus(BO.Bus bus);
        void updateBusStatus(BO.Bus bus);
        void DeleteBus(int id);
        #endregion***Bus***
        #region***LineOnTrip***
        IEnumerable<BO.LineOnTrip> getAllLinesOntripByBusStopAndHour(int stationCode, TimeSpan hour);
        void addLinesOnTrip(int lineId,int frequencyInHour);
        #endregion***LineOnTrip***
        #region***Enums***
        IEnumerable<string> getAllRegions();
        #endregion***Enums***
        #region***User***
        IEnumerable<User> GetAllUser();//gets a list of Users
        IEnumerable<User> GetAllUserBy(Predicate<User> predicate);//gets a list of Users according to a condition
        User GetUser(string name);
        void AddUser(string username,string password);
        void UpdateUser(BO.User user);
        void DeleteUser(int id1);
        #endregion***User***
        #region***UserTrip***
        IEnumerable<BO.BusLine> GetUserTrip(BO.BusStop bus1,BO.BusStop bus2);
        #endregion***UserTrip***
    }
}

