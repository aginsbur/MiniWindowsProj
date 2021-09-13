using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.Threading;

namespace dotNet5781_03B_7128_3442
{
    public enum status {NEEDREFUEL,READY2GO, MIDOFRIDE,REFUELING,BEINGSERV, NEEDSERVICE};
    public class Bus: INotifyPropertyChanged
    {
        private status currstate;//status of bus
        private string licenseNumber;//license number of bus
        private DateTime date;//date of last maintenance
        private DateTime startdate;//date of aquisition
        private int travel;//travel distance from lats refuel
        private int totalTravel;//travel distance from last maintenance
        private int km;//km  requested for travel by user
        private string Choice = "";//variable that holds choice of event called from another window
        private double _progress;//times progress of a bus status
        #region***properties***
        /// <summary>
        /// license property
        /// </summary>
        public string L
        { 
         
            set
            {
                bool isIntString = value.All(char.IsDigit);//scans string for any invalid characters in the input
                bool isLength = ((value.Length >6) && (value.Length < 9));
                if (isIntString && isLength)
                { licenseNumber = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("L"));
                    }
                }
                else if (!isIntString)
                    throw new ArgumentException("License number can only contain numbers!");
                else
                    throw new ArgumentException("License number can only be 7 or 8 digits!");
            }
            get { return licenseNumber; }
        }
      /// <summary>
      /// Date from last maintenance property
      /// </summary>
      public DateTime D
        {
            set {
                date = value;
                if (PropertyChanged != null)
                {   // Call OnPropertyChanged whenever the property is updated
                    PropertyChanged(this, new PropertyChangedEventArgs("D"));
                }
            }
            get { return date; }
        }
      /// <summary>
      /// Date of aquisition of the bus property 
      /// </summary>
      public DateTime SD
        {
            set { 
                startdate = value;
                if (PropertyChanged != null)
                {   // Call OnPropertyChanged whenever the property is updated
                    PropertyChanged(this, new PropertyChangedEventArgs("SD"));
                }
            }
            get { return startdate; }
        }
       /// <summary>
       /// Travel distance from last refual property
       /// </summary>
       public int T
        {
            set { travel = value;
                if (PropertyChanged != null)
                {    // Call OnPropertyChanged whenever the property is updated
                    PropertyChanged(this, new PropertyChangedEventArgs("T"));
                }
            }
            get { return travel; }

        }
     /// <summary>
     /// total travel distance property
     /// </summary>
     public int TT
        {
            set { totalTravel = value;
                if (PropertyChanged != null)
                {   // Call PropertyChanged whenever the property is updated
                    PropertyChanged(this, new PropertyChangedEventArgs("TT"));
                }
            }
            get { return totalTravel; }

        }
    /// <summary>
    /// status of bus property
    /// </summary>
    public status ST
        {
            set
            {  
                currstate = value;
                if (PropertyChanged != null)
                {   // Call PropertyChanged whenever the property is updated
                    PropertyChanged(this, new PropertyChangedEventArgs("ST"));
                }
            }
            get { return currstate; }
        }
        public double progress
        {
            get { return _progress; }
            set { _progress = value;
                if (PropertyChanged != null)
                {   // Call PropertyChanged whenever the property is updated
                    PropertyChanged(this, new PropertyChangedEventArgs("progress"));
                }
            }
        }
        public int KM
        {
            get { return km; }
            set
            {
                km = value;
                if (PropertyChanged != null)
                {   // Call PropertyChanged whenever the property is updated
                    PropertyChanged(this, new PropertyChangedEventArgs("km"));
                }
            }
        }
        public string choice
        {
            get { return Choice; }
            set { Choice = value; }
        }
        #endregion
        #region***c-tors_and_methods***
        /// <summary>
        /// defaul c-tor
        /// </summary>
        public Bus()
        {
            licenseNumber = "";
            date = default(DateTime);
            startdate = date;
            travel =0;
            totalTravel = 0;
            currstate = status.READY2GO;
        }

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="lN"></param>contains users license number and default value 0
        /// <param name="d"></param>contains users date and default value 01/01/0001
        /// <param name="trav"></param>contains users travel distance  and default value 0
        /// <param name="toTrav"></param>contains users total travel distance and default value 0
        public Bus(string lN ="", DateTime d = default(DateTime), int trav = 0, int toTrav = 0)
        {
            if (lN.Length % 7 == 0)//if a 7 digit license number is entered
                licenseNumber = lN.Substring(0, 2) + "-" + lN.Substring(2, 3) + "-" + lN.Substring(5, 2);
            else
            {
                if (lN.Length % 8 == 0)//if an 8 digit license number is entered
                    licenseNumber = lN.Substring(0, 3) + "-" + lN.Substring(3, 2) + "-" + lN.Substring(5, 3);
                else//if no license number was entered
                    licenseNumber = lN;
            }
            currstate = status.READY2GO;
            date = d;
            startdate = d;
            updateStatus();//updates status of bus based on it's data
        } 
        public void updateStatus()
        {
            if (travel == 1200 || travel > 1200)
                currstate = status.NEEDREFUEL;
            else
            {
                TimeSpan t = DateTime.Now - date;
                if (t.TotalDays >= 365)//if bus  needs maintenance
                    currstate = status.NEEDSERVICE;
                else
                    currstate = status.READY2GO;
            }
        }
        /// <summary>
        /// this method returns true if requested travel is possible
        /// </summary>
        /// <param name="km"></param>distance that the user wants to travel
        /// <returns></returns>
        public bool checkTrip()
        {
            if (km + travel > 1200)
                throw new ExceptionCantGoOnTrip("Not enough gas to complete trip!");
            if (km + totalTravel > 20000)
                throw new ExceptionCantGoOnTrip("Distance requested Exceeds max allowed Km traveld since last maintenance!");
            TimeSpan t = DateTime.Now - date;
            if (t.TotalDays >= 365)
                throw new ExceptionCantGoOnTrip("The Bus cannot complete the trip. Send for maintenance!");
            return true;

        }
       /// <summary>
       /// maintains the bus sent for service
       /// </summary>
       public void maintain()
        {
            currstate = status.BEINGSERV;
            date = DateTime.Now;//updates buses last maintenance date to today
            currstate = status.READY2GO;
        }
        public override string ToString()//returns bus license number
        {
            return L+"  "+TT;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        ~Bus() { }
        #endregion

    }
}

