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
        #region Station
        public DO.Station StationBoDoAdapter(BO.Station boStation)
        {
            BO.Station bStation = boStation;
            IDL dl = DLFactory.GetDL();
            DO.Station doStation = new DO.Station();
            doStation.Code = dl.GetNewStationCode();
            doStation.Latitude = bStation.Latitude;
            doStation.Longtitude = bStation.Longtitude;
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
            AddAllLineTrips(line);
            List<DO.AdjacentStations> adjacentStations = (from lineStation1 in line.stationsInLine
                                                          from lineStation2 in line.stationsInLine
                                                          where lineStation1.NextStation == lineStation2.Station && dl.AdjacentStationsExists(lineStation1.Station, lineStation2.Station) == false
                                                          let rDistance = Functions.randomDistance()
                                                          select new DO.AdjacentStations
                                                          {   Station1 = lineStation1.Station,
                                                              Station2 = lineStation2.Station,
                                                              Distance = rDistance,
                                                              Time = Functions.MinutesOfTravel(rDistance),
                                                              InService = true
                                                          }).ToList();
            float distance1 = Functions.randomDistance();
            float distance2 = Functions.randomDistance();
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
            boLineStation.Name = GetStation(doLineStation.Station).Name;
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
        /// Return whether the clock is running or not
        /// </summary>
        /// <returns></returns>
        public bool IsClockOn()
        {
            return !simulatorClock.Cancel;
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
            for (int stationIndex = 0; stationIndex < line.stationsInLine.Count(); ++stationIndex)
            {

                LineStation ls = line.stationsInLine.ElementAt(stationIndex);
                //Sleep the amount of time in miliseconds the bus will take to get to current station from previous station
                //It is times 1000 to convert from seconds to milliseconds and times 1/rate to wait less time if the simulation clock is running at a different rate than 1
                int waitingTime = 1000 * ((int)Math.Ceiling((double)(Functions.DelayOrEarlyArrival(ls.TimeFromPreviousStation) * (1 / (double)simulatorClock.Rate))));
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





