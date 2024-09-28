using UserManagementApp.Business.Interfaces;
using UserManagementApp.Data.Models;
using UserManagementApp.Data;
using System;
using System.Linq;
using BCrypt.Net;


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

        // Nuevo método para obtener todos los usuarios como List, útil cuando no necesitas paginación o filtrado
        public List<User> GetAllUsersAsList()
        {
            return _context.Users.ToList();
        }

        //private string HashPassword(string password)
        //{
        //    using (var sha256 = System.Security.Cryptography.SHA256.Create())
        //    {
        //        // Convertir la contraseña en bytes y cifrarla
        //        var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        //        // Convertir los bytes cifrados a una cadena en formato hexadecimal
        //        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        //    }
        //}


        public void RegisterUser(User user)
        {
            //user.Password = HashPassword(user.Password);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

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
            //Buscar el usuario por email
            var user =  _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) 
            {
                return false; //usuario no encontrado
            }

            //Verificar si es usuario está bloqueado
            if (user.IsBlocked) 
            {
                return false ; //Usuario bloqueado
            }

            // Cifrar la contraseña ingresada antes de compararla con la de la base de datos
            //var hashedPassword = HashPassword(password);

            // Buscar el usuario con el email y la contraseña cifrada
            //var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == hashedPassword);

            //return user != null;
            //return user.Password == hashedPassword;
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByEmail(string email) 
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
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
