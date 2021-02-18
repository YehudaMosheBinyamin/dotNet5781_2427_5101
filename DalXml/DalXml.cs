using DalAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
namespace DL
{
   sealed class DalXml:IDL
    {
        #region singleton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }
        DalXml()
        {
            //InitAll.InitializeAll();
        }        
        public static DalXml Instance { get { return instance; } }
        #endregion
        string linePath = @"..\bin\XmlFiles\Line.xml";
        string stationsPath = @"..\bin\XmlFiles\Station.xml";
        string adjacentStationsPath = @"..\bin\XmlFiles\AdjacentStations.xml";
        string lineStationsPath = @"..\bin\XmlFiles\LineStations.xml";
        string lineTripsPath = @"..\bin\XmlFiles\LineTrips.xml";
        XElement stationsRoot;
            #region Station
            public void AddStation(Station station)
            {
            stationsRoot = new XElement("Stations");
            XElement code = new XElement("Code", station.Code);
            XElement name = new XElement("Name", station.Name);
            XElement longtitude = new XElement("Longtitude", station.Longtitude);
            XElement latitude = new XElement("Latitude", station.Latitude);
            XElement inService = new XElement("InService", station.InService);
            stationsRoot.Add("station", code, name, longtitude, latitude,inService);
            stationsRoot.Save(stationsPath);
            }
            public IEnumerable<Station> GetAllStations()
            {
            IEnumerable<Station> requestedStations = from p in stationsRoot.Elements()
                                        select new Station
                                        {
                                            Code = Convert.ToInt32(p.Element("Code").Value),
                                            InService = true,
                                            Latitude = float.Parse(p.Element("Latitude").Value),
                                            Longtitude = float.Parse(p.Element("Longtitude").Value),
                                            Name = p.Element("Name").Value
                                        };
            return requestedStations;
        }
            //unneeded
            public void UpdateStation(Line line, Action<Line> action)
            {


            }
            public Station GetStation(int code)
            {
            try
            {
                List<Station> allStations = (from p in stationsRoot.Elements()
                                            where Convert.ToInt32(p.Element("station").Value) == code
                                            select new Station
                                            {
                                                Code = code,
                                                InService = true,
                                                Latitude = float.Parse(p.Element("Latitude").Value),
                                                Longtitude = float.Parse(p.Element("Longtitude").Value),
                                                Name = p.Element("Name").Value
                                            }).ToList();
                Station requestedStation = allStations.Find(p => p.InService == true && p.Code == code);
                if (requestedStation != null)
                {
                    return requestedStation;
                }
                else
                {
                    throw new NoStationFoundException(code);
                }
            }
           catch (Exception exception)
            {   
                    throw new NoStationFoundException(code, $"This station with code {code} doesn't exist ");    
            }
            }
            
           
         /**
            /**public void UpdateStation(int code,string newName,Action<Station,string> update)
            {
             Action<Station,string>action=update;
             Station tempStation=DataSource.stationsList.Find(p =>p.Code==code);
             if(tempStation!=null&&tempStation.InService==true)
             {
                    action(tempStation,newName);
                }
            }
            public void DeleteStation(int code)
            {
                Station tempStation= DataSource.stationsList.Find(p =>p.Code==code);
                if(tempStation!=null&&tempStation.InService==true)
                {
                    tempStation.InService=false;
                }
                else
                {
                    throw new NoStationFoundException(code,$"This station with code {code} doesn't exist and can't be deleted");
                }
            
            }**/
            #endregion
        /**
            #region Bus
            public void AddBus(Bus bus)
            {
                string LicenseNum = bus.LicenseNum;
                Bus tempBus = DataSource.busesList.Find(p => p.LicenseNum == LicenseNum);
                if (tempBus == null)
                {
                    DataSource.busesList.Add(bus);

                }
                else if (tempBus.InService == false)
                {
                    tempBus.InService = true;
                }
                else
                {
                    throw new BusAlreadyExistsException(LicenseNum, $"The bus with the license num: {LicenseNum} exists and is active already and cannot be added twice");
                }

            }
            public IEnumerable<Bus> GetAllBuses()
            {
                return from bus in DS.DataSource.busesList select bus.Clone();
            }
            public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
            {
                return DS.DataSource.busesList.FindAll(predicate);
            }
            public Bus GetBus(string licenseNum)
            {
                Bus tempBus = DataSource.busesList.Find(p => p.LicenseNum == licenseNum);
                if (tempBus != null && tempBus.InService == true)
                {
                    return tempBus;
                }
                else
                {
                    throw new NoBusFoundException(licenseNum, $"The bus with licenseNum: {licenseNum} doesn't exist on the system so it can't be received");
                }
            }
            public void UpdateBus(string licenseNum, Action<Bus> update)
            {
                Action<Bus> action = update;
                Bus tempBus = DataSource.busesList.Find(p => p.LicenseNum == licenseNum);
                if (tempBus != null && tempBus.InService == true)
                { action(tempBus); }
                else
                {
                    throw new NoBusFoundException(licenseNum, $"No bus with the License Number: {licenseNum}  exists on the system so it cannot be deleted");
                }
            }
            public void DeleteBus(string licenseNum)
            {
                Bus tempBus = DataSource.busesList.Find(p => p.LicenseNum == licenseNum);
                if (tempBus != null && tempBus.InService == true)
                {
                    tempBus.InService = false;
                }
                else
                {
                    throw new NoBusFoundException(licenseNum, $"No bus with the License Number: {licenseNum}  exists on the system so it cannot be deleted");
                }
            }**/
            #region Line
            public void AddLine(Line line)
            {
            List<Line> allLinesList = XmlInputOutput.LoadListFromXml<Line>(linePath);
            Line tempLine = allLinesList.Find(p => p.InService == true && p.Id == line.Id);
                if (tempLine == null || tempLine.InService == false)
                {
                allLinesList.Add(line);
                XmlInputOutput.SaveListToXml<Line>(allLinesList, linePath);
                   
                }
                else
                {
                    throw new LineAlreadyExistsException(line.Id, $"This line with id: {line.Id}  already exists-Bus with code{line.Code} and is active so it can't be added");
                }
            }
            /// <summary>
            /// To get lines from database
            /// </summary>
            /// <returns></returns>
            public IEnumerable<Line> GetAllLines()
            {

            List<Line> allLinesList = XmlInputOutput.LoadListFromXml<Line>(linePath);
            var v = from line in allLinesList select line;
            return v;
            }
            public IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate)
            {
            return null;
           }
            public Line GetLine(int lineId)
            {

            List<Line> linesList = XmlInputOutput.LoadListFromXml<Line>(linePath);
            Line tempLine = linesList.Find(p => p.Id == lineId && p.InService == true);
            if (tempLine != null)
            {
                return tempLine;
            }
            else
            {
                throw new NoLineFoundException(lineId, $"The line with id: {lineId} doesn't exist ");
            }



        }
            public int GetNewLineId()
            {
                return Configuration.LineId;
            }
            /// <summary>
            /// Each new station gets a unique code from configuration 
            /// </summary>
            /// <returns></returns>
            public int GetNewStationCode()
            {
                return Configuration.StationCode;
            }
        //not needed
            public void UpdateLine(int lineId, Action<Line> update)
            {
            }
            public void DeleteLine(int lineId)
            {
                List<Line> allLines = XmlInputOutput.LoadListFromXml<Line>(linePath);
                Line tempLine = allLines.Find(p => p.Id == lineId && p.InService == true);
                if (tempLine != null)
                {
                    tempLine.InService = false;
                XmlInputOutput.SaveListToXml<Line>(allLines, linePath);
                }
                else
                {
                    throw new NoLineFoundException(lineId, $"The line with id: {lineId} doesn't exist ");
                }

            }
            #endregion
            #region LineStation
            public bool InLineStations(int lineId, int stationCode)
            {
                List<LineStation> lineStationList = XmlInputOutput.LoadListFromXml<LineStation>(lineStationsPath);
                int id = lineId;
                int code = stationCode;
                LineStation tempLineStation = lineStationList.Find(p => p.LineId == id && p.Station == code && p.InService == true);
                if (tempLineStation == null)
                {
                    return false;

                }
                return true;
            }

