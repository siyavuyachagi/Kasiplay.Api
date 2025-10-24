using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class MatchRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid MatchId { get; set; }
        [ForeignKey(nameof(MatchId))]
        public virtual Match Match { get; set; }

        public string Summary { get; set; } = string.Empty; // e.g. JSON summary or generated report
        public string Notes { get; set; } = string.Empty;

        // System metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
