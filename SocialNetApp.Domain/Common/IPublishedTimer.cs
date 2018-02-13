using System;

namespace SocialNetApp.Domain.Common
{
    public interface IPublishedTimer
    {
        DateTime CurrentTime();

        PassedTime GetTimePassed(DateTime publishedTime);
    }
}