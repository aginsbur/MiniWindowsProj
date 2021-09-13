using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_7128_3442
{
    class Bus
    {
        private int licenseNumber;
        private DateTime date;
        private int travel;
        private int totalTravel;
        public int L
        {
            set { licenseNumber = value; }
            get { return licenseNumber; }
        }
        public DateTime D
        {
            set { date = value; }
            get { return date; }
        }
        public int T
        {
            set { travel = value; }
            get { return travel; }

        }
        public int TT
        {
            set { totalTravel = value; }
            get { return totalTravel; }

        }
        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="lN"></param>contains users license number and default value 0
        /// <param name="d"></param>contains users date and default value 01/01/0001
        /// <param name="trav"></param>contains users travel distance  and default value 0
        /// <param name="toTrav"></param>contains users total travel distance and default value 0
        public Bus(int lN = 0, DateTime d = default(DateTime), int trav = 0, int toTrav = 0)
        {
            licenseNumber = lN;
            date = d;
            travel = trav;
            totalTravel = toTrav;
        }
        /// <summary>
        /// this method returns true if requested travel is possible
        /// </summary>
        /// <param name="km"></param>distance that the user wants to travel
        /// <returns></returns>
        public bool checkTrip(int km)
        {
            if (km + travel > 1200)
                return false;
            if (km + totalTravel > 20000)
                return false;
            TimeSpan t = DateTime.Now - date;
            if (t.TotalDays >= 365)
                return false;
            return true;

        }
        ~Bus() { }

    }
}

