using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid FederationId { get; set; }
        [ForeignKey(nameof(FederationId))]
        public virtual Federation Federation { get; set; }

        public string Name { get; set; }
        public string Season { get; set; } = $"{DateTime.UtcNow.Year}/{DateTime.UtcNow.Year+1}"; // E.g., "2026/2027"
        public string? Description { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }

        //Audit/System metadata
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
