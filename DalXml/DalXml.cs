﻿using DalAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
namespace DL
{
    sealed class DalXml : IDL
    {
        #region singleton
        static readonly DalXml instance = new DalXml();
        static DalXml()
        {
        }
        DalXml()
        {
        }
        public static DalXml Instance { get { return instance; } }
        #endregion
        string configPath = @"..\bin\xml\Config.xml";
        string linePath = @"..\bin\xml\Line.xml";
        string stationsPath = @"..\bin\xml\Station.xml";
        string adjacentStationsPath = @"..\bin\xml\AdjacentStation.xml";
        string lineStationsPath = @"..\bin\xml\LineStations.xml";
        string lineTripsPath = @"..\bin\xml\LineTrips.xml";
        #region Station
        /// <summary>
        /// Add station to xml file of stations
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            XElement stationsElement = XElement.Load(stationsPath);
            XElement code = new XElement("Code", station.Code);
            XElement name = new XElement("Name", station.Name);
            XElement longtitude = new XElement("Longtitude", station.Longtitude);
            XElement latitude = new XElement("Latitude", station.Latitude);
            XElement inService = new XElement("InService", station.InService);
            XElement stationRoot = new XElement("Station", code, name, longtitude, latitude, inService);//Was Stations
            stationsElement.Add(stationRoot);
            stationsElement.Save(stationsPath);
        }
        public IEnumerable<Station> GetAllStations()
        {
            IEnumerable<Station> requestedStations = from p in XElement.Load(stationsPath).Elements()
                                                     select new Station
                                                     {
                                                         Code = Convert.ToInt32(p.Element("Code").Value),
                                                         InService = true,
                                                         Latitude = float.Parse(p.Element("Latitude").Value),
                                                         Longtitude = float.Parse(p.Element("Longtitude").Value),
                                                         Name = p.Element("Name").Value
                                                     };
            return requestedStations.Where(p=>p.InService==true);
        }
        public Station GetStation(int code)
        {
            try
            {
                List<Station> allStations = (from p in XElement.Load(stationsPath).Elements()
                                             where Convert.ToInt32(p.Element("Code").Value) == code
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
        #endregion
        #region Line
        public void AddLine(Line line)
        {
            //Added threading
            //Thread ioThread = new Thread(() => {
                List<Line> allLinesList = XmlInputOutput.LoadListFromXml<Line>(linePath);
                Line tempLine = allLinesList.Find(p => p.InService == true && p.Id == line.Id);
                if (tempLine == null || tempLine.InService == false)
                {
                    allLinesList.Add(line);
                    XmlInputOutput.SaveListToXml(allLinesList, linePath);

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
            var v = from line in allLinesList where line.InService == true select line;
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
            XElement doc = XElement.Load(configPath);
            XElement newIdElement = doc.Element("LineId");
            int currentId = int.Parse(newIdElement.Value);
            int newIdValue = currentId + 1;
            newIdElement.Value = newIdValue.ToString();
            doc.Save(configPath);
            return currentId;
        }
        /// <summary>
        /// Each new station gets a unique code from configuration 
        /// </summary>
        /// <returns></returns>
        public int GetNewStationCode()
        {
            XElement doc = XElement.Load(configPath);
            XElement newCodeElement =doc.Element("StationCode");
            int currentCode = int.Parse(newCodeElement.Value);
            int newCodeValue = currentCode + 1;
            newCodeElement.Value = newCodeValue.ToString();
            doc.Save(configPath);
            return currentCode;
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
        { List<LineStation> lineStationList = XmlInputOutput.LoadListFromXml<LineStation>(lineStationsPath);
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
            List<LineStation> allLineStationsList = XmlInputOutput.LoadListFromXml<LineStation>(lineStationsPath);
              var v = from lineStation in allLineStationsList where lineStation.InService==true select lineStation;
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
            IEnumerable<LineStation> lineStationsOfLine = from lineStation 
                                                          in XmlInputOutput.LoadListFromXml<LineStation>(lineStationsPath)
                                                          where 
                                                          lineStation.InService==true && lineStation.LineId==lineId
                                                          select lineStation ;
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
            return from adjstat in allAdjacentStations where adjstat.InService == true select adjstat;//b4 wasnt distinct
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
        public void DeleteAdjacentStations(int stationOneCode, int stationTwoCode)
        {
            List<AdjacentStations> allAdjacentStations = XmlInputOutput.LoadListFromXml<AdjacentStations>(adjacentStationsPath);
            AdjacentStations adjStat = allAdjacentStations.Find(p => p.Station1 == stationOneCode && p.Station2 == stationTwoCode && p.InService == true);
            if (adjStat == null)
            {
                throw new AdjacentStationsDoesntExistException(stationOneCode, stationTwoCode, $"The adjacent stations with the codes:{stationOneCode} {stationTwoCode} can't be updated since it doesn't exist on the system");
            }
            adjStat.InService = false;
            XmlInputOutput.SaveListToXml<AdjacentStations>(allAdjacentStations, adjacentStationsPath);

        }
        #endregion
        #region LineTrip
        public int GetNewLineTripId()
        {
            XElement doc = XElement.Load(configPath);
            XElement newLineTripIdElement = doc.Element("LineTripId");
            int currentId = int.Parse(newLineTripIdElement.Value);
            int newIdValue = currentId + 1;
            newLineTripIdElement.Value = newIdValue.ToString();
            doc.Save(configPath);
            return currentId;
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


