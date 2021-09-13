using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DALAPI
{
	public interface IDAL
	{
		#region***CRUD Bus***
		IEnumerable<Bus> GetAllBus();//gets a list of buses
		IEnumerable<Bus> GetAllBusBy(Predicate<Bus> predicate);//gets a list buses according to a condition
		Bus GetBus(int id);
		void AddBus();
		void UpdateBus(Bus bus);
		void DeleteBus(int id);
		#endregion***CRUD Bus***
		#region ***CRUD BusStop***
		IEnumerable<BusStop> GetAllBusStops();//gets a list of bus stops
		IEnumerable<BusStop> GetAllBusStopsBy(Predicate<BusStop> predicate);//gets a list of bus stops according to a condition
		BusStop GetBusStop(int id);
		void AddBusStop(BusStop busStop);
		void UpdateBusStop(BusStop myBusStop, int id);
		void DeleteBusStop(int stationCode);
		#endregion
		#region***CRUD, AdjacentStops***
		IEnumerable<AdjacentStops> GetAllAdjacentStops();//gets a list of AdjacentStops
		IEnumerable<AdjacentStops> GetAllAdjacentStopsBy(Predicate<AdjacentStops> predicate);//gets a list of AdjacentStops according to a condition
		 AdjacentStops GetAdjacentStops(int id1, int id2);
		void AddAdjacentStops(AdjacentStops busStops);
		void UpdateAdjacentStops(AdjacentStops busStops);
		void DeleteAdjacentStops(int id1, int id2);
		#endregion***AdjacentStops***
		#region***CRUD BusLine***
		IEnumerable<BusLine> GetAllBusLines();//gets a list of bus lines
		IEnumerable<BusLine> GetAllBusLinessBy(Predicate<BusLine> predicate);//gets a list of bus lines according to a condition
		BusLine GetBusLine(int id);
		void AddBusLine(BusLine busLine);
		// void UpdateBusLine(int LineID, int BusStopID1,int BusStopID2);//updates begin and end stop of bus line
		void UpdateBusLine(BusLine newBusLine);//updates a busLine
		void DeleteBusLine(int id);
		#endregion***BusLine***
		#region***CRUD LineOnTrip***
		 IEnumerable<LineOnTrip> GetAllLinesOnTrip();//gets a list of lines on trip
		 IEnumerable<LineOnTrip> GetAllLinesOnTripBy(Predicate<LineOnTrip> predicate);//gets a list of lines on trip according to a condition
		 void AddLineOnTrip(LineOnTrip busLine);
		 void UpdateLineOnTrip(LineOnTrip busLine);
		 void DeleteLineOnTrip(int id);
 #endregion***LineOnTrip***
		// #region***CRUD BusOnTrip***
		// / IEnumerable<BusOnTrip> GetAllBusOnTripBy();//gets a list of Buses On Trips
		//   IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<BusOnTrip> predicate);//gets a list of Buses On Trips according to a condition
		//   BusOnTrip GetBusOnTrip(int id1);
		//   void AddBusOnTrip(BusOnTrip bus);
		//   void UpdateBusOnTrip(BusOnTrip bus);
		//   void DeleteBusOnTrip(int id1);
		   
		//#endregion***CRUD BusOnTrip***
		#region***CRUD LineStop***
		IEnumerable<LineStop> GetAllLineStop();//gets a list of stops on a line
		IEnumerable<LineStop> GetAllLineStopBy(Predicate<LineStop> predicate);//gets a list of stops on a line according to a condition
		LineStop GetLineStop(int id1, int id2);
		void AddLineStop(LineStop lineStop);
		void UpdateLineStop(LineStop lineStop);//updates a single  line stops  index in a bus line
		void UpdateLineStopByLine(Predicate<LineStop> predicate);////updates line stops in a spesific line
		void UpdateLineStopByBusStop(int PrevStationCode, int NewStationCode);////updates line stop of a spesific bus stop
		void DeleteLineStop(int lineId, int stationCode);
		void DeleteLineStopBy(Predicate<DO.LineStop> predicate);
		#endregion***CRUD LineStop***
		#region***CRUD User***
		IEnumerable<User> GetAllUser();//gets a list of Users
	   IEnumerable<User> GetAllUserBy(Predicate<User> predicate);//gets a list of Users according to a condition
		User GetUser(string name);
		void AddUser(User user);
		//void UpdateUser(User user);
		void DeleteUser(string username);
		#endregion***CRUD User***
		//  #region***CRUD UserTrip***
		//  IEnumerable<UserTrip> GetAllUserTrip();//gets a list of Users Trips
		////  IEnumerable<UserTrip> GetAllUserTripBy(Predicate<UserTrip> predicate);//gets a list of Users Trips according to a condition
		//  UserTrip GetUserTrip(int id);
		//  void AddUserTrip(UserTrip userTrip);
		//  void UpdateUserTrip(UserTrip userTrip);
		//  void DeleteUserTrip(int id1, int id2);
		//  #endregion***CRUD UserTrip***
	}
}