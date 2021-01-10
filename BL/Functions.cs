using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public static class Functions
    {
        public static float randomDistance()
        {
            Random random = new Random();
            int randomDistance = random.Next(100 , 10000);
            float distance = randomDistance / 1000;
            return distance;

        }
        public static TimeSpan MinutesOfTravel(float distance)
        {   //t[min] = 60 * s[km] / v[km / H]
            float distanceKm = distance;
            Random speed = new Random();
            int randomSpeed = speed.Next(30, 70);
            int minutesInHour = 60;
            int minutesOfTravel = (int)Math.Ceiling(minutesInHour * distanceKm / randomSpeed);
            TimeSpan minutesTravel = new TimeSpan(0, minutesOfTravel, 0);
            return minutesTravel;
        }
    }
}
