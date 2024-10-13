using Microsoft.EntityFrameworkCore;

namespace BlazorFinalProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Definir las tablas de la base de datos
        public DbSet<User> Users { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Form> Forms { get; set; }
    }

    // Ejemplo de modelo (User)
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Template
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class Form
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string Responses { get; set; }
    }
}
