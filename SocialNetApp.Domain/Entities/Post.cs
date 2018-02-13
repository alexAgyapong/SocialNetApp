using SocialNetApp.Domain.Common;
using System;

namespace SocialNetApp.Domain.Entities
{
    public class Post
    {
        private readonly IPublishedTimer timer;
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime PublishedTime { get; set; }

        public Post(string username, string message, IPublishedTimer timer)
        {
            this.timer = timer;

            this.Username = username;
            this.Message = message;
            this.PublishedTime = timer.CurrentTime();
        }

        public Post(string username, string message)
        {
            this.Username = username;
            this.Message = message;
            this.PublishedTime = DateTime.Now;
        }

        public string ToPostFormat()
        {
            var elapsedTime = timer.GetTimePassed(PublishedTime);
            return $"{Message} ({elapsedTime.TimeAmount} {elapsedTime.TimeUnit} ago)";
        }

        public virtual string ToWallFormat()
        {
            var elapsedTime = timer.GetTimePassed(PublishedTime);
            return $"{Username} - {Message} ({elapsedTime.TimeAmount} {elapsedTime.TimeUnit} ago)";
        }
    }
}