using System;
using System.Collections.Generic;
using System.Threading;

namespace dotNet5781_02_2427_5101
{//class to represent a bus line
    public class BusLine : IComparable
    {//constructor
        public BusLine(List<BusLineStop> bls)
        {
            bls[0].DistanceFromPrevious = 0.0;//the distance from previous of first stop is zero since it's the first stop
            bls[0].TimeFromPrevious = 0.0;//the time from previous of first stop is zero since it's the first stop
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
        //constructor for new route for existing line.Note the assumption that it is possible for lines with the same number can have different routes and that there can be many lines with the same number
        public BusLine(BusLine blsForOtherDirection, List<BusLineStop> blsForNew)
        {
            operatingArea = blsForOtherDirection.OperatingArea;
            busNumber = blsForOtherDirection.BusNumber;
            foreach (BusLineStop bls in blsForNew)
            {
                ListStations.Add(bls);
            }
            ListStations[0].DistanceFromPrevious = 0.0;
            ListStations[0].TimeFromPrevious = 0.0;
        }

        public Area operatingArea;
        public Area OperatingArea { get { return operatingArea; } set { operatingArea = value; } }
        internal int busNumber;
        public int BusNumber { get { return busNumber; } }
        public static List<int> busNumbers = new List<int>();
        private BusLineStop firstStation;
        public BusLineStop FirstStation { get { return firstStation; } set { FirstStation = value; } }
        private BusLineStop lastStation;
        public BusLineStop LastStation { get { return lastStation; } set { LastStation = value; } }
        private List<BusLineStop> listStations = new List<BusLineStop>();
        public List<BusLineStop> ListStations { get { return listStations; } set { listStations = value; } }
        internal Area RandomArea()
        {
            Array values = Enum.GetValues(typeof(Area));
            Random randomNumber = new Random();
            Thread.Sleep(1);
            return (Area)values.GetValue(randomNumber.Next(values.Length));
        }
        //3.1
        public override string ToString()
        {
            List<int> codesStations = new List<int>();//list of bus codes of all bus line stops the bus goes past
            foreach (BusLineStop bls in ListStations)
            {
                codesStations.Add(bls.BusStationKey);
            }
            string allStations = string.Join(",", codesStations);
            return String.Format("Bus number: {0} Area: {1} Stations: {2}", BusNumber, OperatingArea, allStations);
        }
        //3.2 to remove bus stop line
        public void RemoveBusStop(BusLineStop bs)
        {
            if (inList(bs)) { if (this.ListStations.Count == 2)
                { 
                    throw new NoLineException("cannot delete station because doing so will create a line with one stop which is pointless"); 
                }
                else 
                { 
                    listStations.Remove(bs); 
                }
            
         }
            else { 
                throw new NotInListException("This bus stop cannot be deleted from list of stations since it isn't there");
            }
        }
        //3.2 to add bus stop to line
        public void AddBusStop(BusLineStop bs, BusLineStop beforeStop)
        {
 
            if (inList(bs)) { throw new AlreadyInException("The stop is already in list,cannot be in same route twice"); }
            else
            {
                int indexBefore = ListStations.IndexOf(beforeStop);
                int currentAfter = indexBefore + 1;//the bus list stop which is currently after previousStop,and will after function be after bs
                if (ListStations.Count - 1 != indexBefore)//if  there is stop afterwards there is a need to update its previous time and distance
                {
                    ListStations[currentAfter].DistanceFromPrevious = ListStations[currentAfter].RandomDistance(new Random());
                    ListStations[currentAfter].TimeFromPrevious = ListStations[currentAfter].RandomTime(new Random());
                }
                ListStations.Insert(currentAfter, bs);
            }
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
       
        //distance between two stops 3.4
        public double distance(BusLineStop bs1, BusLineStop bs2)
        {
            double distance = 0.0;
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
        public double timeDifference(BusLineStop bls1, BusLineStop bls2)
        {
            double time = 0.0;
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
                 double timeDifferenceBuses = this.timeDifference(FirstStation, LastStation) -
                      otherBusLine.timeDifference(otherBusLine.FirstStation, otherBusLine.LastStation);
                if (timeDifferenceBuses > 0)//it takes longer for this than other bus line
                {
                    return 1;
                }
                else if (timeDifferenceBuses == 0.0)//time is the same
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
