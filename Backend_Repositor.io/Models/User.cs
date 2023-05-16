namespace Backend_Repositor.io.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Relation> Relations { get; set; }
    }
}
