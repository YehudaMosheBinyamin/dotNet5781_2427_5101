using System;
using System.Collections.Generic;
using System.Text;
using DalAPI;
using BlApi;
using BO;
using System.Linq;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;

namespace BL
{
    public sealed class BlImplementation : IBL
    {
        #region Singleton
        static readonly BlImplementation instance = new BlImplementation();
        static BlImplementation() { }
        BlImplementation() { stopwatch = new Stopwatch(); simulatorClock = new Clock(new TimeSpan(0, 0, 0)); }



        public static BlImplementation Instance { get { return instance; } }

        #endregion
        /**
        #region Bus
        DO.Bus busBoDoAdapter(BO.Bus busBo)
        {
            BO.Bus bus = busBo;
            IDL dl = DLFactory.GetDL();
            DO.Bus busDO = new DO.Bus();
            busDO.LicenseNum = bus.LicenseNum;
            busDO.Status = (DO.BusStatus)bus.Status;
            busDO.TotalTrip = bus.TotalTrip;
            busDO.FuelRemain = bus.FuelRemain;
            busDO.FromDate = bus.FromDate;
            return busDO;
        }
        Bus busDoBoAdapter(DO.Bus busDO)
        {
            IDL dl = DLFactory.GetDL();
            BO.Bus busBO = new BO.Bus();
            string licenseNum = busDO.LicenseNum;
            try
            {
                busDO = dl.GetBus(licenseNum);
            }
            catch (DO.NoBusFoundException ex)
            {
                throw new BO.NoBusFoundException("Bus doesn't exist in system", ex);
            }
            busBO.LicenseNum = busDO.LicenseNum;
            busBO.Status = (BusStatus)busDO.Status;
            busBO.TotalTrip = busDO.TotalTrip;
            busBO.FuelRemain = busDO.FuelRemain;
            busBO.FromDate = busDO.FromDate;
            return busBO;
        }
        public void AddBus(Bus bus)
        {
            DO.Bus doBus = busBoDoAdapter(bus);
            IDL dl = DLFactory.GetDL();
            try
            {
                dl.AddBus(doBus);
            }
            catch (DO.BusAlreadyExistsException exp)
            {
                throw new BO.BusAlreadyExistsException("The bus already exists in the system", exp);
            }
        }
        void RefuelBusAction(DO.Bus bus)
        {
            bus.FuelRemain = 1200;
            bus.Status = DO.BusStatus.Ready;
        }
        void TreatBusAction(DO.Bus bus)
        {
            bus.FuelRemain = 1200;
            bus.LastTreated = DateTime.Now;
            bus.Status = DO.BusStatus.Ready;
            bus.KmSinceTreated = 0;
        }
        void RefuelBus(string licenseNumber)
        {
            IDL dl = DLFactory.GetDL();
            DO.Bus bus;
            try
            {
                bus = dl.GetBus(licenseNumber);
            }
            catch (DO.NoBusFoundException ex)
            {
                throw new BO.NoBusFoundException("The bus isn't on the system so cannot be refueled", ex);
            }
            if (bus.IsDangerous)
            {
                throw new BO.BusIsDangerousException(licenseNumber, $"The bus with license number:{licenseNumber}is dangerous and must be treated before refueling");

            }
            else
            { Action<DO.Bus> busAction = RefuelBusAction;
                dl.UpdateBus(licenseNumber, busAction);
            }
        }

        void TreatBus(string licenseNumber)
        {
            IDL dl = DLFactory.GetDL();
            DO.Bus bus;
            try
            {
                bus = dl.GetBus(licenseNumber);
            }
            catch (DO.NoBusFoundException ex)
            {
                throw new BO.NoBusFoundException("The bus isn't on the system so cannot be refueled", ex);
            }
            if (bus.IsDangerous)
            {
                throw new BO.BusIsDangerousException(licenseNumber, $"The bus with license number:{licenseNumber}is dangerous and must be treated before refueling");
            }
            else
            {
                Action<DO.Bus> busAction = TreatBusAction;
                dl.UpdateBus(licenseNumber, busAction);
            }
        }
        public void DeleteBus(string licenseNumber)
        {
            IDL dl = DLFactory.GetDL();
            try
            {
                dl.DeleteBus(licenseNumber);
            }
            catch (DO.NoBusFoundException ex)
            {
                throw new BO.NoBusFoundException("Bus doesn't exist in system so cannot be deleted", ex);
            }
        }
        #endregion**/
        #region Station
        public DO.Station StationBoDoAdapter(BO.Station boStation)
        {
            BO.Station bStation = boStation;
            IDL dl = DLFactory.GetDL();
            DO.Station doStation = new DO.Station();
            doStation.Code = dl.GetNewStationCode();
            doStation.Latitude = bStation.Latitude;
            doStation.Longtitude = bStation.Longtitude;
            /**foreach(LineStation ls in boStation.LineStationsOfStation)
            {
                if (ls.NextStation == -1)
                {
                    ls.NextStation = doStation.Code;
                }
                if (ls.PrevStation == -1)
                {
                    ls.PrevStation = doStation.Code;
                }
                if (ls.Station == -1)
                {
                    ls.Station=doStation.Code;
                }
                dl.AddLineStation(LineStationBoDoAdapter(ls));
                Line line = GetLine(ls.LineId);
                float distance = GetRandomDistance();
                DO.AdjacentStations newDoAdjStat = new DO.AdjacentStations()
                {
                    Distance = distance,
                    Time = GetMinutesOfTravel(distance),
                    InService = true,
                    Station1 = line.stationsInLine.ElementAt(ls.LineStationIndex - 1).Station,
                    Station2 = ls.Station

                };
                distance = GetRandomDistance();
                DO.AdjacentStations newLastAdjStat = new DO.AdjacentStations()
                {
                    Distance = distance,
                    Time = GetMinutesOfTravel(distance),
                    InService = true,
                    Station1 = line.stationsInLine.ElementAt(ls.LineStationIndex).Station,
                    Station2 = ls.Station
                };**/
            DO.AdjacentStations newAdjStat = new DO.AdjacentStations()
            {
                Distance = 0,
                Time = new TimeSpan(0, 0, 0),
                InService = true,
                Station1 = doStation.Code,
                Station2 = doStation.Code
            };
            dl.AddAdjacentStations(newAdjStat);
            //dl.AddAdjacentStations(newDoAdjStat);
            //dl.AddAdjacentStations(newLastAdjStat);
            //}
            doStation.InService = true;
            doStation.Name = bStation.Name;
            return doStation;

        }
        /// <summary>
        /// Convert A DO Station instance to a BO station instance
        /// </summary>
        /// <param name="doStation"></param>
        /// <returns></returns>
        public BO.Station StationDoBoAdapter(DO.Station doStation)
        {
            IDL dl = DLFactory.GetDL();

            BO.Station boStation = new BO.Station();
            DO.Station dStation = doStation;
            boStation.Code = doStation.Code;
            boStation.Latitude = doStation.Latitude;
            boStation.Longtitude = doStation.Longtitude;
            boStation.Name = doStation.Name;
            boStation.LineStationsOfStation = from linestation in dl.GetAllLineStations() where linestation.Station == boStation.Code select LineStationDoBoAdapter(linestation);
            return boStation;

        }
        public void AddStation(BO.Station station)
        {
            IDL dl = DLFactory.GetDL();
            try
            {
                dl.AddStation(StationBoDoAdapter(station));
            }
            catch (DO.StationAlreadyExistsException ex)
            {
                throw new BO.StationAlreadyExistsException("The station is already in the system and cannot be addded twice", ex);
            }
        }
        public Station GetStation(int code)
        {
            IDL dl = DLFactory.GetDL();
            DO.Station station;
            try
            {
                station = dl.GetStation(code);
            }
            catch (DO.NoStationFoundException ex)
            {
                throw new BO.NoStationFoundException("The station doesn't exist on the system", ex);
            }
            BO.Station boStation = new Station();
            boStation = StationDoBoAdapter(station);
            return boStation;
        }
        /// <summary>
        /// To get all stations in line
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetAllStations()
        {
            IDL dl = DLFactory.GetDL();
            return from station in dl.GetAllStations() select StationDoBoAdapter(station);

        }
        /**
        /// <summary>
        /// To remove a line from lines that go by station
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="stationCode"></param>
        public void DeleteLineFromStation(int lineId, int stationCode)
        {
            IDL dl = DLFactory.GetDL();
            BO.Line boLine= LineDoBoAdapter(dl.GetLine(lineId));
            List<int> indexesWhereStation = (from ls in boLine.stationsInLine where ls.Station == stationCode select ls.LineStationIndex).ToList();
            int countStations = 0;
           foreach(LineStation ls in boLine.stationsInLine)
            {
                countStations++;
            }
            List<LineStation> 
            int countStation = 0;
            int amountOfStationInLine = indexesWhereStation.Count;//amount of times station appears in line
            //for each time the station appears in the line
            int whereToChange;
            while (countStation < amountOfStationInLine)
            {
                whereToChange = indexesWhereStation.ElementAt(0);
                //if is not first or last station there's a need to change previous station's distance and time to be of next station
                if (whereToChange > 0 && boLine.stationsInLine.ElementAt(whereToChange).LineStationIndex != countStations)
                {
                    float distanceFromPrevious = bl.GetRandomDistance();
                    //update previous and next lineStations
                    LineStation previousLineStation = new LineStation()
                    {
                        LineStationIndex = whereToChange - 1,
                        NextStation = boLine.stationsInLine.ElementAt(whereToChange + 1).Station,
                        PrevStation = boLine.stationsInLine.ElementAt(whereToChange - 2).Station,
                        DistanceFromPreviousStation = boLine.stationsInLine.ElementAt(whereToChange - 1).DistanceFromPreviousStation,
                        TimeFromPreviousStation = boLine.stationsInLine.ElementAt(whereToChange - 1).TimeFromPreviousStation,
                        InService = true,
                        Station = boLine.stationsInLine.ElementAt(whereToChange - 1).Station,
                        LineId = boLine.Id,
                        LastStationName = boLine.LastStationName,
                        Name = boLine.stationsInLine.ElementAt(whereToChange - 1).Name
                    };
                    DeleteLineStation(boLine.Id, whereToChange);
                    UpdateLineStation(lineId, whereToChange - 1, newLineStation);
                    LineStation nextLineStation = new LineStation()
                    {
                        LineStationIndex = whereToChange + 1,
                        NextStation = boLine.stationsInLine.ElementAt(whereToChange + 2).Station,
                        PrevStation = previousLineStation.Station,
                        DistanceFromPreviousStation = boLine.stationsInLine.ElementAt(whereToChange + 1).DistanceFromPreviousStation + boLine.stationsInLine.ElementAt(whereToChange).DistanceFromPreviousStation,//distance is distance between this station to the one getting deleted plus to the one before that.Same with time.
                        TimeFromPreviousStation = boLine.stationsInLine.ElementAt(whereToChange + 1).TimeFromPreviousStation + boLine.stationsInLine.ElementAt(whereToChange).TimeFromPreviousStation,
                        InService = true,
                        Station = boLine.stationsInLine.ElementAt(whereToChange + 1).Station,
                        LineId = boLine.Id,
                        LastStationName = boLine.LastStationName,
                        Name = boLine.stationsInLine.ElementAt(whereToChange + 1).Name
                    };
                    UpdateLineStation(lineId, whereToChange + 1, nextLineStation);

                }
                else if (whereToChange == 0)
                {//in this case,the second line station will become the first line station
                    LineStation newFirstLineStation = new LineStation()
                    {
                        LineStationIndex = whereToChange,
                        NextStation = boLine.stationsInLine.ElementAt(whereToChange + 1).Station,
                        PrevStation = boLine.stationsInLine.ElementAt(whereToChange - 1).Station,
                        DistanceFromPreviousStation = 0f,
                        TimeFromPreviousStation = new TimeSpan(0, 0, 0),
                        InService = true,
                        Station = boLine.stationsInLine.ElementAt(whereToChange + 1).Station,
                        LineId = boLine.Id,
                        LastStationName = boLine.LastStationName,
                        Name = boLine.stationsInLine.ElementAt(whereToChange + 1).Name
                    };

                    DeleteLineStation(boLine.Id, whereToChange);
                    UpdateLineStation(lineId,whereToChange+1,newFirstLineStation)

                }
                else if (whereToChange == countStations)//we're deleting the last station
                {
                 
                    LineStation newSecondLastStation = new LineStation()
                    {
                        LineStationIndex = whereToChange-2,
                        NextStation = boLine.stationsInLine.ElementAt(whereToChange - 1).Station,
                        PrevStation = boLine.stationsInLine.ElementAt(whereToChange - 3).Station,
                        DistanceFromPreviousStation = boLine.stationsInLine.ElementAt(whereToChange-2).DistanceFromPreviousStation,
                        TimeFromPreviousStation = boLine.stationsInLine.ElementAt(whereToChange - 2).TimeFromPreviousStation,
                        InService = true,
                        Station = boLine.stationsInLine.ElementAt(whereToChange - 2).Station,
                        LineId = boLine.Id,
                        LastStationName = boLine.LastStationName,
                        Name = boLine.stationsInLine.ElementAt(whereToChange - 2).Name
                    };
                    

                }
                indexesWhereStation.RemoveAt(0);
            }
           


        }
        /** public void DeleteStation(int code)
         {
             IDL dl = DLFactory.GetDL();
             IEnumerable<DO.LineStation> lineStations=dl.GetAllLineStations();
             DO.LineStation lineStation = (from ls in lineStations let lineCodeStation = code where ls.Station == code select ls).First();
             if (lineStation != null)
             {
                 throw new BO.CannotDeleteStationException(code, $"This station cannot be deleted before the line station with code{code} is deleted first");
             }
             else
             {
                 dl.DeleteStation(code);
             }  
         }**/
        #endregion
        #region Line
        BO.Line LineDoBoAdapter(DO.Line doLine)
        {
            BO.Line boLine = new BO.Line();
            boLine.Code = doLine.Code;
            boLine.Id = doLine.Id;
            boLine.Area = (Areas)doLine.Area;
            IDL dl = DLFactory.GetDL();
            IEnumerable<BO.LineStation> lineStations = from lineStation in GetAllLineStationsByLine(doLine.Id)
                                                       where lineStation.InService == true
                                                       orderby lineStation.LineStationIndex
                                                       select lineStation;
            boLine.stationsInLine = lineStations;
            boLine.InService = doLine.InService;
            boLine.LastStationName = dl.GetStation(boLine.stationsInLine.Last().Station).Name;
            boLine.lineExits = GetAllLineTripsInLine(doLine.Id);
            return boLine;
        }
        DO.Line LineBoDoAdapter(BO.Line boLine)
        {
            DO.Line doLine = new DO.Line();
            doLine.Id = boLine.Id;
            doLine.Code = boLine.Code;
            doLine.FirstStation = LineStationBoDoAdapter((from station in boLine.stationsInLine select station).First()).Station;
            doLine.LastStation = LineStationBoDoAdapter((from station in boLine.stationsInLine select station).Last()).Station;
            doLine.Area = (DO.Areas)boLine.Area;
            doLine.InService = true;
            return doLine;
        }
        public BO.Line GetLine(int lineId)
        {
            IDL dl = DLFactory.GetDL();
            BO.Line line = LineDoBoAdapter(dl.GetLine(lineId));
            return line;
        }
        /// <summary>
        /// For addition of line to system
        /// </summary>
        /// <param name="line">Line of type BO from PL </param>
        public void AddLine(Line line)
        {
            IDL dl = DLFactory.GetDL();
            line.Id = dl.GetNewLineId();
            try
            {
                dl.AddLine(LineBoDoAdapter(line));
            }
            catch (DO.LineAlreadyExistsException ex)
            {
                throw new BO.LineAlreadyExistsException("Line already exists", ex);
            }
            AddAllLineStations(line);
            //to make all elements of line trip with proper,new ids.
            /**List<BO.LineTrip> lineTripsOfLine = (from lt in line.lineExits 
                                                       select new BO.LineTrip 
                                                       {
                                                           LineId = line.Id,
                                                           InService = true,
                                                           StartAt = lt.StartAt,
                                                           Id =dl.GetNewLineTripId()
                                                       }).ToList();**/
            //line.lineExits = lineTripsOfLine;
            AddAllLineTrips(line);
            List<DO.AdjacentStations> adjacentStations = (from lineStation1 in line.stationsInLine
                                                          from lineStation2 in line.stationsInLine
                                                          where lineStation1.NextStation == lineStation2.Station && dl.AdjacentStationsExists(lineStation1.Station, lineStation2.Station) == false
                                                          let rDistance = Functions.randomDistance()
                                                          select new DO.AdjacentStations
                                                          {   //LineId= lineStation1.LineId,
                                                              Station1 = lineStation1.Station,
                                                              Station2 = lineStation2.Station,
                                                              Distance = rDistance,
                                                              Time = Functions.MinutesOfTravel(rDistance),
                                                              InService = true
                                                          }).ToList();
            float distance1 = Functions.randomDistance();
            float distance2 = Functions.randomDistance();

            //DO.AdjacentStations toFirst = new DO.AdjacentStations() { Station1 =00000,Station2=line.stationsInLine.First().Station, Distance = 0f, Time = new TimeSpan(0,0,0), InService = true };
            //adjacentStations.Add(toFirst);
            foreach (DO.AdjacentStations adj in adjacentStations)
            {
                bool lineStationsExists = AdjacentStationsExists(adj.Station1, adj.Station2);
                if (lineStationsExists == false)
                {
                    try
                    {
                        dl.AddAdjacentStations(adj);
                    }
                    catch (DO.AdjacentStationsAlreadyExistsException ex)
                    {
                        throw new BO.AdjacentStationsAlreadyExistsException("The line station already exists and cannot be added", ex);
                    }
                }
            }
        }

