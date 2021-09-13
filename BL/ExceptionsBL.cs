using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    //class handles bad bus stop id exception
    public class BadBusStopIdException : Exception
    {
        public int ID;
        public BadBusStopIdException(string message, Exception innerException) :
        base(message, innerException) => ID = ((DO.BadBusStopIdException)innerException).ID;
        public override string ToString() => base.ToString() + $", bad bus stop id: {ID}";

    }
    public class BadBusLineIdException : Exception
    {
        public int ID;
        public BadBusLineIdException(string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.BadBusLineIdException)innerException).ID;
        public override string ToString() => base.ToString() + $", bad bus line id: {ID}";

    }
    public class BadBusIdException : Exception
    {
        public int ID;
        public BadBusIdException() { }
        public BadBusIdException(string message) : base(message) { }
        public BadBusIdException(string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.BadBusIdException)innerException).ID;
        public override string ToString() => base.ToString() + $", bad bus id: {ID}";

    }

    public class BadBusLineToDelException : Exception
    {
        public  BadBusLineToDelException(string message) : base(message){ }

    }

    public class BadUserNameException : Exception
    {
        public string Name;
        public BadUserNameException(string userName) : base() => Name = userName;
        public BadUserNameException(string userName, string message) :
            base(message) => Name = userName;
        public BadUserNameException(string userName, string message, Exception innerException) :
            base(message, innerException) => Name = userName;

        public override string ToString() => base.ToString() + $", bad user Name: {Name}";

    }
}
