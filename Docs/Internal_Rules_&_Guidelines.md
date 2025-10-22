# KasiPlay API - Internal Rules & Guidelines 📋

This document outlines the internal development standards, naming conventions, coding practices, and architectural guidelines for the KasiPlay API project. All team members must adhere to these rules to maintain code consistency, quality, and maintainability.

## Table of Contents
- [Naming Conventions](#naming-conventions)
- [Code Organization](#code-organization)
- [C# Coding Standards](#c-coding-standards)
- [Database Standards](#database-standards)
- [API Design Standards](#api-design-standards)
- [Error Handling](#error-handling)
- [Documentation Requirements](#documentation-requirements)
- [Git & Version Control](#git--version-control)
- [Testing Requirements](#testing-requirements)
- [Security Standards](#security-standards)
- [Performance Guidelines](#performance-guidelines)
- [Code Review Checklist](#code-review-checklist)
- [Commit Message Rules](#commit-message-rules)

---

## Naming Conventions

### Project & File Naming

**Project Names**
```
KasiPlay.API              # Main API project
KasiPlay.Domain           # Shared domain logic
KasiPlay.Infrastructure   # Data access & external services
KasiPlay.API.Tests        # Unit & integration tests
```

**File Names**
- Use PascalCase for all C# files
- Match filename to primary class name
- One public class per file (exceptions: related small classes)

```
✓ PlayerController.cs
✓ PlayerService.cs
✓ IPlayerRepository.cs
✗ player-controller.cs
✗ playerController.cs
```

### Namespace Naming

Follow the project structure hierarchy:

```csharp
// Controllers
namespace KasiPlay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase { }
}

// Services
namespace KasiPlay.API.Services
{
    public interface IPlayerService { }
    public class PlayerService : IPlayerService { }
}

// Repositories
namespace KasiPlay.Infrastructure.Repositories
{
    public interface IPlayerRepository { }
    public class PlayerRepository : IPlayerRepository { }
}

// Models
namespace KasiPlay.Domain.Entities
{
    public class Player { }
}

// DTOs
namespace KasiPlay.API.DTOs
{
    public class CreatePlayerDto { }
    public class UpdatePlayerDto { }
    public class PlayerResponseDto { }
}
```

### Class & Interface Naming

**Interfaces**
- Prefix with `I`
- Use adjective or noun describing the contract

```csharp
✓ IPlayerService
✓ IPlayerRepository
✓ INotificationHub
✓ IAuthenticationService
✗ PlayerInterface
✗ PlayersService (interface)
```

**Classes**
- Use descriptive names without prefixes
- Append category suffix: Service, Repository, Controller, Handler

```csharp
✓ PlayerService
✓ PlayerRepository
✓ PlayerController
✓ CreatePlayerCommandHandler
✗ IPlayer
✗ PlayerSvc
```

**Enums**
- Use singular PascalCase
- Name should describe the value

```csharp
✓ public enum MatchStatus { Pending, Scheduled, InProgress, Completed }
✓ public enum UserRole { Admin, Manager, Coach, Player }
✗ public enum MatchStatuses { ... }
✗ public enum Status { ... }
```

### Method Naming

- Use PascalCase
- Start with verb
- Be descriptive and concise
- Async methods must end with `Async`

```csharp
✓ public async Task<Player> GetPlayerByIdAsync(int id)
✓ public async Task<bool> CreatePlayerAsync(CreatePlayerDto dto)
✓ public void ValidatePlayerData(Player player)
✓ public IQueryable<Player> FilterByTeam(int teamId)
✗ public async Task GetPlayer(int id) // Missing Async suffix
✗ public Task<Player> GetPlayerAsync(int id) // Missing async keyword
✗ public async Task<Player> PlayerById(int id)
```

### Property Naming

- Use PascalCase
- Use auto-properties where applicable
- Use private backing fields for complex properties

```csharp
✓ public int PlayerId { get; set; }
✓ public string FirstName { get; set; }
✓ public DateTime CreatedAt { get; set; }

✓ private string _encryptedPassword;
✓ public string Password 
{
    get => DecryptPassword(_encryptedPassword);
    set => _encryptedPassword = EncryptPassword(value);
}

✗ public int player_id { get; set; }
✗ public int playerId;
```

### Parameter Naming

- Use camelCase for parameters
- Use descriptive names
- Prefix boolean parameters with `is`, `has`, `can`, `should`

```csharp
✓ public async Task<Player> GetPlayerAsync(int playerId)
✓ public void FilterPlayers(string searchTerm, bool isActive)
✓ public void ProcessMatch(int matchId, bool hasCompleted)
✓ public void SendNotification(int userId, string message, bool canRetry)

✗ public void GetPlayer(int pid)
✗ public void FilterPlayers(string term, bool active)
```

### Variable Naming

- Use camelCase for local variables
- Be descriptive even if longer
- Use abbreviations sparingly and only if widely understood

```csharp
✓ var playerCount = players.Count();
✓ var isValidEmail = ValidateEmail(email);
✓ var activeTeams = teams.Where(t => t.IsActive);

✗ var pc = players.Count();
✗ var valid = ValidateEmail(email);
```

### Database/Entity Naming

- Table names: Plural PascalCase
- Column names: PascalCase
- Foreign keys: `{EntityName}Id`
- Junction tables: `{Entity1}{Entity2}` (alphabetical order)

```csharp
// Entity
public class Player
{
    public int PlayerId { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
    public ICollection<PlayerStatistic> Statistics { get; set; }
}

// Database tables
✓ Players
✓ Teams
✓ Matches
✓ PlayerStatistics
✓ PlayerTeamHistory
✗ player (singular)
✗ Player_Team (with underscore)
```

### DTO Naming

- Include suffix: `Dto`
- Add prefix for operation: `Create`, `Update`, `Response`, `Patch`

```csharp
✓ public class PlayerDto { }
✓ public class CreatePlayerDto { }
✓ public class UpdatePlayerDto { }
✓ public class PlayerResponseDto { }
✓ public class PatchPlayerDto { }

✗ public class PlayerCreateRequest { }
✗ public class PlayerModel { }
```

---

## Code Organization

### Project Structure

```
src/
├── KasiPlay.API/
│   ├── Controllers/
│   │   ├── PlayersController.cs
│   │   ├── TeamsController.cs
│   │   └── MatchesController.cs
│   │
│   ├── Services/
│   │   ├── IPlayerService.cs
│   │   ├── PlayerService.cs
│   │   ├── ITeamService.cs
│   │   └── TeamService.cs
│   │
│   ├── DTOs/
│   │   ├── Players/
│   │   │   ├── CreatePlayerDto.cs
│   │   │   ├── UpdatePlayerDto.cs
│   │   │   └── PlayerResponseDto.cs
│   │   ├── Teams/
│   │   └── Matches/
│   │
│   ├── Middleware/
│   │   ├── ExceptionHandlingMiddleware.cs
│   │   ├── AuthenticationMiddleware.cs
│   │   └── RateLimitingMiddleware.cs
│   │
│   ├── Validators/
│   │   ├── CreatePlayerValidator.cs
│   │   └── UpdatePlayerValidator.cs
│   │
│   ├── Hubs/
│   │   ├── TeamsHub.cs
│   │   └── NotificationHub.cs
│   │
│   ├── Mappings/
│   │   └── MappingProfile.cs
│   │
│   ├── Extensions/
│   │   ├── ServiceCollectionExtensions.cs
│   │   └── ApplicationBuilderExtensions.cs
│   │
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── appsettings.Production.json
│   ├── Program.cs
│   └── Startup.cs
│
├── KasiPlay.Domain/
│   ├── Entities/
│   │   ├── Player.cs
│   │   ├── Team.cs
│   │   └── Match.cs
│   │
│   ├── Interfaces/
│   │   ├── IRepository.cs
│   │   └── IEntity.cs
│   │
│   └── Enums/
│       ├── MatchStatus.cs
│       └── UserRole.cs
│
├── KasiPlay.Infrastructure/
│   ├── Repositories/
│   │   ├── IPlayerRepository.cs
│   │   ├── PlayerRepository.cs
│   │   └── BaseRepository.cs
│   │
│   ├── Data/
│   │   ├── KasiPlayDbContext.cs
│   │   └── Migrations/
│   │
│   ├── ExternalServices/
│   │   └── EmailService.cs
│   │
│   └── Extensions/
│       └── InfrastructureExtensions.cs
│
└── tests/
    ├── KasiPlay.API.Tests/
    │   ├── Unit/
    │   │   ├── Services/
    │   │   │   └── PlayerServiceTests.cs
    │   │   └── Validators/
    │   │
    │   └── Integration/
    │       ├── Controllers/
    │       │   └── PlayerControllerTests.cs
    │       └── Repositories/
    │
    └── KasiPlay.API.Integration/
```

---

## C# Coding Standards

### General Principles

- Follow Microsoft C# coding conventions
- Use meaningful names over comments
- SOLID principles must be adhered to
- DRY (Don't Repeat Yourself) principle
- Prefer composition over inheritance

### Code Style

**Braces**
```csharp
// Always use braces, even for single statements
✓ if (condition)
{
    DoSomething();
}

✗ if (condition) DoSomething();
```

**Access Modifiers**
```csharp
// Always specify access modifiers explicitly
✓ public class Player { }
✓ private string _name;
✓ protected virtual void ValidateData() { }

✗ class Player { } // Implicitly internal
```

**LINQ Queries**
```csharp
// Prefer method syntax, query syntax acceptable for complex scenarios
✓ var activePlayers = players
    .Where(p => p.IsActive)
    .OrderBy(p => p.LastName)
    .ToList();

✓ var result = from p in players
              where p.TeamId == teamId && p.IsActive
              orderby p.JerseyNumber
              select p;

✗ var activePlayers = players.Where(x => x.Active).ToList();
```

**String Interpolation**
```csharp
// Use string interpolation, not concatenation
✓ var message = $"Player {player.FirstName} {player.LastName} created";
✗ var message = "Player " + player.FirstName + " " + player.LastName + " created";
```

**Null Checking**
```csharp
// Use null-coalescing operator and null-conditional operators
✓ var name = player?.FirstName ?? "Unknown";
✓ var count = teams?.Count ?? 0;
✓ if (player is null) return NotFound();
✓ if (player is not null) ProcessPlayer(player);

✗ if (player == null) return NotFound();
✗ var name = player == null ? "Unknown" : player.FirstName;
```

**Exception Handling**
```csharp
// Catch specific exceptions
✓ try
{
    await _repository.SaveAsync();
}
catch (DbUpdateException ex)
{
    _logger.LogError(ex, "Database error occurred");
    throw new ApiException("Failed to save changes", ex);
}
catch (ArgumentNullException ex)
{
    _logger.LogError(ex, "Invalid argument provided");
    throw;
}

✗ try { }
catch (Exception ex) { } // Too generic
```

**Async/Await**
```csharp
// Always use async/await, never Task.Result or Task.Wait()
✓ public async Task<Player> GetPlayerAsync(int id)
{
    return await _repository.GetByIdAsync(id);
}

✗ public Player GetPlayer(int id)
{
    return _repository.GetByIdAsync(id).Result; // Deadlock risk
}

// Use ConfigureAwait(false) in libraries
✓ var player = await _repository.GetByIdAsync(id).ConfigureAwait(false);
```

### Dependency Injection

```csharp
// Always use constructor injection
✓ public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PlayerService> _logger;

    public PlayerService(
        IPlayerRepository playerRepository,
        IMapper mapper,
        ILogger<PlayerService> logger)
    {
        _playerRepository = playerRepository;
        _mapper = mapper;
        _logger = logger;
    }
}

✗ public class PlayerService : IPlayerService
{
    public PlayerService()
    {
        _playerRepository = new PlayerRepository(); // Direct instantiation
    }
}
```

### Logging

```csharp
// Use structured logging with appropriate levels
✓ _logger.LogInformation("Player {PlayerId} retrieved successfully", playerId);
✓ _logger.LogWarning("Player {PlayerId} not found", playerId);
✓ _logger.LogError(ex, "Failed to create player: {@CreatePlayerDto}", dto);
✓ _logger.LogDebug("Validating player data");

// Log parameters with @ for object serialization
✗ _logger.LogInformation("Player created: " + playerId);
✗ _logger.LogError("Error: " + ex.Message);
```

### Guard Clauses

```csharp
// Use guard clauses to reduce nesting
✓ public async Task<PlayerResponseDto> GetPlayerAsync(int playerId)
{
    if (playerId <= 0)
        throw new ArgumentException("Player ID must be positive", nameof(playerId));

    var player = await _playerRepository.GetByIdAsync(playerId);
    if (player is null)
        throw new NotFoundException($"Player {playerId} not found");

    return _mapper.Map<PlayerResponseDto>(player);
}

✗ public async Task<PlayerResponseDto> GetPlayerAsync(int playerId)
{
    if (playerId > 0)
    {
        var player = await _playerRepository.GetByIdAsync(playerId);
        if (player is not null)
        {
            return _mapper.Map<PlayerResponseDto>(player);
        }
    }
    return null;
}
```

### Property Initialization

```csharp
// Use init accessor for immutable properties in DTOs
✓ public class PlayerResponseDto
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime CreatedAt { get; init; }
}

// Use records for simple DTOs
✓ public record PlayerDto(int Id, string FirstName, string LastName, DateTime CreatedAt);

// Use auto-properties for entity models
✓ public class Player
{
    public int PlayerId { get; set; }
    public string FirstName { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

---

## Database Standards

### Entity Configuration

```csharp
// Use Fluent API for complex configurations
public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(p => p.PlayerId);

        builder.Property(p => p.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.HasOne(p => p.Team)
            .WithMany(t => t.Players)
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Statistics)
            .WithOne(s => s.Player)
            .HasForeignKey(s => s.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Timestamps
        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();
    }
}
```

### Migrations

```bash
# Naming convention for migrations
✓ Add-Migration AddPlayerTable
✓ Add-Migration AddEmailUniqueConstraintToPlayers
✓ Add-Migration CreateTeamsAndPlayersAssociation

✗ Add-Migration Update1
✗ Add-Migration AddStuff
```

### Timestamps

All entities must include audit timestamps:

```csharp
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class Player : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    // ...
}
```

### Soft Delete Pattern

```csharp
// Implement soft delete with DeletedAt
public class Player : BaseEntity
{
    public string FirstName { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public bool IsDeleted => DeletedAt.HasValue;
}

// In DbContext OnModelCreating
builder.Entity<Player>()
    .HasQueryFilter(p => !p.IsDeleted);
```

---

## API Design Standards

### Route Naming

- Use kebab-case for routes
- Use plural noun for collection endpoints
- Use resource hierarchy

```csharp
✓ GET    /api/players                           # List all
✓ GET    /api/players/{id}                      # Get single
✓ POST   /api/players                           # Create
✓ PUT    /api/players/{id}                      # Update
✓ DELETE /api/players/{id}                      # Delete
✓ GET    /api/teams/{teamId}/players            # Get nested resource
✓ POST   /api/teams/{teamId}/players            # Create nested resource

✗ GET    /api/getPlayers
✗ POST   /api/createPlayer
✗ GET    /api/player/1
✗ DELETE /api/removePlayer/{id}
```

### HTTP Methods

| Method | Purpose | Status Codes |
|--------|---------|--------------|
| GET | Retrieve resource | 200, 404 |
| POST | Create resource | 201, 400, 409 |
| PUT | Replace resource | 200, 204, 400, 404 |
| PATCH | Partial update | 200, 400, 404 |
| DELETE | Remove resource | 204, 404 |

### Response Format

```csharp
// Successful responses
✓ GET /api/players/1
{
  "id": 1,
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "position": "Forward",
  "jerseyNumber": 7,
  "teamId": 1,
  "createdAt": "2025-01-15T10:30:00Z",
  "updatedAt": "2025-01-15T10:30:00Z"
}

// List response with pagination
✓ GET /api/players?page=1&pageSize=10
{
  "data": [
    { /* player objects */ }
  ],
  "pagination": {
    "currentPage": 1,
    "pageSize": 10,
    "totalItems": 50,
    "totalPages": 5
  }
}
```

### Request Body

```csharp
// DTO for creation
public class CreatePlayerDto
{
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Position { get; set; }

    [Range(1, 99)]
    public int JerseyNumber { get; set; }

    [Required]
    public int TeamId { get; set; }
}

// DTO for update
public class UpdatePlayerDto
{
    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? Position { get; set; }

    [Range(1, 99)]
    public int? JerseyNumber { get; set; }
}
```

### Controller Implementation

```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly ILogger<PlayersController> _logger;

    public PlayersController(
        IPlayerService playerService,
        ILogger<PlayersController> logger)
    {
        _playerService = playerService;
        _logger = logger;
    }

    /// <summary>
    /// Get all players with pagination
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<PlayerResponseDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetPlayers(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Fetching players. Page: {Page}, PageSize: {PageSize}", page, pageSize);
        
        var result = await _playerService.GetPlayersAsync(page, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Get player by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PlayerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlayer(int id)
    {
        _logger.LogInformation("Fetching player {PlayerId}", id);
        
        var player = await _playerService.GetPlayerByIdAsync(id);
        return Ok(player);
    }

    /// <summary>
    /// Create new player
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(PlayerResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerDto dto)
    {
        _logger.LogInformation("Creating new player: {@CreatePlayerDto}", dto);
        
        var result = await _playerService.CreatePlayerAsync(dto);
        return CreatedAtAction(nameof(GetPlayer), new { id = result.Id }, result);
    }

    /// <summary>
    /// Update player
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PlayerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<IActionResult> UpdatePlayer(int id, [FromBody] UpdatePlayerDto dto)
    {
        _logger.LogInformation("Updating player {PlayerId}: {@UpdatePlayerDto}", id, dto);
        
        var result = await _playerService.UpdatePlayerAsync(id, dto);
        return Ok(result);
    }

    /// <summary>
    /// Delete player
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Manager,Admin")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        _logger.LogInformation("Deleting player {PlayerId}", id);
        
        await _playerService.DeletePlayerAsync(id);
        return NoContent();
    }
}
```

---

## Error Handling

### Custom Exceptions

```csharp
public abstract class ApiException : Exception
{
    protected ApiException(string message) : base(message) { }
}

public class NotFoundException : ApiException
{
    public NotFoundException(string message) : base(message) { }
}

public class DuplicateException : ApiException
{
    public DuplicateException(string message) : base(message) { }
}

public class ValidationException : ApiException
{
    public List<ValidationError> Errors { get; set; }

    public ValidationException(string message, List<ValidationError> errors)
        : base(message)
    {
        Errors = errors;
    }
}
```

### Exception Handling Middleware

```csharp
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            Message = exception.Message,
            TraceId = context.TraceIdentifier
        };

        return exception switch
        {
            NotFoundException => HandleNotFoundException(context, (NotFoundException)exception, response),
            ValidationException => HandleValidationException(context, (ValidationException)exception, response),
            DuplicateException => HandleDuplicateException(context, response),
            _ => HandleInternalException(context, response)
        };
    }

    private static Task HandleNotFoundException(HttpContext context, NotFoundException ex, ErrorResponse response)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        response.Code = "NOT_FOUND";
        return context.Response.WriteAsJsonAsync(response);
    }

    private static Task HandleValidationException(HttpContext context, ValidationException ex, ErrorResponse response)
    {
        context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
        response.Code = "VALIDATION_ERROR";
        response.Errors = ex.Errors.Select(e => new { e.Field, e.Message }).ToList();
        return context.Response.WriteAsJsonAsync(response);
    }

    private static Task HandleDuplicateException(HttpContext context, ErrorResponse response)
    {
        context.Response.StatusCode = StatusCodes.Status409Conflict;
        response.Code = "DUPLICATE_RESOURCE";
        return context.Response.WriteAsJsonAsync(response);
    }

    private static Task HandleInternalException(HttpContext context, ErrorResponse response)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        response.Code = "INTERNAL_SERVER_ERROR";
        response.Message = "An unexpected error occurred";
        return context.Response.WriteAsJsonAsync(response);
    }
}

public class ErrorResponse
{
    public string Code { get; set; }
    public string Message { get; set; }
    public object? Errors { get; set; }
    public string TraceId { get; set; }
}
```

---

## Documentation Requirements

### XML Documentation

All public classes, methods, and properties must have XML documentation:

```csharp
/// <summary>
/// Retrieves a player by their unique identifier.
/// </summary>
/// <param name="playerId">The unique identifier of the player</param>
/// <returns>A <see cref="PlayerResponseDto"/> containing player details</returns>
/// <exception cref="NotFoundException">Thrown when player is not found</exception>
/// <example>
/// <code>
/// var player = await playerService.GetPlayerByIdAsync(1);
/// </code>
/// </example>
public async Task<PlayerResponseDto> GetPlayerByIdAsync(int playerId)
{
    if (playerId <= 0)
        throw new ArgumentException("Player ID must be positive", nameof(playerId));

    var player = await _playerRepository.GetByIdAsync(playerId);
    if (player is null)
        throw new NotFoundException($"Player {playerId} not found");

    return _mapper.Map<PlayerResponseDto>(player);
}
```

### README Files

Each major folder should have its own README explaining purpose and usage:

```
Services/
├── README.md          # Explains service layer patterns
├── IPlayerService.cs
└── PlayerService.cs

Repositories/
├── README.md          # Explains repository pattern
├── IPlayerRepository.cs
└── PlayerRepository.cs
```

### Inline Comments

```csharp
// Use comments sparingly; code should be self-documenting
✓ // This prevents N+1 query problem
var players = await _playerRepository.GetPlayersWithTeamsAsync();

✗ // Loop through players
foreach (var player in players)
{
    // Add to list
    result.Add(player);
}
```

---

## Git & Version Control

### Branch Naming

```
feature/add-player-statistics
feature/implement-real-time-notifications
bugfix/fix-authentication-issue
bugfix/player-repository-memory-leak
hotfix/critical-security-patch
chore/update-dependencies
docs/add-api-documentation
refactor/improve-service-layer
test/add-player-service-tests

✗ feature/feature1
✗ bugfix/bug
✗ my-branch
```

### Commit Message Format

```
<type>(<scope>): <subject>

<body>

<footer>
```

**Types:**
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (no logic change)
- `refactor`: Code refactoring
- `perf`: Performance improvements
- `test`: Test additions or modifications
- `chore`: Build, dependency updates, etc.

**Examples:**
```
feat(players): add jersey number validation

- Add validation for jersey number range (1-99)
- Update PlayerValidator with new rules
- Add unit tests for validation

Closes #123

fix(auth): prevent duplicate token generation

Validates existing active sessions before issuing

docs(readme): update api documentation for v2.0

Added new endpoints and authentication changes

refactor(services): reduce service coupling with interfaces

Extract common logic into base service class
Implement dependency injection consistently

test(repositories): add integration tests for player repository

Add tests for CRUD operations
Add tests for soft delete functionality
```

### Pull Request Process

1. Create branch from `develop`
2. Keep branch up-to-date with develop
3. Create descriptive PR title and description
4. Link related issues
5. Ensure CI/CD passes
6. Request code review (minimum 2 reviewers)
7. Resolve comments and re-request review
8. Squash commits before merge
9. Delete branch after merge

**PR Template:**
```markdown
## Description
Brief description of changes

## Related Issues
Closes #123

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
- [ ] Unit tests added/updated
- [ ] Integration tests added/updated
- [ ] Manual testing performed

## Checklist
- [ ] Code follows style guidelines
- [ ] Self-review completed
- [ ] Comments added for complex logic
- [ ] Documentation updated
- [ ] No new warnings generated
- [ ] Tests pass locally
```

### Code Review Standards

**Reviewer Responsibilities:**
- Verify code follows naming conventions and standards
- Check for security vulnerabilities
- Ensure tests are comprehensive
- Verify error handling
- Check for performance issues
- Provide constructive feedback

**Author Responsibilities:**
- Respond to all comments
- Request re-review after changes
- Keep commits atomic and logical
- Provide context in PR description

---

## Testing Requirements

### Test Project Structure

```
tests/KasiPlay.API.Tests/
├── Unit/
│   ├── Services/
│   │   ├── PlayerServiceTests.cs
│   │   └── TeamServiceTests.cs
│   ├── Repositories/
│   │   └── PlayerRepositoryTests.cs
│   ├── Validators/
│   │   └── CreatePlayerValidatorTests.cs
│   └── Controllers/
│       └── PlayersControllerTests.cs
│
├── Integration/
│   ├── Controllers/
│   │   └── PlayersControllerIntegrationTests.cs
│   ├── Repositories/
│   │   └── PlayerRepositoryIntegrationTests.cs
│   └── Services/
│       └── PlayerServiceIntegrationTests.cs
│
├── Fixtures/
│   ├── PlayerFixture.cs
│   ├── TeamFixture.cs
│   └── DatabaseFixture.cs
│
└── Helpers/
    ├── TestDataBuilder.cs
    └── AuthenticationHelper.cs
```

### Unit Testing Standards

```csharp
[TestClass]
public class PlayerServiceTests
{
    // Arrange
    private PlayerService _playerService;
    private Mock<IPlayerRepository> _mockPlayerRepository;
    private Mock<IMapper> _mockMapper;
    private Mock<ILogger<PlayerService>> _mockLogger;

    [TestInitialize]
    public void Setup()
    {
        _mockPlayerRepository = new Mock<IPlayerRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<PlayerService>>();

        _playerService = new PlayerService(
            _mockPlayerRepository.Object,
            _mockMapper.Object,
            _mockLogger.Object);
    }

    // Test naming: MethodName_Condition_ExpectedResult
    [TestMethod]
    public async Task GetPlayerByIdAsync_WithValidId_ReturnsPlayer()
    {
        // Arrange
        int playerId = 1;
        var player = new Player { PlayerId = 1, FirstName = "John", LastName = "Doe" };
        var playerDto = new PlayerResponseDto { Id = 1, FirstName = "John", LastName = "Doe" };

        _mockPlayerRepository
            .Setup(r => r.GetByIdAsync(playerId))
            .ReturnsAsync(player);

        _mockMapper
            .Setup(m => m.Map<PlayerResponseDto>(player))
            .Returns(playerDto);

        // Act
        var result = await _playerService.GetPlayerByIdAsync(playerId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(playerDto.FirstName, result.FirstName);
        
        _mockPlayerRepository.Verify(r => r.GetByIdAsync(playerId), Times.Once);
        _mockMapper.Verify(m => m.Map<PlayerResponseDto>(player), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetPlayerByIdAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        int playerId = 999;
        _mockPlayerRepository
            .Setup(r => r.GetByIdAsync(playerId))
            .ReturnsAsync((Player)null);

        // Act & Assert
        await _playerService.GetPlayerByIdAsync(playerId);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetPlayerByIdAsync_WithNegativeId_ThrowsArgumentException()
    {
        // Act & Assert
        await _playerService.GetPlayerByIdAsync(-1);
    }

    [TestMethod]
    public async Task CreatePlayerAsync_WithValidDto_CreatesAndReturnsPlayer()
    {
        // Arrange
        var createDto = new CreatePlayerDto
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane@example.com",
            Position = "Midfielder",
            JerseyNumber = 10,
            TeamId = 1
        };

        var player = new Player { PlayerId = 2, FirstName = "Jane", LastName = "Doe" };
        var resultDto = new PlayerResponseDto { Id = 2, FirstName = "Jane", LastName = "Doe" };

        _mockMapper
            .Setup(m => m.Map<Player>(createDto))
            .Returns(player);

        _mockPlayerRepository
            .Setup(r => r.AddAsync(It.IsAny<Player>()))
            .Returns(Task.CompletedTask);

        _mockPlayerRepository
            .Setup(r => r.SaveAsync())
            .Returns(Task.CompletedTask);

        _mockMapper
            .Setup(m => m.Map<PlayerResponseDto>(player))
            .Returns(resultDto);

        // Act
        var result = await _playerService.CreatePlayerAsync(createDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(createDto.FirstName, result.FirstName);
        
        _mockPlayerRepository.Verify(r => r.AddAsync(It.IsAny<Player>()), Times.Once);
        _mockPlayerRepository.Verify(r => r.SaveAsync(), Times.Once);
    }
}
```

### Integration Testing Standards

```csharp
[TestClass]
public class PlayersControllerIntegrationTests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;

    [ClassInitialize]
    public static void ClassSetup(TestContext context)
    {
        // Setup test database once for all tests
    }

    [TestInitialize]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Override services for testing
                    var descriptor = services
                        .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<KasiPlayDbContext>));
                    
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddDbContext<KasiPlayDbContext>(options =>
                        options.UseInMemoryDatabase("TestDb"));
                });
            });

        _client = _factory.CreateClient();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    [TestMethod]
    public async Task GetPlayers_ReturnsOkAndPlayerList()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<KasiPlayDbContext>();
            dbContext.Players.Add(new Player { PlayerId = 1, FirstName = "John", LastName = "Doe" });
            await dbContext.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync("/api/players");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        
        var content = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(content.Contains("John"));
    }

    [TestMethod]
    public async Task GetPlayerById_WithValidId_ReturnsOkAndPlayer()
    {
        // Arrange
        int playerId = 1;
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<KasiPlayDbContext>();
            dbContext.Players.Add(new Player 
            { 
                PlayerId = playerId, 
                FirstName = "John", 
                LastName = "Doe",
                Email = "john@example.com"
            });
            await dbContext.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync($"/api/players/{playerId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsAsync<PlayerResponseDto>();
        Assert.AreEqual(playerId, content.Id);
    }

    [TestMethod]
    public async Task CreatePlayer_WithValidDto_ReturnsCreatedAndPlayer()
    {
        // Arrange
        var createDto = new CreatePlayerDto
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane@example.com",
            Position = "Forward",
            JerseyNumber = 9,
            TeamId = 1
        };

        var content = new StringContent(
            JsonSerializer.Serialize(createDto),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/api/players", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        var responseContent = await response.Content.ReadAsAsync<PlayerResponseDto>();
        Assert.AreEqual(createDto.FirstName, responseContent.FirstName);
    }
}
```

### Test Coverage Requirements

Minimum coverage targets:
- **Services**: 90%
- **Repositories**: 85%
- **Controllers**: 80%
- **Validators**: 95%

Run coverage locally:
```bash
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover /p:Exclude="[*]*.Migrations.*"
```

---

## Security Standards

### Authentication & Authorization

```csharp
// Always validate user permissions
[Authorize]
[HttpPost("teams/{id}/players")]
public async Task<IActionResult> AddPlayerToTeam(int id, [FromBody] AddPlayerDto dto)
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var userTeamId = User.FindFirst("TeamId")?.Value;

    // Verify user has permission to manage this team
    if (userTeamId != id.ToString())
        return Forbid();

    return Ok(await _teamService.AddPlayerAsync(id, dto));
}

// Use role-based access control
[Authorize(Roles = "Admin,Manager")]
[HttpDelete("{id}")]
public async Task<IActionResult> DeletePlayer(int id)
{
    await _playerService.DeletePlayerAsync(id);
    return NoContent();
}
```

### Input Validation

```csharp
// Always validate input with FluentValidation
public class CreatePlayerValidator : AbstractValidator<CreatePlayerDto>
{
    public CreatePlayerValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MaximumLength(100)
            .WithMessage("First name must not exceed 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email must be valid");

        RuleFor(x => x.JerseyNumber)
            .InclusiveBetween(1, 99)
            .WithMessage("Jersey number must be between 1 and 99");

        RuleFor(x => x.TeamId)
            .GreaterThan(0)
            .WithMessage("Team ID must be valid");
    }
}

// Use validation in controller
[HttpPost]
public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerDto dto)
{
    await _validator.ValidateAndThrowAsync(dto);
    return CreatedAtAction(nameof(GetPlayer), await _playerService.CreatePlayerAsync(dto));
}
```

### Password Security

```csharp
// Never store plain text passwords
public class AuthService : IAuthService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        
        if (user is null)
            throw new UnauthorizedAccessException("Invalid credentials");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        
        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid credentials");

        return GenerateToken(user);
    }

    public void RegisterUser(User user, string password)
    {
        user.PasswordHash = _passwordHasher.HashPassword(user, password);
        // Save user
    }
}
```

### SQL Injection Prevention

```csharp
// Always use parameterized queries
✓ using (var command = connection.CreateCommand())
{
    command.CommandText = "SELECT * FROM Players WHERE PlayerId = @id";
    command.Parameters.AddWithValue("@id", playerId);
    var result = command.ExecuteReader();
}

