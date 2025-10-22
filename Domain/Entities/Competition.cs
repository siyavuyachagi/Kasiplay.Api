using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Competition
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid FederationId { get; set; }
        [ForeignKey(nameof(FederationId))]
        public virtual Federation Federation { get; set; }

        public string Name { get; set; }
        public string Season { get; set; } // E.g., "2023/2024"
        public string? Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }


        //Audit/System metadata 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
