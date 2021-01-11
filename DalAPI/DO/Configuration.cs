namespace DO
{
    public static class Configuration
{
    static Configuration() { lineId = 1; }
    private static int lineId;
    public static int LineId { get { lineId = lineId + 1; return lineId; } }
    //private static int tripId;//for stagetwo
} }