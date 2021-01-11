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
        //public TimeSpan TimeFromPreviousStation { get; set; }-possible to receive same information from AdjacentStations
        //public double distanceFromPrevious { get; set; }--//
        public bool InService { get; set; }
        public override string ToString()
        {
            return String.Format("WHOOOOOO");
        }
    }
}