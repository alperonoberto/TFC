namespace Backend_Repositor.io.Models
{
    public class Relation
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public long UserId { get; set; }
        public long UserFollowed { get; set; }
    }
}
