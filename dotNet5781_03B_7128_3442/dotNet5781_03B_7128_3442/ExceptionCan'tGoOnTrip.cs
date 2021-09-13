using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_7128_3442
{
    class ExceptionCantGoOnTrip:Exception//exception class for all errors that can occur when user wants to take a bus for a trip
    {
        ExceptionCantGoOnTrip() { }
        public ExceptionCantGoOnTrip(string message):base(message)//c-tro that takes a message from the programer
        {
        }

    }
}
