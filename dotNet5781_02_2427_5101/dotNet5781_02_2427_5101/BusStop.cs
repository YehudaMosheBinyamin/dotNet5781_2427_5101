using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{
    public class BusStop
    {
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
        private static List<int> busKeyList;
            List<int> BusKeyList { get { return busKeyList; } set { busKeyList = value; } }
            int busStationKey;
            double latitude;
            double longtitude;
            public int BusStationKey
            {
                get { return busStationKey; }
            }
            public double Latitude { get { return latitude; } }
            public double Longtitude { get { return longtitude; } }


            public override string ToString()
            {
                return String.Format("Bus station code: {0}, {1} °N {2} °E ", BusStationKey, Latitude, Longtitude);
            }
        }

    
}
