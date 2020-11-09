using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_2427_5101
{
    public class BusLineCollection : IEnumerable
    {
        List<BusLine> busList = new List<BusLine>(50);
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyEnmrtr(this);

        }
        public void addBusLine(BusLine bl)
        {
            if (!busList.Contains(bl))
            {
                busList.Append(bl);
            }

        }
        public void removeBusLine(BusLine bl)
        {
            bool found = false;
            BusLineStop blFirstBusStop = bl.FirstStation;
            BusLineStop blLastBusStop = bl.LastStation;
            if (busList.Contains(bl))
            {
                found = true;
                busList.Remove(bl);
            }
            if (found == true)
            {
                bl.FirstStation = blLastBusStop;
                bl.LastStation = blFirstBusStop;
                if (busList.Contains(bl))
                {
                    busList.Remove(bl);
                }
            }
        }
        public List<BusLine> busesStopStation(int busStopCode)
        {   List<BusLine> stationBusLines = new List<BusLine>();
            foreach (BusLine busl in busList)
            {
                busl.ListStations.Find()
            }
        }
        public BusLine this[int busLineNumber]
        {
            get
            {
                foreach(BusLine bs in busList)
                {
                    if (bs.BusNumber == busLineNumber)
                    {
                        return bs;
                    }
                }
                throw new Exception("Bus Line is not in list");
            }
        }
        public class MyEnmrtr : IEnumerator
            {
                BusLineCollection coll;
                int cntr = -1;
                internal MyEnmrtr(BusLineCollection coll) { this.coll = coll; }
                public void Reset() { } 
                public object Current { get { return coll.busList[cntr]; } }
                public bool MoveNext() { return (++cntr < coll.busList.Length); }
            }
        }
    }
