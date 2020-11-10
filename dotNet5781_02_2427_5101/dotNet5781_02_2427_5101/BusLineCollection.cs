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
        private List<BusLine> busList;//list that contains bus lines
        List<BusLine> BusList { get { return busList; }  }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyEnumerator(this);

        }
        //4.1 add bus line to collection busList
        public void addBusLine(BusLine bl)
        {     
           if (!busList.Contains(bl))
           {
                   BusList.Append(bl);
           }
    }
        
        //4.2 remove bus line from busList
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
        //4.2 list of all buses that go past station,if there are none throws exception
        public List<BusLine> busesStopStation(int busStopCode)
        {   List<BusLine> stationBusLines = new List<BusLine>();
            foreach (BusLine busl in busList)
            {
                foreach(BusLineStop bls in  busl.ListStations)
                {
                    if (bls.BusStationKey == busStopCode)
                        stationBusLines.Add(busl);
                }
            }
            if (stationBusLines.Count > 0)
            {
                return stationBusLines;
            }
            else
            {
                throw new NoLineAtStopException("EXCEPTION:NO BUS LINES GO PAST THIS STATION");
            }
        }
        //4.3 returnes busList after it is sorted from shortest lines to longest lines
        public List<BusLine> sortListLines()
        {
            BusList.Sort();
            return BusList;
        }
        //4.4 indexer-gets index and returns the BusLine with that index
        public BusLine this[int busLineNumber]
        {
            get
            {
                foreach (BusLine bs in busList)
                {
                    if (bs.BusNumber == busLineNumber)
                    {
                        return bs;
                    }
                }
                throw new NoLineException("Bus Line is not in  busLineCollection");
            }
        }
        public class MyEnumerator : IEnumerator
            {
                BusLineCollection collection;
                int center = -1;
                internal MyEnumerator(BusLineCollection collection) { this.collection = collection; }
                public void Reset() { } 
                public object Current { get { return collection.busList[center]; } }
                public bool MoveNext() { return (++center < collection.busList.Count); }
            }
        }
    }
