using UserManagementApp.Business.Interfaces;
using UserManagementApp.Data.Models;
using UserManagementApp.Data;
using System;
using System.Linq;

namespace UserManagementApp.Business.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAllUsers()
        {
            // Devolvemos una consulta para que pueda ser paginada o filtrada
            return _context.Users.AsQueryable();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                // Convertir la contraseña en bytes y cifrarla
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                // Convertir los bytes cifrados a una cadena en formato hexadecimal
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }


        public void RegisterUser(User user)
        {
            user.Password = HashPassword(user.Password);

            // Registration logic here
            _context.Users.Add(user);
            _context.SaveChanges();

        }

        public void BlockUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.IsBlocked = true;
                _context.SaveChanges();
            }
        }

        public void UnblockUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.IsBlocked = false;
                _context.SaveChanges();
            }
        }

        public bool AuthenticateUser(string email, string password)
        {
            // Cifrar la contraseña ingresada antes de compararla con la de la base de datos
            var hashedPassword = HashPassword(password);

            // Buscar el usuario con el email y la contraseña cifrada
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == hashedPassword);

            return user != null;
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }
        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
