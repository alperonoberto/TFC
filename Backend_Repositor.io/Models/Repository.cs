namespace Backend_Repositor.io.Models
{
    public class Repository
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? OwnerId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
