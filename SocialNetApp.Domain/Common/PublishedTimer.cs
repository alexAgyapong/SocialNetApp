using System;

namespace SocialNetApp.Domain.Common
{
    public class PublishedTimer : IPublishedTimer
    {
        public DateTime CurrentTime()
        {
            return DateTime.Now;
        }

        public PassedTime GetTimePassed(DateTime publishedTime)
        {
            var now = DateTime.Now;
            if (MoreThanAMinutePassed(publishedTime, now))
            {
                return MinutesBasedTimePassed(publishedTime, now);
            }

            return SecondsBasedTimePassed(publishedTime, now);
        }

        private bool MoreThanAMinutePassed(DateTime publishedTime, DateTime now)
        {
            return now.Subtract(publishedTime).Minutes > 0;
        }

        
        private PassedTime MinutesBasedTimePassed(DateTime publishedTime, DateTime now)
        {
            var numberOfMinutes = GetNumberOfMinutes(publishedTime, now);
            var numberOfHours = GetNumberOfHours(publishedTime, now);
            if (numberOfMinutes <= 60)
            {
                return new PassedTime(numberOfMinutes, PassedTime.Minutes);
            }
            return new PassedTime(numberOfHours, PassedTime.Hours);
        }

        private int GetNumberOfMinutes(DateTime publishedTime, DateTime now)
        {
            return (now.Hour - publishedTime.Hour) * 60 + now.Minute - publishedTime.Minute;
        }

        private PassedTime SecondsBasedTimePassed(DateTime publishedTime, DateTime now)
        {
            var numberOfSeconds = GetNumberOfSeconds(publishedTime, now);
            return new PassedTime(numberOfSeconds, PassedTime.Seconds);
        }

        private int GetNumberOfSeconds(DateTime publishedTime, DateTime now)
        {
            return (now.Minute - publishedTime.Minute) * 60 + now.Second - publishedTime.Second;
        }

        private int GetNumberOfHours(DateTime publishedTime, DateTime now)
        {
            return (now.Hour - publishedTime.Hour) + now.Hour - publishedTime.Hour;
        }
    }
}