        /// <summary>
        /// For update of line-deletion of old line and receiving new line
        /// </summary>
        /// <param name="oldLineId"></param>
        /// <param name="newLine"></param>
        public void UpdateLine(int oldLineId, Line newLine)
        {
            DeleteLine(oldLineId);
            AddLine(newLine);
        }

        /// <summary>
        /// For deletion of a line
        /// </summary>
        /// <param name="lineId">Id of line</param>
        public void DeleteLine(int lineId)
        {
            IDL dl = DLFactory.GetDL();

            try
            {
                DeleteLineStations(lineId);
                dl.DeleteLineTrips(lineId);
                dl.DeleteLine(lineId);

            }
            catch (DO.NoLineFoundException ex)
            {
                throw new BO.NoLineFoundException("It's impossible to delete line because it doesn't exist on the system", ex);
            }
        }

        public IEnumerable<BO.Line> GetAllLines()
        {
            IDL dl = DLFactory.GetDL();
            return from line in dl.GetAllLines() orderby line.Code select LineDoBoAdapter(line);
        }
        /// <summary>
        /// Get all lines that go by station,for Station Window
        /// </summary>
        /// <param name="stationCode">code of station</param>
        /// <returns></returns>
        public IEnumerable<BO.Line> GetAllLinesByStation(int stationCode)
        {
            IDL dl = DLFactory.GetDL();
            IEnumerable<LineStation> allLineStationsOfStation = from ls in dl.GetAllLineStations() where ls.Station == stationCode select LineStationDoBoAdapter(ls);
            IEnumerable<Line> allLines =
            from linestation in allLineStationsOfStation
                //let id = linestation.LineId
            from line in dl.GetAllLines()
            where line.Id == linestation.LineId
            orderby line.Code
            select LineDoBoAdapter(line);
            return allLines;
        }
        /// <summary>
        /// To add new adjacent stations into the system
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <param name="distance"></param>
        /// <param name="waitingTime"></param>
        public void AddAdjacentStations(int station1, int station2, float distance, TimeSpan waitingTime)
        {
            IDL dl = DLFactory.GetDL();
            BO.AdjacentStations newAdjStat = new BO.AdjacentStations()
            {
                Station1 = station1,
                Station2 = station2,
                Distance = distance,
                InService = true,
                Time = waitingTime
            };
            dl.AddAdjacentStations(AdjacentStationsBoDoAdapter(newAdjStat));
        }
        /// <summary>
        /// To check if adjacent stations already in system so that the distance and time between them is already known
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <returns></returns>
        public bool AdjacentStationsExists(int station1, int station2)
        {
            IDL dl = DLFactory.GetDL();
            return dl.AdjacentStationsExists(station1, station2);
        }/// <summary>
         /// For deletion of Adjacent Stations(in case of updated time and/or distance
         /// </summary>
         /// <param name="station1"></param>
         /// <param name="station2"></param>
        public void DeleteAdjacentStations(int station1, int station2)
        {
            IDL dl = DLFactory.GetDL();
            dl.DeleteAdjacentStations(station1, station2);
        }
        /// <summary>
        /// Updating time and distance between two stations for all their appearances in the same order in all lines.
        /// </summary>
        /// <param name="station1"></param>
        /// <param name="station2"></param>
        /// <param name="newDistance"> </param>
        /// <param name="newTime"></param>
        public void UpdateAdjacentStations(int station1, int station2, float newDistance, TimeSpan newTime)
        {
            IDL dl = DLFactory.GetDL();
            DeleteAdjacentStations(station1, station2);
            AddAdjacentStations(station1, station2, newDistance, newTime);
        }
        /**
        /// <summary>
        /// function for adding new linestation to line at chosen index
        /// </summary>
        /// <param name="line"></param>
        /// <param name="indexForInsertion"></param>
        /// <param name="station"></param>
        public void AddStationToLine(Line line,int indexForInsertion,Station station)
        {
            IDL dl = DLFactory.GetDL();
            float distancePrevious= Functions.randomDistance();
            LineStation prevStation = (from linestation in line.stationsInLine where linestation.LineStationIndex == indexForInsertion - 1 select linestation).FirstOrDefault();
            LineStation nextStation = (from linestation in line.stationsInLine where linestation.LineStationIndex == indexForInsertion + 1 select linestation).FirstOrDefault();
            LineStation ls = new LineStation
            {   Station=station.Code,
                LineId = prevStation.LineId,
                LineStationIndex = indexForInsertion,
                PrevStation = prevStation.Station,
                NextStation = nextStation.Station,
                //distanceFromPrevious = Functions.randomDistance(),
                //TimeFromPreviousStation = Functions.MinutesOfTravel(distancePrevious),
                InService = true
            };
            List<LineStation> lineStationsToUpdate = (from linestation in line.stationsInLine
                                                      where linestation.LineStationIndex >= indexForInsertion
                                                      select linestation).ToList(); 
            foreach(LineStation linestat in lineStationsToUpdate)
            {
                UpdateAfterLineStation(linestat.LineId,linestat.Station,ls.Station);
            }
            UpdatePrevLineStation(prevStation.LineId,prevStation.Station,ls.Station);
            UpdateNextLineStation(nextStation.LineId, nextStation.Station, ls.Station);
            /**List < LineStation > lineStationsAfterInserted = (from linestation in line.stationsInLine
                                                              where linestation.LineStationIndex >= indexForInsertion
                                                              select new LineStation
                                                              {
                                                                  Station = linestation.Station,
                                                                  LineStationIndex = linestation.LineStationIndex + 1,
                                                                  PrevStation = linestation.PrevStation,
                                                                  NextStation = linestation.NextStation,
                                                                  TimeFromPreviousStation = linestation.TimeFromPreviousStation,
                                                                  distanceFromPrevious = linestation.distanceFromPrevious,
                                                                  InService = linestation.InService

                                                              }).ToList();**/
        /** LineStation updatedPrevStation = new LineStation();
         updatedPrevStation = prevStation;
         updatedPrevStation.NextStation = line.Code;
         LineStation updatedNextStation = new LineStation();
         updatedNextStation.PrevStation = line.Code;
         

     }**/
        public DO.LineStation LineStationBoDoAdapter(BO.LineStation boLineStation)
        {
            DO.LineStation doLineStation = new DO.LineStation();
            doLineStation.LineId = boLineStation.LineId;
            doLineStation.Station = boLineStation.Station;
            doLineStation.LineStationIndex = boLineStation.LineStationIndex;
            doLineStation.PrevStation = boLineStation.PrevStation;
            doLineStation.NextStation = boLineStation.NextStation;
            doLineStation.InService = boLineStation.InService;
            return doLineStation;
        }
        public void DeleteAllLineStationsInLine(Line line)
        {
            IDL dl = DLFactory.GetDL();
            foreach (LineStation ls in line.stationsInLine)
            {
                try
                {
                    dl.DeleteLineStation(line.Id, line.Code);
                }
                catch (DO.NoLineStationFoundException ex)
                {
                    throw new BO.LineStationNotFoundException("No line station exists so it cannot be deleted", ex);
                }
            }
        }
        void AddAllLineStations(BO.Line line)
        {

            IDL dl = DLFactory.GetDL();
            foreach (LineStation ls in line.stationsInLine)
            {
                ls.LineId = line.Id;
                try
                {

                    dl.AddLineStation(LineStationBoDoAdapter(ls));
                }
                catch (DO.LineStationAlreadyExistsException ex)
                {
                    throw new BO.LineStationNotFoundException(" line station exists so it cannot be added again", ex);
                }
            }

        }
        /**
        
        void ChangeOneAfter(DO.LineStation ls,int stationBefore)
        {
            ls.PrevStation = stationBefore;
        }
        /// <summary>
        /// promotes the index of a lineStation after the one added 
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="promotion"></param>
        void ChangeNextLineStation(DO.LineStation ls,int promotion= 1)
        {
            ls.LineStationIndex += promotion;
           
        }
        /// <summary>
        /// Changes the fields of the station immediately preceding the newly added line station in line route
        /// </summary>
        /// <param name="newStationCode">receives code of line station that is added right after this station.This code will be the previous stations NextStation</param>
        /// <param name="ls">line station to be edited</param>
        void ChangePreviousLineStation(DO.LineStation ls,int newStationCode)
        {
            ls.NextStation = newStationCode;
        }
        void UpdateAfterLineStation(int idLine, int stationCode,int newStationCode)
        {
            IDL dl = DLFactory.GetDL();
            Action<DO.LineStation,int> lineStationNextUpdate = ChangeNextLineStation;
            try
            {
                dl.UpdateLineStation(idLine, stationCode,newStationCode, lineStationNextUpdate);
            }
            catch(DO.NoLineStationFoundException ex)
            {
                throw new BO.LineStationNotFoundException("No line station available for update since it doesn't exist on the system", ex);
            }
        }

        void UpdateNextLineStation(int idLine, int stationCode,int newStationCode)
        {
            IDL dl = DLFactory.GetDL();
            Action<DO.LineStation,int> lineStationNextUpdate = ChangeOneAfter;
            try
            {
                dl.UpdateLineStation(idLine, stationCode,newStationCode, lineStationNextUpdate);
            }
            catch (DO.NoLineStationFoundException ex)
            {
                throw new BO.LineStationNotFoundException("No line station available for update since it doesn't exist on the system", ex);
            }
        }
        void UpdatePrevLineStation(int idLine, int stationCode,int newStationCode)
        {
            IDL dl = DLFactory.GetDL();
            Action<DO.LineStation,int> lineStationPreviousUpdate = ChangePreviousLineStation;
            try
            {
                dl.UpdateLineStation(idLine, stationCode,newStationCode, lineStationPreviousUpdate);
            }
            catch (DO.NoLineStationFoundException ex)
            {
                throw new BO.LineStationNotFoundException("No line station available for update since it doesn't exist on the system", ex);
            }
        }
      
**/

