namespace Domain.Entities
{
    public enum CupType
    {
        SingleElimination,
        DoubleElimination,
        RoundRobin,
        Swiss
    }

    public class Cup: Competition
    {
        public CupType Type { get; set; }
        public virtual List<CupRound> CupRounds { get; set; }
    }
}
