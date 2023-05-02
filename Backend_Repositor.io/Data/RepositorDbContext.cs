using Microsoft.EntityFrameworkCore;
using Backend_Repositor.io.Models;

namespace Backend_Repositor.io.Data
{
    public class RepositorDbContext : DbContext
    {
        public RepositorDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<UserFile> Files { get; set; }
    }
}
