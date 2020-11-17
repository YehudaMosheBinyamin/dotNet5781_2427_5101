using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{//2
    public class BusLineStop : BusStop
    {   int distanceFromPrevious;
        public int DistanceFromPrevious { get { return distanceFromPrevious; } }
        int timeFromPrevious;
        public int TimeFromPrevious { get { return timeFromPrevious; } }
        public BusLineStop():base()
        {
            Random r = new Random();
            distanceFromPrevious = r.Next(1, 101);
            timeFromPrevious = r.Next(1, 60);
        }
    }
}
