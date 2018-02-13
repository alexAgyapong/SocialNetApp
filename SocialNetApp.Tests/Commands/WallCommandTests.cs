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
    public class WallCommandTests
    {
        private const string Command = " wall";
        private const string Username = "Alice";
        private const string UsernameToFollow = "Bob";
        private const string Message = "I love the weather today";
        private const string Message2 = "Definitely need to work on unit testing";
        private string CommandLine = Username + Command;

        [TestMethod]
        public void Matches_WallCommand_Returns_True()
        {
            var mockRepository = new Mock<IUserRepository>();
            var wallCommand = new WallCommand(mockRepository.Object);
            bool result = wallCommand.Matches(CommandLine);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ExecuteWallCommand_When_Users_Exists_Returns_Posts()
        {
            //arrange
            var mockRepository = new Mock<IUserRepository>();
            var currentUser = new User(Username);
            var userToFollow = new User(UsernameToFollow);
            currentUser.FollowingUsers.Add(UsernameToFollow);
            currentUser.Posts.Add(new Post(Username, Message, new PublishedTimer()));
            userToFollow.Posts.Add(new Post(UsernameToFollow, Message2, new PublishedTimer()));

            mockRepository.Setup(m => m.GetUser(It.IsAny<string>()))
                          .Returns(() => currentUser);
            mockRepository.Setup(m => m.GetUser(It.IsAny<string>()))
                          .Returns(() => userToFollow);

            //act
            var wallCommand = new WallCommand(mockRepository.Object);
            wallCommand.ExecuteCommand(CommandLine);
            var postForUser = currentUser.Posts.Select(m => m.Message).FirstOrDefault();
            var postForFollowedUser = userToFollow.Posts.Select(m => m.Message).FirstOrDefault();

            //assert
            Assert.AreEqual(Message, postForUser);
            Assert.AreEqual(Message2, postForFollowedUser);
        }
    }
}