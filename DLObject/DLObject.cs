﻿using System;
using DS;
using DalAPI;
using System.Runtime.Serialization;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace DL
{
     sealed class DLObject:IDL
    {
        #region singleton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }
        DLObject() { }
        public static DLObject Instance { get => instance; }
        #endregion
        #region Station
        public void AddStation(Station station)
        {
            int code=station.Code;
            Station tempStation= DataSource.stationsList.Find(p=>p.Code==code);
            if(tempStation==null )
            {
             DataSource.stationsList.Add(station.Clone());
            }
            else if(tempStation.InService == false)
            {
                tempStation.InService = true;
            }
             else
            {
                throw new StationAlreadyExistsException(code,$"This station with code {code} exists and is active already");
            }

        }
       
        public IEnumerable<Station> GetAllStations()
        {
            return from station in DS.DataSource.stationsList select station.Clone();
        }
       public Station GetStation(int code)
        {
           
                Station tempStation= DataSource.stationsList.Find(p =>p.Code==code);
                if(tempStation!=null)
                {
                    return tempStation.Clone();
                }
                else
                {
                throw new NoStationFoundException(code,$"This station with code {code} doesn't exist ");
                }

        }
  
        public int GetNewStationCode()
        {
            return Configuration.StationCode;
        }
        #endregion
        #region Line
        public void AddLine(Line line)
        { 
            int id = line.Id;
            Line tempLine = (from l in DataSource.linesList where l.Id == id select l).ToList().FirstOrDefault();
            if (tempLine == null||tempLine.InService==false)
            {
                //DataSource.linesList.Add(line.Clone());
                DataSource.linesList.Add(line);
            }
            else
            {
                throw new LineAlreadyExistsException(id, $"This line with id: {id}  already exists-Bus with code{tempLine.Code} and is active so it can't be added");
            }
        }
        /// <summary>
        /// To get lines from database
        /// </summary>
        /// <returns></returns>
       public IEnumerable<Line> GetAllLines()
        {
            return from line in DS.DataSource.linesList where line.InService == true select line;
        }
        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate)
        {
            return from line in DS.DataSource.linesList where predicate(line)==true &&line.InService==true select line;
        }
        public Line GetLine(int lineId)
        {
           
                Line tempLine= DataSource.linesList.Find(p =>p.Id==lineId&&p.InService==true);
                if(tempLine!=null)
                {
                    return tempLine.Clone();
                }
                else
                {
                throw new NoLineFoundException(lineId,$"The line with id: {lineId} doesn't exist ");
                }

        }
        public int GetNewLineId()
        {
            return Configuration.LineId;
        }

        public void UpdateLine(int lineId,Action<Line> update)
        {
         Action<Line>action=update;
            Line tempLine = DataSource.linesList.Find((p) => p.Id == lineId);
         if(tempLine!=null&&tempLine.InService==true)
         {
                action(tempLine);
            }
         else
            {
            throw new NoLineFoundException(lineId,$"The line with id: {lineId} doesn't exist ");
            }
        }
        public void DeleteLine(int lineId)
        {
            Line tempLine = DataSource.linesList.Find(p => p.Id == lineId&&p.InService==true);
            if (tempLine != null)
            {
                tempLine.InService = false;
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
            int id = lineId;
            int code = stationCode; 
            LineStation tempLineStation = DataSource.lineStationsList.Find(p => p.LineId == id && p.Station == code&&p.InService==true);
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
            LineStation tempLineStation = (from ls in DataSource.lineStationsList where ls.Station == code &&ls.InService==true && ls.LineId == id select ls).ToList().FirstOrDefault();
            if (tempLineStation == null||lineStation.LineStationIndex!=tempLineStation.LineStationIndex)
            {
                DataSource.lineStationsList.Add(lineStation.Clone());
            }
            
            else
            {
                throw new LineStationAlreadyExistsException(id,code, $"This line station with id: {id} code{code} already exists and is active so it can't be added");
            }
        }

        public IEnumerable<LineStation> GetAllLineStations()
        {
            return from lineStation in DS.DataSource.lineStationsList where lineStation.InService==true select lineStation.Clone();
        }

        public LineStation GetLineStation(int lineId, int stationCode)
        {
            LineStation tempLineStation = DataSource.lineStationsList.FirstOrDefault(p => (p.LineId == lineId) && (p.Station == stationCode)&&(p.InService==true));
            if (tempLineStation != null)
            {
                return tempLineStation.Clone();
            }
            else
            {
                throw new NoLineStationFoundException(lineId,stationCode, $"The lineStation with id: {lineId} doesn't exist ");
            }

        }
        public IEnumerable<LineStation> GetAllLineStationsByLine(int lineId)
        {
             return from lineStation in DS.DataSource.lineStationsList where lineStation.LineId==lineId && lineStation.InService==true orderby lineStation.LineStationIndex select  lineStation  ;
        }
        public void UpdateLineStation(int lineId, int stationCode,int newStationCode, Action<LineStation,int> update)
        {
            Action<LineStation,int> action = update;
            LineStation tempLineStation = DataSource.lineStationsList.Find(p => p.LineId == lineId && p.Station == stationCode);
            if (tempLineStation != null && tempLineStation.InService == true)
            {
                action(tempLineStation,newStationCode);
            }
            else
            {
                throw new NoLineStationFoundException(lineId,stationCode, $"The lineStation with id: {lineId} doesn't exist ");
            }
        }
        public void DeleteLineStation(int lineId,int stationCode)
        {
            LineStation tempLineStation = DataSource.lineStationsList.Find(p => p.LineId == lineId&&p.Station==stationCode&&p.InService==true);
            if (tempLineStation != null)
            {
                tempLineStation.InService = false;
            }
            else
            {
                throw new NoLineStationFoundException(lineId,stationCode, $"The line with id: {lineId} doesn't exist ");
            }
        }
        public void DeleteLineStations(int lineId)
        {
            foreach (LineStation ls in DS.DataSource.lineStationsList)
            {
                if (ls.LineId == lineId&&ls.InService==true)
                {
                    ls.InService = false;
                }
            }
        }


        #endregion
        #region AdjacentStations
        /// <summary>
        /// To check if adjacent stations exist in system
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns></returns>
        public bool AdjacentStationsExists(int station1,int station2)
        {
            AdjacentStations tempAdjStat = DataSource.adjacentStationsList.Find(p => p.Station1 == station1 && p.Station2 == station2&&p.InService==true); 
            if(tempAdjStat==null)//if not found any adjacentstations that fulfill the predicate
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
            AdjacentStations tempAdjStat = DataSource.adjacentStationsList.Find(p => p.Station1==station1 &&p.Station2==station2);
            if (tempAdjStat == null || tempAdjStat.InService == false)
            {
                DataSource.adjacentStationsList.Add(adjacentStations.Clone());
            }
            else
            {
                throw new AdjacentStationsAlreadyExistsException(station1,station2, $"The adjacentStations  codes:{adjacentStations.Station1} {adjacentStations.Station2} already exist and cannot be added again to the system");
            }
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            return from adjstat in DS.DataSource.adjacentStationsList where adjstat.InService==true select adjstat.Clone();
        }

        public IEnumerable<AdjacentStations> GetAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            return from adjstat in DS.DataSource.adjacentStationsList where predicate(adjstat) && adjstat.InService == true select adjstat;
        }
      
        /// <summary>
        /// To get adjacent station instance
        /// </summary>
        /// <param name="stationOneCode"></param>
        /// <param name="stationTwoCode"></param>
        /// <returns></returns>
        public AdjacentStations GetAdjacentStations(int stationOneCode, int stationTwoCode)
        {
            return DS.DataSource.adjacentStationsList.Find(p => p.Station1 == stationOneCode && p.Station2 == stationTwoCode&&p.InService==true);
        }

        public void DeleteAdjacentStations(int stationOneCode, int stationTwoCode)
        {
            List<AdjacentStations> adjStat = DS.DataSource.adjacentStationsList.FindAll(p => p.Station1 == stationOneCode && p.Station2 == stationTwoCode&&p.InService==true);
            if (adjStat == null)
            {
                throw new AdjacentStationsDoesntExistException(stationOneCode, stationTwoCode, $"The adjacent stations with the codes:{stationOneCode} {stationTwoCode} can't be updated since it doesn't exist on the system");
            }
            foreach(AdjacentStations adj in adjStat) 
            { 
                adj.InService = false; 
            }
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
            LineTrip tempLineTrip = DataSource.lineTripsList.Find(p => p.LineId == lineId && p.Id == id && p.InService == true) ;
            if (tempLineTrip == null||tempLineTrip.InService==false)
            {
                DataSource.lineTripsList.Add(lineTrip);

            }
            else
            {
                throw new LineTripAlreadyExistsException(id,lineId, $"The line trip of line with id:{lineTrip.Id} of line with id: {lineTrip.LineId} already exists and is active so it can't be added");
            }
        }
        /// <summary>
        /// To get all line trips
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineTrip> GetAllLineTrips()
        {
            return from lineTrip in DS.DataSource.lineTripsList where lineTrip.InService==true select lineTrip;
        }

        public LineTrip GetLineTrip(int id,int busId)
        {
            return DS.DataSource.lineTripsList.Find(p => p.Id == id&& p.LineId == busId && p.InService == true);
        }

        public IEnumerable<LineTrip> GetLineTripsBy(Predicate<LineTrip> predicate)
        {
            return from linetrip in DS.DataSource.lineTripsList where predicate(linetrip) && linetrip.InService == true select linetrip;
        }

        public void UpdateLineTrip(int id,int lineId, Action<LineTrip> update)
        {
            LineTrip lineTripUpdate = DS.DataSource.lineTripsList.Find(p => p.Id == id && p.LineId == lineId);
            if (lineTripUpdate == null || lineTripUpdate.InService == false)
            {
                throw new NoLineTripExistsException(id, lineId, $"The line trip with lineId {id} and lineId {lineId} cannot be deleted since it does not exist in the system ");
            }
        }
        /// <summary>
        /// To delete line trip
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lineId"></param>
        public void DeleteLineTrip(int id,int lineId)
        {
            LineTrip lineTrip = DS.DataSource.lineTripsList.Find(p => p.Id == id && p.LineId == lineId&&p.InService==true);
            if (lineTrip == null)
            {
                throw new NoLineTripExistsException(id, lineId, $"The line trip with lineId {id} and lineId {lineId} cannot be deleted since it does not exist in the system ");
            }
            lineTrip.InService = false;
        }
        /// <summary>
        /// To delete line trips of line
        /// </summary>
        /// <param name="lineId"></param>
        public void DeleteLineTrips(int lineId)
        {
            List<LineTrip> lineTripsOfLine = DS.DataSource.lineTripsList;
            foreach(LineTrip lt in DS.DataSource.lineTripsList)
            {
                if (lt.LineId == lineId && lt.InService == true)
                {
                    lt.InService = false;
                }
            }
        }
        #endregion
    }
}




