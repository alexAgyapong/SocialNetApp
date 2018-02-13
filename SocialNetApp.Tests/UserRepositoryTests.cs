using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetApp.Domain.Concrete;
using System.Linq;

namespace SocialNetApp.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private string username = "Alice";

        [TestMethod]
        public void GetUsername_If_User_Exist_Returns_Username()
        {
            var repository = new UserRepository();
            var user = repository.GetUser(username);
            var existingUser = repository.GetUser(username);

            Assert.AreEqual(existingUser, user);
        }

        // change test
        [TestMethod]
        public void AddUsername_If_User_Does_Not_Exist_Returns_True()
        {
            //arrange
            var repository = new UserRepository();
            repository.AddUser(username);

            //act
            var result = repository.GetAllUsers()
                .Where(u => u.Name == username)
                .Count() > 0;

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllUsers_When_Users_List_Not_Empty_Returns_True()
        {
            //arrange
            var repository = new UserRepository();
            repository.AddUser(username);
            //act
            var result = repository.GetAllUsers()
                .Count() > 0;

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllUsers_When_Users_List_Is_Empty_Returns_False()
        {
            //arrange
            var repository = new UserRepository();

            //act
            var result = repository.GetAllUsers()
                .Count() > 0;

            //assert
            Assert.IsFalse(result);
        }
    }
}