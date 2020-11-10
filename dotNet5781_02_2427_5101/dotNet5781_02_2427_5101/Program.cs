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
            int numOfStasions = 50;
            List<BusStop> busStops;
            List<BusLine> busLines;
            //---------------------
            for (int i = 0; i < numOfStasions; i++)
            {
                BusStop tempStasion = new BusStop();
                busStops.Add(tempStasion);
            }
            for (int i = 0; i < numOfBuses; i++)
            {
                BusLine tempBus = new BusLine();
                busLines.Add(tempBus);
            }
            //---------------------
            menuChoice myMenuChoice;
            busOrStasion myBusOrStasion;
            do
            {
                myMenuChoice = GetMenuChoice();
                switch (myMenuChoice)
                {
                    case menuChoice.ADD:
                        myBusOrStasion = GetBusOrStasion();
                        if (myBusOrStasion == busOrStasion.BUS) { addBus(busLines); }
                        if (myBusOrStasion == busOrStasion.STASION) { addStasion(busStops); }
                        break;
                    case menuChoice.DELETE:
                        myBusOrStasion = GetBusOrStasion();
                        if (myBusOrStasion == busOrStasion.BUS) { deleteBus(busLines); ; }
                        if (myBusOrStasion == busOrStasion.STASION) { deleteStasion(busStops); }
                        break;
                    case menuChoice.SEARCH:
                        myBusOrStasion = GetBusOrStasion();
                        if (myBusOrStasion == busOrStasion.BUS) { searchBus(busLines); }
                        if (myBusOrStasion == busOrStasion.STASION) { searchStasion(busStops); }
                        break;
                    case menuChoice.SHOW:
                        myBusOrStasion = GetBusOrStasion();
                        if (myBusOrStasion == busOrStasion.BUS) { showBus(busLines); }
                        if (myBusOrStasion == busOrStasion.STASION) { showStasion(busStops); }
                        break;
                    default:
                        break;
                }
            } while (myMenuChoice != 0) ;     
        }

        private static void showStasion(List<BusStop> busStops)
        {
            throw new NotImplementedException();
        }

        private static void showBus(List<BusLine> busLines)
        {
            throw new NotImplementedException();
        }

        private static void searchStasion(List<BusStop> busStops)
        {
            throw new NotImplementedException();
        }

        private static void searchBus(List<BusLine> busLines)
        {
            throw new NotImplementedException();
        }

        private static void deleteStasion(List<BusStop> busStops)
        {
            throw new NotImplementedException();
        }

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
    