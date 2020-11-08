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

        private static void deleteBus(List<BusLine> busLines)
        {
            throw new NotImplementedException();
        }

        private static void addStasion(List<BusStop> busStops)
        {
            throw new NotImplementedException();
        }

        private static void addBus(List<BusLine> busLines)
        {
            throw new NotImplementedException();
        }



        //--------------------------------------------
        public static menuChoice GetMenuChoice()
        {
            int myMenuChoice;
            bool isCorect;
            do
            {
                Console.WriteLine("Type your choice:");
                Console.WriteLine("0 - Exit program.");
                Console.WriteLine("1 - Add a bus or stasion.");
                Console.WriteLine("2 - Delete a bus or stasion.");
                Console.WriteLine("3 - Search a bus or stasion.");
                Console.WriteLine("4 - Show informationabut a bus or stasion.");
                isCorect = int.TryParse(Console.ReadLine(), out myMenuChoice);
            } while (!isCorect || myMenuChoice < 0 || myMenuChoice > 5);
            return (menuChoice)myMenuChoice;
        }
        public static busOrStasion GetBusOrStasion()
        {
            int myBusOrStasion;
            bool isCorect;
            do
            {
                Console.WriteLine("Type your choice:");
                Console.WriteLine("0 - Bus.");
                Console.WriteLine("1 - Stasion.");
                isCorect = int.TryParse(Console.ReadLine(), out myBusOrStasion);
            } while (!isCorect || myBusOrStasion < 0 || myBusOrStasion > 1);
            return (busOrStasion)myBusOrStasion;
        }
    }
    
}