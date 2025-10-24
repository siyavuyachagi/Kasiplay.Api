using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public enum PlayerStatus
    {
        FreeAgent,       // Not attached to any club
        Trial,           // On trial at a club
        Contracted,      // Under contract with a club
        Loaned,          // On loan to another team
        Injured,         // Temporarily unavailable due to injury
        Suspended,       // Banned for one or more matches
        Transferred,     // Recently transferred, pending registration
        Unregistered,    // Not registered for current season
        Retired          // No longer active
    }


    public enum PlayerPosition
    {
        // Goalkeepers
        GK,            // Goalkeeper

        // Defenders
        RB,            // Right Back
        RWB,           // Right Wing Back
        CB,            // Centre Back
        LWB,           // Left Wing Back
        LB,            // Left Back

        // Midfielders
        CDM,           // Central Defensive Midfielder
        DM,            // Defensive Midfielder
        CM,            // Central Midfielder
        CAM,           // Central Attacking Midfielder
        RM,            // Right Midfielder
        LM,            // Left Midfielder
        AM,            // Attacking Midfielder

        // Forwards / Attackers
        RW,            // Right Winger
        LW,            // Left Winger
        SS,            // Second Striker
        CF,            // Centre Forward
        ST,            // Striker
        WF,            // Wide Forward
        F              // Forward (generic)
    }


    public class Player : ApplicationUser
    {
        public Guid? ClubId { get; set; }
        [ForeignKey(nameof(ClubId))]
        public virtual Club? Club { get; set; }

        public Guid? TeamId { get; set; }
        [ForeignKey(nameof(TeamId))]
        public virtual Team? Team { get; set; }

        public PlayerStatus Status { get; set; } = PlayerStatus.FreeAgent;
        public PlayerPosition Position { get; set; }
        public int SquadNumber { get; set; }
        [NotMapped]
        public int Age => (int)((DateTime.Now - DateOfBirth).Value.TotalDays / 365.25);
    }

}
