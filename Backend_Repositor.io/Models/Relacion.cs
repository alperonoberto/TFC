
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Repositor.io.Models
{
    public class Relacion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long SeguidorId { get; set; }
        public long SeguidoId { get; set; }
        public DateTime FechaMod { get; set; }

        public User? Seguidor { get; set; }
        public User? Seguido { get; set; }
    }
}
