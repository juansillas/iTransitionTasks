using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserManagementApp.Web.Controllers;
using UserManagementApp.Business.Interfaces;
using UserManagementApp.Web.Models;
using UserManagementApp.Data.Models;

namespace UserManagementApp.Tests.UnitTests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _controller;
        private Mock<IUserService> _userServiceMock;

        [SetUp]
        public void Setup()
        {
            // Crear el mock de IUserService
            _userServiceMock = new Mock<IUserService>();

            // Pasar el mock al controlador
            _controller = new AccountController(_userServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public void Login_ShouldReturnView_WhenCalled()
        {
            var result = _controller.Login() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void Register_ShouldReturnView_WhenCalled()
        {
            var result = _controller.Register() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void Register_ShouldRedirectToLogin_OnValidRegistration()
        {
            // Configuramos el mock para que el registro sea válido
            var registerViewModel = new RegisterViewModel
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "password123"
            };

            _userServiceMock.Setup(x => x.RegisterUser(It.IsAny<User>()));

            var result = _controller.Register(registerViewModel) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ActionName);
        }
    }
}
