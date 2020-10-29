using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet5781_01_2427_5101
{

    enum Choice
    {
        EXIT,ADD,TRAVEL,TREATMENT,SHOW
    }
    class program
    {

        static void Main(string[] arg)
        {
            Choice choice;//for menu
            bool ans;//for checking whether TryParse is possible
            List<Bus> busList = new List<Bus>();
            Bus chosenBus=null;
            bool busInList = false;
            do
            {
                Console.WriteLine("Enter one of the following:");
                foreach(Choice cho in (Choice[])Enum.GetValues(typeof(Choice))) { Console.WriteLine(cho); }
                
                ans = Choice.TryParse(Console.ReadLine(), out choice);
                string licensePlate;
                DateTime current;//date and time of adding bus
                switch (choice)
                {
                    case Choice.EXIT:
                        break;
                    case Choice.ADD:
                        {
                            Console.WriteLine("Enter license plate ");
                            licensePlate = Console.ReadLine();
                            Console.WriteLine("Enter date of start:");
                            bool convert=DateTime.TryParse(Console.ReadLine(),out current);
                            if (convert == false) 
                            {
                                throw new Exception("must give time and date in DateTime format");
                             }
                            //busList.Add(new Bus(licensePlate, DateTime.Now));
                            AddBus(current,licensePlate, busList);
                            break;
                        }
                    case Choice.TRAVEL:
                        {
                            if (busList == null) { throw new Exception("Cannot search in null list"); }
                            Console.WriteLine("Insert license number of bus");
                            licensePlate = Console.ReadLine();
                            Random r = new Random(DateTime.Now.Millisecond);//generates random number 
                            foreach (Bus bus in busList)
                            {
                                if (bus.LicenseNumber == licensePlate)
                                {
                                    chosenBus = bus;
                                    busInList = true;
                                }
                            }
                            if (chosenBus==null)
                            {
                                Console.WriteLine("This bus doesn't exist,try again");
                                break;
                            }
                            
                                int randomKmJourney = r.Next(1201);//random number for trip's length
                                if (chosenBus.KmPossible < randomKmJourney)
                                {
                                    Console.WriteLine("Bus doesn't have enough gas for journey");
                                    break;
                                }
                            if (chosenBus.IsDangerous)
                            {
                                Console.WriteLine("Bus is dangerous cannot be taken for journey");
                                break;
                            }
                            chosenBus.drive(randomKmJourney);  
                            break;
                            
                        }
                    case Choice.TREATMENT:
                        {
                            Console.WriteLine("Enter license plate");
                            licensePlate = Console.ReadLine();
                            Console.WriteLine("Insert 0 to refill, any other number  to maintain");
                            string refillOrMaintain = Console.ReadLine();
                            if (busList == null) { throw new Exception("Cannot search in null list"); }
                            foreach (Bus bus in busList)
                            {
                                if (bus.LicenseNumber == licensePlate)
                                {
                                    chosenBus = bus;
                                    busInList = true;
                                }
                            }
                            if (!busInList)
                            {
                                Console.WriteLine("This bus doesn't exist,try again");
                            }
                            else
                            {
                                if (refillOrMaintain == "0")
                                {
                                    chosenBus.refueling();
                                }
                                else
                                {
                                    chosenBus.treatment();
                                }
                                break;
                            }
                            
                            break;
                        }
                    case Choice.SHOW:
                        {
                            if (busList == null) { throw new Exception("Cannot search in null list"); }
                            Console.WriteLine("List of buses and their kilometrage since their last treatment");
                            foreach (Bus bus in busList)
                            {
                                Console.WriteLine("License number: {0} Km since treated: {1} ", bus.ToString(),
                                    bus.KmSinceTreated);

                            }
                            break;
                        }
                    default:
                        break;
                }

            } while (choice !=Choice.EXIT);
        }

        private static void AddBus(DateTime currentTime,string licensePlate, List<Bus> busList)
        {
            busList.Add(new Bus(licensePlate, currentTime));
        }
    }
}
