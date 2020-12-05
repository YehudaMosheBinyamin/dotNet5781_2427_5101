using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_01_2427_5101
{
    public class Bus
    {

        //data members:
        Status state;
        public Status State { get { return state; } set { state= value; } }
        private readonly string  licenseNumber; // מספר רישוי
        private readonly bool  isDangerous; // האם מסוכן
        private DateTime start; // תאריך תחילת שרות
        private DateTime lastTreated; // תאריך טיפול אחרון
        private int allKmTrav; // קילומטרז
        private int kmSinceTreated; // קמ מאז טיפול אחרון
        private int kmPossible; // כמה קמ אפשר לנסוע כעת כתלות במצב הדלק
        static DateTime currentDate; // משתנה זה מצין את היום הנוכחי
        //------------------------------------------- 
        // set/get for  all...
        public string LicenseNumber

        { get { return licenseNumber; } }
      
        public bool IsDangerous
        {
            get
            { // אם עברה שנה או ש האוטובוס נסע יותר מ20,000 קמ אז מסוכן 
                //DateTime oneYear = new DateTime(1, 0, 0, 0, 0, 0);
                if ((kmSinceTreated >= 20000) || (lastTreated-(DateTime.Now)).TotalDays >365)
                {
                    return true;
                }
                return false; // אחרת לא מסוכן
            }
        }
        public DateTime Start
        {
            get { return start; }
            set { start = value; }
        }
        public DateTime LastTreated
        {
            get { return lastTreated; }
            set
            {
              
                lastTreated = value;
            }
        }
        public int AllKmTrav
        {
            get { return allKmTrav; }
            set
            {
                if (value < allKmTrav) // אי אפשר להוריד ערך מהקילומטראג
                {
                    throw new Exception("Not possible! - Negative number is not acceptable.");
                }
                allKmTrav = value;
            }
        }
        public int KmSinceTreated
        {
            get { return kmSinceTreated; }
            set
            {
                if (value < 0) // אי אפשר שיהיה ערך קטן מ0 בקילומטראג
                {
                    throw new Exception("Not possible! - Negative number is not acceptable.");
                }
                kmSinceTreated = value;
            }
        }
        public int KmPossible
        {
            get { return kmPossible; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Not possible! - Negative number is not acceptable.");
                 }
                if (value > 1200) // אי אפשר לעבור יותר מ1,200 קמ בלי תדלוק
                {
                    throw new Exception("Not possible! - the way is too long.");
                }
                kmPossible = value;
            }

        }

   

        //----------------------------------------
        //constructor:
        public Bus(string _licenseNumber, DateTime _start)
        {
            for(int i = 0; i < _licenseNumber.Length; i++)
            {
                if (_licenseNumber[i] > '9' || _licenseNumber[i] < '0') 
                {
                    throw new Exception("License number must be a number");
                }

            }
                DateTime reform = new DateTime(2018, 1, 1, 0, 0, 0); // תאריך כניסת הרפורמה על לוחות הרישוי
                if (_start < reform) // אם לפני הרפורמה אז
                {
                    if (_licenseNumber.Length!=7) // אם יש יותר מ7 ספרות אז מקבל חריגה
                    {   
                        throw new Exception("Must be 7 digits long!");
                    }
                    licenseNumber = _licenseNumber;
                }
                else // אם אחרי הרפורמה אז 
                {
                    if (_licenseNumber.Length!=8) // אם יותר מ8 ספרות מקבל חריגה
                    {
                        throw new Exception("Must be 8 digits long!");
                    }
                    this.licenseNumber = _licenseNumber;
                }
            start = _start;
            lastTreated = _start;
            allKmTrav = 0;
            kmSinceTreated = 0;
            kmPossible = 0;
            state = Status.Ready;
        }
        //----------------------------------------
        // פונקציות בסיסיות של האוטובוס: תדלוק ,טיפול,נסיעה  
        public  void drive(int km)
        {
            if (IsDangerous) // האם האוטובוס במצב לא תקין אז אי אפשר לבצעה נסיע
            {
                throw new Exception("The Bus is in a dangerous state and isn't available.");
            }
            allKmTrav += km;
            kmSinceTreated += km;
            kmPossible -= km;
        }
        public void treatment() // בטיפול מקבלים גם דלק
        {
            lastTreated = DateTime.Now;
            kmSinceTreated = 0;
            kmPossible = 0;
        }
        public void refueling()
        {
            KmPossible=1200;
        }

        public override string ToString()
        {
            {
                if (licenseNumber.Length == 7)
                {
                    string beginning = licenseNumber.Substring(0, 2);
                    string middle = licenseNumber.Substring(2, 3);
                    string end = licenseNumber.Substring(5, 2);
                    return string.Format("{0}-{1}-{2}", beginning, middle, end);
                }
                else
                {
                    string beginning = licenseNumber.Substring(0, 3);
                    string middle = licenseNumber.Substring(3, 2);
                    string end = licenseNumber.Substring(5, 3);
                    return string.Format("{0}-{1}-{2}", beginning, middle, end);


                }
            }
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        //------------------------------------------

    }
    public enum Status
    {
        Ready, Transit, Refilling, Treatment
    }
}

