using UserManagementApp.Data.Models;

namespace UserManagementApp.Business.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(User user);
        void BlockUser(int userId);
        void UnblockUser(int userId);

        bool AuthenticateUser(string email, string password);
        User GetUserById(int userId);

        void DeleteUser(int userId);
        IQueryable<User> GetAllUsers();
    }
}
