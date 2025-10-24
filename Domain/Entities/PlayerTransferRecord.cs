using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public enum PlayerTransferType
    {
        Permanent,
        Loan,
        FreeTransfer
    }

    public enum PlayerTransferStatus
    {
        Pending,
        Negotiating,
        Approved,
        Completed,
        Rejected,
        Cancelled,
        Failed
    }

    public class PlayerTransferRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public PlayerTransferType Type { get; set; }
        public PlayerTransferStatus Status { get; set; } = PlayerTransferStatus.Pending;

        public string PlayerId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public virtual Player Player { get; set; }

        public Guid ToClubId { get; set; }
        [ForeignKey(nameof(ToClubId))]
        public virtual Club ToClub { get; set; }

        public Guid? FromClubId { get; set; }
        [ForeignKey(nameof(FromClubId))]
        public virtual Club? FromClub { get; set; }

        public Guid ToTeamId { get; set; }
        [ForeignKey(nameof(ToTeamId))]
        public virtual Team ToTeam { get; set; }

        public Guid? FromTeamId { get; set; }
        [ForeignKey(nameof(FromTeamId))]
        public virtual Team? FromTeam { get; set; }

        public decimal? TransferFee { get; set; } // Optional but realistic

        // System metadata
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
