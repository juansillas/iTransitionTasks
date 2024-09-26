using UserManagementApp.Business.Interfaces;
using UserManagementApp.Data.Models;

namespace UserManagementApp.Business.Services
{
    public class UserService : IUserService
    {
        public void RegisterUser(User user)
        {
            // Registration logic here
        }

        public void BlockUser(int userId)
        {
            // Block user logic here
        }

        public void UnblockUser(int userId)
        {
            // Unblock user logic here
        }
    }
}
