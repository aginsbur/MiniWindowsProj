using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.Threading;

namespace BO
{
    public class AdjacentStops: INotifyPropertyChanged
    {
        private int stopCode1;
        private int stopCode2;
        private double distance;
        private TimeSpan avgTravelTime;

        #region***properties***
        public int StopCode1 { get => stopCode1; 
            set { stopCode1 = value;
                if (PropertyChanged != null)
                {   // Call PropertyChanged whenever the property is updated
                    PropertyChanged(this, new PropertyChangedEventArgs("StopCode1"));
                }
            }
        }
        public int StopCode2 { get => stopCode2; 
            set { stopCode2 = value;
                if (PropertyChanged != null)
                {   // Call PropertyChanged whenever the property is updated
                    PropertyChanged(this, new PropertyChangedEventArgs("StopCode2"));
                }
            } }
        public double Distance { get => distance;
            set { distance = value;
                if (PropertyChanged != null)
                {   // Call PropertyChanged whenever the property is updated
                    PropertyChanged(this, new PropertyChangedEventArgs("Distance"));
                }
            }
        }
        public TimeSpan AvgTravelTime { get => avgTravelTime; set => avgTravelTime = value; }
        #endregion***properties***
        public event PropertyChangedEventHandler PropertyChanged;
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