            public void AddLineStation(LineStation lineStation)
            {
                int id = lineStation.LineId;
                int code = lineStation.Station;
                List<LineStation> allLineStations = XmlInputOutput.LoadListFromXml<LineStation>(lineStationsPath);
                LineStation tempLineStation = (from ls in allLineStations where ls.Station == code && ls.InService == true && ls.LineId == id select ls).ToList().FirstOrDefault();
                if (tempLineStation == null || lineStation.LineStationIndex != tempLineStation.LineStationIndex)
                {
                    allLineStations.Add(lineStation);
                    XmlInputOutput.SaveListToXml<LineStation>(allLineStations, lineStationsPath);
                }

                else
                {
                    throw new LineStationAlreadyExistsException(id, code, $"This line station with id: {id} code{code} already exists and is active so it can't be added");
                }
            }

            public IEnumerable<LineStation> GetAllLineStations()
            {
            List<LineStation> allLineStationsList = XmlInputOutput.LoadListFromXml<LineStation>(this.lineStationsPath);
            var v = from lineStation in allLineStationsList select lineStation;
            return v;
            }

            public LineStation GetLineStation(int lineId, int stationCode)
            {
                List<LineStation> lineStationsList = (from lineStation in XmlInputOutput.LoadListFromXml<LineStation>(lineStationsPath) select lineStation).ToList();
                LineStation tempLineStation = lineStationsList.FirstOrDefault(p => (p.LineId == lineId) && (p.Station == stationCode) && (p.InService == true));
                if (tempLineStation != null)
                {
                    return tempLineStation;
                }
                else
                {
                    throw new NoLineStationFoundException(lineId, stationCode, $"The lineStation with id: {lineId} doesn't exist ");
                }

            }


