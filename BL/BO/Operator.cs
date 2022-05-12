using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    /*A class to alert a bus station about an upcoming bus*/
    sealed internal class Operator
    {
        private static Operator instance;
        private Operator() { }
        private LineTiming arrivalTime;
        public int Station { get; set; }
        public LineTiming UpdateLineArrive { get { return arrivalTime; } set { arrivalTime = value; } }
        public static Operator Instance { get { if (instance == null) { instance = new Operator(); }return instance; }}
        private event Action<LineTiming,int> lineApproachingStationEvent;
        public event Action<LineTiming,int> LineApproachingStationEvent
        {
            add
            {
                lineApproachingStationEvent = value;
            }
            remove
            {
                lineApproachingStationEvent -= value;
            }
        }
        /*To update how long a bus line will take until it will approach station*/
        public void DoUpdateArrivalTime()
        {
            lineApproachingStationEvent?.Invoke(UpdateLineArrive,Station);
        }
    }
}
