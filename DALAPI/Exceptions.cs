using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    //class handles bad bus stop id exception
    public class BadBusStopIdException:Exception
    {
        public int ID;
        public BadBusStopIdException(int id) : base() => ID = id;
        public BadBusStopIdException(int id, string message) :
            base(message) => ID = id;
        public BadBusStopIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad bus stop id: {ID}";

    }

    //class handles bad bus line id exception
    public class BadBusLineIdException : Exception
    {
        public int ID;
        public BadBusLineIdException(int id) : base() => ID = id;
        public BadBusLineIdException(int id, string message) :
            base(message) => ID = id;
        public BadBusLineIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad bus line id: {ID}";

    }

    //class handles bad bus id exception
    public class BadBusIdException : Exception
    {
        public int ID;
        public BadBusIdException(int id) : base() => ID = id;
        public BadBusIdException(int id, string message) :
            base(message) => ID = id;
        public BadBusIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad bus id: {ID}";

    }
    //class handles bad user name exception
    public class BadUserNameException: Exception
    {
        public string Name;
        public BadUserNameException(string userName) : base() => Name = userName;
        public BadUserNameException(string userName, string message) :
            base(message) => Name = userName;
        public BadUserNameException(string userName, string message, Exception innerException) :
            base(message, innerException) => Name = userName;

        public override string ToString() => base.ToString() + $", bad user Name: {Name}";

    }
    public class BadUserTripIdException : Exception
    {
        public int ID;
        public BadUserTripIdException(int id) : base() => ID = id;
        public BadUserTripIdException(int id, string message) :
            base(message) => ID = id;
        public BadUserTripIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad user trip id: {ID}";

    }
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }

}

