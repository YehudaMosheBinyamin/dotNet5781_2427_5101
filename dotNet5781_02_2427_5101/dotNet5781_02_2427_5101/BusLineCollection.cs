using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{
    public class BusLineCollection : IEnumerable
    {
            BusLine[] busList = new BusLine[50];
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyEnmrtr(this);

        }

        public class MyEnmrtr : IEnumerator
            {
                BusLineCollection coll;
                int cntr = -1;
                internal MyEnmrtr(BusLineCollection coll) { this.coll = coll; }
                public void Reset() { } 
                public object Current { get { return coll.busList[cntr]; } }
                public bool MoveNext() { return (++cntr < coll.busList.Length); }
            }
        }
    }
