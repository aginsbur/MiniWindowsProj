using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   /* public struct position
    {
        private double latitude;//latitude of bus stop
        private double longitude;//longitude of bus stop

        #region***properties***
        public double Latitude
        {
            get => latitude;
            set { latitude = value; }

        }
        public double Longitude
        {
            get => longitude;
            set { Longitude = value; }

        }
        #endregion
    }
    */
    public class BusStop : IClonable
    {
        private int stationCode;
        //private position position;
        private double latitude;//latitude of bus stop
        private double longitude;//longitude of bus stop
        private string address;
        private Status availability;

        #region***properties***
        public int StationCode { get => stationCode; set => stationCode = value; }
        //public position Position { get => position; set => position = value; }
        public string Address { get => address; set => address = value; }
        public Status Availability { get => availability; set => availability = value; }
        public double Latitude
        { get => latitude; set => latitude = value; }
        public double Longitude
        { get => longitude; set => longitude = value; }
        #endregion

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
