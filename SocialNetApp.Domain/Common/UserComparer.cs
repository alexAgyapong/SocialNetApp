using SocialNetApp.Domain.Entities;
using System.Collections.Generic;

namespace SocialNetApp.Domain.Common
{
    public class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(User obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}