// With Entity Framework (always safe)
✓ var player = await _context.Players.Where(p => p.PlayerId == id).FirstOrDefaultAsync();

✗ var query = $"SELECT * FROM Players WHERE PlayerId = {id}";
✗ connection.CreateCommand($"SELECT * FROM Players WHERE PlayerId = {id}");
```

### CORS Policy

```csharp
// Configure strict CORS policy
public static class ServiceExtensions
{
    public static void AddCorsPolicy(this IServiceCollection services, IConfiguration config)
    {
        var allowedOrigins = config
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? new[] { "http://localhost:3000" };

        services.AddCors(options =>
        {
            options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder
                    .WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("X-Total-Count", "X-Page-Count");
            });
        });
    }
}

// In Program.cs
app.UseCors("ApiCorsPolicy");
```

### Secret Management

```csharp
// Never commit secrets to version control
// Use Azure Key Vault or environment variables

// In appsettings.json (NEVER commit sensitive data)
{
  "Jwt": {
    "SecretKey": "USE_KEY_VAULT_OR_ENV_VAR"
  }
}

// In development use User Secrets
dotnet user-secrets set "Jwt:SecretKey" "your-secret-key"

// In production use Azure Key Vault
services.AddAzureKeyVault(
    new Uri($"https://{vaultName}.vault.azure.net/"),
    new DefaultAzureCredential());
