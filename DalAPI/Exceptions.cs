using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    #region LineStation
    [Serializable]
    public class LineStationAlreadyExistsException : Exception
    {
        public int LineId;
        public int Station;
        public LineStationAlreadyExistsException(int lineId,int codeStation, string message) : base(message)
        {
            LineId = lineId;
            this.Station = codeStation;
        }
        public LineStationAlreadyExistsException(int lineId,int codeStation, string message, Exception innerException) : base(message, innerException)
        {
            LineId = lineId;
             Station= codeStation;
        }
    }
    [Serializable]
    public class NoLineStationFoundException : Exception
    {
        public int LineId;
        public int stationCode;
        public NoLineStationFoundException(int lineId,int sCode) : base()
        {
            LineId = lineId;
            stationCode = sCode;
        }

        public NoLineStationFoundException(int lineId,int sCode, string message) : base(message)
        {
            LineId = lineId;
            stationCode = sCode;
        }
        public NoLineStationFoundException(int lineId,int sCode, string message, Exception innerException) : base(message, innerException)
        {
            LineId = lineId;
            stationCode = sCode;
        }

        public override string ToString()
        {
            return base.ToString() + $"There is no lineId in existence with Line Id:{LineId}";
        }
    }
    #endregion
    #region Station
    [Serializable]
    public class StationAlreadyExistsException : Exception
    {
        public int Code;
        public StationAlreadyExistsException(int code) : base()
        {
            Code = code;
        }

        public StationAlreadyExistsException(int code, string message) : base(message)
        {
            Code = code;

        }
        public StationAlreadyExistsException(int code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        public override string ToString()
        {
            return base.ToString() + $"There is already station with the code:{Code}";
        }
    }
    [Serializable]
    public class NoStationFoundException : Exception
    {
        public int Code;
        public NoStationFoundException(int code) : base()
        {
            Code = code;
        }

        public NoStationFoundException(int code, string message) : base(message)
        {
            Code = code;

        }
        public NoStationFoundException(int code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        public override string ToString()
        {
            return base.ToString() + $"There is no station with the code:{Code}";
        }
    }
    #endregion
    #region Bus
    [Serializable]
    public class NoBusFoundException : Exception
    {
        public string LicenseNum;
        public NoBusFoundException(string licenseNum) : base()
        {
            LicenseNum = licenseNum;
        }

        public NoBusFoundException(string licenseNum, string message) : base(message)
        {
            LicenseNum = licenseNum;

        }
        public NoBusFoundException(string licenseNum, string message, Exception innerException) : base(message, innerException)
        {
            LicenseNum = licenseNum;
        }

        public override string ToString()
        {
            return base.ToString() + $"There is no bus with the license number:{LicenseNum}";
        }
    }
    [Serializable]
    public class BusAlreadyExistsException : Exception
    {
        public string LicenseNum;
        public BusAlreadyExistsException(string licenseNum) : base()
        {
            LicenseNum = licenseNum;
        }

        public BusAlreadyExistsException(string licenseNum, string message) : base(message)
        {
            LicenseNum = licenseNum;

        }
        public BusAlreadyExistsException(string licenseNum, string message, Exception innerException) : base(message, innerException)
        {
            LicenseNum = licenseNum;
        }

        public override string ToString()
        {
            return base.ToString() + $"There is already a bus with the license number:{LicenseNum}";
        }
    }
    #endregion
    #region Line
    [Serializable]
    public class LineAlreadyExistsException : Exception
    {
        public int LineId;
        public LineAlreadyExistsException(int lineId) : base()
        {
            LineId = lineId;
        }

        public LineAlreadyExistsException(int lineId, string message) : base(message)
        {
            LineId = lineId;

        }
        public LineAlreadyExistsException(int lineId, string message, Exception innerException) : base(message, innerException)
        {
            LineId = lineId;
        }

        public override string ToString()
        {
            return base.ToString() + $"There is already a line with the code:{LineId}";
        }
    }
    [Serializable]
    public class NoLineFoundException : Exception
    {
        public int LineId;
        public NoLineFoundException(int lineId) : base()
        {
            LineId = lineId;
        }

        public NoLineFoundException(int lineId, string message) : base(message)
        {
            LineId = lineId;

        }
        public NoLineFoundException(int lineId, string message, Exception innerException) : base(message, innerException)
        {
            LineId = lineId;
        }

        public override string ToString()
        {
            return base.ToString() + $"There is no line with the Id:{LineId}";
        }
    }
    #endregion
    #region User
    [Serializable]
    public class NoUserFoundException : Exception
    {
        public string UserName;
        public NoUserFoundException(string userName) : base(userName)
        {
            UserName = userName;

        }
        public NoUserFoundException(string userName,string message, Exception innerException) : base(userName, innerException)
        {
            UserName = userName;
        }

        public override string ToString()
        {
            return base.ToString() + $"There is no user on the system with the name: {UserName}";
        }
    }
    public class UserAlreadyExistsException : Exception
    {
        public string UserName;
        public UserAlreadyExistsException(string userName) : base(userName)
        {
            UserName = userName;

        }
        public UserAlreadyExistsException(string userName, string message, Exception innerException) : base(userName, innerException)
        {
            UserName = userName;
        }

        public override string ToString()
        {
            return base.ToString() + $"There is no user on the system with the name: {UserName}";
        }
    }
    #endregion
    #region LineTrip
    [Serializable]
    public class NoLineTripExistsException : Exception
    {
        int id;
        int busCode;

        public NoLineTripExistsException(int id, int busCode)
        {
            this.id = id;
            this.busCode = busCode;

        }
        public NoLineTripExistsException(int id, int busCode, string message, Exception innerException) : base(message, innerException)
        {
            this.id = id;
            this.busCode = busCode;
        }

        public NoLineTripExistsException(int id, int busCode, string message) : base(message)
        {
            this.id = id;
            this.busCode = busCode;
        } 
    }
    public class LineTripAlreadyExistsException : Exception
    {
        int id;
        int busCode;

        public LineTripAlreadyExistsException(int id, int busCode)
        {
            this.id = id;
            this.busCode = busCode;

        }
        public LineTripAlreadyExistsException(int id, int busCode, string message, Exception innerException) : base(message, innerException)
        {
            this.id = id;
            this.busCode = busCode;
        }

        public LineTripAlreadyExistsException(int id, int busCode, string message) : base(message)
        {
            this.id = id;
            this.busCode = busCode;
        }
    }
    #endregion
    #region AdjacentStations
    public class AdjacentStationsAlreadyExistsException : Exception
        {
            public int station1;
            public int station2;
            public AdjacentStationsAlreadyExistsException(int station1,int station2,string message) : base(message)
            {
                this.station1 = station1;
                this.station2 = station2;
            }
        }
    public class AdjacentStationsDoesntExistException : Exception
    {
        public int station1;
        public int station2;
        public AdjacentStationsDoesntExistException(int station1, int station2, string message) : base(message)
        {
            this.station1 = station1;
            this.station2 = station2;
        }
    }
    #endregion
}



