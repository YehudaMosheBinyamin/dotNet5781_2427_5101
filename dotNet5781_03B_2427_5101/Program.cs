using dotNet5781_01_2427_5101;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_2427_5101
{
    class Program
    {
        public static void main(string[] args)
        {  List<string> licenseNotPossible = new List<string>();
        List<Bus> busList = new List<Bus>();
        Bus randomBus = null;
        string licenseNumber=null;
        DateTime randomDate;
            for (int i = 0; i < 10; i++)
            { randomDate = RandomDate();
                licenseNumber = RandomLicense();
                randomBus = new Bus(licenseNumber, randomDate);
                busList.Add(randomBus); 
               
            }
            busList[7].KmPossible = 1;//low gasoline so can only travel 1 km
            busList[8].KmSinceTreated = 19950;//close to need treatment almost 20000 km
            busList[9].LastTreated = busList[9].Start.AddDays(-1);//start after last treated
         //foreach (Choice cho in (Choice[])Enum.GetValues(typeof(Choice))) { Console.WriteLine(cho); }
              string RandomLicense()
                { string randomLicense;
                 do
                 {
                        Random random = new Random(DateTime.Now.Millisecond);
                        randomLicense = Convert.ToString(random.Next(0000000, 99999999));
                 } while (licenseNotPossible.Contains(randomLicense));
                    return licenseNumber;
                }

         DateTime RandomDate()
    {
        Random random = new Random(DateTime.Now.Millisecond);
        DateTime minDate = new DateTime(1, 1, 1970);
        DateTime today = DateTime.Now;
        //int year = random % (int(today.Year - minDate.Year)+1) + minDate.Year;
        int range = (today - minDate).Days;
        return minDate.AddDays(random.Next(range));
    }


}
}
