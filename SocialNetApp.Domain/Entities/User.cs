using System.Collections.Generic;

namespace SocialNetApp.Domain.Entities
{
    public class User
    {
        public string Name { get; set; }
        public IList<Post> Posts { get; set; } = new List<Post>();
        public IList<string> FollowingUsers { get; set; } = new List<string>();

        public User()
        {
            HashSet<Post> Posts = new HashSet<Post>();
            HashSet<User> FollowingUsers = new HashSet<User>();
        }

        public User(string username)
        {
            this.Name = username;
        }
    }
}