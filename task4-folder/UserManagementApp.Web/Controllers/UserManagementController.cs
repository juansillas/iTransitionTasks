using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Business.Interfaces;

namespace UserManagementApp.Web.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        private readonly IUserService _userService;

        // Inyectar el servicio en el constructor
        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            // Obtener todos los usuarios para paginación y búsqueda
            var users = _userService.GetAllUsers();

            // Aplicar búsqueda si hay un criterio de búsqueda
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.Name.Contains(searchString) || u.Email.Contains(searchString));
            }

            // Paginación
            var paginatedUsers = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Calcular el total de páginas y pasar a la vista
            ViewBag.TotalPages = (int)Math.Ceiling(users.Count() / (double)pageSize);
            ViewBag.CurrentPage = page;

            return View(paginatedUsers);
        }

        [HttpPost]
        public IActionResult BlockUser(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            _userService.BlockUser(userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UnblockUser(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            _userService.UnblockUser(userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            _userService.DeleteUser(userId);
            return RedirectToAction("Index");
        }
    }
}
