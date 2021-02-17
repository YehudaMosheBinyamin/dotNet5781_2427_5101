using DalAPI;
using DO;
using System;
using System.Collections.Generic;

namespace DL
{
    public sealed class DalXml:IDL
    {
        #region singleton
        DalXml instance = new DalXml();
        public DalXml Instance { get { return instance; } }
        static DalXml() { }
        DalXml() { }

        public void AddBus(Bus bus)
        {
          
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            return null;
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            return null;
        }

        public Bus GetBus(string licenseNum)
        {
            return null;
        }

        public void UpdateBus(string licenseNum, Action<Bus> update)
        {
            
        }

        public void DeleteBus(string licenseNum)
        {
            
        }

        public void AddLine(Line line)
        {
            
        }

        public IEnumerable<Line> GetAllLines()
        {
            return null;
        }

        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate)
        {
            return null;
        }

        public Line GetLine(int lineId)
        {
            return null;
        }

        public void UpdateLine(int lineId, Action<Line> update)
        {
            
        }

        public void DeleteLine(int lineId)
        {
            
        }

        public int GetNewLineId()
        {
            return 1;
        }

        public int GetNewStationCode()
        {
            return 1;
        }

        public void AddStation(DO.Station station)
        {
          
        }

        public IEnumerable<DO.Station> GetAllStations()
        {
            return null;
        }

        public DO.Station GetStation(int code)
        {
            return null;
        }

        public void AddUser(User user)
        {
           
        }

        public IEnumerable<User> GetAllUsers()
        {
            return null;
        }

        public User GetUser(string usernName)
        {
            return null;
        }

        public void UpdateUser(string userName, Action<User> update)
        {
            
        }

        public void DeleteUser(string userName)
        {
            
        }

        public void AddLineTrip(LineTrip lineTrip)
        {
           
        }

        public IEnumerable<LineTrip> GetAllLineTrips()
        {
            return null;
        }

        public LineTrip GetLineTrip(int id, int busCode)
        {
            return null;
        }

        public int GetNewLineTripId()
        {
            return 1;
        }

        public IEnumerable<LineTrip> GetLineTripsBy(Predicate<LineTrip> predicate)
        {
            return null;
        }

        public void DeleteLineTrip(int id, int busCode)
        {
           
        }

        public void DeleteLineTrips(int lineId)
        {
          
        }

        public bool AdjacentStationsExists(int station1, int station2)
        {
            return false;
        }

        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            return null;
        }

        public IEnumerable<AdjacentStations> GetAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            return null;
        }

        public AdjacentStations GetAdjacentStations(int stationOneCode, int stationTwoCode)
        {
            return null;
        }

        public void DeleteAdjacentStations(int stationOneCode, int stationTwoCode)
        {
            
        }

        public void AddLineStation(LineStation lineStation)
        {
           
        }

        public IEnumerable<LineStation> GetAllLineStations()
        {
            return null;
        }

        public IEnumerable<LineStation> GetAllLineStationsByLine(int lineId)
        {
            return null;
        }

        public LineStation GetLineStation(int lineId, int stationCode)
        {
            return null;
        }

        public void UpdateLineStation(int lineId, int stationCode, int newStationCode, Action<LineStation, int> update)
        {
           
        }

        public void DeleteLineStation(int lineId, int stationCode)
        {
          
        }

        public void DeleteLineStations(int lineId)
        {
           
        }

        public bool InLineStations(int station1, int station2)
        {
           
        }
        #endregion


    }
}
