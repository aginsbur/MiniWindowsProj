using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class AdjacentStops
    {
        private int stopCode1;
        private int stopCode2;
        private double distance;
        private TimeSpan avgTravelTime;

        #region***properties***
        public int StopCode1 { get => stopCode1; set => stopCode1 = value; }
        public int StopCode2 { get => stopCode2; set => stopCode2 = value; }
        public double Distance { get => distance; set => distance = value; }
        public TimeSpan AvgTravelTime { get => avgTravelTime; set => avgTravelTime = value; }
        #endregion***properties***
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
