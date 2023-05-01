namespace Backend_Repositor.io.Models
{
    public class File
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public virtual Repository Repository { get; set; }
    }
}
