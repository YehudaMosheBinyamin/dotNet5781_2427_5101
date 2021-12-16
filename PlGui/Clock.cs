using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    class Clock
    {   
        TimeSpan TimeStarted { get; set; }
        internal volatile bool Cancel;
        public event EventHandler TimeChanged;
        public Clock(TimeSpan time)
        {
            TimeStarted = time;
        }
    }
}
