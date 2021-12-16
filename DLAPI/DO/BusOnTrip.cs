using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class BusOnTrip
    {
        public int Id { get; }
        public int LicenseNum { get; }//has connection to field in Bus
        public int LineId { get; }//..
        public TimeSpan PlannedTakeOff { get; set; }
        public TimeSpan ActualTakeOff { get; set; }
        public int PrevStation { get;  }//..in Station
        public TimeSpan PrevStationAt { get;}
        public TimeSpan NextStationAt { get; }
        public bool InService;
    }
}
