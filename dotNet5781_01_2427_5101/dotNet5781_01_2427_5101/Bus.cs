using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_2427_5101
{
    class Bus
    {
        //data members:
        private int licenceNumber; // מספר רישוי
        private bool isDangerous; // האם מסוכן
        private DateTime start; // תאריך תחילת שרות
        private DateTime lastTreated; // תאריך טיפול אחרון
        private int allKmTrav; // קילומטרז
        private int kmSinceTreated; // קמ מאז טיפול אחרון
        private int kmPossible; // כמה קמ אפשר לנסוע כעת כתלות במצב הדלק
        static DateTime currentDate = new DateTime.now; // משתנה זה מצין את היום הנוכחי
        //-------------------------------------------
        // set/get for  all...
        public int LicenceNumber
        {
            get { return licenceNumber; }
            set
            {
                if (value < 0) // אם שלילי אז מקבל חריגה
                {
                    throw "Licence Number not correct! - Negative number not possible.";
                }
                DateTime reform = new DateTime(2018, 0, 0, 0, 0, 0); // תאריך כניסת הרפורמה על לוחות הרישוי
                if (start < reform) // אם לפני הרפורמה אז
                {
                    if (value > 9999999) // אם יש יותר מ7 ספרות אז מקבל חריגה
                    {
                        throw "Too many digits!";
                    }
                    licenceNumber = value;
                }
                else // אם אחרי הרפורמה אז 
                {
                    if (value > 99999999) // אם יותר מ8 ספרות מקבל חריגה
                    {
                        throw "Too many digits!";
                    }
                    licenceNumber = value;
                }
            }
        }
        public bool IsDangerous
        {
            get 
            { // אם עברה שנה או ש האוטובוס נסע יותר מ20,000 קמ אז מסוכן 
                DateTime oneYear = new DateTime(1, 0, 0, 0, 0, 0);
                if ((kmSinceTreat >= 20000) || (lastTreat - today > oneYear))
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
        public DateTime LastTreat
        {
            get { return lastTreat; }
            set
            {
                if (value < start) // לא אפשרי שטיפול יהיה לפני שנקנה
                {
                    throw "Not possible! - the bus didn't exist yet.";
                }
                lastTreat = value;
            }
        }
        public int AllKmTrav
        {
            get { return allKmTrav; }
            set
            {
                if (value < allKmTrav) // אי אפשר להוריד ערך מהקילומטראג
                {
                    throw "Not possible! - Negative number is not acceptable."
                }
                allKmTrav = value;
            }
        }
        public int KmSinceTreat
        {
            get {return kmSinceTreat;}
            set
            {
                if (value < 0) // אי אפשר שיהיה ערך קטן מ0 בקילומטראג
                {
                    throw "Not possible! - Negative number is not acceptable."
                }
                kmSinsTreat = value;
            }
        }
        public int KmPossible
        {
            get { return kmPossible; }
            set
            {
               if (value < 0) 
                {
                    throw "Not possible! - Negative number is not acceptable."
                }
                if ( value > 1200) // אי אפשר לעבור יותר מ1,200 קמ בלי תדלוק
                {
                    throw "Not possible! - the way is too long."
                }
               kmPossible = value;
            }
               
        }
        //----------------------------------------
        //constructor:
        public Bus(int _licenceNumber, DateTime _start)
        {
            licenceNumber = _licenceNumber;
            start = new DateTime(_start);
            lastTreat = new DateTime(_start);
            allKmTrav = 0;
            kmSinceTreat = 0;
            kmPossible=0; 
        }
        //----------------------------------------
        // פונקציות בסיסיות של האוטובוס: תדלוק ,טיפול,נסיע  
        void drive(int km)
        {
            if (IsDangerous) // אם האוטובוס במצב לא תקין אז אי אפשר לבצעה נסיע
            {
                throw "The Bus is in a dangerous state and isn't available."
            }
            allKmTrav += km;
            kmSinceTreat += km;
            //isFull += km;
            kmPossible -= km;
        }
        void treatment() // בטיפול מקבלים גם דלק
        {
            lastTreat = DateTime.now;
            kmSinceTreat = 0;
            kmPossible = 0;
        }
        void refueling()
        {
            KmPossible(1200);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

       
        //------------------------------------------
    }
}