```

---

## Performance Guidelines

### Query Optimization

```csharp
// Use .AsNoTracking() for read-only queries
✓ var players = await _context.Players
    .AsNoTracking()
    .Where(p => p.TeamId == teamId)
    .ToListAsync();

// Use .Select() to project only needed fields
✓ var playerNames = await _context.Players
    .Where(p => p.IsActive)
    .Select(p => new { p.Id, p.FirstName, p.LastName })
    .ToListAsync();

// Use eager loading to prevent N+1 queries
✓ var team = await _context.Teams
    .Include(t => t.Players)
    .Include(t => t.Coach)
    .FirstOrDefaultAsync(t => t.Id == teamId);

// Use pagination
✓ var players = await _context.Players
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();

✗ var allPlayers = await _context.Players.ToListAsync();
```

### Caching Strategy

```csharp
// Implement caching for frequently accessed data
public class PlayerService : IPlayerService
{
    private readonly IMemoryCache _cache;
    private const string PLAYER_CACHE_KEY = "player_{0}";
    private const int CACHE_DURATION_MINUTES = 30;

    public PlayerService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<PlayerResponseDto> GetPlayerByIdAsync(int playerId)
    {
        string cacheKey = string.Format(PLAYER_CACHE_KEY, playerId);

        if (_cache.TryGetValue(cacheKey, out PlayerResponseDto cachedPlayer))
            return cachedPlayer;

        var player = await _playerRepository.GetByIdAsync(playerId);
        if (player is null)
            throw new NotFoundException($"Player {playerId} not found");

        var dto = _mapper.Map<PlayerResponseDto>(player);

        _cache.Set(cacheKey, dto, TimeSpan.FromMinutes(CACHE_DURATION_MINUTES));

        return dto;
    }

