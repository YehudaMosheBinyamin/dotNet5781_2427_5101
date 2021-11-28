using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class LineTiming
    {
        public int LineId { get; set; }
        public int LineCode { get; set; }
        public TimeSpan StartTime { get; set; }
        public string LastStationName { get; set; }
        public TimeSpan EstWaitingTime { get; set; }
        public override string ToString()
        {
            return $"Code: {LineCode} Time From Start: {StartTime} LastStationName: {LastStationName} Arrival Time: {EstWaitingTime}";
        }
    }
}
