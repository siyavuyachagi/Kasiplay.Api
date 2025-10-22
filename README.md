# KasiPlay API 🚀
**RESTful Backend Service for Sports Team Management**

The KasiPlay API is a robust, enterprise-grade REST API built with ASP.NET Core that powers the comprehensive sports team management platform. It provides secure, scalable endpoints for player management, team operations, real-time communications, and analytics.

## 📋 Table of Contents
- [Quick Start](#quick-start)
- [Architecture](#architecture)
- [API Endpoints](#api-endpoints)
- [Authentication & Authorization](#authentication--authorization)
- [Data Models](#data-models)
- [Real-Time Communication](#real-time-communication)
- [Error Handling](#error-handling)
- [Rate Limiting](#rate-limiting)
- [Testing](#testing)
- [Deployment](#deployment)
- [Contributing](#contributing)

## 🚀 Quick Start

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server 2019+ or PostgreSQL 12+
- Azure Account (for production deployment)
- Git

### Local Development Setup

```bash
# Clone the repository
git clone https://github.com/yourusername/kasiplay-api.git
cd kasiplay-api

# Restore dependencies
dotnet restore

# Setup environment variables
cp .env.example .env

# Apply database migrations
dotnet ef database update

# Run the API
dotnet run --launch-profile Development
```

The API will be available at `https://localhost:5001` by default.

### Using Docker Compose

```bash
docker-compose -f docker-compose.dev.yml up --build
```

## 🏗️ Architecture

### Project Structure

```
KasiPlay.API/
├── src/
│   ├── KasiPlay.API/                 # Main API project
│   │   ├── Controllers/              # API endpoints
│   │   ├── Services/                 # Business logic
│   │   ├── Repositories/             # Data access layer
│   │   ├── Models/                   # Domain models
│   │   ├── DTOs/                     # Data transfer objects
│   │   ├── Middleware/               # Custom middleware
│   │   ├── Hubs/                     # SignalR hubs
│   │   └── Startup.cs                # Configuration
│   │
│   └── KasiPlay.Domain/              # Shared domain logic
│       ├── Entities/
│       ├── Interfaces/
│       └── Enums/
│
├── tests/
│   ├── KasiPlay.API.Tests/           # Unit tests
│   └── KasiPlay.API.Integration/     # Integration tests
│
├── docker-compose.dev.yml
├── docker-compose.prod.yml
├── Dockerfile
└── README.md
```

### Core Technologies

**Framework & Libraries**
- ASP.NET Core 8.0
- Entity Framework Core 8.0
- SignalR (Real-time communication)
- AutoMapper (Object mapping)
- FluentValidation (Input validation)
- MediatR (CQRS pattern)

**Database**
- Entity Framework Core ORM
- SQL Server or PostgreSQL
- Migrations for version control

**Cloud & DevOps**
- Azure App Services
- Azure SQL Database
- Azure Blob Storage
- Application Insights
- Azure SignalR Service

## 📡 API Endpoints

### Base URL
```
Development: https://localhost:5001/api
Production: https://api.kasiplay.com/api
```

### Players

```
GET     /players                 # List all players (paginated)
GET     /players/{id}            # Get player details
POST    /players                 # Create new player
PUT     /players/{id}            # Update player
DELETE  /players/{id}            # Delete player
GET     /players/{id}/stats      # Get player statistics
```

### Teams

```
GET     /teams                   # List all teams
GET     /teams/{id}              # Get team details
POST    /teams                   # Create new team
PUT     /teams/{id}              # Update team
DELETE  /teams/{id}              # Delete team
GET     /teams/{id}/players      # Get team roster
POST    /teams/{id}/players      # Add player to team
DELETE  /teams/{id}/players/{pid} # Remove player from team
```

### Matches

```
GET     /matches                 # List matches
GET     /matches/{id}            # Get match details
POST    /matches                 # Schedule match
PUT     /matches/{id}            # Update match
DELETE  /matches/{id}            # Cancel match
POST    /matches/{id}/results    # Submit match results
GET     /matches/{id}/report     # Get detailed match report
```

### Communications

```
GET     /messages                # Get messages
POST    /messages                # Send message
GET     /messages/{id}           # Get specific message
DELETE  /messages/{id}           # Delete message
```

### Content & Media

```
POST    /media/upload            # Upload media file
GET     /media/{id}              # Get media details
DELETE  /media/{id}              # Delete media
GET     /media/team/{teamId}     # List team media
```

### Analytics

```
GET     /analytics/team/{id}     # Team performance analytics
GET     /analytics/player/{id}   # Player performance analytics
GET     /analytics/match/{id}    # Match analytics
```

### Administration

```
GET     /admin/users             # List users
POST    /admin/users             # Create user
PUT     /admin/users/{id}        # Update user role
DELETE  /admin/users/{id}        # Delete user
GET     /admin/audit-logs        # View audit logs
```

## 🔐 Authentication & Authorization

### JWT Authentication

All endpoints (except public ones) require JWT Bearer token authentication.

**Login Endpoint**
```
POST /auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "securePassword123"
}

Response:
{
  "accessToken": "eyJhbGciOiJIUzI1NiIs...",
  "refreshToken": "eyJhbGciOiJIUzI1NiIs...",
  "expiresIn": 3600
}
```

### Token Usage

Include the token in the Authorization header:
```
Authorization: Bearer <your_jwt_token>
```

### Refresh Token

```
POST /auth/refresh
Content-Type: application/json

{
  "refreshToken": "eyJhbGciOiJIUzI1NiIs..."
}
```

### Role-Based Access Control (RBAC)

Roles define access levels across the platform:

- **Admin** - Full system access
- **Federation Manager** - Manage federations and competitions
- **Club Manager** - Manage club operations and staff
- **Coach** - Access training and match planning
- **Player** - Personal statistics and communication
- **Public** - Limited public content access

### Authorization Example

```csharp
[Authorize(Roles = "Coach,ClubManager")]
[HttpPost("teams/{id}/training")]
public async Task<IActionResult> CreateTrainingSession(int id, TrainingSessionDto dto)
{
    // Endpoint restricted to Coaches and Club Managers
}
```

## 📊 Data Models

### Player Entity

```csharp
public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Position { get; set; }
    public int JerseyNumber { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
    public ICollection<Statistics> Statistics { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

### Team Entity

```csharp
public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ClubId { get; set; }
    public Club Club { get; set; }
    public ICollection<Player> Players { get; set; }
    public ICollection<Match> Matches { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

### Match Entity

```csharp
public class Match
{
    public int Id { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public DateTime ScheduledDate { get; set; }
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public MatchStatus Status { get; set; }
    public string Venue { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

## 💬 Real-Time Communication

### SignalR Hubs

The API includes SignalR hubs for real-time features like messaging, notifications, and live match updates.

**Connection URL**
```
Development: https://localhost:5001/hubs/teams
Production: https://api.kasiplay.com/hubs/teams
```

**JavaScript/TypeScript Client Example**

```typescript
import * as SignalR from '@signalr/signalr';

const connection = new SignalR.HubConnectionBuilder()
  .withUrl('https://api.kasiplay.com/hubs/teams', {
    accessTokenFactory: () => localStorage.getItem('token')
  })
  .withAutomaticReconnect()
  .build();

connection.on('MessageReceived', (message) => {
  console.log('New message:', message);
});

connection.on('PlayerUpdated', (player) => {
  console.log('Player updated:', player);
});

await connection.start();
```

### Available Hub Methods

**Teams Hub**
- `SendMessage(teamId, message)` - Send team message
- `UpdatePlayerStatus(playerId, status)` - Update player availability
- `NotifyMatchUpdate(matchId, update)` - Real-time match updates
- `BroadcastTeamNotification(notification)` - Send team-wide notification

## ⚠️ Error Handling

### Standard Error Response

```json
{
  "error": {
    "code": "INVALID_INPUT",
    "message": "Validation failed",
    "details": [
      {
        "field": "email",
        "message": "Invalid email format"
      }
    ],
    "timestamp": "2025-01-15T10:30:00Z",
    "traceId": "0HN7L1G2K3:00000001"
  }
}
```

### HTTP Status Codes

- `200 OK` - Successful request
- `201 Created` - Resource created successfully
- `204 No Content` - Successful request with no content
- `400 Bad Request` - Invalid input or validation error
- `401 Unauthorized` - Authentication required or invalid token
- `403 Forbidden` - Access denied
- `404 Not Found` - Resource not found
- `409 Conflict` - Resource conflict (e.g., duplicate entry)
- `422 Unprocessable Entity` - Validation failed
- `429 Too Many Requests` - Rate limit exceeded
- `500 Internal Server Error` - Server error
- `503 Service Unavailable` - Service temporarily unavailable

### Exception Handling Middleware

The API includes a global exception handler that catches unhandled exceptions and returns standardized error responses.

```csharp
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

## 🚦 Rate Limiting

API requests are rate limited to prevent abuse.

**Default Limits**
- Anonymous users: 100 requests per 15 minutes
- Authenticated users: 1000 requests per 15 minutes
- Admin users: 5000 requests per 15 minutes

**Rate Limit Headers**

```
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 999
X-RateLimit-Reset: 1642267800
```

When rate limit is exceeded, the API returns a `429 Too Many Requests` status.

## 🧪 Testing

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/KasiPlay.API.Tests

# Run with coverage
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

### Test Structure

**Unit Tests**
```csharp
[TestClass]
public class PlayerServiceTests
{
    private PlayerService _service;
    private Mock<IPlayerRepository> _mockRepository;

    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<IPlayerRepository>();
        _service = new PlayerService(_mockRepository.Object);
    }

    [TestMethod]
    public async Task GetPlayerById_WithValidId_ReturnsPlayer()
    {
        // Arrange
        var playerId = 1;
        var expectedPlayer = new Player { Id = playerId, FirstName = "John" };
        _mockRepository.Setup(r => r.GetByIdAsync(playerId))
            .ReturnsAsync(expectedPlayer);

        // Act
        var result = await _service.GetPlayerByIdAsync(playerId);

        // Assert
        Assert.AreEqual(expectedPlayer, result);
    }
}
```

**Integration Tests**
```csharp
[TestClass]
public class PlayerApiTests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;

    [TestInitialize]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TestMethod]
    public async Task GetPlayers_ReturnsSuccessAndContent()
    {
        // Act
        var response = await _client.GetAsync("/api/players");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.AreEqual("application/json", response.Content.Headers.ContentType?.MediaType);
    }
}
```

## 🚀 Deployment

### Azure App Service

```bash
# Create resource group
az group create --name kasiplay-rg --location eastus

# Create App Service plan
az appservice plan create --name kasiplay-plan \
  --resource-group kasiplay-rg --sku B3 --is-linux

# Create Web App
az webapp create --resource-group kasiplay-rg \
  --plan kasiplay-plan --name kasiplay-api \
  --runtime "DOTNETCORE|8.0"
```

### Environment Configuration

Create `.env` files for different environments:

**Development (.env.development)**
```
ConnectionStrings__DefaultConnection=Server=localhost;Database=KasiPlay_Dev;
ASPNETCORE_ENVIRONMENT=Development
JWT_SECRET=dev-secret-key-change-in-production
CORS_ALLOWED_ORIGINS=http://localhost:3000
```

**Production (.env.production)**
```
ConnectionStrings__DefaultConnection=Server=your-server.database.windows.net;Database=KasiPlay;
ASPNETCORE_ENVIRONMENT=Production
JWT_SECRET=<secure-secret-from-keyvault>
CORS_ALLOWED_ORIGINS=https://kasiplay.com
APPLICATIONINSIGHTS_CONNECTION_STRING=InstrumentationKey=...
```

### CI/CD Pipeline

GitHub Actions workflow for automated deployment:

```yaml
name: Deploy API

on:
  push:
    branches: [main]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - uses: actions/setup-dotnet@v1
        with:
          dotnet-version: '8.0.x'
      - run: dotnet build
      - run: dotnet test
      - run: dotnet publish -c Release -o ./publish
      - uses: azure/webapps-deploy@v2
        with:
          app-name: 'kasiplay-api'
          publish-profile: ${{ secrets.AZURE_PUBLISH_PROFILE }}
```

## 📚 API Documentation

Interactive API documentation is available via Swagger UI:

```
Development: https://localhost:5001/swagger
Production: https://api.kasiplay.com/swagger
```

## 🔗 Related Documentation

- [Internal Rules & Guidelines](./docs/INTERNAL_RULES.md)
- [Database Schema](./docs/DATABASE_SCHEMA.md)
- [API Security](./docs/SECURITY.md)
- [Performance Optimization](./docs/PERFORMANCE.md)
- [Deployment Guide](./docs/DEPLOYMENT.md)

## 💡 Best Practices

**API Versioning**
```
/api/v1/players
/api/v2/players
```

**Pagination**
```
GET /api/players?page=1&pageSize=20&sort=name&order=asc
```

**Filtering**
```
GET /api/matches?status=scheduled&teamId=5&date=2025-01-20
```

**Expansion**
```
GET /api/teams/1?expand=players,matches
```

## 📝 Changelog

See [CHANGELOG.md](./CHANGELOG.md) for version history and updates.

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

Please ensure:
- Code follows the existing style
- Tests are included for new features
- Documentation is updated
- No breaking changes without discussion

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

- **Documentation**: [docs.kasiplay.com/api](https://docs.kasiplay.com/api)
- **Email**: dev@kasiplay.com
- **Issues**: [GitHub Issues](https://github.com/yourusername/kasiplay-api/issues)
- **Discussions**: [GitHub Discussions](https://github.com/yourusername/kasiplay-api/discussions)

---

<div align="center">

**Built with ❤️ by the KasiPlay Team**

[Website](https://kasiplay.com) • [Documentation](https://docs.kasiplay.com) • [API Docs](https://api.kasiplay.com/swagger)

</div>