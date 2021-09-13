using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineOnTrip
    {
        private int lineId;
        private TimeSpan startTime;//save in 3 sections

        #region***properties***
        public int LineId { get => lineId; set => lineId = value; }
        public TimeSpan StartTime { get => startTime; set => startTime = value; }
        #endregion***properties***
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
