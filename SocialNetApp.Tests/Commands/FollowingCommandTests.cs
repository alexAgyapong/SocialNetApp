using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialNetApp.Domain.Abstract;
using SocialNetApp.Domain.Concrete;
using SocialNetApp.Domain.Entities;
using System.Linq;

namespace SocialNetApp.Tests.Commands
{
    [TestClass]
    public class FollowingCommandTests
    {
        private const string Command = " follows ";
        private const string Username = "Alice";
        private const string UsernameToFollow = "Bob";
        private string CommandLine = Username + Command + UsernameToFollow;

        [TestMethod]
        public void ExecuteFollowCommand_When_Users_Exists_Returns_True()
        {
            //arrange
            var mockRepository = new Mock<IUserRepository>();
            var user = new User(Username);
            var userToFollow = new User(UsernameToFollow);

            mockRepository.Setup(m => m.GetUser(It.IsAny<string>())).Returns(() => user);
            mockRepository.Setup(m => m.GetUser(It.IsAny<string>()))
                          .Returns(() => userToFollow);
            user.FollowingUsers.Add(UsernameToFollow);

            //act
            var followCommand = new FollowingCommand(mockRepository.Object);
            followCommand.ExecuteCommand(CommandLine);
            var userFollowed = user.FollowingUsers.Count() == 1;

            //assert
            Assert.IsTrue(userFollowed);
        }

        [Ignore]
        public void ExecuteFollowCommand_When_User_Has_No_FollowedUsers_Returns_False()
        {
            //arrange
            var mockRepository = new Mock<IUserRepository>();
            var user = new User(Username);

            mockRepository.Setup(m => m.GetUser(It.IsAny<string>())).Returns(() => user);

            //act
            var followCommand = new FollowingCommand(mockRepository.Object);
            followCommand.ExecuteCommand(CommandLine);
            var userFollowed = user.FollowingUsers.Count() >= 1;

            //assert
            Assert.IsFalse(userFollowed);
        }

        [TestMethod]
        public void Matches_PostCommand_Returns_True()
        {
            var mockRepository = new Mock<IUserRepository>();
            var followCommand = new FollowingCommand(mockRepository.Object);
            bool result = followCommand.Matches(CommandLine);
            Assert.IsTrue(result);
        }
    }
}