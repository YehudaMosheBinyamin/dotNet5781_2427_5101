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
            int numOfStations = 40;
            List<BusLineStop> busStops = new List<BusLineStop>();
            BusLineCollection busLineCollection = new BusLineCollection();
            //---------------------                                                                                                                                           
            for (int i = 0; i < numOfStations; i++)
            {

                busStops.Add(new BusLineStop());
            }
            List<BusLineStop> busStopsForLine = new List<BusLineStop>();
            BusLineStop bls1=new BusLineStop();
            BusLineStop bls2=new BusLineStop();
            BusLineStop bls3=new BusLineStop();
            BusLineStop bls4=new BusLineStop();
            for (int i = 0; i < numOfBuses; i++)
            {
                bls1 = busStops[i];
                bls2 = busStops[i + 1];
                 bls3 = busStops[i + 2];
                bls4 = busStops[i + 3];
                busStopsForLine.Add(bls1);
                busStopsForLine.Add(bls2);
                busStopsForLine.Add(bls3);
                busStopsForLine.Add(bls4);
                busLineCollection.addBusLine(new BusLine(busStopsForLine));
                busStopsForLine.Clear();
            }
            bool ans;
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
                        Console.WriteLine("Enter 0 for bus 1 for adding station");
                        busOrStation = Convert.ToInt32(Console.ReadLine());
                        if (busOrStation == 0)
                        {
                            Console.WriteLine("Enter codes for stops,minimum two stops,insert -1 to stop");

                            List<BusLineStop> busLineStopList = new List<BusLineStop>();
                            codeBusStopFirst = Convert.ToInt32(Console.ReadLine());
                            BusLineStop busLineStopOne = null;
                            BusLineStop busLineStop = null;
                            foreach (BusLineStop bls in busStops)
                            {
                                if (bls.BusStationKey == codeBusStopFirst)
                                { busLineStopOne = bls; }
                            }
                            busLineStopList.Add(busLineStopOne);
                            BusLine busLine = new BusLine(busLineStopList);
                            do
                            {
                                codeBusStop = Convert.ToInt32(Console.ReadLine());
                                if (codeBusStop == -1)
                                {
                                    foreach (BusLineStop bls in busStops)
                                    {
                                        if (bls.BusStationKey == codeBusStop)
                                        { busLineStop = bls; }
                                    }
                                    busLineStopList.Add(busLineStop);
                                    busLineCollection.addBusLine(busLine);
                                }
                            }
                            while (codeBusStop != 1);
                        }
                        else
                        {


                            Console.WriteLine("Enter bus Number");
                            int busLineNumber = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter code of bus stop");
                            int busStopCode = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter what number stop will be bus stop");
                            int lineStopNumber = Convert.ToInt32(Console.ReadLine());
                            BusLineStop busl = null;
                            foreach (BusLineStop bls in busStops)
                            {
                                if (bls.BusStationKey == busStopCode)
                                { busl = bls; }
                            }
                            busLineCollection[busLineNumber].AddBusStop(busl, lineStopNumber);
                        }
                        break;
                    case MenuChoice.DELETE:
                        Console.WriteLine("Enter 0 for bus 1 for station");
                        busOrStation = Convert.ToInt32(Console.ReadLine());

                        if (busOrStation == 0)
                        {
                            Console.WriteLine("Enter line for removal from list");
                            int lineForDeletion = Convert.ToInt32(Console.ReadLine());
                            busLineCollection.removeBusLine(busLineCollection[lineForDeletion]);
                        }
                        if (busOrStation == 1)
                        {
                            Console.WriteLine("Enter line to be edited");
                            int lineToBeEdited = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter station for removal from bus list");
                            int stationForDeletion = Convert.ToInt32(Console.ReadLine());
                            busLineCollection[lineToBeEdited].RemoveBusStop(busLineCollection[lineToBeEdited].ListStations[stationForDeletion]);
                        }
                        break;
                    case MenuChoice.SEARCH:
                        Console.WriteLine("Enter 0 for find all buses that go past bus stop or 1 to find out travel options between" +
                            " two bus stations sorted by lowest duration to highest duration");
                        busOrStation = Convert.ToInt32(Console.ReadLine());
                        if (busOrStation == 0)
                        {   
                            Console.WriteLine("Enter code of station");
                            int codeStation = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine(busLineCollection.busesStopStation(codeStation));
                        }
                        else
                        {
                            Console.WriteLine("Enter code of source station");
                            int codeStationSource = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter code of destination station");
                            int codeStationDestination = Convert.ToInt32(Console.ReadLine());
                            BusLineCollection suitableBuses = new BusLineCollection();
                            BusLineStop sourceBusStop = null;
                            BusLineStop destinationBusStop = null;

                            foreach (BusLineStop bls in busStops)
                            {
                                if (bls.BusStationKey == codeStationSource)
                                { sourceBusStop = bls; }
                            }
                            foreach (BusLineStop bls in busStops)
                            {
                                if (bls.BusStationKey == codeStationDestination)
                                { destinationBusStop = bls; }
                            }
                            foreach (BusLine bl in busLineCollection)
                            {
                                if (bl.inList(sourceBusStop) && bl.inList(destinationBusStop))
                                {
                                    suitableBuses.addBusLine(bl);
                                }


                            }
                            suitableBuses.sortListLines();
                            foreach (BusLine bl in suitableBuses)
                            {
                                Console.WriteLine(bl);
                            }
                        }
                        break;
                    case MenuChoice.SHOW:
                        Console.WriteLine("Enter 0 for print all bus lines or 1 to print all stations and the bus lines that go past them");
                        busOrStation = Convert.ToInt32(Console.ReadLine());

                        if (busOrStation == 0)
                        {
                            List<BusLineStop> busLineStops = new List<BusLineStop>();
                            foreach (BusLine bl in busLineCollection)
                            {
                                Console.WriteLine(bl.ToString());
                            }
                        }
                        if (busOrStation == 1)
                        {
                            foreach (BusStop bs in busStops)
                            {
                                busLineCollection.busesStopStation(bs.BusStationKey);
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
