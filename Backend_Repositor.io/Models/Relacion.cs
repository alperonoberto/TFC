using System.ComponentModel.DataAnnotations;

namespace Backend_Repositor.io.Models
{
    public class Relacion
    {
        [Key]
        public long Id { get; set; }
        public long UserSEGUIDORid { get; set; }
        public long UserSEGUIDOid { get; set; }
        public DateTime FechaMod { get; set; }

        public long UsuarioId { get; set; }
        public User? Usuario { get; set; }
    }
}
