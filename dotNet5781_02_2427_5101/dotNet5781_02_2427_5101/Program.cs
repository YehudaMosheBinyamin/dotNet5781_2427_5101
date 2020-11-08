using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dntemp2
{
    using dotNet5781_02_2427_5101;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace dotNet5781_02_2427_5101
    {
        class BusStop
        {
            static List<int> busKeyList;
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

            public class BusLineStop : BusStop
            {
                int distanceFromPrevious;
                int timeFromPrevious;
                public BusLine()
                {
                    Random r = new Random();
                    distanceFromPrevious = r.Next(1, 101);

                }
            }
            public enum Area { General, North, South, Center, Jerusalem };
            public class BusLine : IComparable
            {
                Area operatingArea;
                public Area OperatingArea { get { return operatingArea; } }
                public int BusNumber => BusNumber;
                BusStop firstStation;
                public BusStop FirstStation { get { return firstStation; } }
                BusStop lastStation;
                public BusStop LastStation { get { return lastStation; } }
                public List<BusStop> listStations = new List<BusStop>();
                public List<BusStop> ListStations { get { return listStations; } }
                public override string ToString()
                {
                    return String.Format("Bus number: {0} Area: {1} Stations: {2}", BusNumber, OperatingArea, ListStations);
                }
                public bool inList(BusStop busStop)
                {
                    if (listStations.Contains(busStop))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                public void RemoveBusStop(BusStop bs)
                {
                    if (inList(bs)) { listStations.Remove(bs); }
                }
                public void AddBusStop(BusStop bs, int index)
                {
                    if (inList(bs)) { listStations.Insert(index, bs); }
                }
                public float distance(BusStop bs1, BusStop bs2)
                {
                    float distance = 0f;
                    if (listStations.Contains(bs1) && listStations.Contains(bs2))
                    {
                        int firstBSIndex = listStations.IndexOf(bs1) + 1;
                        int secondBSIndex = listStations.IndexOf(bs2);
                        foreach (BusStop bs in listStations)
                        {
                            if (firstBSIndex <= listStations.IndexOf(bs) && listStations.IndexOf(bs) <= secondBSIndex)
                            {

                            }
                        }
                    }
                    //bus line from two bus stops
                    BusLine makeLine(BusStop stop1, BusStop stop2)
                    {
                        BusLine newLine = new BusLine() { firstStation = stop1, lastStation = stop2 };
                        return newLine;
                    }
                    public int CompareTo(object obj)
                    {
                        throw new NotImplementedException();
                    }
                }

                public int CompareTo(object obj)
                {
                    throw new NotImplementedException();
                }
            }
            class Program
            {
                static void Main(string[] args)
                {

                }
            }
        }
    }
}  
    