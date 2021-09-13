﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7128_3442
{
    class BusStop
    {
        protected int stopCode;
        protected double latitude;
        protected double longitude;
       // string address;
        #region***properties***
        /// <summary>
        /// get  method for stopCode
        /// </summary>
        public int SC
        {
            get => stopCode; 
        }
        /// <summary>
        /// get  method for latitude
        /// </summary>
        public double LA
        {
            get=> latitude; 
        }
        /// <summary>
        /// get  method for longitude
        /// </summary>
        public double LO
        {
            get=> longitude; 
        }
        /// <summary>
        /// get method for address
        /// </summary>
        /*public string A
        {
            get=>address; 
        }*/
        #endregion
        #region***c-tor***
        /// <summary>
        /// default c-tor
        /// </summary>
        public BusStop()
        {
            Random r = new Random();
            stopCode = r.Next(100000,1000000);
            latitude =  31 + r.NextDouble() * (33.3 - 31);
            longitude = 34.3 + r.NextDouble() * (35.5 - 34.3);
        }
        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="userCode"></param> users stop code
         public BusStop(int userCode)
        {
            Random r = new Random();
            stopCode = userCode;
            latitude =  31 + r.NextDouble() * (33.3 - 31);
            longitude = 34.3 + r.NextDouble() * (35.5 - 34.3);
        }
        #endregion
        public override string ToString()
        {
            return "Bus stop Code:"+SC+" latitude:"+LA+ "°, longitude:" + LO+ "°";
        }
    }
}
