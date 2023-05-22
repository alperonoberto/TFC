using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Backend_Repositor.io.Models;

namespace Backend_Repositor.io.Models
{
    public class Repositorio
    {
        [Key]
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaMod { get; set; }
        
        public long UsuarioId { get; set; }
        public User? Usuario { get; set; }
    }
}
