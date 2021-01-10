using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{   [Serializable]
    public class NotInListException:Exception
    {
       public  NotInListException() : base() { }
       public  NotInListException(string exception) : base(exception) { }
    }
}
