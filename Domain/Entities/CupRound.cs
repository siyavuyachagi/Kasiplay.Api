using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public enum CupRoundBracketSide
    {
        Traditional,
        Mirrored,
        Ladder
    }

    public class CupRound
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid CupId { get; set; }
        [ForeignKey(nameof(CupId))]
        public Cup Cup { get; set; }

        public int RoundNumber { get; set; }
        public string RoundName { get; set; } = string.Empty;

        // Visualization properties
        public CupRoundBracketSide? BracketSide { get; set; }
        public int? BracketPosition { get; set; } // Position within the bracket side
        public List<Guid>? ParentMatches { get; set; } // IDs of matches that feed into this one
        public bool IsCompleted { get; set; }

        // System metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
