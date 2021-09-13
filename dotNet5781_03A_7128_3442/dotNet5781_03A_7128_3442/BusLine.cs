using System;
using System.Collections;
using System.Collections.Generic;
using System.Device.Location;
using System.Threading;

namespace dotNet5781_03A_7128_3442
{
    public enum Region { NORTH, SOUTH, EAST, WEST, CENTER, GENERAL };

    public class BusLine : IEnumerable, IComparable<BusLine>
    {
        static int busLineCounter = 0;
        public Random rnd = new Random();
        public List<Route_Bus_Stop> Line = new List<Route_Bus_Stop>();
        private int lineNumber;
        Region region;
        Route_Bus_Stop begStop;
        Route_Bus_Stop endStop;
        TimeSpan Ttime;
        #region***properties***
        /// <summary>
        /// get method for BusLine's list- "Line"
        /// </summary>
        public List<Route_Bus_Stop> L
        { get => Line; }
        /// <summary>
        /// get method for lineNumber
        /// </summary>
        public int LN
        {
            get => lineNumber;
            //set => lineNumber;
        }
        /// <summary>
        /// get method Line region
        /// </summary>
        public Region R
        { get => region; }
        /// <summary>
        /// get method for beginStop in busLine
        /// </summary>
        public Route_Bus_Stop BG
        { get => begStop; }
        /// <summary>
        /// get method for endStop in busLine
        /// </summary>
        public Route_Bus_Stop ES
        { get => endStop; }
        /// <summary>
        /// get method for Ttime-total travel time  in busLine
        /// </summary>
        public TimeSpan TT
        { get => Ttime; }
        #endregion
        /// <summary>
        /// default c-tor
        /// </summary>
        public BusLine()
        {
            // lineNumber = rnd.Next(100,1000);
            lineNumber = ++busLineCounter;
            Ttime = new TimeSpan(0, 0, 0);
            Type type = typeof(Region);
            Array values = type.GetEnumValues();
            region = (Region)values.GetValue(rnd.Next(values.Length));
            Thread.Sleep(100);
        }
        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="userLNum"></param> users bus line number
        public BusLine(int userLNum)
        {
            lineNumber = userLNum;
            Ttime = new TimeSpan(0, 0, 0);
            Type type = typeof(Region);
            Array values = type.GetEnumValues();
            region = (Region)values.GetValue(rnd.Next(values.Length));
            Thread.Sleep(100);
        }
        public IEnumerator GetEnumerator()
        {
            return Line.GetEnumerator();
        }
        public int CompareTo(BusLine other)
        {
            return Ttime.CompareTo(other.Ttime);
        }
        /// <summary>
        /// returns the number,region and buses in the busline
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s1 = null;
            foreach (Route_Bus_Stop item in Line)//adds each bus_route_stop in the bus line into a string s
                s1 += (item.CS.SC + "\n");
            return "Bus line number:" + this.lineNumber + "\nRegion of bus line:" + this.region + "\nBus stops in line:\n" + s1;
        }
        /// <summary>
        /// adds a busStop to the bus line
        /// </summary>
        /// <param name="busStopCode"></param>
        public void Add(Route_Bus_Stop userStop)
        {
            int index = rnd.Next(0, this.Line.Count);
            Thread.Sleep(100);
            if (index == 0)//if the busStop is being added to the front of the Line
            {
                if (Line.Count != 0)
                    begStop.TT = new TimeSpan(rnd.Next(0, 12), rnd.Next(0, 60), rnd.Next(0, 60));//updates travel time from previous stop
                begStop = userStop;
            }
            else
            {
                if (index == Line.Count - 1)//if the busStop is being added to the end of the Line
                    endStop = userStop;
                var scoord = new GeoCoordinate(userStop.CS.LA, userStop.CS.LO);
                var ecoord = new GeoCoordinate(Line[index - 1].CS.LA, Line[index - 1].CS.LO);//gets coordinates of the previous bus stop
                userStop.DT = scoord.GetDistanceTo(ecoord);//calculates distance from previous bus stop
                userStop.TT = new TimeSpan(rnd.Next(0, 12), rnd.Next(0, 60), rnd.Next(0, 60));//inserts travel time from previous stop
                Thread.Sleep(100);
            }
            Line.Insert(index, userStop);
        }
        /// <summary>
        /// removes a bus stop from the bus line
        /// </summary>
        /// <param name="userBusStop"></param>the bus stop to remove
        public void remove(Route_Bus_Stop userBusStop)
        {
            try
            {
                int index = Line.IndexOf(userBusStop);
                if (index == 0)//if we are removing the first bus stop
                {
                    if (Line.Count != 1)
                    {
                        begStop = Line[1];
                        begStop.DT = 0;
                        begStop.TT = new TimeSpan(0, 0, 0);
                    }
                    else
                        begStop = null;
                }
                else
                {
                    if (index != Line.Count - 1)//if we are not removing the last bus stop
                    {
                        var scoord = new GeoCoordinate(Line[index].CS.LA, Line[index].CS.LO);
                        var ecoord = new GeoCoordinate(Line[index - 2].CS.LA, Line[index - 2].CS.LO);//gets coordinates of the previous bus stop
                        Line[index].DT = scoord.GetDistanceTo(ecoord);//calculates distance from previous bus stop
                    }
                    else
                        endStop = Line[Line.Count - 1];

                }
                Line.Remove(userBusStop);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// returns true if a bus stop is found in this busline
        /// </summary>
        /// <param name="userCode"></param> users bus stop code 
        /// <returns></returns>
        public bool search(int userCode)
        {
            foreach (var item in Line)//iterates through the bus stops in the bus line
                if (item.CS.SC == userCode)//compares each  bus stop code  in the bus line with the users chosen bus stop
                    return true;
            throw new ArgumentException("Error! bus Stop was not found in this bus Line!");
        }
        /// <summary>
        /// searches for a bus stop and returns it
        /// </summary>
        /// <param name="userCode"></param> users bus stop code
        /// <returns></returns>
        public Route_Bus_Stop searchStop(int userCode)
        {
            foreach (var item in Line)//iterates through the bus stops in the bus line
                if (item.CS.SC == userCode)//compares each  bus stop code  in the bus line with the users chosen bus stop
                    return item;
            throw new ArgumentException("Error! bus Stop was not found in this bus Line!");
        }

        /// <summary>
        /// calculates distance between two bus stops
        /// </summary>
        /// <param name="stop1"></param>stop that distance is calculated from
        /// <param name="stop2"></param>stop that distance is calculated to
        /// <returns></returns>
        public double distance(Route_Bus_Stop stop1, Route_Bus_Stop stop2)
        {
            try
            {
                search(stop1.CS.SC);//checks if stop1 is in the bus Line
                search(stop2.CS.SC);//checks if stop2 is in the bus Line
                double distance = 0;
                foreach (var item in Line)//iterates through the bus_route_stops in the bus line 
                { //if the current bus stop is in between the first and last stop
                    if (Line.IndexOf(item) > Line.IndexOf(stop1) || Line.IndexOf(item) <= Line.IndexOf(stop2))
                        distance += item.DT;
                }
                return distance;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error! Could not calculate distance for inputed bus stops");
            }
        }
        /// <summary>
        /// returns a subroute starting at stop a and ending at point b
        /// </summary>
        /// <returns></returns>
        public BusLine subRoute(Route_Bus_Stop s1, Route_Bus_Stop s2)
        {
            try
            {
                BusLine temp = new BusLine(this.lineNumber);
                temp.region = this.region;
                foreach (var item in Line)
                    temp.Add(item);
                return temp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// calculates timespan between two bus stops
        /// </summary>
        /// <param name="stop1"></param>stop that trip time is calculated from
        /// <param name="stop2"></param>stop that trip time is calculated to
        /// <returns></returns>
        public TimeSpan tripTime(Route_Bus_Stop stop1, Route_Bus_Stop stop2)
        {
            try
            {
                search(stop1.CS.SC);//checks if stop1 is in the bus Line
                search(stop2.CS.SC);//checks if stop2 is in the bus Line
                TimeSpan tripTime = new TimeSpan(0, 0, 0);
                foreach (var item in Line)//iterates through the bus_route_stops in the bus line 
                { //if the current bus stop is in between the first and last stop
                    if (Line.IndexOf(item) > Line.IndexOf(stop1) || Line.IndexOf(item) <= Line.IndexOf(stop2))
                        tripTime += item.TT;
                }
                return tripTime;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error! Could not canlculate trip time for inputed bus stops");
            }
        }

    }
}
