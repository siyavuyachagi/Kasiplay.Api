namespace Domain.Entities
{
    /// <summary>
    /// Comprehensive staff role enumeration covering:
    /// - Club sports staff
    /// - Club administrative staff
    /// - Federation/Organization staff
    /// - Platform employees
    /// </summary>
    public enum StaffRole
    {
        // --- Club Staff (Sports) ---
        HeadCoach = 1,
        AssistantCoach = 2,
        GoalkeeperCoach = 3,
        Trainer = 4,                    // Fitness & Conditioning
        Physiotherapist = 5,
        MedicalDoctor = 6,
        Analyst = 7,                    // Performance/Data Analyst
        KitManager = 8,
        MediaOfficer = 9,
        Scout = 10,
        TeamManager = 11,
        DirectorOfFootball = 12,

        // --- Club Administrative Staff ---
        President = 13,
        Chairman = 14,
        AcademyManager = 15,
        EventCoordinator = 16,
        MarketingManager = 17,
        SponsorshipManager = 18,

        // --- Federation/Organization Staff ---
        FederationAdmin = 19,
        LeagueCoordinator = 20,
        TournamentDirector = 21,
        RefereeCoordinator = 22,
        ComplianceOfficer = 23,

        // --- Platform Staff (KasiPlay Employees) ---
        PlatformDeveloper = 24,
        ProductManager = 25,
        CustomerSuccess = 26,
        SupportAgent = 27,
        ContentModerator = 28,
        DataAnalyst = 29,
        SalesRep = 30,
        SystemAdmin = 31,

        // --- Miscellaneous ---
        Other = 32
    }

    public class Staff: ApplicationUser
    {
        public StaffRole Role { get; set; }
    }
}
