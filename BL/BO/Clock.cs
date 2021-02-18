using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    sealed class Clock
    {  
        public TimeSpan Time{ get; set; }
        internal volatile bool Cancel;
        public event EventHandler TimeChanged;
        public Clock(TimeSpan time)
        {
            Time = time;
        }
    }
}
