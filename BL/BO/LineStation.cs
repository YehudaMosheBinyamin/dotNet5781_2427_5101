using System;

namespace BO
{
    public class LineStation
    {
        public int LineId { get; set; }
        public int Station { get; set; }
        public int LineStationIndex { get; set; }
        public int PrevStation { get; set; }
        public int NextStation { get; set; }
        public string LastStationName { get; set; }
        public string Name { get; set; }
        public TimeSpan TimeFromPreviousStation { get; set; }
        public double DistanceFromPreviousStation { get; set; }
        public bool InService { get; set; }
        public override string ToString()
        {
            return String.Format($"Station: {Station} LineStationIndex: {LineStationIndex} NextStation: {NextStation} TimeFromPrevious:{TimeFromPreviousStation} DistanceFromPrevious{Math.Round(DistanceFromPreviousStation,2)} ");
        }
    }
}