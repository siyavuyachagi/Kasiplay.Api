namespace Domain.Entities
{
    public enum AdministratorRole
    {
        Super,
        System,
        Support,
        Management
    }


    public class Administrator: ApplicationUser
    {
        public AdministratorRole Role { get; set; }
    }
}
