using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Line
    {
            public ObservableCollection<LineStation> stationsInLine { get; set; }
            public ObservableCollection<LineTrip> lineExits {get; set;}
            public int Id { get; set; }
            public int Code { get; set; }
            public Areas Area { get; set; }
            public bool InService;
            public string LastStationName { get; set; }
    }
        public enum Areas { General, North, South, Center, Jerusalem };
}
