using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    /*A class that represents a scheduled bus line departure at a specific time*/
    public class LineTrip
    {
        public int Id { get; set; }
        public int LineId { get; set; }//Id in line
        public TimeSpan StartAt { get; set; }
        public bool InService;
    }
}
