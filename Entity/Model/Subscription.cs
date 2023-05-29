using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("subscription")]
    public class Subscription : BaseEntity
    {
        public Subscription()
        {
            Profiles = new HashSet<Profile>();
            Movies = new HashSet<Movie>();
        }
        [Column("key")]
        public string Key { get; set; }="";
        [Column("description")]
        public string Description { get; set; }="";
        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}