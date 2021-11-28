using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public static class Functions
    {
        static Random random=new Random();
        public static float randomDistance()
        {
            int randomDistance = random.Next(100 , 10000);
            float distance = randomDistance / 1000;
            return distance;
        }

        /// <summary>
        /// Calculates random minutes of travel as a function of the distance
        /// </summary>
        /// <param name="distance">Decimal distance</param>
        /// <returns>minutesTravel Minutes it will take on average to travel distance of "distance"</returns>
        public static TimeSpan MinutesOfTravel(float distance)
        {   //t[min] = 60 * s[km] / v[km / H]
            float distanceKm = distance;
            int randomSpeed = random.Next(30, 70);
            int minutesInHour = 60;
            int minutesOfTravel = (int)Math.Ceiling(minutesInHour * distanceKm / randomSpeed);
            TimeSpan minutesTravel = new TimeSpan(0, minutesOfTravel, 0);
            return minutesTravel;
        }
        /// <summary>
        /// Calculates a random amount of time in seconds between slightly bigger than 0.9*originalWaitingTime(earlier by about 10%)  
        /// to just  2*originalWaitingTime (late and will take twice the amount of time to reach station)
        /// </summary>
        /// <param name="originalWaitingTime">Time from previous station to current station</param>
        /// 
        public static int DelayOrEarlyArrival(TimeSpan originalWaitingTime)
        {
            int hours = originalWaitingTime.Hours;
            int minutes = originalWaitingTime.Minutes;
            int seconds = originalWaitingTime.Seconds;
            int totalSeconds = (hours * 3600) + (minutes * 60) + seconds;
            int newSecondsTotal= (int)Math.Ceiling(totalSeconds*(random.NextDouble() * (2.0 - 0.9) + 0.9));//New time expressed in seconds
            return newSecondsTotal;
        }
    }
}
