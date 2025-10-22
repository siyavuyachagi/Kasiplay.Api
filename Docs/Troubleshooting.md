# Troubleshooting Guide

## Microsoft.AspNetCore.Identity.EntityFrameworkCore - v9.0.10 Compatibility
**Date**: 22/10/2025
**Error**: NU1202 - Package not compatible with net8.0
**Fix**: Downgrade to v8.0.21 in
```xml
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.21" />
```

Remove old .NET Framework packages from `Domain.csproj` if present, then run:
```bash
dotnet restore && dotnet clean && dotnet build
```