using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusStop: IComparable,IEnumerable
    {
        private int stationCode;
        private double latitude;//latitude of bus stop
        private double longitude;//longitude of bus stop
        private string address;
        private Status availability;
        private int indexInLine;
        private IEnumerable<BusLine> busLinesUsingStation=new List<BusLine>();

        #region***properties***
        public int StationCode { get => stationCode; set => stationCode = value; }
        public string Address { get => address; set => address = value; }
        public Status Availability { get => availability; set => availability = value; }
        public double Latitude
        {get => latitude; set=> latitude = value;  }
        public double Longitude
        {get => longitude; set =>longitude = value; }
        public int IndexInLine
        { get => indexInLine; set => indexInLine = value; }
        public IEnumerable<BusLine> BusLinesUsingStation
        { get => busLinesUsingStation; set => busLinesUsingStation = value; }
        #endregion
        /// <summary>
        /// sorts the bus stops according to there station code
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        int IComparable.CompareTo(object obj)
        {
          BusStop bs = (BusStop)obj;
            if ( this.StationCode >bs.stationCode)
                return 1;
            else if (this.stationCode == bs.stationCode)
                return 0;
            else
                return -1;

        }
        
        public IEnumerator GetEnumerator()
        {
            return BusLinesUsingStation.GetEnumerator();
        }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
  }
