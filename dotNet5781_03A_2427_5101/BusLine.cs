using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace dotNet5781_02_2427_5101
{
    public class BusLine : IComparable
    {
        public BusLine(List<BusLineStop> bls)
        {
            
                Random random = new Random();
                operatingArea = RandomArea();
                busNumber = random.Next(1, 1000);
                while (busNumbers.Contains(busNumber))
                {
                    busNumber = random.Next(1, 1000);
                }
                this.firstStation = bls[0];
                this.lastStation = bls[bls.Count - 1];
                foreach (BusLineStop buslinestop in bls)
                {
                    listStations.Add(buslinestop);
                }
                busNumbers.Add(busNumber);
               
            
        }

        public BusLine(BusLine blsForOtherDirection)
        {
            int countBlsNumber=0;
            int busNumberToCheck = blsForOtherDirection.BusNumber;//check if exist twice if exists twice,exception because you cannot have a bus line with more than two directions
           foreach(int busNumber in BusLine.busNumbers)
            {
                if (busNumber == countBlsNumber)
                {
                    countBlsNumber++;
                }
            }
            if (countBlsNumber > 1)
            {
                throw new TooManyDirectionsException("TooManyDirectionsExceptions:Must have only two directions maximum per bus line"); 
            }
            operatingArea = blsForOtherDirection.OperatingArea;
            busNumber = blsForOtherDirection.BusNumber;
            ListStations.Add(blsForOtherDirection.LastStation);
            ListStations.Add(blsForOtherDirection.FirstStation);
        }
        public Area operatingArea;
        public Area OperatingArea { get { return operatingArea; } set { operatingArea = value; } }
        internal int busNumber;
        public int BusNumber { get { return busNumber; }}
        public static List<int> busNumbers=new List<int>();
        private BusLineStop firstStation;
        public BusLineStop FirstStation { get { return firstStation; }set { FirstStation = value; } }
        private BusLineStop lastStation;
        public BusLineStop LastStation { get { return lastStation; }set { LastStation = value; } }
        private List<BusLineStop> listStations=new List<BusLineStop>();
        public List<BusLineStop> ListStations { get { return listStations; } set { listStations = value; } }
        internal Area RandomArea()
        {   
            Array values = Enum.GetValues(typeof(Area));
            Random randomNumber = new Random();
            Thread.Sleep(1);
            return  (Area)values.GetValue(randomNumber.Next(values.Length));
        }
        //3.1
        public override string ToString()
        {
            List<int> codesStations = new List<int>();//list of bus codes of all bus line stops the bus goes past
            foreach(BusLineStop bls in ListStations)
            {
                codesStations.Add(bls.BusStationKey);
            }
            string allStations = string.Join(",", codesStations);
            return String.Format("Bus number: {0} Area: {1} Stations: {2}", BusNumber, OperatingArea, allStations);
        }
        //3.2 to remove bus stop from collection
        public void RemoveBusStop(BusLineStop bs)
        {
            if (inList(bs)) { listStations.Remove(bs); }
        }
        //3.2 to add bus stop to collection
        public void AddBusStop(BusLineStop bs, int index)
        {
            if (index > ListStations.Count + 1)
            {
                index = ListStations.Count + 1;
                LastStation = bs;
            }
            if (index == 0)
            {
                FirstStation = bs;
            }
            if (!inList(bs)) { listStations.Insert(index, bs); }
        }
        //3.3 check if bus line stop is in bus list
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
        public BusLineStop this[int key]
        {
            get
            {
                foreach (BusLineStop bls in listStations) { if (bls.BusStationKey == key) { return bls; } }

                throw new Exception("number doesn't exist");
            }
        }
        //distance between two stops 3.4
        public float distance(BusLineStop bs1, BusLineStop bs2)
        {
            float distance = 0f;
            if (listStations.Contains(bs1) && listStations.Contains(bs2))
            {

                foreach (BusLineStop bs in listStations)
                {
                    if (listStations.IndexOf(bs1) < listStations.IndexOf(bs) && listStations.IndexOf(bs) <=
                        listStations.IndexOf(bs2))
                    {
                        distance += bs.DistanceFromPrevious;

                    }
                }

            }
            return distance;
        }
        //3.5 time difference between two stop
        public int timeDifference(BusLineStop bls1, BusLineStop bls2)
        {
            int time = 0;
            if (listStations.Contains(bls1) && listStations.Contains(bls2))
            {

                foreach (BusLineStop bls in listStations)
                {
                    if (listStations.IndexOf(bls1) < listStations.IndexOf(bls) && listStations.IndexOf(bls) <=
                        listStations.IndexOf(bls2))
                    {
                        time += bls.TimeFromPrevious;

                    }
                }

            }
            return time;
        }
        //create bus line from two bus stops 3.6
        public BusLine makeLine(BusLineStop stop1, BusLineStop stop2)
        {
            List<BusLineStop> bls = new List<BusLineStop>();
            bls.Add(stop1);
            bls.Add(stop2);
            BusLine newLine = new BusLine(bls);
            return newLine;
        }
        //for sort method,sorting based on quicker overall trip length 
        public int CompareTo(object obj)
        {
            BusLine otherBusLine = obj as BusLine;
            if (otherBusLine != null)
            {
                int timeDifference = this.timeDifference(FirstStation, LastStation) -
                      otherBusLine.timeDifference(otherBusLine.FirstStation, otherBusLine.LastStation);
                if (timeDifference > 0)//it takes longer for this than other bus line
                {
                    return 1;
                }
                else if (timeDifference == 0)//time is the same
                {
                    return 0;
                }
                else//shorter to take this busline than other bus line
                {
                    return -1;
                }
            }
            throw new BusLineDoesNotExistException("BUSLINEDOESNTEXISTEXCEPTION:This bus line doesn't exist");

        }
    }
}
