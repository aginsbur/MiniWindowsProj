using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineOnTrip
    {
        private int lineId;
        private int lineNum;
        private TimeSpan startTime;
        private string destination;
        private TimeSpan timeOfArrival;

        #region***properties***
        public int LineId { get => lineId; set => lineId = value; }
        public int LineNum { get => lineNum; set => lineNum = value; }
        public TimeSpan StartTime { get => startTime; set => startTime = value; }
        public string Destination { get => destination; set => destination = value; }
          public TimeSpan TimeOfArrival { get => timeOfArrival; set => timeOfArrival = value; }
        #endregion***properties***
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
