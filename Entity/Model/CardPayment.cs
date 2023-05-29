using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("card_payment")]
    public class CardPayment : BaseEntity
    {
        [Column("user_id")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Column("payment_type")]
        public string PaymentType { get; set; }="";
        [Column("card_number")]
        public string CardNumber { get; set; }="";
        [Column("card_holder_name")]
        public string CardHolderName { get; set; }="";
        [Column("expire_date")]
        public string ExpireDate { get; set; }="";
        public virtual User? User { get; set; }
    
    }
}