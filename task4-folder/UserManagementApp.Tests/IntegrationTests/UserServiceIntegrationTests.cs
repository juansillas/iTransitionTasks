using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Business.Services;
using UserManagementApp.Data;
using UserManagementApp.Data.Models;

namespace UserManagementApp.Tests.IntegrationTests
{
    [TestFixture]
    public class UserServiceIntegrationTests
    {
        private UserService _userService;
        private DataContext _context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options);
            _userService = new UserService(_context);
        }

        [Test]
        public void RegisterUser_ShouldAddUserToDatabase()
        {
            var user = new User
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "password123",
                RegistrationDate = System.DateTime.Now,
                LastLoginDate = System.DateTime.Now,
                IsBlocked = false
            };

            _userService.RegisterUser(user);
            Assert.AreEqual(1, _context.Users.Count());
        }

        [Test]
        public void BlockUser_ShouldUpdateIsBlockedField()
        {
            var user = new User
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "password123",
                RegistrationDate = System.DateTime.Now,
                LastLoginDate = System.DateTime.Now,
                IsBlocked = false
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            _userService.BlockUser(user.Id);
            Assert.IsTrue(_context.Users.FirstOrDefault(u => u.Id == user.Id).IsBlocked);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