    public async Task<PlayerResponseDto> UpdatePlayerAsync(int id, UpdatePlayerDto dto)
    {
        var result = await _playerRepository.UpdateAsync(id, dto);
        
        // Invalidate cache
        string cacheKey = string.Format(PLAYER_CACHE_KEY, id);
        _cache.Remove(cacheKey);

        return _mapper.Map<PlayerResponseDto>(result);
    }
}
```

### Async/Await Best Practices

```csharp
// Always use async all the way down
✓ public async Task<IEnumerable<PlayerResponseDto>> GetPlayersAsync()
{
    var players = await _playerRepository.GetAllAsync();
    return _mapper.Map<IEnumerable<PlayerResponseDto>>(players);
}

// Use ValueTask for hot paths
✓ public async ValueTask<Player> GetPlayerByIdAsync(int id)
{
    return await _playerRepository.GetByIdAsync(id);
}

// Configure await in libraries
✓ var players = await _repository.GetAllAsync().ConfigureAwait(false);
```

### Database Connection Pooling

```csharp
// Configure connection pool in appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=KasiPlay;Max Pool Size=100;Min Pool Size=10;"
  }
}

// or in code
services.AddDbContext<KasiPlayDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions =>
        {
            sqlOptions.MaxBatchSize(100);
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetialDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        }));
```

---

## Code Review Checklist

### Before Submitting PR

- [ ] Code follows naming conventions
- [ ] All methods have XML documentation
- [ ] No commented-out code
- [ ] No hardcoded values
- [ ] Proper error handling with specific exceptions
- [ ] Input validation on all public methods
- [ ] Async/await properly implemented
- [ ] No N+1 query problems
- [ ] Security best practices followed
- [ ] Unit tests added with good coverage
- [ ] Integration tests for controller actions
- [ ] Logging implemented for important operations
- [ ] Performance optimized queries
- [ ] SOLID principles followed
- [ ] DRY principle applied
- [ ] No code duplication

### Reviewer Checklist

- [ ] Code follows style guidelines
- [ ] Logic is correct and efficient
- [ ] Error handling is appropriate
- [ ] Security vulnerabilities addressed
- [ ] Tests are comprehensive
- [ ] Documentation is accurate
- [ ] No performance regressions
- [ ] Database migrations are safe
- [ ] Breaking changes documented

---

## Quick Reference

### Common Commands

```bash
# Git
git checkout -b feature/my-feature
git add .
git commit -m "feat(scope): description"
git push origin feature/my-feature

