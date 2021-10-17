using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
        public class Station
        {
            public int Code { get; set; }
            public string Name { get; set; }
            public double Longtitude { get; set; }
            public double Latitude { get; set; }
            public ObservableCollection<LineStation> LineStationsOfStation { get; set; }
            //public bool InService;
            public override string ToString()
            {
                return $"{Code} {Name} ";
            }
        }
}
