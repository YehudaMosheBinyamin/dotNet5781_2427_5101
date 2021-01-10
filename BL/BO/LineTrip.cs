using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class LineTrip
    {
        public int Id { get; set; }
        public int LineId { get; set; }//Id in line
        public TimeSpan StartAt { get; set; }
        public TimeSpan Frequency { get; set; }
        public TimeSpan FinishAt { get; set; }
        public bool InService;
    }
}