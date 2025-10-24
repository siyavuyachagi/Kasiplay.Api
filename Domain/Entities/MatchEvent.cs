using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public enum MatchEventType
    {
        KickOff,
        Goal,
        Foul,
        Substitution,
        Injury,
        Offside,
        PenaltyAwarded,
        PenaltyMissed,
        PenaltyScored,
        OwnGoal,
        Corner,
        FreeKick,
        ThrowIn,
        GoalKick,
        Save,
        YellowCard,
        RedCard,
        HalfTime,
        FullTime,
        ExtraTimeStart,
        ExtraTimeEnd,
        PenaltyShootoutStart,
        PenaltyShootoutEnd,
        VideoAssistDecision
    }

    public class MatchEvent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int Minute { get; set; }
        public MatchEventType Type { get; set; }

        public string? Description { get; set; }

        // Team context
        public Guid? TeamId { get; set; }
        [ForeignKey(nameof(TeamId))]
        public virtual Team? Team { get; set; }

        // Players involved
        public string? PlayerId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public virtual Player? Player { get; set; }

        public string? AssistingPlayerId { get; set; }
        [ForeignKey(nameof(AssistingPlayerId))]
        public virtual Player? AssistingPlayer { get; set; }

        public string? FoulerId { get; set; }
        [ForeignKey(nameof(FoulerId))]
        public virtual Player? Fouler { get; set; }

        public string? FouledId { get; set; }
        [ForeignKey(nameof(FouledId))]
        public virtual Player? Fouled { get; set; }

        public string? SubstituteInId { get; set; }
        [ForeignKey(nameof(SubstituteInId))]
        public virtual Player? SubstituteIn { get; set; }

        public string? SubstituteOutId { get; set; }
        [ForeignKey(nameof(SubstituteOutId))]
        public virtual Player? SubstituteOut { get; set; }

        // Injuries
        public Guid? InjuryId { get; set; }
        [ForeignKey(nameof(InjuryId))]
        public virtual PlayerInjury? Injury { get; set; } = null!;

        // Link to match
        public Guid MatchId { get; set; }
        [ForeignKey(nameof(MatchId))]
        public virtual Match Match { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // System metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
 