using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetApp.Domain.Entities;
using System.Linq;

namespace SocialNetApp.Tests
{
    [TestClass]
    public class UserTests
    {
        private const string Command = " follows ";
        private const string Username = "Alice";
        private const string UsernameToFollow = "Bob";
        private const string Message = "I love the weather today";

        [TestMethod]
        public void Follow_User_Returns_User()
        {
            var user = new User(Username);
            user.FollowingUsers.Add(UsernameToFollow);
            Assert.AreEqual(1, user.FollowingUsers.Count());
        }

        [TestMethod]
        public void User_Followed_Already_Returns_True()
        {
            var user = new User(Username);
            user.FollowingUsers.Add(UsernameToFollow);
            var isFollowed = user.FollowingUsers.Contains(UsernameToFollow);

            Assert.IsTrue(isFollowed);
        }

        [TestMethod]
        public void AddPost_Returns_Post_Count()
        {
            var user = new User(Username);
            user.Posts.Add(new Post(Username, Message));

            Assert.AreEqual(1, user.Posts.Count());
        }
    }
}