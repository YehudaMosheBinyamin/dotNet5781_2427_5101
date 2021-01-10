using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{//2
    public class BusLineStop : BusStop
    { double distanceFromPrevious;
      public double DistanceFromPrevious { get { return distanceFromPrevious; }set {distanceFromPrevious=value; } }
      double timeFromPrevious;
      public double TimeFromPrevious { get { return timeFromPrevious; }set { timeFromPrevious = value; } }
      public BusLineStop():base()
      {
          Random r = new Random();
            distanceFromPrevious = RandomDistance(r); 
            timeFromPrevious = RandomTime(r);
      }
     public double RandomTime(Random r)
        {
            return r.NextDouble() * (60 - 1) + 1;
        }
        public double RandomDistance(Random r)
        {
            return r.NextDouble() * (101 - 1) + 1;
        }
    }
}
