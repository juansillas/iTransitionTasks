using System;
using System.ComponentModel.DataAnnotations;



namespace UserManagementApp.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastLoginDate { get; set; }
    }
}
