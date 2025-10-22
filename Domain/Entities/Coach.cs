namespace Domain.Entities
{
    public enum CoachRole
    {
        HeadCoach,
        AssistantCoach,
        FitnessCoach,
        GoalkeepingCoach,
        TechnicalDirector
    }

    public class Coach: ApplicationUser
    {
        public CoachRole CoachRole { get; set; }
    }
}
