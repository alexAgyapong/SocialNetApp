using SocialNetApp.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SocialNetApp.Domain.Concrete
{
    public class ReadingCommand : ICommand
    {
        private readonly IUserRepository userRepository;
        private string username;
        private string command;
        private Regex pattern = new Regex(@"^(?<username>\S+)$");

        public ReadingCommand(IUserRepository userRepository)
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
            }

            return userRepository.GetUser(username)
                .Posts
                .OrderByDescending(p => p.PublishedTime)
                .Select(post => post.ToPostFormat())
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