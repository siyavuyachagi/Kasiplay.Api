using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public enum CompetitionStatus
    {
        Planned,
        Ongoing,
        Completed,
        Cancelled
    }
    public class Competition
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid FederationId { get; set; }
        [ForeignKey(nameof(FederationId))]
        public virtual Federation Federation { get; set; }

        public string Name { get; set; }
        public string Season { get; set; } = $"{DateTime.UtcNow.Year}/{DateTime.UtcNow.Year + 1}"; // E.g., "2026/2027"
        public string? Description { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }

        //Audit/System metadata
        public bool IsDeleted { get; set; } = false; //Soft delete
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
