namespace DO
{
    public static class Configuration
{
    static Configuration() { lineId = 0;stationCode = 50000; }
    private static int lineId;
    public static int LineId { get { lineId = lineId + 1; return lineId; } }
    private static int stationCode;
    public static int StationCode { get{ stationCode = stationCode + 1;return stationCode; } }
    //private static int tripId;//for stagetwo
} }