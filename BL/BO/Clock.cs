using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BL
{
    sealed internal class Clock
    {
        public TimeSpan Time { get; set; }
        public int Rate { get; set; }
        internal volatile bool Cancel;
        private event Action<TimeSpan> timeChangeEvent;
        public event Action<TimeSpan> TimeChangeEvent
            {
            add
            {
                timeChangeEvent = value;
            }
            remove
            {
                timeChangeEvent -= value;
            }
            }

        public void DoTimeChangeEvent()
        {
            timeChangeEvent?.Invoke(Time);
        }
        public Clock(TimeSpan time)
        {
            Time= time;
        }
    }
}

