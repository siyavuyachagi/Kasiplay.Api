namespace Domain.Entities
{
    public class Tournament: Competition
    {
        // Group stages (leagues)
        public List<League>? Groups { get; set; }
        // Knockout stages (cups)
        public List<Cup>? Cups { get; set; }
    }
}
