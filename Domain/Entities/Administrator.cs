namespace Domain.Entities
{
    public enum AdministratorRole
    {
        System,
        Operations,
        Management
    }


    public class Administrator: ApplicationUser
    {
        public AdministratorRole AdminRole { get; set; }
    }
}
