
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03A_7128_3442
{

    public class Route_Bus_Stop
    {
        protected BusStop currentStop;
        protected double distance = 0;//distance from previous bus stop
        protected TimeSpan travTime;// travel time from previous bus stop
        public static int counter = 0;
        Random r = new Random();
        /// <summary>
        /// default c-tor
        /// </summary>
        public Route_Bus_Stop()
        {
            currentStop = new BusStop();
            distance = 0;
            travTime = new TimeSpan(0, 0, 0);
            counter++;
        }
        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="userCode"></param>
        public Route_Bus_Stop(int userCode)
        {
            currentStop = new BusStop(userCode);
            distance = 0;
            travTime = new TimeSpan(0, 0, 0);
            counter++;
        }
        #region***properties***
        /// <summary>
        /// get method for distance
        /// </summary>
        public double DT
        {
            get => distance;
            set { distance = value; }
        }
        /// <summary>
        /// get method for travelTime
        /// </summary>
        public TimeSpan TT
        {
            get => travTime;
            set { travTime = value; }
        }
        /// <summary>
        /// get method for currentStop 
        /// </summary>
        public BusStop CS
        {
            get => currentStop;
        }
        #endregion

        public override string ToString()
        {
            return "Bus stop number:" + this.CS.SC + " longitude" + this.CS.LO + " latitude" + this.CS.LA + " " + this.TT;
        }
    }

}
