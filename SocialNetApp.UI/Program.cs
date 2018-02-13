using SocialNetApp.Domain;
using SocialNetApp.Domain.Abstract;
using SocialNetApp.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetApp.UI
{
    internal class Program
    {
        private const string PROMPT = "> ";

        private static void Main(string[] args)
        {
            var userRepository = new UserRepository();
            var allCommands = new List<ICommand>
            { new PostingCommand(userRepository),
              new ReadingCommand(userRepository),
              new FollowingCommand(userRepository),
              new WallCommand(userRepository),
              new NoCommand()
            };

            var commandParser = new CommandParser(allCommands, userRepository);

            //Get input and execute commands
            while (true)
            {
                Console.Write(PROMPT);
                var userInput = Console.ReadLine();
                var result = commandParser.ProcessCommand(userInput);
                result.ToList().ForEach(Console.WriteLine);
                Console.Write("\n");
            }
        }
    }
}