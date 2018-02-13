using SocialNetApp.Domain.Abstract;
using System.Collections.Generic;

namespace SocialNetApp.Domain
{
    public class CommandParser
    {
        private readonly IList<ICommand> commands;
        private readonly IUserRepository userRepository;

        public CommandParser(IList<ICommand> commands, IUserRepository userRepository)
        {
            this.commands = commands;
            this.userRepository = userRepository;
        }

        //Process commands
        public IList<string> ProcessCommand(string commandAction)
        {
            var command = GetCommand(commandAction);
            return command.ExecuteCommand(commandAction);
        }

        //Get each command that matches the commandline input
        public ICommand GetCommand(string commandAction)
        {
            foreach (ICommand command in commands)
            {
                if (command.Matches(commandAction))
                    return command;
            }
            return null;
        }
    }
}