using Microsoft.AspNetCore.Mvc;

namespace UserManagementApp.Web.Controllers
{
    public class UserManagementController : Controller
    {
        public IActionResult Index()
        {
            // Retrieve user management data
            return View();
        }

        [HttpPost]
        public IActionResult BlockUser(int userId)
        {
            // Block user logic
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UnblockUser(int userId)
        {
            // Unblock user logic
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            // Delete user logic
            return RedirectToAction("Index");
        }
    }
}
