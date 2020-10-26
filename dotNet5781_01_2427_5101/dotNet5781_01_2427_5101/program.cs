using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet5781_01_2427_5101
{

    enum choice
    {
        EXIT,ADD,TRAVEL,TREATMENT,SHOW
    }
    class program
    {
      
        static void Main(string[] arg)
        {int choice;
         List<Bus> busList=new List<Bus>();
         
        do{Console.WriteLine("For add a bus enter 1 to travel-2 for treatment-3 for show -4 for exit-0");
         choice=Console.ReadLine();
         int licensePlate;
          switch(choice)
            {
            case EXIT:
                break;
            case ADD:Console.WriteLine("Enter license plate and date of start");
                licensePlate=Console.ReadLine();
                DateTime currentTime=Console.ReadLine();
                busList.Add(new Bus(licensePlate,currentTime));
                break;
            case TRAVEL:Console.WriteLine("Insert license number of bus");
                    licensePlate=Console.ReadLine();
                     Random r=new Random(DateTime.Now.Milisecond);//generates random number 
                    int randomKmJourney=r.Next(1201);//random number for trip's length
                    int indexBus=busList.FindIndex(licensePlate);
                   if(indexBus==-1)
                    {
                     Console.WriteLine("This bus doesn't exist,try again");
                     continue;
                    }
                    
                    if(busList[indexBus].PossibleKm<randomKmJourney)
                    {
                        Console.WriteLine("Bus doesn't have enough gas for journey");
                        continue;
                    }
                    if(busList[indexBus].IsDangerous)
                        {
                    Console.WriteLine("Bus is dangerous cannot be taken for journey");
                    continue;}
                    busList[indexBus].travel();
                    break;
           case TREATMENT:
                    Console.WriteLine("Enter license plate and date of start");
                    licensePlate=Console.ReadLine();
                    Console.WriteLine("Insert 0 to refill, any other number  to maintain");
                    int refillOrMaintain=Console.ReadLine();
                     indexBus=busList.FindIndex(refillOrMaintain);
                        if (refillOrMaintain==0)
                        {
                         busList[indexBus].refill();
                        }
                        else{
                             busList[indexBus].treatment();    
                            }
                        break;
           case SHOW:
                    Console.WriteLine("List of buses and their kilometrage since their last treatment");
                    foreach(Bus bus in busList)
                     {Console.WriteLine("License number: {} Km since treated: {} ",bus.LicenseNumber,bus.KmSinceTreat);
                           
                      }   
                    break;
            
          }

        }while(choice!=0);
    }
}
}
