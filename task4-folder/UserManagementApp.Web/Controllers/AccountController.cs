using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        //[HttpPost]
        //public IActionResult Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var isAuthenticated = _userService.AuthenticateUser(model.Email, model.Password);
        //        if (isAuthenticated)
        //        {
        //            // Redirigir al panel de administración o página principal
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {

        //            ModelState.AddModelError(string.Empty, "Incorrect email or password.");
        //            //// Verificar si el usuario está bloqueado
        //            //var user = _userService.GetUserByEmail(model.Email);
        //            //if (user != null && user.IsBlocked)
        //            //{
        //            //    ModelState.AddModelError(string.Empty, "This user is blocked. They cannot log in.");
        //            //}
        //            //else
        //            //{
        //            //    ModelState.AddModelError(string.Empty, "Incorrect email or password.");
        //            //}
        //        }
        //    }
        //    return View(model);
        //}

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isAuthenticated = _userService.AuthenticateUser(model.Email, model.Password);
                if (isAuthenticated)
                {
                    // Obtener al usuario de la base de datos
                    var user = _userService.GetUserByEmail(model.Email);

                    // Crear las claims (información de la identidad)
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

                    // Crear el ticket de autenticación
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Configurar las propiedades de autenticación
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, // Mantener la sesión
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Tiempo de expiración de la sesión
                    };

                    // Iniciar la sesión con la cookie
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Redirigir al panel de administración o a la página de inicio de usuarios
                    return RedirectToAction("Index", "UserManagement");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect email or password.");
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
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
