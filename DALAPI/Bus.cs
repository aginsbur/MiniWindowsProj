using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    public class Bus : IClonable
    {
        private int licenseNum;
        private DateTime startdate;//date of aquisition
        private DateTime date;//date of last maintenance
        private double totalTravel;//travel distance from last maintenance
        private double travel;//travel distance from lats refuel
       // private int km;//km  requested for travel by user
       // private double mileage;
       // private double fuelTank;
        private BusStatus currstate;
     //   private string Choice = "";//variable that holds choice of event called from another window
      //  private double _progress;//times progress of a bus status


        #region***properties***
        public int LicenseNum { get => licenseNum; set => licenseNum = value; }
        public DateTime StartDate { get => startdate; set => startdate = value; }
        public DateTime Date { get => date; set => date = value; }
        public double TotalTravel { get => totalTravel; set => totalTravel = value; }
        public double Travel { get => travel; set => travel = value; }
       // public DateTime RegisterDate { get => registerDate; set => registerDate = value; }
       // public double Mileage { get => mileage; set => mileage = value; }
       // public double FuelTank1 { get => fuelTank; set => fuelTank = value; }
        public BusStatus Currstate { get => currstate; set => currstate = value; }
        #endregion

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
 