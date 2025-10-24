using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public enum InjuryType
    {
        Unknown,
        Muscle,
        Collision,
        Head,
        Leg,
        Ankle,
        Knee,
        Arm,
        Shoulder,
        Other
    }

    public enum InjurySeverity
    {
        Minor,
        Moderate,
        Severe,
        CareerThreatening
    }

    public class PlayerInjury
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        // Player reference
        public Guid PlayerId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public virtual Player Player { get; set; }

        public InjuryType Type { get; set; }
        public InjurySeverity Severity { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime InjuryDate { get; set; }
        public DateTime? ExpectedRecoveryDate { get; set; }
        public DateTime? ActualRecoveryDate { get; set; }

        public string TreatmentPlan { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // System metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
