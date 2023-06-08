using System.ComponentModel.DataAnnotations;

namespace Backend_Repositor.io.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string Rol { get; set; }
        public string ProfilePicture { get; set; }

        public ICollection<Repositorio>? Repositorios { get; set; }
        public ICollection<Relacion>? Seguidor { get; set; }
        public ICollection<Relacion>? Seguido { get; set; }
    }
}