            public IEnumerable<LineStation> GetAllLineStationsByLine(int lineId)
            {
            IEnumerable<LineStation> lineStationsOfLine = from lineStation in XmlInputOutput.LoadListFromXml<LineStation>(lineStationsPath) select lineStation;
            return lineStationsOfLine;
              
            }


            public void DeleteLineStation(int lineId, int stationCode)
            {
            List<LineStation> allLineStations = XmlInputOutput.LoadListFromXml<LineStation>(lineStationsPath);
                LineStation tempLineStation = allLineStations.Find(p => p.LineId == lineId && p.Station == stationCode && p.InService == true);
                if (tempLineStation != null)
                {
                    tempLineStation.InService = false;
                    XmlInputOutput.SaveListToXml(allLineStations, lineStationsPath);
                }
                else
                {
                    throw new NoLineStationFoundException(lineId, stationCode, $"The line with id: {lineId} doesn't exist ");
                }
            }
            public void DeleteLineStations(int lineId)
            {
            List<LineStation> allLineStations = XmlInputOutput.LoadListFromXml<LineStation>(lineStationsPath);
                foreach (LineStation ls in allLineStations)
                {
                    if (ls.LineId == lineId && ls.InService == true)
                    {
                        ls.InService = false;
                    }
                }
            XmlInputOutput.SaveListToXml<LineStation>(allLineStations, lineStationsPath);
            }
        #endregion
        public User GetUser(string userName)
        {

            return null;

        }
        /**
            #endregion
            #region User
            public void AddUser(User user)
            {
                User tempUser = DataSource.usersList.Find(p => p.UserName == user.UserName);
                if (tempUser != null && tempUser.InService == false)
                {
                    //var md5 = new MD5CryptoServiceProvider();
                    //var md5data = md5.ComputeHash(user.Password);
                    DataSource.usersList.Add(tempUser);

                }
                else
                {
                    throw new UserAlreadyExistsException(user.UserName);
                }
            }
            public IEnumerable<User> GetAllUsers()
            {
                return from user in DS.DataSource.usersList where user.InService == true select user.Clone();
            }
           
            public void UpdateUser(string userName, Action<User> update)
            {
                Action<User> action = update;
                User tempUser = DataSource.usersList.Find((p) => p.UserName == userName);
                if (tempUser != null && tempUser.InService == true)
                {
                    action(tempUser);
                }
                else
                {
                    throw new NoUserFoundException(userName);
                }
            }

            public void DeleteUser(string userName)
            {
                User tempUser = DataSource.usersList.Find(p => p.UserName == userName);
                if (tempUser != null && tempUser.InService == true)
                {
                    tempUser.InService = false;
                }
                else
                {
                    throw new NoUserFoundException(userName);

                }
            }
            #endregion User
        **/
        #region AdjacentStations
        /// <summary>
        /// To check if adjacent stations exist in system
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns></returns>
        public bool AdjacentStationsExists(int station1, int station2)
            {
                List<AdjacentStations> allAdjacentStations = XmlInputOutput.LoadListFromXml<AdjacentStations>(adjacentStationsPath);
                AdjacentStations tempAdjStat = allAdjacentStations.Find(p => p.Station1 == station1 && p.Station2 == station2 && p.InService == true);
                if (tempAdjStat == null)//if not found any adjacentstations that fulfill the predicate
                {
                    return false;
                }
                return true;
            }
            /// <summary>
            /// To add adjacent stations to database
            /// </summary>
            /// <param name="adjacentStations"></param>
            public void AddAdjacentStations(AdjacentStations adjacentStations)
            {
                int station1 = adjacentStations.Station1;
                int station2 = adjacentStations.Station2;
                List<AdjacentStations> allAdjacentStations = XmlInputOutput.LoadListFromXml<AdjacentStations>(adjacentStationsPath);
                AdjacentStations tempAdjStat = allAdjacentStations.Find(p => p.Station1 == station1 && p.Station2 == station2);
                if (tempAdjStat == null || tempAdjStat.InService == false)
                {
                    allAdjacentStations.Add(adjacentStations);
                    XmlInputOutput.SaveListToXml<AdjacentStations>(allAdjacentStations, adjacentStationsPath);
                }
                else
                {
                    throw new AdjacentStationsAlreadyExistsException(station1, station2, $"The adjacentStations  codes:{adjacentStations.Station1} {adjacentStations.Station2} already exist and cannot be added again to the system");
                }
            }

