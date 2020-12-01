using dotNet5781_01_2427_5101;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_2427_5101
{
    public static class Functions
    {
        public static string RandomLicense(ObservableCollection<string> licenseNotPossible,int lengthOfLicense)
        {
            string randomLicense;
            Random random = new Random();
            if (lengthOfLicense == 7)
            {

                randomLicense = (random.Next(0000000, 9999999)).ToString("D7");
                while (licenseNotPossible.Contains(randomLicense))
                { 
                    randomLicense = (random.Next(0000000, 9999999)).ToString("D7"); }
                }
            else if (lengthOfLicense == 8)
            {
                randomLicense = (random.Next(00000000, 99999999)).ToString("D8");
                while (licenseNotPossible.Contains(randomLicense))
                { 
                    randomLicense = (random.Next(00000000, 99999999)).ToString("D8"); 
                }
            }
               
                else throw new Exception("Length is illegal");
            return randomLicense;
        }
        public static DateTime RandomDate(DateTime today)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            DateTime minDate = new DateTime(1999,1,1);
            int range = (today.Subtract(minDate)).Days;
            return minDate.AddDays(random.Next(range));
        }
    }
}
