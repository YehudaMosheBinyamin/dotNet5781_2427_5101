using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{   [Serializable]
    public class BusLineDoesNotExistException : Exception
    {
        public BusLineDoesNotExistException() : base() { }
        public BusLineDoesNotExistException(string exception):base(exception){}
     
    }
}
