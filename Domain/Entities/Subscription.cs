using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum SubscriptionTier
    {
        Free,
        Basic,
        Premium,
        Enterprise
    }


    public class Subscription
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public SubscriptionTier Tier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerMonth { get; set; }

        // System metadata
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<TenantSubscription> TenantSubscriptions { get; set; }
    }
}
