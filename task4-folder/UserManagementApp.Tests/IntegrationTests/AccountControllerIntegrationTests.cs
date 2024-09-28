using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserManagementApp.Web.Controllers;
using UserManagementApp.Business.Interfaces;
using UserManagementApp.Web.Models;

namespace UserManagementApp.Tests.IntegrationTests
{
    [TestFixture]
    public class AccountControllerIntegrationTests
    {
        private AccountController _accountController;
        private Mock<IUserService> _userServiceMock;

        [SetUp]
        public void SetUp()
        {
            _userServiceMock = new Mock<IUserService>();
            _accountController = new AccountController(_userServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _accountController?.Dispose();  // Asegúrate de eliminar el controlador
            //_userServiceMock?.Dispose();    // Si _userServiceMock implementa IDisposable
        }


        [Test]
        public void Login_ValidUser_ReturnsView()
        {
            var result = _accountController.Login(new LoginViewModel
            {
                Email = "testuser@example.com",
                Password = "password123"
            });

            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Register_ValidUser_ReturnsRedirectToAction()
        {
            var user = new RegisterViewModel
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "password123"
            };

            var result = _accountController.Register(user);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}
