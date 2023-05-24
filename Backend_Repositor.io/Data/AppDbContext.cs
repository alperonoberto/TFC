using Microsoft.EntityFrameworkCore;
using Backend_Repositor.io.Models;

namespace Backend_Repositor.io.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Backend_Repositor.io.Models.User> Users { get; set; }
        public DbSet<Backend_Repositor.io.Models.Repositorio> Repositorios { get; set; }
        public DbSet<Backend_Repositor.io.Models.Archivo> Archivos { get; set; }
        public DbSet<Backend_Repositor.io.Models.Relacion> Relaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Relacion>()                                            //  1.
                .HasKey(k => new { k.SeguidorId, k.SeguidoId });

            builder.Entity<Relacion>()                                            //  2.
                .HasOne(u => u.Seguido)
                .WithMany(u => u.Seguidor)
                .HasForeignKey(u => u.SeguidorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Relacion>()                                            //  3.
                .HasOne(u => u.Seguidor)
                .WithMany(u => u.Seguido)
                .HasForeignKey(u => u.SeguidoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
