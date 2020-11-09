using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{
    public class BusLine : IComparable
    {
        Area operatingArea;
        public Area OperatingArea { get { return operatingArea; } }
        public int BusNumber => BusNumber;
        public BusLineStop firstStation;
        public BusLineStop FirstStation { get { return firstStation; } set { firstStation = value; } }
        public BusLineStop lastStation;
        public BusLineStop LastStation { get { return lastStation; }set { LastStation = value; } }
        public List<BusLineStop> listStations = new List<BusLineStop>();
        public List<BusLineStop> ListStations { get { return listStations; } }
        public override string ToString()
        {
            return String.Format("Bus number: {0} Area: {1} Stations: {2}", BusNumber, OperatingArea, ListStations);
        }
        public bool inList(BusLineStop busStop)
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
        public void RemoveBusStop(BusLineStop bs)
        {
            if (inList(bs)) { listStations.Remove(bs); }
        }
        public void AddBusStop(BusLineStop bs, int index)
        {
            if (inList(bs)) { listStations.Insert(index, bs); }
        }
        public float distance(BusLineStop bs1, BusLineStop bs2)
        {
            float distance = 0f;
            if (listStations.Contains(bs1) && listStations.Contains(bs2))
            {

                foreach (BusLineStop bs in listStations)
                {
                    if (listStations.IndexOf(bs1) <= listStations.IndexOf(bs) && listStations.IndexOf(bs) <=
                        listStations.IndexOf(bs2))
                    {
                        distance += bs.TimeFromPrevious;

                    }
                }

            }
            return distance;
        }
            //bus line from two bus stops
         public  BusLine makeLine(BusLineStop stop1, BusLineStop stop2)
         {
                BusLine newLine = new BusLine() { firstStation = stop1, lastStation = stop2 };
                return newLine;
         }
            public int CompareTo(object obj)
         {
                throw new NotImplementedException();
         }
    }
}
