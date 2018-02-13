using System.Collections.Generic;

namespace SocialNetApp.Domain.Abstract
{
    public interface ICommand
    {
        bool Matches(string input);

        IList<string> ExecuteCommand(string commandAction);
    }
}