using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{/// <summary>
/// Represents the time a line leaves the station in the timetable of the line.
/// </summary>
    public class LineTrip
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public TimeSpan StartAt { get; set; }
        public bool InService;
    }
}
