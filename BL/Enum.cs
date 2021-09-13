using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
        public enum BusStatus { READY2GO, NEEDSERVICE, NEEDREFUEL };
        public enum Status { INUSE, NOTINUSE };//used for busStop, busLine,
        public enum Region { NORTH, SOUTH, EAST, WEST, CENTER, GENERAL };
    
}
