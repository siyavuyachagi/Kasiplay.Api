using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Federation: Organization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        // Navigation properties
        public virtual ICollection<League> Leagues { get; set; } = new List<League>();
        public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
        public virtual ICollection<Cup> Cups { get; set; } = new List<Cup>();
    }
}
