using System;
using System.Collections.Generic;
using System.Text;
using DalAPI;
using BlApi;
using BO;
using System.Linq;

namespace BL
{
    class BlImplementation : IBL
    {
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
        #endregion
        #region Station
        public DO.Station StationBoDoAdapter(BO.Station boStation)
        { BO.Station bStation = boStation;
            IDL dl = DLFactory.GetDL();
            DO.Station doStation = new DO.Station();
            doStation.Code = bStation.Code;
            doStation.Latitude = bStation.Latitude;
            doStation.Longtitude = bStation.Longtitude;
            doStation.Name = bStation.Name;
            return doStation;

        }
        public BO.Station StationDoBoAdapter(DO.Station doStation)
        {
            BO.Station boStation = new BO.Station();
            DO.Station dStation = doStation;
            boStation.Code = doStation.Code;
            boStation.Latitude = doStation.Latitude;
            boStation.Longtitude = doStation.Longtitude;
            boStation.Name = doStation.Name;
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

        public IEnumerable<Station> GetAllStations()
        {
            IDL dl = DLFactory.GetDL();
            IEnumerable<Station> allStations = from station in dl.GetAllStations() select StationDoBoAdapter(station);
            return allStations;
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
            IDL dl = DLFactory.GetDL();
            IEnumerable<BO.LineStation> lineStations = from lineStation in dl.GetAllLineStations() orderby lineStation.LineStationIndex where lineStation.Station == boLine.Id select LineStationDoBoAdapter(lineStation);
            IEnumerable<BO.LineTrip> lineTrips = from lineTrip in dl.GetAllLineTrips() where lineTrip.LineId == doLine.Id select LineTripDoBoAdapter(lineTrip);
            boLine.stationsInLine = lineStations;
            boLine.lineExits = lineTrips;
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
            return doLine;
        }
      public BO.Line GetLine(int lineId)
        {   IDL dl = DLFactory.GetDL();
            BO.Line line = LineDoBoAdapter(dl.GetLine(lineId));
            return line;
        }
      
       public void AddLine(Line line)
        {
            IDL dl = DLFactory.GetDL();
            try
            {
                dl.AddLine(LineBoDoAdapter(line));
            }
            catch (DO.LineAlreadyExistsException ex)
            {
                throw new BO.LineAlreadyExistsException("The line already exists and so cannot be added twice", ex);
            }
           
            IEnumerable<DO.AdjacentStations> adjacentStations = from lineStation1 in line.stationsInLine
                                                                from lineStation2 in line.stationsInLine
                                                                where lineStation1.NextStation == lineStation2.Station &&dl.AdjacentStationsExists(lineStation1.Station,lineStation2.Station)==false
                                                                let rDistance=Functions.randomDistance()
                                                                select new DO.AdjacentStations
                                                                {
                                                                    Station1 = lineStation1.Station,
                                                                    Station2 = lineStation2.Station,
                                                                    Distance = rDistance,
                                                                    Time = Functions.MinutesOfTravel(rDistance),
                                                                    InService = true
                                                                };
            foreach (DO.AdjacentStations adj in adjacentStations)
            {// bool lineStationsExists = dl.InLineStations(adj.Station1, adj.Station2);-to do
                //if (lineStationsExists == false)
               // {
                    try
                    {
                        dl.AddAdjacentStations(adj);
                    }
                    catch (DO.AdjacentStationsAlreadyExistsException ex)
                    {
                        throw new BO.AdjacentStationsAlreadyExistsException("The line station already exists and cannot be added", ex);
                    }
                //}
            }
        }
        public IEnumerable<BO.Line> GetAllLines()
        {
            IDL dl = DLFactory.GetDL();
            return from line in dl.GetAllLines() select LineDoBoAdapter(line);
        }
        public IEnumerable<Line> GetAllLinesByStation(int stationCode)
        {
            IDL dl = DLFactory.GetDL();
            return from line in dl.GetAllLines() where line.Code == stationCode select LineDoBoAdapter(line);
        }
        bool AdjacentStationsExists(int station1,int station2)
        {
            IDL dl = DLFactory.GetDL();
            return dl.AdjacentStationsExists(station1, station2);
        }
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
            { Station=station.Code,
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
            updatedNextStation.PrevStation = line.Code;**/
            

        }
        DO.LineStation LineStationBoDoAdapter(BO.LineStation boLineStation)
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
            foreach(LineStation ls in line.stationsInLine)
            {
                try
                {
                    dl.DeleteLineStation(line.Id, line.Code);
                }
                catch(DO.NoLineStationFoundException ex)
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
                try
                {
                    dl.AddLineStation(LineStationBoDoAdapter(ls));
                }
                catch (DO.NoLineStationFoundException ex)
                {
                    throw new BO.LineStationNotFoundException("No line station exists so it cannot be deleted", ex);
                }
            }

        }
        /// <summary>
        /// changes the station immediately after the station added
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="stationBefore"></param>
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
        void DeleteLine(int lineId)
        {
            IDL dl = DLFactory.GetDL();
            try
            {
                dl.DeleteLine(lineId);
            }
            catch (DO.NoLineFoundException ex)
            {
                throw new BO.NoLineFoundException("It's impossible to delete line because it doesn't exist on the system", ex);
            }
        }

 
        #endregion
        #region LineStation
        bool InLine(DO.LineStation ls,int lineId)
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
         boLineStation.Station = doLineStation.Station;
         boLineStation.LineStationIndex = doLineStation.LineStationIndex;
         boLineStation.PrevStation = doLineStation.PrevStation;
         boLineStation.NextStation = doLineStation.NextStation;
         //boLineStation.TimeFromPreviousStation = dl.GetAdjacentStations(doLineStation.PrevStation, doLineStation.Station).Time;
         //boLineStation.distanceFromPrevious= dl.GetAdjacentStations(doLineStation.PrevStation, doLineStation.Station).Distance;
         boLineStation.InService = doLineStation.InService;
         return boLineStation;
        }
        public IEnumerable<LineStation> GetAllLineStationsByLine(int lineId)
        {
            IDL dl = DLFactory.GetDL();
            IEnumerable<BO.LineStation> lineStationsInLine = from lineStation in dl.GetAllLineStationsByLine(lineId) select LineStationDoBoAdapter(lineStation);
            return lineStationsInLine;
        }
        public BO.LineTrip LineTripDoBoAdapter(DO.LineTrip doLineTrip)
        {
            BO.LineTrip boLineTrip = new BO.LineTrip();
            boLineTrip.StartAt = doLineTrip.StartAt;
            boLineTrip.FinishAt = doLineTrip.FinishAt;
            boLineTrip.Frequency = doLineTrip.Frequency;
            boLineTrip.Id = doLineTrip.Id;
            boLineTrip.LineId = doLineTrip.LineId;
            boLineTrip.InService = doLineTrip.InService;
            return boLineTrip;
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
       public IEnumerable<BO.AdjacentStations> GetAllAdjacentStations()
        { IDL dl = DLFactory.GetDL();
            return from adjacentStation in dl.GetAllAdjacentStations() select AdjacentStationsDoBoAdapter(adjacentStation);
        }
        #endregion
    }
}
