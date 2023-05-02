using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Repositor.io.Models
{
    public class User
    { 

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual List<User> FriendList { get; set; }
        public virtual List<Repository> Repositories { get; set; } 
    }
}
