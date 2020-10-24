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
        private int allKmTrav; // קילומטרג
        private int kmSinsTreat; // קמ מאז טיפול אחרון
        private int isFull; // האם המיכל מלא בדלק
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
                    throw "Licence Number not corecct! - Negative number not possible.";
                }
                DateTime reform = new DateTime(2018, 0, 0, 0, 0, 0); // תאריך כניסת הרפורמה על לוחות הרישוי
                if (start < reform) // אם לפני הרפורמה אז
                {
                    if (value > 9999999) // אם יש יותר מ7 ספרות אז מקבל חריגה
                    {
                        throw "Too manny diggets!";
                    }
                    licenceNumber = value;
                }
                else // אם אחרי הרפורמה אז 
                {
                    if (value > 99999999) // אם יותר מ8 ספרות מקבל חריגה
                    {
                        throw "Too manny diggets!";
                    }
                    licenceNumber = value;
                }
            }
        }
        public bool IsDengerous
        {
            get 
            { // אם עברה שנה או ש האוטובוס נסע יותר מ20,000 קמ אז מסוכן 
                DateTime oneYear = new DateTime(1, 0, 0, 0, 0, 0);
                if ((kmSinsTreat >= 20000) || (lastTreat - today > oneYear))
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
                    throw "Not passible! - the bus wasn't exist.";
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
                    throw "Not passible! - Negative number is not exepteble."
                }
                allKmTrav = value;
            }
        }
        public int KmSinsTreat
        {
            get { return kmSinsTreat; }
            set
            {
                if (value < 0) // אי אפשר שיהיה ערך קטן מ0 בקילומטראג
                {
                    throw "Not passible! - Negative number is not exepteble."
                }
                kmSinsTreat = value;
            }
        }
        public int IsFull
        {
            get { return IsFull; }
            set
            {
                if (value < 0) 
                {
                    throw "Not passible! - Negative number is not exepteble."
                }
                if ( value > 1200) // אי אפשר לעבור יותר מ1,200 קמ בלי תדלוק
                {
                    throw "Not passible! - the way is too long."
                }
                isFull = value;
            }
               
        }
        //----------------------------------------
        //constractor:
        public Bus(int _licenceNumber, DateTime _start)
        {
            licenceNumber = _licenceNumber;
            start = new DateTime(_start);
            lastTreat = new DateTime(_start);
            allKmTrav = 0;
            kmSinsTreat = 0;
            isFull = 0; 
        }
        //----------------------------------------
        // פונקציות בסיסיות של האוטובוס: תדלוק ,טיפול,נסיע  
        void drive(int km)
        {
            if (IsDengerous) // אם האוטובוס במצב לא תקין אז אי אפשר לבצעה נסיע
            {
                throw "The Bus is in a dengerous stat and isn't avalible."
            }
            allKmTrav += km;
            kmSinsTreat += km;
            isFull += km;
        }
        void treatment() // בטיפול מקבלים גם דלק
        {
            lastTreat = DateTime.now;
            kmSinsTreat = 0;
            isFull = 0;
        }
        void refueling()
        {
            isFull = 0;
        }
        //------------------------------------------
    }
}

