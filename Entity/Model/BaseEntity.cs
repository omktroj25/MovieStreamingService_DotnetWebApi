using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; } = true;
        [Column("created_by")]
        public Guid CreatedBy { get; set; }
        [Column("created_on")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [Column("updated_by")]
        public Guid UpdatedBy { get; set; }
        [Column("updated_on")]
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}