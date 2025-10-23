using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Billing
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        // Tenant association
        public Guid TenantId { get; set; }
        [ForeignKey(nameof(TenantId))]
        public virtual Tenant Tenant { get; set; }

        //Payment info 
        public string BillingEmail { get; set; }
        public string? BillingPhone { get; set; }
        public string? TaxId { get; set; } // VAT/Tax number

        //Billing address 
        public Guid BillingAddressId { get; set; }
        [ForeignKey(nameof(BillingAddressId))]
        public virtual PhysicalAddress BillingAddress { get; set; }

        // Optional payment method references
        public string? CardLast4 { get; set; }           // last 4 digits if using card
        public string? CardBrand { get; set; }           // Visa, MasterCard, etc.
        public string? PaymentProvider { get; set; }     // Stripe, PayPal, etc.
        public bool AutoPay { get; set; } = false;           // auto-billing enabled

        // System metadata
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
