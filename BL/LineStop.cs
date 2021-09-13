using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    public class LineStop : IComparable
    {
        int lineID;
        int stationCode;
        int stationIndex;
        #region***properties***
        public int LineID { get => lineID; set => lineID = value; }
        public int StationCode { get => stationCode; set => stationCode = value; }
        public int StationIndex { get => stationIndex; set => stationIndex = value; }
        public IEnumerable<BusLine> LinesPassingThroughStop { get; set; }
        #endregion
        int IComparable.CompareTo(object obj)
        {
            LineStop ls = (LineStop)obj;
            if (this.StationIndex > ls.StationIndex)
                return 1;
            else if (this.StationIndex == ls.StationIndex)
                return 0;
            else 
                return -1;
        }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
