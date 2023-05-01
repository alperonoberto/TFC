

namespace Backend_Repositor.io.Models
{
    public class Repository
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
