using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{
    class Program
    {
        public static void Main(string[] str)
        {
            int numOfBuses = 10;
            //int numOfStations = 40;
            List<BusStop> busStops = new List<BusStop>();
            BusLineCollection busLineCollection = new BusLineCollection();
            //---------------------                                                                                                                                           
            
            List<BusLineStop> busStopsForLine = new List<BusLineStop>();
            new BusLineStop();
            BusLineStop bls1 = new BusLineStop();
            BusLineStop bls2 = new BusLineStop();
            BusLineStop bls3 = new BusLineStop();
            BusLineStop bls4 = new BusLineStop();
            int j = 0;
            for (int i = 0; i < numOfBuses; i++)
            {
                bls1 = busStops[j] as BusLineStop;
                bls2 = busStops[j + 1] as BusLineStop;
                bls3 = busStops[j + 2] as BusLineStop;
                bls4 = busStops[j + 3] as BusLineStop;
                j = j + 4;
                busStopsForLine.Add(bls1);
                busStopsForLine.Add(bls2);
                busStopsForLine.Add(bls3);
                busStopsForLine.Add(bls4);
                busLineCollection.addBusLine(new BusLine(busStopsForLine));
                busStopsForLine.Clear();
            }
            //adding buses to lines so that 10 bus stops will have more than one line that goes past them
            j = 39;
            BusLineStop busLineStopExtra = new BusLineStop();
            foreach (BusLine bl in busLineCollection)
            {
                int codeLast = bl.ListStations[bl.ListStations.Count - 1].BusStationKey;
                busLineStopExtra = busStops[j] as BusLineStop;
                BusLineStop beforeStop = null;
                foreach(BusLineStop bls in bl.ListStations)
                {
                    if (bls.BusStationKey == codeLast) 
                        {
                        beforeStop = bls;
                        } 
                }
                bl.AddBusStop(busLineStopExtra,beforeStop);
                j = j - 4;
            }
            bool ans;
            bool tryAndParse;//for TryParse
            //---------------------
            MenuChoice myMenuChoice;
            int busOrStation;//will receive 0 for bus 1 for station
            do
            {
                Console.WriteLine("Enter one of the following:");
                foreach (MenuChoice MenuChoice in (MenuChoice[])Enum.GetValues(typeof(MenuChoice)))
                {
                    Console.WriteLine(MenuChoice);
                }
                ans = MenuChoice.TryParse(Console.ReadLine(), out myMenuChoice);
                switch (myMenuChoice)
                {
                    case MenuChoice.ADD:
                        int codeBusStopFirst;
                        int codeBusStop;
                        Console.WriteLine("Enter 0 for bus with new number,1 for adding new route for existing bus number, 2 for adding station ");
                        tryAndParse = int.TryParse(Console.ReadLine(), out busOrStation);
                        if (busOrStation == 0)
                        {
                            Console.WriteLine("Enter codes for stops,minimum two stops,insert -1 to stop");
                            List<BusLineStop> busLineStopList = new List<BusLineStop>();
                            tryAndParse = int.TryParse(Console.ReadLine(), out codeBusStopFirst);
                            BusLineStop busLineStopOne = null;
                            BusLineStop busLineStop = null;
                            foreach (BusLineStop bls in busStops)
                            {
                                if (bls.BusStationKey == codeBusStopFirst)
                                { busLineStopOne = bls; }
                            }
                            busLineStopList.Add(busLineStopOne);
                            do
                            {
                                tryAndParse = int.TryParse(Console.ReadLine(), out codeBusStop);
                                if (codeBusStop != -1)
                                {
                                    foreach (BusLineStop bls in busStops)
                                    {
                                        if (bls.BusStationKey == codeBusStop)
                                        { busLineStop = bls; }
                                    }
                                    //busLineStopList.Add(busLineStop);
                                    busLineStopList.Add(busLineStop);

                                }
                            }
                            while (codeBusStop != -1);
                            BusLine busLine = new BusLine(busLineStopList);
                            busLineCollection.addBusLine(busLine);
                        }
                        else if (busOrStation == 1)
                        {
                            Console.WriteLine("Insert bus that you want to add to it another route");
                            int busNumber;
                            tryAndParse = int.TryParse(Console.ReadLine(), out busNumber);
                            if (busLineCollection.busList.Contains(busLineCollection[busNumber][0]) == false)
                            {
                                throw new NotInListException("This bus line doesn't exist yet");
                            };
                            Console.WriteLine("Enter the codes of stations for new route,until -1 is inserted");
                            int codeStation;
                            List<BusLineStop> blsForNew = new List<BusLineStop>();
                            do
                            {
                                tryAndParse = int.TryParse(Console.ReadLine(), out codeStation);
                                if (codeStation != -1)
                                {
                                    BusLineStop busl = null;
                                    foreach (BusLineStop bls in busStops)
                                    {
                                        if (bls.BusStationKey == codeStation)
                                        {
                                            busl = bls;
                                            blsForNew.Add(busl);
                                        }
                                    }
                                }
                            } while (codeStation != -1);
                            busLineCollection.addBusDirection(busLineCollection[busNumber][0], blsForNew);
                        }
                        else
                        {
                            Console.WriteLine("Enter bus Number");
                            int busLineNumber;

                            tryAndParse = int.TryParse(Console.ReadLine(), out busLineNumber);
                            int busesSameNumberAmount = busLineCollection[busLineNumber].Count;
                            if (busesSameNumberAmount == 0) { throw new BusLineDoesNotExistException("The bus doesn't exist so you cannot add more routes"); }
                            BusLine lineToEdit = null;
                            if (busesSameNumberAmount > 1)
                            {
                                Console.WriteLine("Which bus out of the buses you want to add a stop to,you have" +
                                    "{0}", busesSameNumberAmount);
                                int numberBus;
                                tryAndParse = int.TryParse(Console.ReadLine(), out numberBus);
                                numberBus--;//user will insert 1 to n but list is from 0 to n-1
                                lineToEdit = busLineCollection[busLineNumber][numberBus];
                            }
                            else//if only one bus with the number exists
                            {
                                lineToEdit = busLineCollection[busLineNumber][0];
                            }
                            Console.WriteLine("Enter code of bus stop");
                            int busStopCode;
                            tryAndParse = int.TryParse(Console.ReadLine(), out busStopCode);
                            Console.WriteLine("Enter previous bus stop code");
                            int beforeStopNumber;
                            tryAndParse = int.TryParse(Console.ReadLine(), out beforeStopNumber);
                            BusLineStop beforeStop = null;
                            foreach (BusLineStop bls in busStops)
                            {
                                if (bls.BusStationKey == beforeStopNumber)
                                {
                                    beforeStop = bls;
                                }
                            }
                            if (!lineToEdit.inList(beforeStop)) { throw new NotInListException("The bus stop before the bus stop for insertion doesn't exist in bus line"); }
                            BusLineStop newStop = null;
                            foreach (BusLineStop bls in busStops)
                            {
                                if (bls.BusStationKey == busStopCode)
                                {
                                    newStop = bls;
                                }
                            }
                            lineToEdit.AddBusStop(newStop,beforeStop);
                        }

                        break;
                    case MenuChoice.DELETE:
                        Console.WriteLine("Enter 0 for deleting a bus line, or 1 for deleting station");
                        tryAndParse = int.TryParse(Console.ReadLine(), out busOrStation);
                        if (busOrStation == 0)
                        {
                            Console.WriteLine("Enter line for removal from list");
                            int lineForDeletion;
                            tryAndParse = int.TryParse(Console.ReadLine(), out lineForDeletion);
                            int busesSameNumberAmount = busLineCollection[lineForDeletion].Count;
                            if (busesSameNumberAmount == 0) { throw new BusLineDoesNotExistException("The bus doesn't exist so you cannot add more routes"); }
                            BusLine lineToDelete = null;
                            if (busesSameNumberAmount > 1)
                            {
                                Console.WriteLine("Which bus out of the buses you want to remove,you have" +
                                    "{0}", busesSameNumberAmount);
                                int numberBus;
                                tryAndParse = int.TryParse(Console.ReadLine(), out numberBus);
                                numberBus--;//user will insert 1 to n but list is from 0 to n-1
                                lineToDelete = busLineCollection[lineForDeletion][numberBus];
                            }
                            else
                            {
                                lineToDelete = busLineCollection[lineForDeletion][0];
                            }
                            List<BusStop> bsOnlyOnce = new List<BusStop>();//list of stations that only the line for deletion goes past them.They must be deleted from busList so that they won't be a bus stop without bus lines
                            foreach (BusStop bls in lineToDelete.ListStations)

                            {
                                List<BusLine> blStopStation = busLineCollection.busesStopStation(bls.BusStationKey);
                                if (blStopStation.Count == 1)//if the bus stop on line serves only line,it will be deleted since it has no bus that goes past it anymore
                                {
                                    bsOnlyOnce.Add(bls);
                                }
                            }
                            busLineCollection.removeBusLine(lineToDelete);
                            //removal of bus stops that don't have any bus going by them anymore
                            foreach(BusStop bs in bsOnlyOnce)
                            {
                                busStops.Remove(bs);
                            }
                        }
                        else if (busOrStation == 1)
                        {
                            Console.WriteLine("Enter line to be edited");
                            int lineToBeEdited;
                            tryAndParse = int.TryParse(Console.ReadLine(), out lineToBeEdited);

                            Console.WriteLine("Enter station for removal from bus list");
                            int stationForDeletion;
                            tryAndParse = int.TryParse(Console.ReadLine(), out stationForDeletion);
                            int stopForDeletion;
                            tryAndParse = int.TryParse(Console.ReadLine(), out stopForDeletion);
                            int busesSameNumberAmount = busLineCollection[lineToBeEdited].Count;
                            if (busesSameNumberAmount == 0) { throw new BusLineDoesNotExistException("The bus doesn't exist so you cannot add more routes"); }
                            BusLine lineToEdit = null;
                            if (busesSameNumberAmount > 1)
                            {
                                Console.WriteLine("Which bus out of the buses you want to remove a stop from,you have" +
                                    "{0}", busesSameNumberAmount);
                                int numberBus;
                                tryAndParse = int.TryParse(Console.ReadLine(), out numberBus);
                                numberBus--;//user will insert 1 to n but list is from 0 to n-1
                                lineToEdit = busLineCollection[lineToBeEdited][numberBus];
                            }
                            else
                            {
                                lineToEdit = busLineCollection[lineToBeEdited][0];
                            }
                            BusStop busStopForDeletion = null;
                            foreach (BusStop bs in lineToEdit.ListStations)
                            {
                                if (bs.BusStationKey == stationForDeletion)
                                {
                                    busStopForDeletion = bs;
                                    
                                }
                            }
                            BusLineStop bls = busStopForDeletion as BusLineStop;
                            List<BusLine> blStopStation = busLineCollection.busesStopStation(busStopForDeletion.BusStationKey);
                            if (blStopStation.Count == 1)//if the bus stop on line serves only line,it will be deleted since it has no bus that goes past it anymore
                            {
                                busStops.Remove(busStopForDeletion);
                            }
                            lineToEdit.RemoveBusStop(bls);
                        }
                        break;
                    case MenuChoice.SEARCH:
                        Console.WriteLine("Enter 0 for find all buses that go past bus stop or 1 to find out travel options between" +
                            " two bus stations sorted by lowest duration to highest duration");
                        tryAndParse = int.TryParse(Console.ReadLine(), out busOrStation);
                        if (busOrStation == 0)
                        {
                            Console.WriteLine("Enter code of station");
                            int codeStation;
                            tryAndParse = int.TryParse(Console.ReadLine(), out codeStation);
                            List<BusLine> blStopStation = busLineCollection.busesStopStation(codeStation);
                            foreach (BusLine bl in blStopStation)
                            {
                                Console.WriteLine(bl.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Enter code of source station");
                            int codeStationSource;
                            tryAndParse = int.TryParse(Console.ReadLine(), out codeStationSource);
                            Console.WriteLine("Enter code of destination station");
                            int codeStationDestination;
                            tryAndParse = int.TryParse(Console.ReadLine(), out codeStationDestination);
                            BusLineCollection suitableBuses = new BusLineCollection();
                            BusLineStop sourceBusStop = null;
                            BusLineStop destinationBusStop = null;
                            foreach (BusLineStop bls in busStops)
                            {
                                if (bls.BusStationKey == codeStationSource)
                                {
                                    sourceBusStop = bls;
                                }
                            }
                            foreach (BusLineStop bls in busStops)
                            {
                                if (bls.BusStationKey == codeStationDestination)
                                {
                                    destinationBusStop = bls;
                                }
                            }
                            foreach (BusLine bl in busLineCollection)
                            {
                                if (bl.inList(sourceBusStop) && bl.inList(destinationBusStop) &&
                                    bl.ListStations.IndexOf(sourceBusStop) < bl.ListStations.IndexOf(destinationBusStop))
                                {
                                    suitableBuses.addBusLine(bl);
                                }
                            }
                            suitableBuses.sortListLines();
                            foreach (BusLine bl in suitableBuses)
                            {
                                Console.WriteLine(bl);
                            }
                            if (suitableBuses.BusList.Count == 0)
                            {
                                Console.WriteLine("It is impossible to travel between these stations with one bus only");
                            }
                        }
                        break;
                    case MenuChoice.SHOW:
                        Console.WriteLine("Enter 0 for print all bus lines or 1 to print all stations and the bus lines that go past them");
                        tryAndParse = int.TryParse(Console.ReadLine(), out busOrStation);
                        if (busOrStation == 0)
                        {
                            foreach (BusLine bl in busLineCollection)
                            {
                                Console.WriteLine(bl.ToString());
                            }
                        }
                        if (busOrStation == 1)
                        {

                            foreach (BusLineStop bs in busStops)
                            {
                                Console.Write("Buses that go past " + bs.ToString() + " : ");
                                List<int> busesStopStation = new List<int>();//list for bus numbers that stop at station
                                List<BusLine> blines = busLineCollection.busesStopStation(bs.BusStationKey);
                                foreach (BusLine bl in blines)
                                {
                                    busesStopStation.Add(bl.BusNumber);
                                }
                                string busesPastStation = string.Join(",", busesStopStation);
                                Console.WriteLine(busesPastStation);
                            }
                        }
                        break;
                    default:
                        break;
                }
            } while (myMenuChoice != MenuChoice.EXIT);
        }
    }
}