# Entity Framework
dotnet ef migrations add AddPlayerTable
dotnet ef database update
dotnet ef database update 0 # Revert all migrations
dotnet ef migrations script # Generate SQL script

# Testing
dotnet test
dotnet test --filter "TestClassName"
dotnet test /p:CollectCoverage=true

# Building
dotnet clean
dotnet build
dotnet build -c Release
dotnet publish -c Release -o ./publish

# Running
dotnet run
dotnet run --launch-profile Development
```

### Project Structure Template

When creating a new feature, follow this structure:

```
Features/
└── Players/
    ├── Commands/
    │   ├── CreatePlayerCommand.cs
    │   └── CreatePlayerCommandHandler.cs
    ├── Queries/
    │   ├── GetPlayerQuery.cs
    │   └── GetPlayerQueryHandler.cs
    ├── Controllers/
    │   └── PlayersController.cs
    ├── Services/
    │   ├── IPlayerService.cs
    │   └── PlayerService.cs
    ├── Repositories/
    │   ├── IPlayerRepository.cs
    │   └── PlayerRepository.cs
    ├── DTOs/
    │   ├── CreatePlayerDto.cs
    │   └── PlayerResponseDto.cs
    └── Validators/
        └── CreatePlayerValidator.cs
```

---

## Version History

| Version | Date | Changes |
|---------|------|---------|
| 1.0 | 2025-01-15 | Initial internal rules document |
| 1.1 | 2025-01-20 | Added performance guidelines |
| 1.2 | 2025-02-01 | Enhanced security standards |

---

## Questions & Support

For clarifications on these standards:

1. Check this document first
2. Review Microsoft C# documentation
3. Consult the lead developer
4. Create a discussion in GitHub

**Last Updated:** October 22, 2025
**Maintained By:** KasiPlay Development Team
**Document Version:** 1.0