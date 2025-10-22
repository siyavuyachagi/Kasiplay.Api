# Common Commands

## Entity Framework Core - Migrations

### Add Migration
Run from `Api` folder:
```bash
dotnet ef migrations add MigrationName --project ../Infrastructure
```

### Update Database
```bash
dotnet ef database update --project ../Infrastructure
```

### Remove Last Migration
```bash
dotnet ef migrations remove --project ../Infrastructure
```

### List Migrations
```bash
dotnet ef migrations list --project ../Infrastructure
```

---

## Database

### Clean Database
```bash
dotnet ef database drop --project ../Infrastructure
```

---

## Build & Clean

### Clean Solution
```bash
dotnet clean
```

### Build Solution
```bash
dotnet build
```

### Restore Packages
```bash
dotnet restore
```

---

## Development Server

### Run Api
```bash
dotnet run
```

---

## Notes
- Always run migration commands from `Api` folder with `--project ../Infrastructure`
- Migrations output to `Infrastructure/Data/Migrations/`