            public IEnumerable<AdjacentStations> GetAllAdjacentStations()
            {
                List<AdjacentStations> allAdjacentStations = XmlInputOutput.LoadListFromXml<AdjacentStations>(adjacentStationsPath); 
                return from adjstat in allAdjacentStations where adjstat.InService == true select adjstat;
            }

            public IEnumerable<AdjacentStations> GetAdjacentStationsBy(Predicate<AdjacentStations> predicate)
            {
            List<AdjacentStations> allAdjacentStations = XmlInputOutput.LoadListFromXml<AdjacentStations>(adjacentStationsPath);
            return from adjstat in allAdjacentStations where predicate(adjstat) && adjstat.InService == true select adjstat;
            }

            /// <summary>
            /// To get adjacent station instance
            /// </summary>
            /// <param name="stationOneCode"></param>
            /// <param name="stationTwoCode"></param>
            /// <returns></returns>
            public AdjacentStations GetAdjacentStations(int stationOneCode, int stationTwoCode)
            {
                List<AdjacentStations> allAdjacentStations = XmlInputOutput.LoadListFromXml<AdjacentStations>(adjacentStationsPath);
            
                AdjacentStations adjStat = allAdjacentStations.Find(p => p.Station1 == stationOneCode && p.Station2 == stationTwoCode && p.InService == true);
                if (adjStat == null)
                {
                    throw new AdjacentStationsDoesntExistException(stationOneCode, stationTwoCode, $"There is no two adjacent stations codes{stationOneCode} {stationTwoCode}");
                }
                return adjStat; 
            }
            /**
            public void UpdateAdjacentStations(int stationOneCode, int stationTwoCode, Action<AdjacentStations> update)
            {
                AdjacentStations adjStationsToUpdate= DS.DataSource.adjacentStationsList.Find(p => p.Station1 == stationOneCode && p.Station2 == stationTwoCode);
                if (adjStationsToUpdate == null||adjStationsToUpdate.InService==false)
                {
                    throw new AdjacentStationsDoesntExistException(stationOneCode, stationTwoCode, $"The adjacent stations with the codes:{stationOneCode} {stationTwoCode} can't be updated since it doesn't exist on the system");
                }
            }**/

