using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("movie")]
    public class Movie : BaseEntity
    {
        [Column("title")]
        public string Title { get; set; }="";
        [Column("genere")]
        public string Genere { get; set; }="";
        [Column("director")]
        public string Director { get; set; }="";
        [Column("actor")]
        public string Actor { get; set; }="";
        [Column("rating")]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 to 5")]
        public decimal Rating { get; set; }
        [Column("subscription_id")]
        [ForeignKey("Subscription")]
        public Guid SubscriptionId { get; set; }
        public virtual Subscription? Subscription { get; set; }
    }
}