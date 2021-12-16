using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    internal class Clock
    {
        internal volatile bool Cancel;//For stopping clock
        TimeSpan currentTime;
        Clock(TimeSpan currentTime)
        {
            this.currentTime = currentTime;
        }
    }
}
