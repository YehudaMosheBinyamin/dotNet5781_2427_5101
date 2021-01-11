using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Line
    {
        public IEnumerable<LineStation> stationsInLine {get; set;}
        //public IEnumerable<LineTrip> lineExits {get; set;}
        public int Id { get; set; }
        public int Code { get; set;}
        public Areas Area { get; set; }
        public bool InService;
        public override string ToString()
        {
            return String.Format($"Code: {Code} ");
        }
    }
    public enum Areas { General, North, South, Center, Jerusalem };
   
}
