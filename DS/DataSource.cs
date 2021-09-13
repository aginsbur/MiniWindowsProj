using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    public static class DataSource//list of buses, list of lines, list of bus stops, colections of simple things
    {
        public static List<Bus> ListBuses;
        public static List<User> ListUsers;
        public static List<UserTrip> ListUserTrips;
        public static List<BusStop> ListBusStops;
        public static List<BusLine> ListBusLines;
        public static List<LineStop> ListBusLineStops;
        public static List<AdjacentStops> ListAdjacentStops;
        public static int busOnTripID;//runing number for bus on a trip ID
       private static int busLineID=1;//runing number for bus line ID
       public static int BusOnTripID { get => busOnTripID++; }//increases Id by 1 when bus is sent on a trip
       public static int BusLineID { get => ++busLineID; }//increases by 1 when bus line is created

        public static int LineId = 0;
        public static int LineStationId = 0;
        public static int FollowingStationsId = 0;

        static DataSource()
        {
            InitAllLists();
        }

        static void InitAllLists()
        {


            ListBusStops = new List<BusStop>
            {
                new BusStop
                {
                     StationCode  = 38831,
					Address =" רחוב:בן יהודה 76 עיר: כפר סבא רציף: קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 38894,
					Address =" רחוב:פיינברג 4 עיר: גדרה רציף:   קומה",
                    Availability=Status.INUSE
                },
                new BusStop
                {
                   StationCode = 38903,
                    Availability=Status.INUSE,
					Address ="רחוב:יוסף קרוננברג  עיר: רחובות רציף:   קומה",
					Latitude = 31.878667,
                    Longitude = 34.81138,
                },
                new BusStop
                {
                    StationCode = 38912,
					Address ="רחוב:השומר 22 עיר: ראשון לציון רציף:   קומה",
					Latitude = 31.959821,
                    Longitude = 34.814747,
                    Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 38916,
				Address ="רחוב:ד''ר יוסף בורג 9 עיר: ראשון לציון רציף:   קומה",
					Latitude = 31.968049,
                    Longitude = 34.818099,
                    Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode= 38922,
                    Address ="רחוב:השר חיים משה שפירא 16 עיר: ראשון לציון רציף:   קומה",
					Latitude = 31.990757,
                    Longitude = 34.755683,
                    Availability=Status.INUSE
                },
                new BusStop
                {
                   StationCode = 39001,
                  Address ="רחוב:שדרות יעקב 65 עיר: ראשון לציון רציף:   קומה",
					Latitude = 31.950254,
                    Longitude = 34.819244 ,
                    Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39002,
                    Address ="רחוב:שדרות יעקב 59 עיר: ראשון לציון רציף:   קומה",
					Latitude = 31.95111,
                    Longitude = 34.819766 ,
                    Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39004,
                    Address ="רחוב:יהודה לייב יוספזון  עיר: רחובות רציף:   קומה",
					Latitude = 31.905052,
                    Longitude = 34.818909,
                    Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39005,
					Address =" רחוב:הרב יעקב ברמן 4 עיר: רחובות רציף:   קומה",
					Latitude = 31.901879,
                    Longitude = 34.819443,
                    Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39006,Address ="רחוב:הרב יעקב ברמן  עיר: רחובות רציף:   קומה",
					Latitude = 31.90281,
                    Longitude = 34.818922,
                    Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode= 39007,
					Address ="רחוב:הנשיא הראשון 55 עיר: רחובות רציף:   קומה",
					Latitude = 31.904567,
                    Longitude = 34.815296,
                    Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39008,
                    Address ="רחוב:הנשיא הראשון 56 עיר: רחובות רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39012,
                    Address ="רחוב:הירדן 23 עיר: באר יעקב רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39013,
                    Address ="רחוב:הירדן 22 עיר: באר יעקב רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode= 39014,
Address ="רחוב:שדרות האלונים  עיר: באר יעקב רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39017,
					Address ="רחוב:שדרות האלונים  עיר: באר יעקב רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39018,
					Address ="רחוב:דרך הזית  עיר: שילת רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
StationCode=39024,
					Address ="רחוב:  עיר: מודיעין מכבים רעות רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                   StationCode= 39028,
					Address ="רחוב:חיים ברלב 30 עיר: מודיעין מכבים רעות רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode= 39028,
					Address ="רחוב:חיים ברלב 30 עיר: מודיעין מכבים רעות רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39028,
					Address ="רחוב:חיים ברלב 30 עיר: מודיעין מכבים רעות רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode= 39028,
					Address ="רחוב:חיים ברלב 30 עיר: מודיעין מכבים רעות רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode= 39028,
					Address ="רחוב:חיים ברלב 30 עיר: מודיעין מכבים רעות רציף:   קומה",
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39040, 
					Address = " רחוב:רבאט  עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39041, 
					Address = " רחוב:דוכיפת  עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39042, 
					Address = " רחוב:היצירה 2 עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode= 39043, 
					Address = " רחוב:היצירה  עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39044, 
					Address = " רחוב:עמל  עיר: רמלה רציף:   קומה:  ", 
				Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39049, 
					Address = " רחוב:ישראל פרנקל 10 עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39050, 
					Address = " רחוב:ישראל פרנקל 11 עיר: רמלה רציף:   קומה:  ", 
				Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39051, 
					Address = " רחוב:ישראל פרנקל 17 עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39052, 
					Address = " רחוב:גיורא יוספטל 6 עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode= 39056, 
					Address = " רחוב:שמחה הולצברג  עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39057, 
					Address = " רחוב:שמחה הולצברג 10 עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39058, 
					Address = " רחוב:שמחה הולצברג 4 עיר: רמלה רציף:   קומה:  ", 
				Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39059, 
					Address = " רחוב:לוי אשכול 11 עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39060, 
					Address = " רחוב:יהודה שטיין  עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39066, 
					Address = " רחוב:שמשון הגיבור  עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39068, 
					Address = " רחוב:ח.נ. ביאליק 19 עיר: רמלה רציף:   קומה:", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39069, 
					Address = " רחוב:ח.נ. ביאליק 43 עיר: רמלה רציף:   קומה:", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode= 39070, 
					Address = " רחוב:א.ס. לוי 1 עיר: רמלה רציף:   קומה:  ", 
                    Latitude = 31.9209,
                    Longitude = 34.861221
                },
                new BusStop
                {
                    StationCode = 39071, 
					Address = " רחוב:שדרות דוד רזיאל 5 עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39072, 
					Address = " רחוב:חרוד  עיר: ישרש רציף:   קומה:  ", 
				Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode= 39073, 
					Address = " רחוב:חרוד  עיר: ישרש רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode= 39075, 
					Address = " רחוב:הירדן  עיר: ישרש רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39076, 
					Address = " רחוב:עוזי חיטמן 44 עיר: רמלה רציף:   קומה:  ", 
					Availability=Status.INUSE
                },
                new BusStop
                { 
                    StationCode = 39091, 
				    Address = "רחוב:דרך החרוב  עיר: שילת רציף:   קומה", 
					Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39092,
				     Address = "רחוב:  עיר: כפר רות רציף:   קומה",
                    Availability=Status.INUSE
                },
                new BusStop
                {
                    StationCode = 39093,
					Address = "רחוב:  עיר: כפר רות רציף:   קומה",
                    Availability=Status.INUSE
                }

            };

            ListBusLines = new List<BusLine>
            {
                new BusLine
                {
                    ID = LineId++,
                    LineNumber = 12,
                    Region = Region.CENTER,
                    BegStopNum = 38831,
                    EndStopNum= 39007,
                     Availability=Status.INUSE
                },
                new BusLine
                {
                    ID = LineId++,
                   LineNumber = 30,
                    Region = Region.GENERAL,
                    BegStopNum = 38894,
                   EndStopNum = 39024,
                  Availability=Status.INUSE
                },
                new BusLine
                {
                    ID = LineId++,
                    LineNumber = 50,
                    Region= Region.NORTH,
                    BegStopNum = 38903,
                    EndStopNum = 39024,
                    Availability=Status.INUSE

                },
                new BusLine
                {
                    ID = LineId++,
                    LineNumber = 113,
                    Region = Region.CENTER,
                    BegStopNum = 38831,
                    EndStopNum = 39019,
                    Availability=Status.INUSE
				}
            };

            ListBusLineStops = new List<LineStop>
            {
                new LineStop
                {
                    LineID=1,
                    StationCode = 38831,
                    StationIndex = 1
                },
                new LineStop
                {
                    LineID = 1,
                    StationCode = 38894,
                   StationIndex = 2
                },
                new LineStop
                {
                   LineID= 1,
                    StationCode = 39002,
                    StationIndex= 3
                },
                new LineStop
                {
                    LineID = 1,
                    StationCode = 39006,
                    StationIndex = 4
                },
                new LineStop
                {
                    LineID = 1,
                    StationCode = 39007,
                    StationIndex= 5
                },

                new LineStop
                {
                    LineID = 2,
                    StationCode = 38894,
                    StationIndex = 1
                },
                new LineStop
                {
                    LineID = 2,
                    StationCode = 39002,
                    StationIndex = 2
                },
                new LineStop
                {
                    LineID = 2,
                    StationCode = 39006,
                    StationIndex = 3
                },
                new LineStop
                {
                    LineID= 2,
                    StationCode = 39024,
                    StationIndex = 4
                },

                new LineStop
                {
                    LineID = 3,
                    StationCode = 38903,
                    StationIndex= 1
                },
                new LineStop
                {
                    LineID = 3,
                   StationCode= 39002,
                    StationIndex= 2
                },
                new LineStop
                {
                   LineID= 3,
                    StationCode = 39024,
                    StationIndex = 3
                },

                new LineStop
                {
                    LineID= 4,
                    StationCode= 38831,
                   StationIndex = 1
                },
                new LineStop
                {
                    LineID = 4,
                  StationCode = 39004,
                   StationIndex = 2
                },

				//MISSING STATION 3 IN THIS LINE????
				new LineStop
                {
                    LineID= 4,
                    StationCode= 39019,
                    StationIndex = 3
                },
            };

            ListAdjacentStops = new List<AdjacentStops>
            {
                new AdjacentStops
                {
                    
                    StopCode1 = 38831,
                    StopCode2 = 38894,
                    Distance = 24
                },
                new AdjacentStops
                {
                    
                    StopCode1= 38894,
                    StopCode2 = 39002,
                    Distance = 22
                },
                new AdjacentStops
                {
                    StopCode1 = 39002,
                    StopCode2 = 39006,
                    Distance =23
                },
            };
        }
    }
}
