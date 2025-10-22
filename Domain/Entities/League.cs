using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class League: Competition
    {
        public string Name { get; set; }
        public string Season { get; set; } // "2024/2025"
        public LeagueStatus Status { get; set; }

        public virtual ICollection<Club> Clubs { get; set; }
        //public virtual ICollection<LeagueMatch> Matches { get; set; }
        //public virtual ICollection<Standings> Standings { get; set; }
    }
}
