
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7128_3442
{
    
    class Route_Bus_Stop
    {
        protected BusStop currentStop;
        protected double distance=0;//distance from previous bus stop
        protected TimeSpan travTime;// travel time from previous bus stop
        public static int counter = 0;
        /// <summary>
        /// default c-tor
        /// </summary>
        public Route_Bus_Stop()
        { 
            Random r = new Random(); 
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
            Random r = new Random();
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
    }

}
