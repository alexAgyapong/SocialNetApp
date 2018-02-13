using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialNetApp.Domain.Abstract;
using SocialNetApp.Domain.Concrete;
using SocialNetApp.Domain.Entities;
using System.Linq;

namespace SocialNetApp.Tests.Commands
{
    [TestClass]
    public class PostingCommandTests
    {
        private const string Command = " -> ";
        private const string Username = "Alice";
        private const string Message = "I love the weather today";
        private string CommandLine = Username + Command + Message;

        [TestMethod]
        public void Matches_PostCommand_Return_True()
        {
            var mockRepository = new Mock<IUserRepository>();
            var postingCommand = new PostingCommand(mockRepository.Object);
            bool result = postingCommand.Matches(CommandLine);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Matches_EmptyCommand_Returns_False()
        {
            var mockRepository = new Mock<IUserRepository>();
            var postingCommand = new PostingCommand(mockRepository.Object);
            bool result = postingCommand.Matches("");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Matches_NullCommand_Returns_False()
        {
            var mockRepository = new Mock<IUserRepository>();
            var postingCommand = new PostingCommand(mockRepository.Object);
            bool result = postingCommand.Matches(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExecutePostCommand_When_User_Exists_Returns_True()
        {
            //arrange
            var mockRepository = new Mock<IUserRepository>();
            var user = new User(Username);
            mockRepository.Setup(m => m.GetUser(It.IsAny<string>())).Returns(() => user);
            var postingCommand = new PostingCommand(mockRepository.Object);

            //act
            postingCommand.ExecuteCommand(CommandLine);
            var postAdded = user.Posts.Select(m => m.Message).FirstOrDefault();

            //assert
            mockRepository.Verify(m => m.GetUser(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(Message, postAdded);
        }

        [TestMethod]
        public void ExecutePostCommand_When_User_Does_Not_Exists_Returns_True()
        {
            //arrange
            var mockRepository = new Mock<IUserRepository>();
            var user = new User(Username);
            mockRepository.Setup(m => m.AddUser(It.IsAny<string>()));
            mockRepository.Setup(m => m.GetUser(It.IsAny<string>())).Returns(() => user);
            var postingCommand = new PostingCommand(mockRepository.Object);

            //act
            postingCommand.ExecuteCommand(CommandLine);
            var postAdded = user.Posts.Select(m => m.Message).FirstOrDefault();

            //assert
            mockRepository.Verify(m => m.GetUser(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(Message, postAdded);
        }
    }
}