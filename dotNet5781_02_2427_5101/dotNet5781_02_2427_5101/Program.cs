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
            List<BusLine> busLines = new List<BusLine>();
            //---------------------                                                                                                                                           
            for (int i = 0; i < numOfStations; i++)
            {
                BusLineStop tempStation = new BusLineStop();
                busStops.Add(tempStation);
            }
            for (int i = 0; i < numOfBuses; i++)
            {
                BusLineStop bls1 = new BusLineStop();
                BusLineStop bls2 = new BusLineStop();
                BusLine tempBus = new BusLine(bls1,bls2);
                busLines.Add(tempBus);
            }
            BusLineCollection busLineCollection = new BusLineCollection();
            busLineCollection.BusList = busLines;
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
                    //case MenuChoice.ADD:
                        //Console.WriteLine("Enter 0 for bus 1 for station");
                       // busOrStation = Convert.ToInt32(Console.ReadLine());
                       // if (busOrStation == 0) { busLineCollection.addBusLine(); }
                       // if (busOrStation == 1) { addStation(busStops); }
                       // break;
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
                            Console.WriteLine("Enter line o be edited");
                            int lineToBeEdited = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter station for removal from bus list");
                            int stationForDeletion = Convert.ToInt32(Console.ReadLine());
                            busLineCollection[lineToBeEdited].RemoveBusStop(busLineCollection[lineToBeEdited].ListStations[stationForDeletion]);
                        }
                        break;
                    //case MenuChoice.SEARCH:
                       // myBusOrStation = GetBusOrStation();
                       // if (myBusOrStation == busOrStasion.BUS) { searchBus(busLines); }
                       // if (myBusOrStation == busOrStasion.STASION) { searchStasion(busStops); }
                        //break;
                   // case MenuChoice.SHOW:
                      //  myBusOrStation = GetBusOrStation();
                      //  if (myBusOrStation == busOrStasion.BUS) { showBus(busLines); }
                       // if (myBusOrStation == busOrStasion.STASION) { showStasion(busStops); }
                       // break;
                    //default:
                      //  break;
                }
            } while (myMenuChoice != 0);
        }
        //bus line from two bus stops
        BusLine makeLine(BusLineStop stop1, BusLineStop stop2)
        {
            BusLine newLine = new BusLine(stop1, stop2);
            return newLine;
        }
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

    }
} 
    