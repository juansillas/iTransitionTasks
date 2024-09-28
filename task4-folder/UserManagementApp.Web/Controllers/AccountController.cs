using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Business.Interfaces;
using UserManagementApp.Web.Models;

namespace UserManagementApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isAuthenticated = _userService.AuthenticateUser(model.Email, model.Password);
                if (isAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserManagementApp.Data.Models.User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password, // Esto debe ser cifrado en el servicio
                    RegistrationDate = DateTime.Now,
                    LastLoginDate = DateTime.Now
                };

                _userService.RegisterUser(user);
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
    }
}
