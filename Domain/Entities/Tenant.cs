using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Central tenant metadata - stores subscription and connection info
    /// This lives in the CENTRAL database, NOT in tenant schemas
    /// </summary>
    public class Tenant
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        // Subscription info
        public Guid TenantSubscriptionId { get; set; }
        [ForeignKey(nameof(TenantSubscriptionId))]
        public TenantSubscription TenantSubscription { get; set; }

        // Connection info
        public string SchemaName { get; set; }
        public string ConnectionString { get; set; }

        // System metadata
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
