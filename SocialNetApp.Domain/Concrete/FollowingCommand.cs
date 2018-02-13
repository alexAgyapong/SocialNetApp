using SocialNetApp.Domain.Abstract;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SocialNetApp.Domain.Concrete
{
    public class FollowingCommand : ICommand
    {
        private readonly IUserRepository userRepository;
        private string username;
        private string usernameToFollow;

        private Regex pattern = new Regex(@"^(?<username>\S+) " +
                                 @"(?<prompt>follows) " +
                                 @"(?<userToFollow>\S+)$");

        public FollowingCommand(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IList<string> ExecuteCommand(string commandAction)
        {
            Match match = pattern.Match(commandAction);
            if (match.Success)
            {
                username = match.Groups["username"].Value;

                usernameToFollow = match.Groups["userToFollow"].Value;
            }

            //var currentUser = repository.GetOrCreateUser(username);
            //var userToFollow = repository.GetOrCreateUser(usernameToFollow);
            ////currentUser.FollowingUsers.Add(userToFollow);
            //currentUser.AddFollowingUsers(userToFollow);

            // working code
            userRepository.GetUser(username).FollowingUsers.Add(usernameToFollow);
            return new List<string>();
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