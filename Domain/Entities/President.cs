using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class President: ApplicationUser
    {
        public Guid TenantId { get; set; }
        [ForeignKey(nameof(TenantId))]
        public virtual Tenant Tenant { get; set; }

        public Guid OrganizationId { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        public virtual Organization Organization { get; set; }
    }
}
