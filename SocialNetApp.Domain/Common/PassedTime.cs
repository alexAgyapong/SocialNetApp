namespace SocialNetApp.Domain.Common
{
    public class PassedTime
    {
        public const string Days = "days";
        public const string Hours = "hours";
        public const string Minutes = "minutes";
        public const string Seconds = "seconds";

        public int TimeAmount { get; }
        public string TimeUnit { get; set; }

        public PassedTime(int amount, string unit)
        {
            TimeAmount = amount;
            TimeUnit = unit;
        }
    }
}