using System;
using System.Collections.Generic;
using BO;
namespace BlApi
{
    /*Contract betwwen PL and BL*/
    public interface IBL
    {
        #region Simulator
        void StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime);
        void StopSimulator();
        #endregion
        #region Line
        void AddLine(Line line);
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetAllLinesByStation(int stationCode);
        Line GetLine(int id);
        void DeleteLine(int lineId);
        void UpdateLine(int oldLineId, Line newLine);
        #endregion
        #region LineStation
        IEnumerable<LineStation> GetAllLineStationsByLine(int lineId);
        void DeleteLineStations(int lineId);
        #endregion
        #region Station
        void AddStation(Station station);
        IEnumerable<Station> GetAllStations();
        Station GetStation(int code);
        int getNewCode();
        #endregion
        #region AdjacentStations
        void AddAdjacentStations(int station1, int station2, float distance, TimeSpan waitingTime);
        bool AdjacentStationsExists(int station1, int station2);
        void UpdateAdjacentStations(int station1, int station2,float newDistance, TimeSpan newTime);
        void DeleteAdjacentStations(int station1, int station2);
        IEnumerable<BO.AdjacentStations> GetAllAdjacentStations();
        BO.AdjacentStations GetAdjacentStations(int codeOne,int codeTwo);
        #endregion
        #region Simulator
        float GetRandomDistance();
        TimeSpan GetMinutesOfTravel(float distance);
        void SetStationPanel(int station, Action<LineTiming,int> updateBus);
        bool IsClockOn();
        #endregion
    }
}


