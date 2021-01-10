using System;
using System.Collections.Generic;
using BO;


namespace BlApi
{
    public interface IBL
    {
        #region Bus
        //void AddBus(Bus bus);
        //IEnumerable<BO.Bus> GetAllBuses();
        //IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> filtering);
        //void RefuelBus(string licenseNum);
        //void TreatBus(string licenseNum);
        //void DeleteBus(string licenseNum);
        #endregion
        #region Line
         void AddLine(Line line);
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetAllLinesByStation(int stationCode);
        Line GetLine(int id);
        //void DeleteLine(int id);
        #endregion
        #region LineStation
        //void AddLineStation(LineStation lineStation,int lineId);
        IEnumerable<LineStation> GetAllLineStationsByLine(int lineId);
        //IEnumerable<LineTrip> GetAllLineTrips(int lineId);
        //IEnumerable<Line> GetAllLinesBy(int lineId,Predicate<Line> filter);
        //LineStation GetLineStation(int idLine,int station,int prevStation,int nextStation);
        //LineStation UpdateLineStation(int idLine,int stationCode, Action<int,LineStation> updateStation);
        //void UpdatePrevLineStation(int idLine, int stationCode,int newStationCode);
        //void UpdateNextLineStation(int idLine, int stationCode,int newStationCode);
        //void UpdateAfterLineStation(int idLine, int stationCode, int newStationCode);
        // void DeleteLine(int idLine, int station, int prevStation, int nextStation);
        #endregion
        #region Station
        //void AddStation(Station station);
        //IEnumerable<Station> GetAllStations();
        //IEnumerable<Station> GetAllStationsBy(Predicate<Station> filter);
        //Station GetStation(int code);
        //void DeleteStation(int code);
        #endregion
        #region User
        // void AddUser(User user);
        //IEnumerable<User> GetAllUsers();
        //IEnumerable<User> GetAllUsersBy(Predicate<Station> filter);
        //User GetUser(string userName);
        //void DeleteUser(string userName);
        #endregion
        #region AdjacentStations
        //bool AdjacentStationsExists(int station1, int station2);
        IEnumerable<BO.AdjacentStations> GetAllAdjacentStations();
        #endregion
    }
}
