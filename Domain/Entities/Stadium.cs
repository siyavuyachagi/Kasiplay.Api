using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Stadium
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? PhysicalAddressId { get; set; }
        [ForeignKey(nameof(PhysicalAddressId))]
        public virtual PhysicalAddress PhysicalAddress { get; set; }

        //Audit/System metadata
        public bool IsDeleted { get; set; } = false; //Soft delete
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();
    }
}
