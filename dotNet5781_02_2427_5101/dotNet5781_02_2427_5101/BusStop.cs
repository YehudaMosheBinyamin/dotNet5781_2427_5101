using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{//1Class to represent a bus stop
    public class BusStop
    {//constructor
        public BusStop()
        {
            Random rand = new Random();
            int randomCode = rand.Next(0, 999999);
            while (busKeyList.Contains(randomCode))
            {
                randomCode = rand.Next(0, 999999);
            }
            busStationKey = randomCode;
            busKeyList.Add(busStationKey);
            double randomLatitude = 31.0 + rand.NextDouble() % (33.3 - 31.0);
            latitude = randomLatitude;
            double randomLongtitude = 34.3 + rand.NextDouble() % (35.5 - 34.3);
            longtitude = randomLongtitude;
        }
            public static List<int> busKeyList;//list of busKeyList
            public static List<int> BusKeyList { get { return busKeyList; } }
            int busStationKey;//code of bus station
            public int BusStationKey
            {
                get { return busStationKey; }
            }
            double latitude;
            public double Latitude { get { return latitude; } }
            double longtitude;
            public double Longtitude { get { return longtitude; } }

            public override string ToString()
            {
                return String.Format("Bus station code: {0}, {1} °N {2} °E ", BusStationKey, Latitude, Longtitude);
            }
        }

    
}
