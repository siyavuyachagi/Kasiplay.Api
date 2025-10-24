using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Match
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid FixtureId { get; set; }
        [ForeignKey(nameof(FixtureId))]
        public virtual Fixture Fixture { get; set; }

        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public bool IsLive { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public virtual ICollection<MatchEvent> Events { get; set; } = new List<MatchEvent>();
        public virtual MatchRecord? Record { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
