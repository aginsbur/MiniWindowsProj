using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;

namespace BO
{
    public class Bus: INotifyPropertyChanged,IComparable
    {
        private int licenseNum;
        private DateTime startdate;//date of aquisition
        private DateTime date;//date of last maintenance
        private double totalTravel;//travel distance from last maintenance
        private double travel;//travel distance from lats refuel
        private double km;//km  requested for travel by user
        private BusStatus currstate;

        #region***properties***
        public int LicenseNum 
        {
            get => licenseNum; 
            set
            {
                licenseNum = value;
            }
        }
        public DateTime StartDate { get => startdate; set => startdate = value; }
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Date"));
                }
            }
        }
        public double TotalTravel { get => totalTravel; 
            set 
            { totalTravel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalTravel"));
                }
            }
        }
        public double Travel { get => travel; 
            set { travel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Travel"));
                }
            }
        }
        public double KM { get => km; set => km = value; }
        public BusStatus Currstate { get => currstate; 
            set { 
                currstate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrState"));
                }
             }
}
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        /// <summary>
        /// sorts the busse by there license number
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        int IComparable.CompareTo(object obj)
        {
            Bus b = (Bus)obj;
            if (this.LicenseNum > b.LicenseNum)
                return 1;
            else if (this.LicenseNum == b.LicenseNum)
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
