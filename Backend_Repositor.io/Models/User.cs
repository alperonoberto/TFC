using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Repositor.io.Models
{
    public class User
    {
        public User()
        {
            this.Repositories = new List<Repository>();
            this.FriendList = new List<User>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<User> FriendList { get; set; }
        public virtual ICollection<Repository> Repositories { get; set; } 
    }
}
