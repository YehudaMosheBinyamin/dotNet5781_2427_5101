using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    class LineTiming
    {
        int LineId { get; set; }
        int LineCode { get; set; }
        TimeSpan TimeFromStart { get; set; }
        string LastStationName { get; set; }
        TimeSpan ActualArrivalTime { get; set; }
    }
}
