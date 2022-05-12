namespace DO
{
    public static class Configuration
{
    static Configuration() { lineId = 0;stationCode = 7000;lineTripId = 1; }
    private static int lineId;
    public static int LineId { get { lineId = lineId + 1; return lineId; } }
    private static int stationCode;
    private static int lineTripId;
    public static int LineTripId { get { lineTripId = lineTripId + 1;return lineTripId; } }
    public static int StationCode { get{ stationCode = stationCode + 1;return stationCode; } }
    //private static int tripId;//for stagetwo
} }