            public void DeleteAdjacentStations(int stationOneCode, int stationTwoCode)
            {
                List<AdjacentStations> allAdjacentStations = XmlInputOutput.LoadListFromXml<AdjacentStations>(adjacentStationsPath);
                AdjacentStations adjStat = allAdjacentStations.Find(p => p.Station1 == stationOneCode && p.Station2 == stationTwoCode && p.InService == true);
                if (adjStat == null)
                {
                    throw new AdjacentStationsDoesntExistException(stationOneCode, stationTwoCode, $"The adjacent stations with the codes:{stationOneCode} {stationTwoCode} can't be updated since it doesn't exist on the system");
                }
                    adjStat.InService = false;
                XmlInputOutput.SaveListToXml<AdjacentStations>(allAdjacentStations,adjacentStationsPath);
                
            }
            #endregion
            #region LineTrip
            public int GetNewLineTripId()
            {
                return Configuration.LineTripId;
            }
            public void AddLineTrip(LineTrip lineTrip)
            {
                int id = lineTrip.Id;
                int lineId = lineTrip.LineId;
                List<LineTrip> allLineTrips = XmlInputOutput.LoadListFromXml<LineTrip>(lineTripsPath);
                LineTrip tempLineTrip = allLineTrips.Find(p => p.LineId == lineId && p.Id == id && p.InService == true);
                if (tempLineTrip == null || tempLineTrip.InService == false)
                {
                   allLineTrips.Add(lineTrip);
                   XmlInputOutput.SaveListToXml<LineTrip>(allLineTrips, lineTripsPath);

                }
                else
                {
                    throw new LineTripAlreadyExistsException(id, lineId, $"The line trip of line with id:{lineTrip.Id} of line with id: {lineTrip.LineId} already exists and is active so it can't be added");
                }
            }
            /// <summary>
            /// To get all line trips
            /// </summary>
            /// <returns></returns>
            public IEnumerable<LineTrip> GetAllLineTrips()
            {
                List<LineTrip> allLineTrips = XmlInputOutput.LoadListFromXml<LineTrip>(lineTripsPath);
                return from lineTrip in allLineTrips where lineTrip.InService == true select lineTrip;
            }

            public LineTrip GetLineTrip(int id, int busId)
            {
               List<LineTrip> allLineTrips = XmlInputOutput.LoadListFromXml<LineTrip>(lineTripsPath);
               return allLineTrips.Find(p => p.Id == id && p.LineId == busId && p.InService == true);
            }

            public IEnumerable<LineTrip> GetLineTripsBy(Predicate<LineTrip> predicate)
            {
               List<LineTrip> allLineTrips = XmlInputOutput.LoadListFromXml<LineTrip>(lineTripsPath);
               return from linetrip in allLineTrips where predicate(linetrip) && linetrip.InService == true select linetrip;
            }

            public void UpdateLineTrip(int id, int lineId, Action<LineTrip> update)
            {
               List<LineTrip> allLineTrips = XmlInputOutput.LoadListFromXml<LineTrip>(lineTripsPath);
               LineTrip lineTripUpdate = allLineTrips.Find(p => p.Id == id && p.LineId == lineId);
                if (lineTripUpdate == null || lineTripUpdate.InService == false)
                {
                    throw new NoLineTripExistsException(id, lineId, $"The line trip with lineId {id} and lineId {lineId} cannot be deleted since it does not exist in the system ");
                }
                update(lineTripUpdate);
                XmlInputOutput.SaveListToXml(allLineTrips, lineTripsPath);
            }
            /// <summary>
            /// To delete line trip
            /// </summary>
            /// <param name="id"></param>
            /// <param name="lineId"></param>
            public void DeleteLineTrip(int id, int lineId)
            {
                List<LineTrip> allLineTrips = XmlInputOutput.LoadListFromXml<LineTrip>(lineTripsPath);
                LineTrip lineTrip = allLineTrips.Find(p => p.Id == id && p.LineId == lineId && p.InService == true);
                if (lineTrip == null)
                {
                    throw new NoLineTripExistsException(id, lineId, $"The line trip with lineId {id} and lineId {lineId} cannot be deleted since it does not exist in the system ");
                }
                lineTrip.InService = false;
                XmlInputOutput.SaveListToXml(allLineTrips, lineTripsPath);
            }
            /// <summary>
            /// To delete line trips of line
            /// </summary>
            /// <param name="lineId"></param>
            public void DeleteLineTrips(int lineId)
            {
                List<LineTrip> lineTripsOfLine = XmlInputOutput.LoadListFromXml<LineTrip>(lineTripsPath);
                foreach (LineTrip lt in lineTripsOfLine)
                {
                    if (lt.LineId == lineId && lt.InService == true)
                    {
                        lt.InService = false;
                    }
                }
                XmlInputOutput.SaveListToXml(lineTripsOfLine, lineTripsPath);
            }

            #endregion
        }
    }


