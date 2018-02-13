using SocialNetApp.Domain.Abstract;
using SocialNetApp.Domain.Common;
using SocialNetApp.Domain.Entities;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SocialNetApp.Domain.Concrete
{
    public class PostingCommand : ICommand
    {
        private readonly IUserRepository userRepository;
        private string username;
        private string command;
        private string message;

        private Regex pattern = new Regex(@"(?<username>\S+) " +
                                  @"(?<command>->) " +
                                  @"(?<message>.+)");

        public PostingCommand(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IList<string> ExecuteCommand(string commandAction)
        {
            Match match = pattern.Match(commandAction);
            if (match.Success)
            {
                username = match.Groups["username"].Value;
                command = match.Groups["command"].Value;
                message = match.Groups["message"].Value;
            }
            userRepository.AddUser(username);
            var user = userRepository.GetUser(username);
            user.Posts.Add(new Post(username, message, new PublishedTimer()));

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