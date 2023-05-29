using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("user")]
    public class User : BaseEntity
    {
        public User()
        {
            Profiles = new HashSet<Profile>();
        }

        [Column("user_name")]
        public string UserName { get; set; }="";
        [Column("password")]
        public string Password { get; set; }="";
        [Column("role")]
        public string Role { get; set; }="User";
        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual UpiPayment? UpiPayment { get; set; }
        public virtual CardPayment? CardPayment { get; set; }
        
    }
}