using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialNetApp.Domain.Abstract;
using SocialNetApp.Domain.Common;
using SocialNetApp.Domain.Concrete;
using SocialNetApp.Domain.Entities;
using System.Linq;

namespace SocialNetApp.Tests.Commands
{
    [TestClass]
    public class ReadingCommandTests
    {
        private const string Command = "  ";
        private const string Username = "Alice";
        private const string Message = "I love the weather today";
        private string CommandLine = Username;

        [TestMethod]
        public void Matches_ReadCommand_Returns_True()
        {
            var mockRepository = new Mock<IUserRepository>();
            var readCommand = new ReadingCommand(mockRepository.Object);
            bool result = readCommand.Matches(CommandLine);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ExecuteReadCommand_When_Post_Exists_Returns_True()
        {
            //arrange
            var mockRepository = new Mock<IUserRepository>();

            var user = new User(Username);

            user.Posts.Add(new Post(Username, Message, new PublishedTimer()));

            mockRepository.Setup(m => m.GetUser(It.IsAny<string>())).Returns(() => user);

            var readingCommand = new ReadingCommand(mockRepository.Object);

            //act
            readingCommand.ExecuteCommand(CommandLine);

            var result = user.Posts.Count() > 0;

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ExecuteCommand_When_Post_Does_Not_Exists_Returns_False()
        {
            //arrange
            var mockRepository = new Mock<IUserRepository>();

            var user = new User(Username);
            mockRepository.Setup(m => m.GetUser(It.IsAny<string>())).Returns(() => user);

            var readingCommand = new ReadingCommand(mockRepository.Object);

            //act
            readingCommand.ExecuteCommand(CommandLine);

            var result = user.Posts.Count() > 0;

            //assert
            Assert.IsFalse(result);
        }
    }
}