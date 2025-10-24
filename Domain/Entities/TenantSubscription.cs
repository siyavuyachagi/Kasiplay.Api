using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Tracks a tenant's subscription lifecycle and metadata
    /// </summary>
    public class TenantSubscription
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }
        [ForeignKey(nameof(TenantId))]
        public virtual Tenant Tenant { get; set; }

        public Guid SubscriptionId { get; set; }
        [ForeignKey(nameof(SubscriptionId))]
        public virtual Subscription Subscription { get; set; }

        // Metadata
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool IsActive { get; set; }
        public string? PaymentReference { get; set; }

        // System metadata
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
