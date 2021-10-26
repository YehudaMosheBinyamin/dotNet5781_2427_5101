using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using DO;

namespace DL
{
    public static class InitAll
    {
        static string linePath = @"..\bin\xml\Line.xml";
        static string stationsPath = @"..\bin\xml\Station.xml";
        static string adjacentStationsPath = @"..\bin\xml\AdjacentStation.xml";
        static string lineStationsPath = @"..\bin\xml\LineStations.xml";
        static string lineTripsPath = @"..\bin\xml\LineTrips.xml";
        public static Random random;
        public static List<AdjacentStations> adjacentStationsList;
        public static List<AdjacentStations> adjacentStationsListTheSameStops;
        public static List<Bus> busesList;
        public static List<BusOnTrip> busesOnTripList;
        public static List<Line> linesList;
        public static List<LineStation> lineStationsList;
        public static List<LineTrip> lineTripsList;
        public static List<Station> stationsList;
        public static List<User> usersList;
        static XElement stationRoot;

        public static XElement StationRoot { get => stationRoot;  }

        static InitAll() 
        {
            InitializeAll();
        }

        /// <summary>
        /// Save list of stations to XML file
        /// From Lecture Slides By Dan Zilberstein
        /// </summary>
        /// <param name="stations"></param>
        public static void SaveStationsList(List<Station> stations)
        {
            foreach (var station in stations)
            {
                XElement newStatRoot = new XElement("Station");
                XElement code = new XElement("Code", station.Code);
                XElement name = new XElement("Name", station.Name);
                XElement longtitude = new XElement("Longtitude", station.Longtitude);
                XElement latitude = new XElement("Latitude", station.Latitude);
                XElement inService = new XElement("InService", station.InService);
                newStatRoot.Add("station", code, name, longtitude, latitude, inService);
                stationRoot.Add(newStatRoot); 
            }
            stationRoot.Save(stationsPath);
        }
        public static void InitializeAll()
        {
            /**stationRoot = new XElement("Stations");
            stationRoot.Save(stationsPath);
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
        new Station{Code=00000, Name="מסוף", Longtitude=32.05907,Latitude=34.774292},//terminal,
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
        new Station{Code=38890, Name="וייצמן/מרבד הקסמים", Longtitude=34.779753, Latitude=31.816579},
        new Station{Code=38891, Name="צאלה/אלמוג", Longtitude=34.787199, Latitude=31.801182}

};
            Action<List<Station>> act = SaveStationsList;
            act.Invoke(stationsList);
            /** random = new Random();

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
         new Station{Code=00000, Name="מסוף", Longtitude=32.05907,Latitude=34.774292},//terminal,
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
         new Station{Code=38890, Name="וייצמן/מרבד הקסמים", Longtitude=34.779753, Latitude=31.816579},
         new Station{Code=38891, Name="צאלה/אלמוג", Longtitude=34.787199, Latitude=31.801182}
};
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
     FirstStation=38831,
     LastStation=38840,
     InService=true
   },

new Line
{Id=Configuration.LineId,
     Code=6,
     Area=Areas.Center,
     FirstStation=38840,
     LastStation=38855,
     InService=true
},
new Line
{Id=Configuration.LineId,
     Code=7,
     Area=Areas.Center,
     FirstStation=38856,
     LastStation=38867,
     InService=true
},
new Line
{Id=Configuration.LineId,
     Code=8,
     Area=Areas.South,
     FirstStation=38845,
     LastStation=38832,
     InService=true
},
new Line
{Id=Configuration.LineId,
     Code=9,
     Area=Areas.South,
     FirstStation=38860,
     LastStation=38846,
     InService=true
},
new Line
{Id=Configuration.LineId,
     Code=10,
     Area=Areas.South,
     FirstStation=38889,
     LastStation=38878,
     InService=true
},
new Line
{Id=Configuration.LineId,
     Code=11,
     Area=Areas.South,
     FirstStation=38870,
     LastStation=38849,
     InService=true
},
new Line
{Id=Configuration.LineId,
     Code=12,
     Area=Areas.South,
     FirstStation=38870,
     LastStation=38886,
     InService=true
},
new Line
{Id=Configuration.LineId,
     Code=13,
     Area=Areas.South,
     FirstStation=38886,
     LastStation=38875,
     InService=true
},
new Line
{
     Id=Configuration.LineId,
     Code=14,
     Area=Areas.South,
     FirstStation=38886,
     LastStation=38883,
     InService=true
},
new Line
{
     Id=Configuration.LineId,
     Code=15,
     Area=Areas.General,
     FirstStation=38890,
     LastStation=38891,
     InService=true
}
};
            **/
             lineStationsList = new List<LineStation>
             {
             new LineStation { LineId = 1, Station = 38831, LineStationIndex = 0, PrevStation = 38831, NextStation = 38832,InService=true},
             new LineStation { LineId = 1, Station = 38832, LineStationIndex = 1, PrevStation = 38831, NextStation = 38833,InService=true},
             new LineStation { LineId = 1, Station = 38833, LineStationIndex = 2, PrevStation = 38832, NextStation = 38834,InService=true},
             new LineStation { LineId = 1, Station = 38834, LineStationIndex = 3, PrevStation = 38833, NextStation = 38836,InService=true},
             new LineStation { LineId = 1, Station = 38836, LineStationIndex = 4, PrevStation = 38834, NextStation = 38837,InService=true},
             new LineStation { LineId = 1, Station = 38837, LineStationIndex = 5, PrevStation = 38836, NextStation = 38838,InService=true},
             new LineStation { LineId = 1, Station = 38838, LineStationIndex = 6, PrevStation = 38837, NextStation = 38839,InService=true},
             new LineStation { LineId = 1, Station = 38839, LineStationIndex = 7, PrevStation = 38838, NextStation = 38840,InService=true},
             new LineStation { LineId = 1, Station = 38840, LineStationIndex = 8, PrevStation = 38839, NextStation = 38841,InService=true},
             new LineStation { LineId = 1, Station = 38841, LineStationIndex = 9, PrevStation = 38840, NextStation = 38841,InService=true},
             new LineStation{LineId=2,Station=38842,LineStationIndex=-1,PrevStation=38842,NextStation=38842,InService=true},
             new LineStation { LineId = 2, Station = 38842, LineStationIndex = 0, PrevStation = 38842, NextStation = 38844,InService=true},
             new LineStation { LineId = 2, Station = 38844, LineStationIndex = 1, PrevStation = 38842, NextStation = 38845,InService=true},
             new LineStation { LineId = 2, Station = 38845, LineStationIndex = 2, PrevStation = 38844, NextStation = 38846,InService=true},
             new LineStation { LineId = 2, Station = 38846, LineStationIndex = 3, PrevStation = 38845, NextStation = 38847,InService=true},
             new LineStation { LineId = 2, Station = 38847, LineStationIndex = 4, PrevStation = 38846, NextStation = 38848,InService=true},
             new LineStation { LineId = 2, Station = 38848, LineStationIndex = 5, PrevStation = 38847, NextStation = 38849,InService=true},
             new LineStation { LineId = 2, Station = 38849, LineStationIndex = 6, PrevStation = 38848, NextStation = 38852,InService=true},
             new LineStation { LineId = 2, Station = 38852, LineStationIndex = 7, PrevStation = 38849, NextStation = 38854,InService=true},
             new LineStation { LineId = 2, Station = 38854, LineStationIndex = 8, PrevStation = 38852, NextStation = 38855,InService=true},
             new LineStation { LineId = 2, Station = 38855, LineStationIndex = 9, PrevStation = 38854, NextStation = 38855,InService=true},
             new LineStation { LineId = 3, Station = 38856, LineStationIndex = 0, PrevStation = 38856, NextStation = 38859,InService=true},
             new LineStation { LineId = 3, Station = 38859, LineStationIndex = 1, PrevStation = 38856, NextStation = 38860,InService=true},
             new LineStation { LineId = 3, Station = 38860, LineStationIndex = 2, PrevStation = 38859, NextStation = 38861,InService=true},
             new LineStation { LineId = 3, Station = 38861, LineStationIndex = 3, PrevStation = 38860, NextStation = 38862,InService=true},
             new LineStation { LineId = 3, Station = 38862, LineStationIndex = 4, PrevStation = 38861, NextStation = 38863,InService=true},
             new LineStation { LineId = 3, Station = 38863, LineStationIndex = 5, PrevStation = 38862, NextStation = 38864,InService=true},
             new LineStation { LineId = 3, Station = 38864, LineStationIndex = 6, PrevStation = 38863, NextStation = 38865,InService=true},
             new LineStation { LineId = 3, Station = 38865, LineStationIndex = 7, PrevStation = 38864, NextStation = 38866,InService=true},
             new LineStation { LineId = 3, Station = 38866, LineStationIndex = 8, PrevStation = 38865, NextStation = 38867,InService=true},
             new LineStation { LineId = 3, Station = 38867, LineStationIndex = 9, PrevStation = 38866, NextStation = 38867,InService=true},
             new LineStation{LineId=4,Station=38832, LineStationIndex=11, PrevStation=38833, NextStation=38832,InService=true},
             new LineStation{LineId=4,Station=38833, LineStationIndex=10, PrevStation=38834, NextStation=38832,InService=true},
             new LineStation{LineId=4,Station=38834, LineStationIndex=9, PrevStation=38836, NextStation=38833,InService=true},
             new LineStation{LineId=4,Station=38836, LineStationIndex=8, PrevStation=38837, NextStation=38834,InService=true},
             new LineStation{LineId=4,Station=38837, LineStationIndex=7, PrevStation=38838, NextStation=38836,InService=true},
             new LineStation{LineId=4,Station=38838, LineStationIndex=6, PrevStation=38839, NextStation=38837,InService=true},
             new LineStation{LineId=4,Station=38839, LineStationIndex=5, PrevStation=38840, NextStation=38838,InService=true},
             new LineStation{LineId=4,Station=38840, LineStationIndex=4, PrevStation=38841, NextStation=38839,InService=true},
             new LineStation{LineId=4,Station=38841, LineStationIndex=3, PrevStation=38842, NextStation=38840,InService=true},
             new LineStation{LineId=4,Station=38842, LineStationIndex=2, PrevStation=38844, NextStation=38841,InService=true},
             new LineStation{LineId=4,Station=38844, LineStationIndex=1, PrevStation=38845, NextStation=38842,InService=true},
             new LineStation{LineId=4,Station=38845, LineStationIndex=0, PrevStation=38845, NextStation=38844,InService=true},
             new LineStation{LineId=5,Station=38846, LineStationIndex=9, PrevStation=38847, NextStation=38846,InService=true},
             new LineStation{LineId=5,Station=38847, LineStationIndex=8, PrevStation=38848, NextStation=38846,InService=true},
             new LineStation{LineId=5,Station=38848, LineStationIndex=7, PrevStation=38849, NextStation=38847,InService=true},
             new LineStation{LineId=5,Station=38849, LineStationIndex=6, PrevStation=38852, NextStation=38848,InService=true},
             new LineStation{LineId=5,Station=38852, LineStationIndex=5, PrevStation=38854, NextStation=38849,InService=true},
             new LineStation{LineId=5,Station=38854, LineStationIndex=4, PrevStation=38855, NextStation=38852,InService=true},
             new LineStation{LineId=5,Station=38855, LineStationIndex=3, PrevStation=38856, NextStation=38854,InService=true},
             new LineStation{LineId=5,Station=38856, LineStationIndex=2, PrevStation=38859, NextStation=38855,InService=true},
             new LineStation{LineId=5,Station=38859, LineStationIndex=1, PrevStation=38860, NextStation=38856,InService=true},
             new LineStation{LineId=5,Station=38860, LineStationIndex=0, PrevStation=38860, NextStation=38859,InService=true},
             new LineStation{LineId=6,Station=38877, LineStationIndex=11, PrevStation=38878, NextStation=38877,InService=true},
             new LineStation{LineId=6,Station=38878, LineStationIndex=10, PrevStation=38879, NextStation=38877,InService=true},
             new LineStation{LineId=6,Station=38879, LineStationIndex=9, PrevStation=38880, NextStation=38878,InService=true},
             new LineStation{LineId=6,Station=38880, LineStationIndex=8, PrevStation=38881, NextStation=38879,InService=true},
             new LineStation{LineId=6,Station=38881, LineStationIndex=7, PrevStation=38883, NextStation=38880,InService=true},
             new LineStation{LineId=6,Station=38883, LineStationIndex=6, PrevStation=38884, NextStation=38881,InService=true},
             new LineStation{LineId=6,Station=38884, LineStationIndex=5, PrevStation=38885, NextStation=38883,InService=true},
             new LineStation{LineId=6,Station=38885, LineStationIndex=4, PrevStation=38886, NextStation=38884,InService=true},
             new LineStation{LineId=6,Station=38886, LineStationIndex=3, PrevStation=38887, NextStation=38885,InService=true},
             new LineStation{LineId=6,Station=38887, LineStationIndex=2, PrevStation=38888, NextStation=38886,InService=true},
             new LineStation{LineId=6,Station=38888, LineStationIndex=1, PrevStation=38889, NextStation=38887,InService=true},
             new LineStation{LineId=6,Station=38889, LineStationIndex=0, PrevStation=38889, NextStation=38888,InService=true},
             new LineStation{LineId=7,Station=38849, LineStationIndex=15, PrevStation=38852, NextStation=38849,InService=true},
             new LineStation{LineId=7,Station=38852, LineStationIndex=14, PrevStation=38854, NextStation=38849,InService=true},
             new LineStation{LineId=7,Station=38854, LineStationIndex=13, PrevStation=38855, NextStation=38852,InService=true},
             new LineStation{LineId=7,Station=38855, LineStationIndex=12, PrevStation=38856, NextStation=38854,InService=true},
             new LineStation{LineId=7,Station=38856, LineStationIndex=11, PrevStation=38859, NextStation=38855,InService=true},
             new LineStation{LineId=7,Station=38859, LineStationIndex=10, PrevStation=38860, NextStation=38856,InService=true},
             new LineStation{LineId=7,Station=38860, LineStationIndex=9, PrevStation=38861, NextStation=38859,InService=true},
             new LineStation{LineId=7,Station=38861, LineStationIndex=8, PrevStation=38862, NextStation=38860,InService=true},
             new LineStation{LineId=7,Station=38862, LineStationIndex=7, PrevStation=38863, NextStation=38861,InService=true},
             new LineStation{LineId=7,Station=38863, LineStationIndex=6, PrevStation=38864, NextStation=38862,InService=true},
             new LineStation{LineId=7,Station=38864, LineStationIndex=5, PrevStation=38865, NextStation=38863,InService=true},
             new LineStation{LineId=7,Station=38865, LineStationIndex=4, PrevStation=38866, NextStation=38864,InService=true},
             new LineStation{LineId=7,Station=38866, LineStationIndex=3, PrevStation=38867, NextStation=38865,InService=true},
             new LineStation{LineId=7,Station=38867, LineStationIndex=2, PrevStation=38869, NextStation=38866,InService=true},
             new LineStation{LineId=7,Station=38869, LineStationIndex=1, PrevStation=38870, NextStation=38867,InService=true},
             new LineStation{LineId=7,Station=38870, LineStationIndex=0, PrevStation=38870, NextStation=38869,InService=true},
             new LineStation{LineId=8,Station=38870, LineStationIndex=13, PrevStation=38872, NextStation=38870,InService=true},
             new LineStation{LineId=8,Station=38872, LineStationIndex=12, PrevStation=38873, NextStation=38870,InService=true},
             new LineStation{LineId=8,Station=38873, LineStationIndex=11, PrevStation=38875, NextStation=38872,InService=true},
             new LineStation{LineId=8,Station=38875, LineStationIndex=10, PrevStation=38876, NextStation=38873,InService=true},
             new LineStation{LineId=8,Station=38876, LineStationIndex=9, PrevStation=38877, NextStation=38875,InService=true},
             new LineStation{LineId=8,Station=38877, LineStationIndex=8, PrevStation=38878, NextStation=38876,InService=true},
             new LineStation{LineId=8,Station=38878, LineStationIndex=7, PrevStation=38879, NextStation=38877,InService=true},
             new LineStation{LineId=8,Station=38879, LineStationIndex=6, PrevStation=38880, NextStation=38878,InService=true},
             new LineStation{LineId=8,Station=38880, LineStationIndex=5, PrevStation=38881, NextStation=38879,InService=true},
             new LineStation{LineId=8,Station=38881, LineStationIndex=4, PrevStation=38883, NextStation=38880,InService=true},
             new LineStation{LineId=8,Station=38883, LineStationIndex=3, PrevStation=38884, NextStation=38881,InService=true},
             new LineStation{LineId=8,Station=38884, LineStationIndex=2, PrevStation=38885, NextStation=38883,InService=true},
             new LineStation{LineId=8,Station=38885, LineStationIndex=1, PrevStation=38886, NextStation=38884,InService=true},
             new LineStation{LineId=8,Station=38886, LineStationIndex=0, PrevStation=38886, NextStation=38885,InService=true},
             new LineStation{LineId=9,Station=38875, LineStationIndex=10, PrevStation=38876, NextStation=38875,InService=true},
             new LineStation{LineId=9,Station=38876, LineStationIndex=9, PrevStation=38877, NextStation=38875,InService=true},
             new LineStation{LineId=9,Station=38877, LineStationIndex=8, PrevStation=38878, NextStation=38876,InService=true},
             new LineStation{LineId=9,Station=38878, LineStationIndex=7, PrevStation=38879, NextStation=38877,InService=true},
             new LineStation{LineId=9,Station=38879, LineStationIndex=6, PrevStation=38880, NextStation=38878,InService=true},
             new LineStation{LineId=9,Station=38880, LineStationIndex=5, PrevStation=38881, NextStation=38879,InService=true},
             new LineStation{LineId=9,Station=38881, LineStationIndex=4, PrevStation=38883, NextStation=38880,InService=true},
             new LineStation{LineId=9,Station=38883, LineStationIndex=3, PrevStation=38884, NextStation=38881,InService=true},
             new LineStation{LineId=9,Station=38884, LineStationIndex=2, PrevStation=38885, NextStation=38883,InService=true},
             new LineStation{LineId=9,Station=38885, LineStationIndex=1, PrevStation=38886, NextStation=38884,InService=true},
             new LineStation{LineId=9,Station=38886, LineStationIndex=0, PrevStation=38886, NextStation=38885,InService=true},
             new LineStation{LineId=10,Station=38883, LineStationIndex=11, PrevStation=38875, NextStation=38883,InService=true},
             new LineStation{LineId=10,Station=38875, LineStationIndex=10, PrevStation=38876, NextStation=38873,InService=true},
             new LineStation{LineId=10,Station=38876, LineStationIndex=9, PrevStation=38877, NextStation=38875,InService=true},
             new LineStation{LineId=10,Station=38877, LineStationIndex=8, PrevStation=38878, NextStation=38876,InService=true},
             new LineStation{LineId=10,Station=38878, LineStationIndex=7, PrevStation=38879, NextStation=38877,InService=true},
             new LineStation{LineId=10,Station=38879, LineStationIndex=6, PrevStation=38880, NextStation=38878,InService=true},
             new LineStation{LineId=10,Station=38880, LineStationIndex=5, PrevStation=38881, NextStation=38879,InService=true},
             new LineStation{LineId=10,Station=38881, LineStationIndex=4, PrevStation=38883, NextStation=38880,InService=true},
             new LineStation{LineId=10,Station=38883, LineStationIndex=3, PrevStation=38884, NextStation=38881,InService=true},
             new LineStation{LineId=10,Station=38884, LineStationIndex=2, PrevStation=38885, NextStation=38883,InService=true},
             new LineStation{LineId=10,Station=38885, LineStationIndex=1, PrevStation=38886, NextStation=38884,InService=true},
             new LineStation{LineId=10,Station=38886, LineStationIndex=0, PrevStation=38886, NextStation=38885,InService=true},
             new LineStation{LineId=11,Station=38891, LineStationIndex=49, PrevStation=38831, NextStation=38891,InService=true},
             new LineStation{LineId=11,Station=38831, LineStationIndex=48, PrevStation=38832, NextStation=38891,InService=true},
             new LineStation{LineId=11,Station=38832, LineStationIndex=47, PrevStation=38833, NextStation=38831,InService=true},
new LineStation{LineId=11,Station=38833, LineStationIndex=46, PrevStation=38834, NextStation=38832,InService=true},
new LineStation{LineId=11,Station=38834, LineStationIndex=45, PrevStation=38836, NextStation=38833,InService=true},
new LineStation{LineId=11,Station=38836, LineStationIndex=44, PrevStation=38837, NextStation=38834,InService=true},
new LineStation{LineId=11,Station=38837, LineStationIndex=43, PrevStation=38838, NextStation=38836,InService=true},
new LineStation{LineId=11,Station=38838, LineStationIndex=42, PrevStation=38839, NextStation=38837,InService=true},
new LineStation{LineId=11,Station=38839, LineStationIndex=41, PrevStation=38840, NextStation=38838,InService=true},
new LineStation{LineId=11,Station=38840, LineStationIndex=40, PrevStation=38841, NextStation=38839,InService=true},
new LineStation{LineId=11,Station=38841, LineStationIndex=39, PrevStation=38842, NextStation=38840,InService=true},
new LineStation{LineId=11,Station=38842, LineStationIndex=38, PrevStation=38844, NextStation=38841,InService=true},
new LineStation{LineId=11,Station=38844, LineStationIndex=37, PrevStation=38845, NextStation=38842,InService=true},
new LineStation{LineId=11,Station=38845, LineStationIndex=36, PrevStation=38846, NextStation=38844,InService=true},
new LineStation{LineId=11,Station=38846, LineStationIndex=35, PrevStation=38847, NextStation=38845,InService=true},
new LineStation{LineId=11,Station=38847, LineStationIndex=34, PrevStation=38848, NextStation=38846,InService=true},
new LineStation{LineId=11,Station=38848, LineStationIndex=33, PrevStation=38849, NextStation=38847,InService=true},
new LineStation{LineId=11,Station=38849, LineStationIndex=32, PrevStation=38852, NextStation=38848,InService=true},
new LineStation{LineId=11,Station=38852, LineStationIndex=31, PrevStation=38854, NextStation=38849,InService=true},
new LineStation{LineId=11,Station=38854, LineStationIndex=30, PrevStation=38855, NextStation=38852,InService=true},
new LineStation{LineId=11,Station=38855, LineStationIndex=29, PrevStation=38856, NextStation=38854,InService=true},
new LineStation{LineId=11,Station=38856, LineStationIndex=28, PrevStation=38859, NextStation=38855,InService=true},
new LineStation{LineId=11,Station=38859, LineStationIndex=27, PrevStation=38860, NextStation=38856,InService=true},
new LineStation{LineId=11,Station=38860, LineStationIndex=26, PrevStation=38861, NextStation=38859,InService=true},
new LineStation{LineId=11,Station=38861, LineStationIndex=25, PrevStation=38862, NextStation=38860,InService=true},
new LineStation{LineId=11,Station=38862, LineStationIndex=24, PrevStation=38863, NextStation=38861,InService=true},
new LineStation{LineId=11,Station=38863, LineStationIndex=23, PrevStation=38864, NextStation=38862,InService=true},
new LineStation{LineId=11,Station=38864, LineStationIndex=22, PrevStation=38865, NextStation=38863,InService=true},
new LineStation{LineId=11,Station=38865, LineStationIndex=21, PrevStation=38866, NextStation=38864,InService=true},
new LineStation{LineId=11,Station=38866, LineStationIndex=20, PrevStation=38867, NextStation=38865,InService=true},
new LineStation{LineId=11,Station=38867, LineStationIndex=19, PrevStation=38869, NextStation=38866,InService=true},
new LineStation{LineId=11,Station=38869, LineStationIndex=18, PrevStation=38870, NextStation=38867,InService=true},
new LineStation{LineId=11,Station=38870, LineStationIndex=17, PrevStation=38872, NextStation=38869,InService=true},
new LineStation{LineId=11,Station=38872, LineStationIndex=16, PrevStation=38873, NextStation=38870,InService=true},
new LineStation{LineId=11,Station=38873, LineStationIndex=15, PrevStation=38875, NextStation=38872,InService=true},
new LineStation{LineId=11,Station=38875, LineStationIndex=14, PrevStation=38876, NextStation=38873,InService=true},
new LineStation{LineId=11,Station=38876, LineStationIndex=13, PrevStation=38877, NextStation=38875,InService=true},
new LineStation{LineId=11,Station=38877, LineStationIndex=12, PrevStation=38878, NextStation=38876,InService=true},
new LineStation{LineId=11,Station=38878, LineStationIndex=11, PrevStation=38879, NextStation=38877,InService=true},
new LineStation{LineId=11,Station=38879, LineStationIndex=10, PrevStation=38880, NextStation=38878,InService=true},
new LineStation{LineId=11,Station=38880, LineStationIndex=9, PrevStation=38881, NextStation=38879,InService=true},
new LineStation{LineId=11,Station=38881, LineStationIndex=8, PrevStation=38883, NextStation=38880,InService=true},
new LineStation{LineId=11,Station=38883, LineStationIndex=7, PrevStation=38884, NextStation=38881,InService=true},
new LineStation{LineId=11,Station=38884, LineStationIndex=6, PrevStation=38885, NextStation=38883,InService=true},
new LineStation{LineId=11,Station=38885, LineStationIndex=5, PrevStation=38886, NextStation=38884,InService=true},
new LineStation{LineId=11,Station=38886, LineStationIndex=4, PrevStation=38887, NextStation=38885,InService=true},
new LineStation{LineId=11,Station=38887, LineStationIndex=3, PrevStation=38888, NextStation=38886,InService=true},
new LineStation{LineId=11,Station=38888, LineStationIndex=2, PrevStation=38889, NextStation=38887,InService=true},
new LineStation{LineId=11,Station=38889, LineStationIndex=1, PrevStation=38890, NextStation=38888,InService=true},
new LineStation{LineId=11,Station=38890, LineStationIndex=0, PrevStation=38890, NextStation=38889,InService=true}
};

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
            adjacentStationsListTheSameStops = (List<AdjacentStations>)(from linestation in lineStationsList
                                                                        from linestation2 in lineStationsList
                                                                        where linestation.LineId == linestation2.LineId && linestation.LineStationIndex == linestation2.LineStationIndex
                                                                        select new AdjacentStations
                                                                        {   //LineId=linestation.LineId,
                                                                            Station1 = linestation.Station,
                                                                            Station2 = linestation2.Station,
                                                                            Distance = 0f,
                                                                            Time = new TimeSpan(0, 0, 0),
                                                                            InService = true
                                                                        }).GroupBy(p => p.Station1).Select(p => p.First());
                                                        //}).Distinct().ToList();
                                                        //adjacentStationsList = new List<AdjacentStations>();
                                                     adjacentStationsList =
             (from lineStation in lineStationsList
              from linestation2 in lineStationsList
              where lineStation.LineId == linestation2.LineId && linestation2.LineStationIndex == lineStation.LineStationIndex + 1 && lineStation.InService
              let rdistance = RandomDistance()
              select new AdjacentStations
              {//LineId=lineStation.LineId,
                  Station1 = lineStation.Station,
                  Station2 = linestation2.Station,
                  Distance = rdistance,
                  Time = MinutesOfTravel(rdistance),
                  InService = true
              }).Distinct().ToList();
             adjacentStationsList.AddRange(adjacentStationsListTheSameStops);
             //lineTripsList = new List<LineTrip>();
             lineTripsList = (from line in linesList
                              let id = Configuration.LineTripId
                              select new LineTrip
                              {
                                  LineId = line.Id,
                                  Id = id,
                                  InService = true,
                                  StartAt = RandomExitTime()
                              }).ToList();
            
             stationRoot = new XElement("Stations");
             stationRoot.Save(stationsPath);
             XmlInputOutput.SaveListToXml(adjacentStationsList, adjacentStationsPath);
             XmlInputOutput.SaveListToXml(linesList, linePath);
             XmlInputOutput.SaveListToXml(lineTripsList, lineTripsPath);
             XmlInputOutput.SaveListToXml(lineStationsList, lineStationsPath);

        }
        /// <summary>
        /// For random distance
        /// </summary>
        /// <returns></returns>
        public static float RandomDistance()
        {
            int randomDistance = random.Next(100, 10000);//between 100 to 10000 meters
            //float distance = randomDistance / 1000 + 0.05f;
            //return distance;
            return randomDistance;
        }
        /// <summary>
        /// For random line exit time
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static TimeSpan RandomExitTime()
        {
            int randomHours = random.Next(0, 23);
            int randomMinutes = random.Next(0, 59);
            int randomSeconds = random.Next(0, 59);
            TimeSpan randomTime = new TimeSpan(randomHours, randomMinutes, randomSeconds);
            return randomTime;
        }
        /// <summary>
        /// For random time between stations
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static TimeSpan MinutesOfTravel(float distance)
        {   //t[min] = 60 * s[km] / v[km / H]
            //float distanceKm = distance;
            float distanceKm = distance / 1000;
            //Random speed = new Random();
            //int randomSpeed = speed.Next(30, 70);
            int randomSpeed = random.Next(30, 70);
            int minutesInHour = 60;
            int minutesOfTravel = (int)Math.Ceiling(minutesInHour * distanceKm / randomSpeed);
            TimeSpan minutesTravel = new TimeSpan(0, minutesOfTravel, 0);
            return minutesTravel;
        }
    }
}
