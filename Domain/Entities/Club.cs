using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Club: Organization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string City { get; set; }
        public string Stadium { get; set; }
        public string ShortName { get; set; }

        // System metadata
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<League> Leagues { get; set; } = new List<League>();
        public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
        public virtual ICollection<Cup> Cups { get; set; } = new List<Cup>();
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
