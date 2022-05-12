using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    #region Station
    public class StationAlreadyExistsException : Exception
    {
        int code;
        public StationAlreadyExistsException(string str, Exception exp) : base(str)
        {
            code = ((DO.StationAlreadyExistsException)exp).Code;
        }

    }
    public class CannotDeleteStationException : Exception
    {
        int code;
        public CannotDeleteStationException(int code, string message) : base(message)
        {
            this.code = code;
        }
    }
    public class NoStationFoundException : Exception
    {
        int code;
        public NoStationFoundException(string str, Exception exp) : base(str)
        {
            code = ((DO.NoStationFoundException)exp).Code;
        }

    }
    #endregion
    #region AdjacentStation
    public class AdjacentStationsAlreadyExistsException : Exception
    {
        int station1;
        int station2;
        public AdjacentStationsAlreadyExistsException(string str, Exception exp) : base(str)
        {
            station1 = ((DO.AdjacentStationsAlreadyExistsException)exp).station1;
            station2 = ((DO.AdjacentStationsAlreadyExistsException)exp).station2;
        }

    }
    public class AdjacentStationsDoesntExistException : Exception
    {
        int station1;
        int station2;
        public AdjacentStationsDoesntExistException(string str, Exception exp) : base(str)
        {
            station1 = ((DO.AdjacentStationsDoesntExistException)exp).station1;
            station2 = ((DO.AdjacentStationsDoesntExistException)exp).station2;
        }

    }
    #endregion
    #region Line
    public class LineAlreadyExistsException : Exception
    {
        int lineId;
        public LineAlreadyExistsException(string msg, Exception ex) : base(msg)
        {
            lineId = ((DO.LineAlreadyExistsException)(ex)).LineId;
        }
    }
    public class NoLineFoundException : Exception
    {
        int lineId;
        public NoLineFoundException(string msg, Exception ex) : base(msg)
        {
            lineId = ((DO.NoLineFoundException)(ex)).LineId;
        }
    }
    #endregion
    #region LineStation
    public class LineStationNotFoundException : Exception
    {
        public int lineId;
        public int stationCode;
        public LineStationNotFoundException(string msg, Exception ex) : base(msg)
        {
            lineId = ((DO.NoLineStationFoundException)ex).LineId;
            stationCode = ((DO.NoLineStationFoundException)ex).stationCode;
        }
    }
    public class LineStationAlreadyExistsException : Exception
    {
        public int lineId;
        public int stationCode;
        public LineStationAlreadyExistsException(string msg, Exception ex) : base(msg)
        {
            lineId = ((DO.LineStationAlreadyExistsException)ex).LineId;
            stationCode = ((DO.LineStationAlreadyExistsException)ex).Station;
        }
    }
    #endregion
}
