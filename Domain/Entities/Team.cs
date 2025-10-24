using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public enum Division
    {
        First,
        Second,
        Third,
        Fourth
    }

    public class Team
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ClubId { get; set; }
        [ForeignKey(nameof(ClubId))]
        public virtual Club Club { get; set; }

        public string Name { get; set; }
        public Division Division { get; set; }
        public DateTime FoundedAt { get; set; }

        // System metadata
        public bool IsDeleted { get; set; } = false; //Soft delete
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
        public virtual string? Discriminator { get; set; }
    }
}
