using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Web.Models;

namespace UserManagementApp.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Authentication logic here
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
                // Registration logic here
            }
            return View(model);
        }
    }
}
