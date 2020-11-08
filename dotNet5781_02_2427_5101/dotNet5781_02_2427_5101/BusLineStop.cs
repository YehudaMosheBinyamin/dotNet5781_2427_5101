using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{
    public class BusLineStop : BusStop
    {
        int distanceFromPrevious;
        int timeFromPrevious;
        public BusLineStop()
        {
            Random r = new Random();
            distanceFromPrevious = r.Next(1, 101);

        }
    }
}
