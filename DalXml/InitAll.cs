using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using DO;

namespace DL
{   /// <summary>
    /// To initialize all xml files.
    /// </summary>
    public static class InitAll
    {
        static string linePath = @"..\bin\xml\Line.xml";
        static string stationsPath = @"..\bin\xml\Station.xml";
        static string adjacentStationsPath = @"..\bin\xml\AdjacentStation.xml";
        static string lineStationsPath = @"..\bin\xml\LineStations.xml";
        static string lineTripsPath = @"..\bin\xml\LineTrips.xml";
        public static Random random;
        public static List<AdjacentStations> adjacentStationsList;
        public static List<AdjacentStations> adjacentStationsListTheSameStops;//Adjacent stations for the first stop(has two stops of first bus stop in route code is useful for presenting information on amount of waiting time from previous stop(zero) and distance(none))
        public static List<Line> linesList;
        public static List<LineStation> lineStationsList;
        public static List<LineTrip> lineTripsList;
        public static List<Station> stationsList;
        public static List<User> usersList;
        static XElement stationRoot;

        public static XElement StationRoot { get => stationRoot; }

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
            lineStationsList = new List<LineStation>
            {
                new LineStation{LineId=1,Station=38831,LineStationIndex=0,PrevStation=38831,NextStation=38832,InService=true},
                new LineStation{LineId=1,Station=38832,LineStationIndex=1,PrevStation=38831,NextStation=38833,InService=true},
                new LineStation{LineId=1,Station=38833,LineStationIndex=2,PrevStation=38832,NextStation=38834,InService=true},
                new LineStation{LineId=1,Station=38834,LineStationIndex=3,PrevStation=38833,NextStation=38836,InService=true},
                new LineStation{LineId=1,Station=38836,LineStationIndex=4,PrevStation=38834,NextStation=38837,InService=true},
                new LineStation{LineId=1,Station=38837,LineStationIndex=5,PrevStation=38836,NextStation=38838,InService=true},
                new LineStation{LineId=1,Station=38838,LineStationIndex=6,PrevStation=38837,NextStation=38839,InService=true},
                new LineStation{LineId=1,Station=38839,LineStationIndex=7,PrevStation=38838,NextStation=38840,InService=true},
                new LineStation{LineId=1,Station=38840,LineStationIndex=8,PrevStation=38839,NextStation=38841,InService=true},
                new LineStation{LineId=1,Station=38841,LineStationIndex=9,PrevStation=38840,NextStation=38841,InService=true},
                new LineStation{LineId=2,Station=38842,LineStationIndex=-1,PrevStation=38842,NextStation=38842,InService=true},
                new LineStation{LineId=2,Station=38842,LineStationIndex=0,PrevStation=38842,NextStation=38844,InService=true},
                new LineStation{LineId=2,Station=38844,LineStationIndex=1,PrevStation=38842,NextStation=38845,InService=true},
                new LineStation{LineId=2,Station=38845,LineStationIndex=2,PrevStation=38844,NextStation=38846,InService=true},
                new LineStation{LineId=2,Station=38846,LineStationIndex=3,PrevStation=38845,NextStation=38847,InService=true},
                new LineStation{LineId=2,Station=38847,LineStationIndex=4,PrevStation=38846,NextStation=38848,InService=true},
                new LineStation{LineId=2,Station=38848,LineStationIndex=5,PrevStation=38847,NextStation=38849,InService=true},
                new LineStation{LineId=2,Station=38849,LineStationIndex=6,PrevStation=38848,NextStation=38852,InService=true},
                new LineStation{LineId=2,Station=38852,LineStationIndex=7,PrevStation=38849,NextStation=38854,InService=true},
                new LineStation{LineId=2,Station=38854,LineStationIndex=8,PrevStation=38852,NextStation=38855,InService=true},
                new LineStation{LineId=2,Station=38855,LineStationIndex=9,PrevStation=38854,NextStation=38855,InService=true},
                new LineStation{LineId=3,Station=38856,LineStationIndex=0,PrevStation=38856,NextStation=38859,InService=true},
                new LineStation{LineId=3,Station=38859,LineStationIndex=1,PrevStation=38856,NextStation=38860,InService=true},
                new LineStation{LineId=3,Station=38860,LineStationIndex=2,PrevStation=38859,NextStation=38861,InService=true},
                new LineStation{LineId=3,Station=38861,LineStationIndex=3,PrevStation=38860,NextStation=38862,InService=true},
                new LineStation{LineId=3,Station=38862,LineStationIndex=4,PrevStation=38861,NextStation=38863,InService=true},
                new LineStation{LineId=3,Station=38863,LineStationIndex=5,PrevStation=38862,NextStation=38864,InService=true},
                new LineStation{LineId=3,Station=38864,LineStationIndex=6,PrevStation=38863,NextStation=38865,InService=true},
                new LineStation{LineId=3,Station=38865,LineStationIndex=7,PrevStation=38864,NextStation=38866,InService=true},
                new LineStation{LineId=3,Station=38866,LineStationIndex=8,PrevStation=38865,NextStation=38867,InService=true},
                new LineStation{LineId=3,Station=38867,LineStationIndex=9,PrevStation=38866,NextStation=38867,InService=true},
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

         adjacentStationsList =
        (from lineStation in lineStationsList
        from linestation2 in lineStationsList
        where lineStation.LineId == linestation2.LineId && linestation2.LineStationIndex == lineStation.LineStationIndex + 1 && lineStation.InService
        let rdistance = RandomDistance()
        select new AdjacentStations
        {
            Station1 = lineStation.Station,
            Station2 = linestation2.Station,
            Distance = rdistance,
            Time = MinutesOfTravel(rdistance),
            InService = true
        }).Distinct().ToList();
            adjacentStationsList.AddRange(adjacentStationsListTheSameStops);
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
