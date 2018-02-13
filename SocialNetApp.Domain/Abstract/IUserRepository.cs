using SocialNetApp.Domain.Entities;
using System.Collections.Generic;

namespace SocialNetApp.Domain.Abstract
{
    public interface IUserRepository
    {
        void AddUser(string username);

        User GetUser(string username);

        User GetOrCreateUser(string username);

        IEnumerable<User> GetAllUsers();

        void AddPost(Post post);
    }
}