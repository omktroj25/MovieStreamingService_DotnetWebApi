using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("upi_payment")]
    public class UpiPayment : BaseEntity
    {
        [Column("user_id")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Column("payment_type")]
        public string PaymentType { get; set; }="";
        [Column("upi_id")]
        public string UpiId { get; set; }="";
        [Column("upi_app")]
        public string UpiApp { get; set; }="";
        public virtual User? User { get; set; }
    }
}