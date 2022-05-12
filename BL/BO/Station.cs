using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    /*A class to represent a physical bus station*/
    public class Station
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public IEnumerable<LineStation> LineStationsOfStation { get; set; }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
