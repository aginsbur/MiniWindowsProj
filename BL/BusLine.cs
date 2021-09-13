using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusLine:IEnumerable,IComparable
    {
        private int id;
        private int lineNumber;
        private Region region;
        private int begStopNum;
        private int endStopNum;
        private Status availability;
        private IEnumerable<BusStop> stopsInLine=new List<BusStop>();
       
        #region***properites***
        public int ID { get => id; set => id = value; }
        public int LineNumber { get => lineNumber; set => lineNumber = value; }
        public Region Region { get => region; set => region = value; }
        public int BegStopNum { get => begStopNum; set => begStopNum = value; }
        public int EndStopNum { get => endStopNum; set => endStopNum = value; }
        public Status Availability { get => availability; set => availability = value; }
        public IEnumerable<BusStop> StopsInLine { get => stopsInLine; set => stopsInLine = value; }
        #endregion***properties***
        public IEnumerator GetEnumerator()
        {
            return stopsInLine.GetEnumerator();
        }
        int IComparable.CompareTo(object obj)
        {
            BusLine bs = (BusLine)obj;
            if (this.ID > bs.ID)
                return 1;
            else if (this.ID ==bs.ID)
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
