using SocialNetApp.Domain.Abstract;
using SocialNetApp.Domain.Entities;
using System.Collections.Generic;

namespace SocialNetApp.Domain.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> users;

        public UserRepository()
        {
            this.users = new List<User>();
        }

        // Add post to user's post

        public void AddPost(Post post)
        {
        }

        //Add a new user if does not exist
        public void AddUser(string username)
        {
            if ((GetUser(username) == null))
            {
                users.Add(new User(username));
            }
        }

        // Get all users
        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }

        // Get or create a new user
        public User GetOrCreateUser(string username)
        {
            var user = users.Find(u => u.Name == username);
            if (user == null)
            {
                users.Add(new User { Name = username });
            }
            return user;
        }

        public User GetUser(string username)
        {
            return users.Find(user => user.Name == username);
        }
    }
}