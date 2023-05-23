using Microsoft.EntityFrameworkCore;

namespace Backend_Repositor.io.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Backend_Repositor.io.Models.User> Users { get; set; }
        public DbSet<Backend_Repositor.io.Models.Repositorio> Repositorios { get; set; }
        public DbSet<Backend_Repositor.io.Models.Archivo> Archivos { get; set; }
        public DbSet<Backend_Repositor.io.Models.Relacion> Relaciones { get; set; }
    }
}
