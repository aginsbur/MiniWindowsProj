using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStop
    {
        int lineID;
        int stationCode;
        int stationIndex;
        #region***properties***
        public int LineID { get => lineID; set => lineID = value; }
        public int StationCode { get => stationCode; set => stationCode = value; }
        public int StationIndex { get => stationIndex; set => stationIndex = value; }
        #endregion

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
