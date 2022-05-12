using System;
using System.Collections.Generic;
using System.Text;
using DO;
namespace DalAPI
{
    //CRUD Logic
    public interface IDL
    {
        #region Line
        void AddLine(Line line);
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate);
        Line GetLine(int lineId);
        void DeleteLine(int lineId);
        int GetNewLineId();
        #endregion
        #region Station
        int GetNewStationCode();
        void AddStation(Station station);
        IEnumerable<Station> GetAllStations();
        Station GetStation(int code);
        #endregion
        
        #region LineTrip
        void AddLineTrip(LineTrip lineTrip);
        IEnumerable<LineTrip> GetAllLineTrips();
        LineTrip GetLineTrip(int id,int busCode);
        int GetNewLineTripId();
        IEnumerable<LineTrip> GetLineTripsBy(Predicate<LineTrip> predicate);
        void DeleteLineTrip(int id,int busCode);
        void DeleteLineTrips(int lineId);
        #endregion
        #region AdjacentStations
        bool AdjacentStationsExists(int station1,int station2);
        void AddAdjacentStations(AdjacentStations adjacentStations);
        IEnumerable<AdjacentStations> GetAllAdjacentStations();
        IEnumerable<AdjacentStations> GetAdjacentStationsBy(Predicate<AdjacentStations> predicate);
        AdjacentStations GetAdjacentStations(int stationOneCode,int stationTwoCode);
        void DeleteAdjacentStations(int stationOneCode,int stationTwoCode);
        
        #endregion
        #region LineStation
        void AddLineStation(LineStation lineStation);
        IEnumerable<LineStation> GetAllLineStations();
        IEnumerable<LineStation> GetAllLineStationsByLine(int lineId);
        LineStation GetLineStation(int lineId,int stationCode);
        void DeleteLineStation(int lineId,int stationCode);
        void DeleteLineStations(int lineId);
        bool InLineStations(int station1, int station2);
        #endregion
        
       
        
    }
}
