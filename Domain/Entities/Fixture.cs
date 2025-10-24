using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public enum FixtureStatus
    {
        Scheduled,   // Created but not yet started
        Postponed,   // Delayed
        Cancelled    // Called off
    }

    public class Fixture
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid HomeTeamId { get; set; }
        [ForeignKey(nameof(HomeTeamId))]
        public virtual Team HomeTeam { get; set; }

        public Guid AwayTeamId { get; set; }
        [ForeignKey(nameof(AwayTeamId))]
        public virtual Team AwayTeam { get; set; }

        public Guid? CompetitionId { get; set; }
        [ForeignKey(nameof(CompetitionId))]
        public virtual Competition Competition { get; set; }

        public Guid? StadiumId { get; set; }
        [ForeignKey(nameof(StadiumId))]
        public Stadium Stadium { get; set; }

        public DateTime KickoffTime { get; set; }

        public FixtureStatus Status { get; set; } = FixtureStatus.Scheduled;

        // A Fixture may *become* a Match when it starts
        public virtual Match? Match { get; set; }

        // Metadata
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
