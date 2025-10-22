using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Organization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        public string TenantId { get; set; }
        [ForeignKey(nameof(TenantId))]
        public virtual Tenant Tenant { get; set; }


        public string Name { get; set; }
        public int YearEstablished { get; set; } = DateTime.UtcNow.Year;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
