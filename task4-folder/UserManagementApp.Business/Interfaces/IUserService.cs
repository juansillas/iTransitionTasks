using UserManagementApp.Data.Models;

namespace UserManagementApp.Business.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(User user);
        void BlockUser(int userId);
        void UnblockUser(int userId);
    }
}
