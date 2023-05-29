using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("profile")]
    public class Profile : BaseEntity
    {
        [Column("user_id")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Column("email")]
        public string Email { get; set; }="";
        [Column("phone_number")]
        public string PhoneNumber { get; set; }="";
        [Column("subscription_id")]
        [ForeignKey("Subscription")]
        public Guid SubscriptionId { get; set; }
        public virtual User? User { get; set; }
        public virtual Subscription? Subscription { get; set; }
        
    }
}