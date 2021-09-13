using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusOnTrip
    {
        int id;
        int licenseNum;
        int lineId;
        TimeSpan plannedTakeOff;
        TimeSpan actualTakeOff;
        int prevStation;
        TimeSpan prevStationAt;
        TimeSpan nextStationAt;

        #region ***properties***
        public int Id { get => id; set => id = value; }
        public int LicenseNum { get => licenseNum; set => licenseNum = value; }
        public int LineId { get => lineId; set => lineId = value; }
        public TimeSpan PlannedTakeOff { get => plannedTakeOff; set => plannedTakeOff = value; }
        public TimeSpan ActualTakeOff { get => actualTakeOff; set => actualTakeOff = value; }
        public int PrevStation { get => prevStation; set => prevStation = value; }
        public TimeSpan PrevStationAt { get => prevStationAt; set => prevStationAt = value; }
        public TimeSpan NextStationAt { get => nextStationAt; set => nextStationAt = value; }
        #endregion

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
