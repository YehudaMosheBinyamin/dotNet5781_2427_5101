using System;
using System.Collections.Generic;
using System.Text;
using DO;
namespace DalAPI
{
    //CRUD Logic
    public interface IDL
    {
        #region Bus
         void AddBus(Bus bus);
         IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
         Bus GetBus(string licenseNum);
         void UpdateBus(string licenseNum,Action<Bus>update);
         void DeleteBus(string licenseNum);
        #endregion
        #region Line
        void AddLine(Line line);
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate);
        Line GetLine(int lineId);
        void UpdateLine(int lineId,Action<Line>update);
        void DeleteLine(int lineId);
        #endregion
        #region Station
        void AddStation(Station station);
        IEnumerable<Station> GetAllStations();
        Station GetStation(int code);
        //void UpdateStation(Line line, Action<Line> update);
        //void DeleteStation(int code);
        #endregion
        #region User
        void AddUser(User user);
        IEnumerable<User> GetAllUsers();
        User GetUser(string usernName);
        void UpdateUser(string userName, Action<User> update);
        void DeleteUser(string userName);
        #endregion
        #region LineTrip
        void AddLineTrip(LineTrip lineTrip);
        IEnumerable<LineTrip> GetAllLineTrips();
        LineTrip GetLineTrip(int id,int busCode);
        IEnumerable<LineTrip> GetLineTripsBy(Predicate<LineTrip> predicate);
        void UpdateLineTrip(int id, int busCode, Action<LineTrip> update);
        void DeleteLineTrip(int id,int busCode);
        void DeleteLineTrips(int lineId);
        #endregion
        #region AdjacentStations
        bool AdjacentStationsExists(int station1,int station2);
        void AddAdjacentStations(AdjacentStations adjacentStations);
        IEnumerable<AdjacentStations> GetAllAdjacentStations();
        IEnumerable<AdjacentStations> GetAdjacentStationsBy(Predicate<AdjacentStations> predicate);
        AdjacentStations GetAdjacentStations(int stationOneCode,int stationTwoCode);
        void UpdateAdjacentStations(int stationOneCode,int stationTwoCode,Action<AdjacentStations>update);
        void DeleteAdjacentStations(int stationOneCode,int stationTwoCode);
        
        #endregion
        #region LineStation
        void AddLineStation(LineStation lineStation);
        IEnumerable<LineStation> GetAllLineStations();
        IEnumerable<LineStation> GetAllLineStationsByLine(int lineId);
        LineStation GetLineStation(int lineId,int stationCode);
        void UpdateLineStation(int lineId,int stationCode,int newStationCode,Action<LineStation,int>update);
        void DeleteLineStation(int lineId,int stationCode);
        void DeleteLineStations(int lineId);
        bool InLineStations(int station1, int station2);
        #endregion
        #region User
        //bool CheckUser(string userName, string password);
        #endregion
    }
}