        #endregion
        #region LineStation
        bool InLine(DO.LineStation ls, int lineId)
        {
            if (ls.LineId == lineId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public BO.LineStation LineStationDoBoAdapter(DO.LineStation doLineStation)
        {
            IDL dl = DLFactory.GetDL();
            BO.LineStation boLineStation = new LineStation();
            boLineStation.LineId = doLineStation.LineId;
            boLineStation.Station = doLineStation.Station;
            boLineStation.LineStationIndex = doLineStation.LineStationIndex;
            boLineStation.PrevStation = doLineStation.PrevStation;
            boLineStation.NextStation = doLineStation.NextStation;
            boLineStation.InService = doLineStation.InService;
            boLineStation.TimeFromPreviousStation = GetAdjacentStations(doLineStation.PrevStation, doLineStation.Station).Time;//before was dl. 
            boLineStation.DistanceFromPreviousStation = GetAdjacentStations(doLineStation.PrevStation, doLineStation.Station).Distance;
            //before was dl.
            //int lastStationCode = (from line in dl.GetAllLines()
            //   where line.Id == boLineStation.LineId
            // select line).FirstOrDefault().LastStation;

            boLineStation.Name = GetStation(doLineStation.Station).Name;//was dl.
                                                                        // boLineStation.LastStationName=
                                                                        //    dl.GetStation(lastStationCode).Name;

            return boLineStation;
        }
        public IEnumerable<LineStation> GetAllLineStationsByLine(int lineId)
        {
            string ls = $"{lineId}";
            IDL dl = DLFactory.GetDL();
            IEnumerable<BO.LineStation> lineStationsInLine = (from lineStation in dl.GetAllLineStationsByLine(lineId)
                                                              where lineStation.LineStationIndex >= 0
                                                              orderby lineStation.LineStationIndex
                                                              select LineStationDoBoAdapter(lineStation)).Distinct();
            return lineStationsInLine;
        }
        public void DeleteLineStations(int lineId)
        {
            IDL dl = DLFactory.GetDL();
            dl.DeleteLineStations(lineId);

        }

        #endregion
        #region AdjacentStations
        BO.AdjacentStations AdjacentStationsDoBoAdapter(DO.AdjacentStations doAdjacentStations)
        {
            BO.AdjacentStations boAdjacentStations = new BO.AdjacentStations();
            boAdjacentStations.Distance = doAdjacentStations.Distance;
            boAdjacentStations.InService = doAdjacentStations.InService;
            boAdjacentStations.Station1 = doAdjacentStations.Station1;
            boAdjacentStations.Station2 = doAdjacentStations.Station2;
            boAdjacentStations.Time = doAdjacentStations.Time;
            return boAdjacentStations;
        }
        DO.AdjacentStations AdjacentStationsBoDoAdapter(BO.AdjacentStations boAdjacentStations)
        {
            DO.AdjacentStations doAdjacentStations = new DO.AdjacentStations();
            doAdjacentStations.Distance = boAdjacentStations.Distance;
            doAdjacentStations.InService = true;
            doAdjacentStations.Station1 = boAdjacentStations.Station1;
            doAdjacentStations.Station2 = boAdjacentStations.Station2;
            doAdjacentStations.Time = boAdjacentStations.Time;
            return doAdjacentStations;
        }
        /// <summary>
        /// To get adjacent stations(for distance and time between two stations )
        /// </summary>
        /// <param name="codeOne"></param>
        /// <param name="codeTwo"></param>
        /// <returns></returns>
        public BO.AdjacentStations GetAdjacentStations(int codeOne, int codeTwo)
        {
            IDL dl = DLFactory.GetDL();
            try
            {
                return AdjacentStationsDoBoAdapter(dl.GetAdjacentStations(codeOne, codeTwo));
            }
            catch (DO.AdjacentStationsDoesntExistException ex)
            {
                throw new BO.AdjacentStationsDoesntExistException("Adjacent stations nonexistant", ex);
            }
        }
        public IEnumerable<BO.AdjacentStations> GetAllAdjacentStations()
        {
            IDL dl = DLFactory.GetDL();
            return (from adjacentStation in dl.GetAllAdjacentStations() orderby adjacentStation.Station1, adjacentStation.Station2 select AdjacentStationsDoBoAdapter(adjacentStation));
        }
        /**
        /// <summary>
        /// To get all adjacent stations in line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public IEnumerable<BO.AdjacentStations> GetAllAdjacentsStationsInLine(BO.Line line)
        {
            IDL dl = DLFactory.GetDL();
            IEnumerable<AdjacentStations> adjInLine = (from adjStat in dl.GetAllAdjacentStations()
                                                       from ls in line.stationsInLine
                                                       where adjStat.LineId==line.Id&&ls.Station==adjStat.Station1&&(adjStat.InService==true||line.stationsInLine.ToList().Count==ls.LineStationIndex)
                                                       orderby adjStat.Station1
                                                       select AdjacentStationsDoBoAdapter(adjStat))
                                                      ;
            
            return adjInLine;

        }**/
        #endregion
        #region User
        BO.User UserDoBoAdapter(DO.User doUser)
        {
            BO.User boUser = new BO.User();
            boUser.UserName = doUser.UserName;
            boUser.Password = doUser.Password;
            boUser.Admin = doUser.Admin;
            boUser.InService = doUser.InService;
            return boUser;
        }
        /// <summary>
        /// Checks user details to see 1)if user exists and  2)if password entered is correct 3)if field requires admin permissions this is checked as well
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="needsAdmin">Whether user needs admin permissions</param>
        /// <returns></returns>
        public bool CheckUserPassword(User user, bool needsAdmin = false)
        {
            IDL dl = DLFactory.GetDL();
            BO.User boUser = new BO.User();
            try
            {
                boUser = UserDoBoAdapter(dl.GetUser(user.UserName));
                if (boUser.Password == user.Password)
                {
                    return true;
                }
                if (needsAdmin == true)
                {
                    if (boUser.Admin == needsAdmin)
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new BO.NoUserFoundException("There is no user with this username", ex);
            }
            return false;

        }
        #endregion
        #region LineTrip
        public BO.LineTrip LineTripDoBoAdapter(DO.LineTrip doLineTrip)
        {
            BO.LineTrip boLineTrip = new BO.LineTrip();
            boLineTrip.StartAt = doLineTrip.StartAt;
            boLineTrip.Id = doLineTrip.Id;
            boLineTrip.LineId = doLineTrip.LineId;
            boLineTrip.InService = doLineTrip.InService;
            return boLineTrip;
        }


        public DO.LineTrip LineTripBoDoAdapter(BO.LineTrip boLineTrip)
        {
            DO.LineTrip doLineTrip = new DO.LineTrip();
            doLineTrip.Id = boLineTrip.Id;
            doLineTrip.InService = boLineTrip.InService;
            doLineTrip.LineId = boLineTrip.LineId;
            doLineTrip.StartAt = boLineTrip.StartAt;
            return doLineTrip;
        }


        /// <summary>
        /// To get all line trips for line
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public IEnumerable<LineTrip> GetAllLineTripsInLine(int lineId)
        {
            IDL dl = DLFactory.GetDL();
            IEnumerable<LineTrip> lineExitsInLine = from lineTrip in dl.GetAllLineTrips() where lineTrip.LineId == lineId && lineTrip.InService == true select LineTripDoBoAdapter(lineTrip);
            return lineExitsInLine;
        }


        /// <summary>
        /// To add line trips of line to database
        /// </summary>
        /// <param name="line"></param>
        public void AddAllLineTrips(BO.Line line)
        {
            IDL dl = DLFactory.GetDL();
            foreach (LineTrip lt in line.lineExits)
            {
                lt.LineId = line.Id;
                lt.Id = dl.GetNewLineTripId();
                dl.AddLineTrip(LineTripBoDoAdapter(lt));
            }
        }


        #endregion
        #region Functions
        /// <summary>
        /// Function to get random distance
        /// </summary>
        /// <returns></returns>
        public float GetRandomDistance()
        {
            return Functions.randomDistance();
        }
        /// <summary>
        /// Function to return new station code from DL
        /// </summary>
        /// <returns></returns>
        public int getNewCode()
        {
            IDL dl = DLFactory.GetDL();
            return dl.GetNewStationCode();
        }
        /// <summary>
        /// Function to return amount of minutes it takes to drive a bus line at a random speed between 30 to 100 km/h
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public TimeSpan GetMinutesOfTravel(float distance)
        {
            return Functions.MinutesOfTravel(distance);
        }
        #endregion
        #region Simulators
        Clock simulatorClock;
        Stopwatch stopwatch;
        /// <summary>
        /// Function to change time of the simulator clock
        /// </summary>
        /// <param name="simulatorStartTime"></param>
        /// <param name="speed"></param>
        /// <param name="action"></param>
        public void StartSimulator(TimeSpan simulatorStartTime, int speed, Action<TimeSpan> action)
        {
            simulatorClock = new Clock(simulatorStartTime + new TimeSpan(stopwatch.ElapsedTicks * speed));
            simulatorClock.Rate = speed;
            stopwatch.Restart();
            while (simulatorClock.Cancel == false)
            {
                simulatorClock = new Clock(simulatorStartTime + new TimeSpan(stopwatch.ElapsedTicks * speed));
                simulatorClock.Rate = speed;
                simulatorClock.TimeChangeEvent += action;
                simulatorClock.DoTimeChangeEvent();
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Function to stop the simulator clock
        /// </summary>
        public void StopSimulator()
        {
            simulatorClock.Cancel = true;
        }

        /// <summary>
        /// A function to dispatch lines on routes
        /// </summary>
        /// <param name="station">A code for a station</param>
        /// <param name="updateBus">A PL Action for updating display of approaching buses</param>
        public void SetStationPanel(int station, Action<LineTiming, int> updateBus)
        {
            IDL dl = DLFactory.GetDL();
            IEnumerable<Line> lines = from line in dl.GetAllLines() select LineDoBoAdapter(line);
            Operator op = Operator.Instance;
            op.LineApproachingStationEvent += updateBus;
            //Select lines which go past station(any of their stations' codes is equal to current station)
            IEnumerable<Line> linesPastStation = from line in lines where line.stationsInLine.Select(l => l.Station).Any(p => p == station) select line;
            List<LineTrip> lineTripCollection = new List<LineTrip>();
            foreach (Line line in linesPastStation)
            {
                foreach (LineTrip lt in line.lineExits)
                {
                    lineTripCollection.Add(lt);
                }
            }
            //Sort line trips by order of exit time
            lineTripCollection = lineTripCollection.OrderBy(lt => lt.StartAt).ToList();
            List<bool> sentYet = new List<bool>();
            for (int i = 0; i < lineTripCollection.Count; ++i)
            {
                sentYet.Add(false);
            }
            //While simulator is running send lines
            while (simulatorClock.Cancel == false)
            {
                foreach (LineTrip lt in lineTripCollection)
                {

                    BackgroundWorker backgroundWorker = new BackgroundWorker();
                    backgroundWorker.DoWork += BackgroundWorker_DoWork;
                    backgroundWorker.WorkerSupportsCancellation = true;
                    //If it is now time to start line
                    if (lt.StartAt <= simulatorClock.Time)
                    {
                        //Get index of line trip about to be sent
                        int indexLineTrip = lineTripCollection.IndexOf(lt);

                        if (sentYet[indexLineTrip] == false)
                        {
                            backgroundWorker.RunWorkerAsync(new List<object> { lt, station });
                            sentYet[indexLineTrip] = true;
                            Thread.Sleep(1000);
                        }
                    }
                }
            }

        }
        /// <summary>
        /// To send new line trip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Operator op = Operator.Instance;
            LineTiming lineTiming = new LineTiming();
            List<object> args = e.Argument as List<object>;
            LineTrip lineTrip = (LineTrip)args.ElementAt(0);
            int station = (int)args.ElementAt(1);
            IDL dl = DLFactory.GetDL();
            Line line = LineDoBoAdapter(dl.GetLine(lineTrip.LineId));
            //TimeSpan beginTime = lineTrip.StartAt;//Est. begin time
            TimeSpan beginTime = simulatorClock.Time;
            lineTiming.LineId = line.Id;
            lineTiming.LineCode = line.Code;
            lineTiming.StartTime = beginTime;
            lineTiming.LastStationName = line.LastStationName;
            op.Station = station;
            List<TimeSpan> timeFromPrev = new List<TimeSpan>();//List of times of arrival from previous stations
            foreach (LineStation ls in line.stationsInLine)
            {
                timeFromPrev.Add(ls.TimeFromPreviousStation);
            }
            List<TimeSpan> waitingTimeStation = new List<TimeSpan>();//List of waiting time for each station from beginning of the line
            //Initialize wait times to be minimum 0 hours,minutes and seconds
            for (int i = 0; i < timeFromPrev.Count; ++i)
            {
                waitingTimeStation.Add(new TimeSpan(0, 0, 0));
            }
            int sIndex = 0;
            //Find all average waiting times of stations from beginning of the route by adding up all waiting times from the beginning of the route until the current station
            foreach (LineStation ls in line.stationsInLine)
            {
                waitingTimeStation.ElementAt(sIndex).Add(timeFromPrev.ElementAt(sIndex));
                for (int index = 0; index < sIndex; ++index)
                {
                    waitingTimeStation.ElementAt(sIndex).Add(timeFromPrev.ElementAt(index));
                }
                sIndex++;
            }

            //foreach (LineStation ls in line.stationsInLine)
            for(int stationIndex=0;stationIndex<line.stationsInLine.Count();++stationIndex)
            {
                
                LineStation ls = line.stationsInLine.ElementAt(stationIndex);
                //Sleep the amount of time in miliseconds the bus will take to get to current station from previous station
                //It is times 1000 to convert from seconds to milliseconds and times 1/rate to wait less time if the simulation clock is running at a different rate than 1
                int waitingTime = 1000 * ((int)Math.Ceiling((double)(Functions.DelayOrEarlyArrival(ls.TimeFromPreviousStation) * (1 / (double)simulatorClock.Rate))));
                if(line.stationsInLine.ElementAt(stationIndex).Station==60010)
                {
                    string pilpel="BLAP";
                }
                Thread.Sleep(waitingTime);
                if (simulatorClock.Cancel == false)
                {
                    waitingTimeStation[stationIndex] = TimeSpan.Zero;//Time to wait for station is zero
                    //If we have reached the station we're tracking
                    if (ls.Station == station)
                    {
                        op.UpdateLineArrive = lineTiming;
                        if (simulatorClock.Cancel == false)
                        {
                            lineTiming.EstWaitingTime = waitingTimeStation[stationIndex];
                            op.DoUpdateArrivalTime();
                        }
                        else
                        {
                            op.Station = -1;
                            op.DoUpdateArrivalTime();
                        }
                    }
                    //Calculate time for next stations
                    for (int statIndex = stationIndex + 1; statIndex < line.stationsInLine.Count(); ++statIndex)
                    {
                        //Time for current station is equal to time to previous station plus time from there to current
                        waitingTimeStation[statIndex] = waitingTimeStation[statIndex - 1] + timeFromPrev.ElementAt(statIndex);
                        if ((line.stationsInLine.ElementAt(statIndex).Station == station) && (simulatorClock.Cancel == false))
                        {
                            lineTiming.EstWaitingTime = waitingTimeStation[statIndex];
                            op.UpdateLineArrive = lineTiming;
                            op.DoUpdateArrivalTime();
                        }
                        else if ((line.stationsInLine.ElementAt(statIndex).Station == station) && (simulatorClock.Cancel == true))
                        {
                            op.UpdateLineArrive = lineTiming;
                            op.Station = -1;
                            op.DoUpdateArrivalTime();
                        }
                    }

                }
            }
        }
    }
    #endregion
}


