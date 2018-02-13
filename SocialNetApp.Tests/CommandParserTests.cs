using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialNetApp.Domain;
using SocialNetApp.Domain.Abstract;
using SocialNetApp.Domain.Concrete;
using System.Collections.Generic;

namespace SocialNetApp.Tests
{
    [TestClass]
    public class CommandParserTests
    {
        private const string Username = "Alice";
        private const string UsernameToFollow = "Bob";
        private const string Message = "I love the weather today";
        private string PostCommand = Username + " -> " + Message;
        private string WallCommand = Username + " wall";
        //private string ReadCommand = Username + " ";
        //private string FollowCommand = Username + " follows " + UsernameToFollow;

        public void SetUp()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var allCommands = new List<ICommand>
            { new PostingCommand(userRepositoryMock.Object),
              new ReadingCommand(userRepositoryMock.Object),
              new FollowingCommand(userRepositoryMock.Object),
              new WallCommand(userRepositoryMock.Object),
              new NoCommand()
            };
        }

        [TestMethod]
        public void GetPostCommand_When_Command_Is_Matched_Returns_True()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var allCommands = new List<ICommand>
            { new PostingCommand(userRepositoryMock.Object),
              new ReadingCommand(userRepositoryMock.Object),
              new FollowingCommand(userRepositoryMock.Object),
              new WallCommand(userRepositoryMock.Object),
              new NoCommand()
            };
            var mockCommands = new Mock<ICommand>();
            mockCommands.Setup(m => m.ExecuteCommand(It.IsAny<string>()))
                        .Returns(() => new List<string>());
            var commandParser = new CommandParser(allCommands, userRepositoryMock.Object);

            var returnedCommand = commandParser.GetCommand(PostCommand);
            Assert.IsTrue(returnedCommand is PostingCommand);
        }

        [TestMethod]
        public void GetWallCommand_When_Command_Is_Matched_Returns_True()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var allCommands = new List<ICommand>
            { new PostingCommand(userRepositoryMock.Object),
              new ReadingCommand(userRepositoryMock.Object),
              new FollowingCommand(userRepositoryMock.Object),
              new WallCommand(userRepositoryMock.Object),
              new NoCommand()
            };
            var mockCommands = new Mock<ICommand>();
            mockCommands.Setup(m => m.ExecuteCommand(It.IsAny<string>()))
                        .Returns(() => new List<string>());
            var commandParser = new CommandParser(allCommands, userRepositoryMock.Object);

            var returnedCommand = commandParser.GetCommand(WallCommand);
            Assert.IsTrue(returnedCommand is WallCommand);
        }
    }
}