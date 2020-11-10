using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_2427_5101
{
    public class NoLineException:Exception
    {
        public NoLineException() {
        }
        public NoLineException(string expMessage):
            base(expMessage) 
        {
        }
    }
}
