using SocialNetApp.Domain.Abstract;
using System.Collections.Generic;

namespace SocialNetApp.Domain.Concrete
{
    public class NoCommand : ICommand
    {
        public IList<string> ExecuteCommand(string commandAction)
        {
            return new List<string>();
        }

        public bool Matches(string input)
        {
            return true;
        }
    }
}