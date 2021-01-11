using System;
using System.Collections.Generic;
using System.Text;
using DO;
using System.Linq;
namespace DS
{
    public static class DataSource
    {
        public static List<AdjacentStations> adjacentStationsList;
        public static List<Bus> busesList;
        public static List<BusOnTrip> busesOnTripList;
        public static List<Line> linesList;
        public static List<LineStation> lineStationsList;
        public static List<LineTrip> lineTripsList;
        public static List<Station> stationsList;
        public static List<User> usersList;
        public static List<int> busKeyList = new List<int>();
        static DataSource()
        {
            InitializeAll();
        }
        static void InitializeAll()
        {
            stationsList = new List<Station>
            {
        new Station{Code=38831, Name="בי''ס בר לב/בן יהודה", Longtitude=34.917806, Latitude=32.183921},
        new Station{Code=38832, Name="הרצל/צומת בילו", Longtitude=34.819541, Latitude=31.870034},
        new Station{Code=38833, Name="הנחשול/הדייגים", Longtitude=34.782828, Latitude=31.984553},
        new Station{Code=38834, Name="פריד/ששת הימים", Longtitude=34.790904, Latitude=31.88855},
        new Station{Code=38836, Name="ת. מרכזית לוד/הורדה", Longtitude=34.898098, Latitude=31.956392},
        new Station{Code=38837, Name="חנה אברך/וולקני", Longtitude=34.796071, Latitude=31.892166},
        new Station{Code=38838, Name="הרצל/משה שרת", Longtitude=34.824106, Latitude=31.857565},
        new Station{Code=38839, Name="הבנים/אלי כהן", Longtitude=34.821857, Latitude=31.862305},
        new Station{Code=38840, Name="ויצמן/הבנים", Longtitude=34.822237, Latitude=31.865085},
        new Station{Code=38841, Name="האירוס/הכלנית", Longtitude=34.818957, Latitude=31.865222},
        new Station{Code=38842, Name="הכלנית/הנרקיס", Longtitude=34.818392, Latitude=31.867597},
        new Station{Code=38844, Name="אלי כהן/לוחמי הגטאות", Longtitude=34.827023, Latitude=31.86244},
        new Station{Code=38845, Name="שבזי/שבת אחים", Longtitude=34.828702, Latitude=31.863501},
        new Station{Code=38846, Name="שבזי/ויצמן", Longtitude=34.827102, Latitude=31.865348},
        new Station{Code=38847, Name="חיים בר לב/שדרות יצחק רבין", Longtitude=34.763896, Latitude=31.977409},
        new Station{Code=38848, Name="מרכז לבריאות הנפש לב השרון", Longtitude=34.912708, Latitude=32.300345},
        new Station{Code=38849, Name="מרכז לבריאות הנפש לב השרון", Longtitude=34.912602, Latitude=32.301347},
        new Station{Code=38852, Name="הולצמן/המדע", Longtitude=34.807944, Latitude=31.914255},
        new Station{Code=38854, Name="מחנה צריפין/מועדון", Longtitude=34.836363, Latitude=31.963668},
        new Station{Code=38855, Name="הרצל/גולני", Longtitude=34.825249, Latitude=31.856115},
        new Station{Code=38856, Name="הרותם/הדגניות", Longtitude=34.81249, Latitude=31.874963},
        new Station{Code=38859, Name="הערבה", Longtitude=34.910842, Latitude=32.300035},
        new Station{Code=38860, Name="מבוא הגפן/מורד התאנה", Longtitude=34.948647, Latitude=32.305234},
        new Station{Code=38861, Name="מבוא הגפן/ההרחבה", Longtitude=34.943393, Latitude=32.304022},
        new Station{Code=38862, Name="ההרחבה א", Longtitude=34.940529, Latitude=32.302957},
        new Station{Code=38863, Name="ההרחבה ב", Longtitude=34.939512, Latitude=32.300264},
        new Station{Code=38864, Name="ההרחבה/הותיקים", Longtitude=34.938705, Latitude=32.298171},
        new Station{Code=38865, Name="רשות שדות התעופה/העליה", Longtitude=34.8976, Latitude=31.990876},
        new Station{Code=38866, Name="כנף/ברוש", Longtitude=34.879725, Latitude=31.998767},
        new Station{Code=38867, Name="החבורה/דב הוז", Longtitude=34.818708, Latitude=31.883019},
        new Station{Code=38869, Name="בית הלוי ה", Longtitude=34.926837, Latitude=32.349776},
        new Station{Code=38870, Name="הראשונים/כביש 5700", Longtitude=34.899465, Latitude=32.352953},
        new Station{Code=38872, Name="הגאון בן איש חי/צאלון", Longtitude=34.775083, Latitude=31.897286},
        new Station{Code=38873, Name="עוקשי/לוי אשכול", Longtitude=34.807039, Latitude=31.883941},
        new Station{Code=38875, Name="מנוחה ונחלה/יהודה גורודיסקי", Longtitude=34.816752, Latitude=31.896762},
        new Station{Code=38876, Name="גורודסקי/יחיאל פלדי", Longtitude=34.823461, Latitude=31.898463},
        new Station{Code=38877, Name="דרך מנחם בגין/יעקב חזן", Longtitude=34.904907, Latitude=32.076535},
        new Station{Code=38878, Name="דרך הפארק/הרב נריה", Longtitude=34.878765, Latitude=32.299994},
        new Station{Code=38879, Name="התאנה/הגפן", Longtitude=34.859437, Latitude=31.865457},
        new Station{Code=38880, Name="התאנה/האלון", Longtitude=34.864555, Latitude=31.866772},
        new Station{Code=38881, Name="דרך הפרחים/יסמין", Longtitude=34.784347, Latitude=31.809325},
        new Station{Code=38883, Name="יצחק רבין/פנחס ספיר", Longtitude=34.778239, Latitude=31.80037},
        new Station{Code=38884, Name="מנחם בגין/יצחק רבין", Longtitude=34.782985, Latitude=31.799224},
        new Station{Code=38885, Name="חיים הרצוג/דולב", Longtitude=34.785069, Latitude=31.800334},
        new Station{Code=38886, Name="בית ספר גוונים/ארז", Longtitude=34.786735, Latitude=31.802319},
        new Station{Code=38887, Name="דרך האילנות/אלון", Longtitude=34.786623, Latitude=31.804595},
        new Station{Code=38888, Name="דרך האילנות/מנחם בגין", Longtitude=34.785098, Latitude=31.805041},
        new Station{Code=38889, Name="העצמאות/וייצמן", Longtitude=34.782252, Latitude=31.816751},
        new Station{Code=38890, Name="וייצמן/מרבד הקסמים", Longtitude=34.779753, Latitude=31.816579} };
            busesList = new List<Bus>
            {
            new Bus
            {
                LicenseNum="12301101",
                FromDate=DateTime.Parse("23.12.20"),
                TotalTrip=100,
                FuelRemain=1000.9,
                Status=BusStatus.Ready
            },
         new Bus
            {
                LicenseNum="8820050",
                FromDate=DateTime.Parse("3.10.9"),
                TotalTrip=10000,
                 FuelRemain=1000,
                 Status=BusStatus.Ready
            }};
            linesList = new List<Line>
{new Line
{
    Id=Configuration.LineId,
    Code=5,
    Area=Areas.North,
    FirstStation=500,
    LastStation=100
  },
new Line
{
    Id=Configuration.LineId,
    Code=10,
    Area=Areas.South,
    FirstStation=100,
    LastStation=500
},
new Line
{Id=Configuration.LineId,
    Code=50,
    Area=Areas.Center,
    FirstStation=100,
    LastStation=500
},
new Line
{Id=Configuration.LineId,
    Code=55,
    Area=Areas.Center,
    FirstStation=100,
    LastStation=500
},
new Line
{Id=Configuration.LineId,
    Code=5001,
    Area=Areas.South,
    FirstStation=100,
    LastStation=500
},
new Line
{Id=Configuration.LineId,
    Code=5001,
    Area=Areas.South,
    FirstStation=100,
    LastStation=500
},
new Line
{Id=Configuration.LineId,
    Code=5001,
    Area=Areas.South,
    FirstStation=100,
    LastStation=500
},
new Line
{Id=Configuration.LineId,
    Code=5001,
    Area=Areas.South,
    FirstStation=100,
    LastStation=500
},
new Line
{Id=Configuration.LineId,
    Code=5001,
    Area=Areas.South,
    FirstStation=100,
    LastStation=500
},
new Line
{Id=Configuration.LineId,
    Code=5001,
    Area=Areas.South,
    FirstStation=100,
    LastStation=500
}
};
            lineStationsList = new List<LineStation>
            {
            new LineStation { LineId = 5, Station = 38831, LineStationIndex = 1, PrevStation = 00000, NextStation = 38832},
            new LineStation { LineId = 5, Station = 38832, LineStationIndex = 1, PrevStation = 38831, NextStation = 38833 },
            new LineStation { LineId = 5, Station = 38833, LineStationIndex = 2, PrevStation = 38832, NextStation = 38834 },
            new LineStation { LineId = 5, Station = 38834, LineStationIndex = 3, PrevStation = 38833, NextStation = 38836 },
            new LineStation { LineId = 5, Station = 38836, LineStationIndex = 4, PrevStation = 38834, NextStation = 38837 },
            new LineStation { LineId = 5, Station = 38837, LineStationIndex = 5, PrevStation = 38836, NextStation = 38838 },
            new LineStation { LineId = 5, Station = 38838, LineStationIndex = 6, PrevStation = 38837, NextStation = 38839 },
            new LineStation { LineId = 5, Station = 38839, LineStationIndex = 7, PrevStation = 38838, NextStation = 38840 },
            new LineStation { LineId = 5, Station = 38840, LineStationIndex = 8, PrevStation = 38839, NextStation = 38841 },
            new LineStation { LineId = 5, Station = 38841, LineStationIndex = 9, PrevStation = 38840, NextStation = 38842 },
            new LineStation { LineId = 6, Station = 38842, LineStationIndex = 0, PrevStation = 38841, NextStation = 38844 },
            new LineStation { LineId = 6, Station = 38844, LineStationIndex = 0, PrevStation = 38842, NextStation = 38845 },
            new LineStation { LineId = 6, Station = 38845, LineStationIndex = 0, PrevStation = 38844, NextStation = 38846 },
            new LineStation { LineId = 6, Station = 38846, LineStationIndex = 0, PrevStation = 38845, NextStation = 38847 },
            new LineStation { LineId = 6, Station = 38847, LineStationIndex = 0, PrevStation = 38846, NextStation = 38848 },
            new LineStation { LineId = 6, Station = 38848, LineStationIndex = 0, PrevStation = 38847, NextStation = 38849 },
            new LineStation { LineId = 6, Station = 38849, LineStationIndex = 0, PrevStation = 38848, NextStation = 38852 },
            new LineStation { LineId = 6, Station = 38852, LineStationIndex = 0, PrevStation = 38849, NextStation = 38854 },
            new LineStation { LineId = 6, Station = 38854, LineStationIndex = 0, PrevStation = 38852, NextStation = 38855 },
            new LineStation { LineId = 6, Station = 38855, LineStationIndex = 0, PrevStation = 38854, NextStation = 38856 },
            new LineStation { LineId = 7, Station = 38856, LineStationIndex = 0, PrevStation = 38855, NextStation = 38859 },
            new LineStation { LineId = 7, Station = 38859, LineStationIndex = 0, PrevStation = 38856, NextStation = 38860 },
            new LineStation { LineId = 7, Station = 38860, LineStationIndex = 0, PrevStation = 38859, NextStation = 38861 },
            new LineStation { LineId = 7, Station = 38861, LineStationIndex = 0, PrevStation = 38860, NextStation = 38862 },
            new LineStation { LineId = 7, Station = 38862, LineStationIndex = 0, PrevStation = 38861, NextStation = 38863 },
            new LineStation { LineId = 7, Station = 38863, LineStationIndex = 0, PrevStation = 38862, NextStation = 38864 },
            new LineStation { LineId = 7, Station = 38864, LineStationIndex = 0, PrevStation = 38863, NextStation = 38865 },
            new LineStation { LineId = 7, Station = 38865, LineStationIndex = 0, PrevStation = 38864, NextStation = 38866 },
            new LineStation { LineId = 7, Station = 38866, LineStationIndex = 0, PrevStation = 38865, NextStation = 38867 },
            new LineStation { LineId = 7, Station = 38867, LineStationIndex = 0, PrevStation = 38866, NextStation = 38869 },
            new LineStation { LineId = 5, Station = 38869, LineStationIndex = 0, PrevStation = 38867, NextStation = 38870 },
            new LineStation { LineId = 5, Station = 38870, LineStationIndex = 0, PrevStation = 38869, NextStation = 38872 },
            new LineStation { LineId = 5, Station = 38872, LineStationIndex = 0, PrevStation = 38870, NextStation = 38873 },
            new LineStation { LineId = 5, Station = 38873, LineStationIndex = 0, PrevStation = 38872, NextStation = 38875 },
            new LineStation { LineId = 5, Station = 38875, LineStationIndex = 0, PrevStation = 38873, NextStation = 38876 } };

            busesList = new List<Bus>()
{
    new Bus
    {
          LicenseNum="12345678",
         FromDate=new DateTime(2018,1,1),
         TotalTrip=1500,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
      new Bus
    {
          LicenseNum="5675544",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
        new Bus
    {
          LicenseNum="11111111",
         FromDate=new DateTime(2020,1,1),
         TotalTrip=0,
         FuelRemain=1200,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,1),
         KmSinceTreated=0
    },
          new Bus
    {
          LicenseNum="3267893",
         FromDate=new DateTime(2000,12,15),
         TotalTrip=1500000,
         FuelRemain=1200,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=1000
    },
            new Bus
    {
          LicenseNum="10101011",
         FromDate=new DateTime(2018,1,1),
         TotalTrip=1500,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
    new Bus{
          LicenseNum="44444444",
         FromDate=new DateTime(2018,1,1),
         TotalTrip=1500,
         FuelRemain=11,
         Status=BusStatus.Ready,
         InService=true,
         IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
       new Bus
    {
          LicenseNum="2345343",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
          new Bus
    {
          LicenseNum="2348343",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
           new Bus
    {
          LicenseNum="2348343",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
                 new Bus
    {
          LicenseNum="9348343",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
                          new Bus
    {
          LicenseNum="9348349",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
                         new Bus
    {
          LicenseNum="3333333",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
    new Bus
    {
          LicenseNum="3333111",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
    new Bus
    {
          LicenseNum="9999999",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
     new Bus
    {
          LicenseNum="98765432",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
      new Bus
    {
          LicenseNum="23456789",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
        new Bus
    {
          LicenseNum="2468357",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
           new Bus
    {
          LicenseNum="7777777",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
                  new Bus
    {
          LicenseNum="7777778",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
        InService=true,
        IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    },
    new Bus
    {
         LicenseNum="7717778",
         FromDate=new DateTime(2011,1,1),
         TotalTrip=150000,
         FuelRemain=11,
         Status=BusStatus.Ready,
         InService=true,
         IsDangerous =false,
         LastTreated=new DateTime(2020,1,7),
         KmSinceTreated=0
    }
};
            adjacentStationsList = new List<AdjacentStations>
            {
                new AdjacentStations
                {
                    Station1 = 38834,
                    Station2 = 38837,
                    Distance = 0.43f,
                    Time = new TimeSpan(0,5,0)
                }
            };
          
        }
    }
}
