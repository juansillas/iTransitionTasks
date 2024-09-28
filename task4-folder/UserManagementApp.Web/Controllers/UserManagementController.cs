using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Business.Interfaces; // Asegúrate de que esto esté presente


namespace UserManagementApp.Web.Controllers
{
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
            var users = _userService.GetAllUsers();

            // Aplicar búsqueda si hay un criterio de búsqueda
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.Name.Contains(searchString) || u.Email.Contains(searchString));
            }

            // Paginación
            var paginatedUsers = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Pasar la lista paginada a la vista
            return View(paginatedUsers);
        }

        [HttpPost]
        public IActionResult BlockUser(int userId)
        {
            var user = _userService.GetUserById(userId); // Supongamos que existe este método en IUserService
            if (user == null)
            {
                ViewBag.ErrorMessage = "User not found.";
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
                ViewBag.ErrorMessage = "User not found.";
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
                ViewBag.ErrorMessage = "User not found.";
                return RedirectToAction("Index");
            }

            _userService.DeleteUser(userId); // Asegúrate de tener este método en IUserService
            return RedirectToAction("Index");
        }
    }
}
