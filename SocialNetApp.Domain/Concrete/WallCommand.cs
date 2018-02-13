using SocialNetApp.Domain.Abstract;
using SocialNetApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SocialNetApp.Domain.Concrete
{
    public class WallCommand : ICommand
    {
        private readonly IUserRepository userRepository;
        private string username;

        private Regex pattern = new Regex(@"^(?<username>\S+) " +
                                 @"(?<prompt>wall)$");

        public WallCommand(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IList<string> ExecuteCommand(string commandAction)
        {
            Match match = pattern.Match(commandAction);
            if (match.Success)
            {
                username = match.Groups["username"].Value;
            }

            // Get current users post with followed users'
            var currentUser = userRepository.GetUser(username);
            var followingUsersPosts = GetFollowingUsersPosts(currentUser, userRepository);
            return currentUser.Posts.Concat(followingUsersPosts)
                .OrderByDescending(p => p.PublishedTime)
                .Select(post => post.ToWallFormat())
                .ToList();
        }

        // Get posts of users followed by current user
        public IList<Post> GetFollowingUsersPosts(User currentUser, IUserRepository repository)
        {
            var followingUsers = repository.GetAllUsers()
               .Where(user => currentUser.FollowingUsers.Contains(user.Name));
            return followingUsers
                 .SelectMany(followingUser => followingUser.Posts)
                 .ToList();
        }

        public bool Matches(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                Match match = pattern.Match(input);
                return match.Success;
            }
            return false;
        }
    }
}