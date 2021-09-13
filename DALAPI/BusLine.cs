using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLine:IClonable
    {
        private int id;
        private int lineNumber;
        private Region region;
        private int begStopNum;
        private int endStopNum;
        private Status availability;
        #region***properites***
        public int ID { get => id; set => id = value; }
        public int LineNumber { get => lineNumber; set => lineNumber = value; }
        public Region Region { get => region; set => region = value; }
        public int BegStopNum { get => begStopNum; set => begStopNum = value; }
        public int EndStopNum { get => endStopNum; set => endStopNum = value; }
        public Status Availability { get => availability; set => availability = value; }
        #endregion***properties***
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
