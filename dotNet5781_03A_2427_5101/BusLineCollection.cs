using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace dotNet5781_02_2427_5101
{
    public class BusLineCollection : IEnumerable
    {
        public List<BusLine> busList=new List<BusLine>();//list that contains bus lines
        public List<BusLine> BusList { get { return busList; }set { busList = value; }  }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyEnumerator(this);

        }

        //4.1 add bus line to collection busList
        public void addBusLine(BusLine bl)
        {
            if (!busList.Contains(bl))
            {
                BusList.Add(bl);
            }
        }
        //add other direction for bus line
        public void addBusDirection(BusLine bl, List<BusLineStop> blsForNew)
        {
            if (busList.Contains(bl))
            {
                BusLine busLine = new BusLine(bl,blsForNew);
                BusList.Add(busLine);
            }
            else
            {
                throw new BusLineDoesNotExistException("BUSLINEDOESNOTEXISTESCEPTION:The bus you want to add the other direction to doesn't exist");
            }

        }

        //4.2 remove bus line from busList
        public void removeBusLine(BusLine bl)
        {
            bool found = false;
            int blBusNumber = bl.BusNumber ;//bl bus number
            if (busList.Contains(bl))
            {
                found = true;
                
            }
            
            if (found == true)
            {  
                busList.Remove(bl);
            }
        }
        //4.2 list of all buses that go past station,if there are none throws exception
        public List<BusLine> busesStopStation(int busStopCode)
        {   
            List<BusLine> stationBusLines = new List<BusLine>();
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
               throw new NoLineAtStopException("NOLINESATSTOPEXCEPTION:No lines go past this bus station");
        }
        //4.3 returns busList after it is sorted from shortest lines to longest lines
        public List<BusLine> sortListLines()
        {
            BusList.Sort();
            return BusList;
        }
        //4.4 indexer-gets index and returns the list of BusLine(s) with that index
        //in the case of multiple bus lines with the ame number(such as different directions,etc. will return more than one BusLine
        public List<BusLine> this[int busLineNumber]
        {
            get
            {
                List<BusLine> busLinesNumber = new List<BusLine>();
                foreach (BusLine bs in busList)
                {
                    if (bs.BusNumber == busLineNumber)
                    {
                        busLinesNumber.Add(bs);
                    }
                }
                if (busLinesNumber.Count > 0) { return busLinesNumber; }
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
