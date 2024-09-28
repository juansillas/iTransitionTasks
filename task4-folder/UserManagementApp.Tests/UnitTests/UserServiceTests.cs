using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using UserManagementApp.Business.Services;
using UserManagementApp.Data;
using UserManagementApp.Data.Models;

namespace UserManagementApp.Tests.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;

            var context = new DataContext(options);
            _userService = new UserService(context);
        }

        [Test]
        public void RegisterUser_ShouldRegisterUserCorrectly()
        {
            var user = new User 
            { Name = "Test", Email = "test@test.com", Password ="password123"};
            _userService.RegisterUser(user);
            Assert.Pass();
        }

        [Test]
        public void BlockUser_ShouldBlockUserCorrectly()
        {
            _userService.BlockUser(1);
            Assert.Pass();
        }
    }
}
