# ECommerceApp

A layered .NET 10 e‑commerce backend demonstrating a clean architecture approach (API → Application → Domain → Infrastructure). It provides a Web API with EF Core persistence, logging, object mapping, and OpenAPI/Swagger for exploring endpoints.

## Stack
- Language(s): C# 
- Framework / runtime: .NET 10 (net10.0)
- Notable libraries:
  - Microsoft.EntityFrameworkCore (EF Core) for data access and migrations
  - Serilog (Serilog.AspNetCore + sinks) for structured logging
  - Swashbuckle / Microsoft.AspNetCore.OpenApi for Swagger / OpenAPI
  - Mapster for object mapping

## Quick architecture overview

How it fits together:
- The Api project references Application, Domain and Infrastructure. Controllers call into application-level services (use-cases) which operate on domain entities and coordinate persistence via repositories implemented in Infrastructure. EF Core migrations live in Infrastructure and map Domain models to the database.

## Features
- Layered clean architecture with clear separation of concerns
- EF Core-backed persistence with migrations
- Swagger/OpenAPI for endpoint discovery
- Structured logging with Serilog
- Mapster for DTO ↔ domain mapping
- Unit test project scaffolded

## Prerequisites
- .NET 10 SDK
- A supported database for EF Core (e.g., SQL Server, PostgreSQL) and corresponding connection string
- (Optional) dotnet-ef global tool for applying migrations:
  ```
  dotnet tool install --global dotnet-ef
  ```

## Run locally (short path)
1. Restore and build:
   ```bash
   dotnet restore
   dotnet build
   ```
2. Configure a connection string (example environment variable or appsettings):
   - appsettings.json / appsettings.Development.json contain startup defaults — set `ConnectionStrings:DefaultConnection` appropriately.
   - OR set an environment variable, e.g.:
     ```bash
     export ConnectionStrings__DefaultConnection="Server=.;Database=ECommerceDb;Trusted_Connection=True;"
     ```
3. Apply EF Core migrations (run from repo root; adjust project paths if needed):
   ```bash
   cd src/ECommerce.Infrastructure
   dotnet ef database update --project ./ECommerce.Infrastructure.csproj --startup-project ../ECommerce.Api
   ```
4. Run the API:
   ```bash
   cd ../ECommerce.Api
   dotnet run
   ```
5. Open Swagger / OpenAPI in your browser:
   - Typically: `https://localhost:5001/swagger` (check console output for the exact URL)

## Tests
Run unit tests from repository root:
```bash
dotnet test
```

## Configuration
- appsettings.json / appsettings.Development.json for environment-specific settings
- Key settings to look for:
  - ConnectionStrings:DefaultConnection — database
  - Serilog settings — logging sinks/levels
  - Any API-specific or feature toggles under ApiConfiguration

## Development notes
- Project references:
  - ECommerce.Api references ECommerce.Application, ECommerce.Domain, ECommerce.Infrastructure
  - Each project has its own csproj in `src/<ProjectName>/`
- Mapping: Mapster is used for DTO ↔ domain mapping — check the Application layer for mapping configuration.
- Dependency injection wiring lives in `DependencyInjection.cs` files across projects.

## Contributing
- Fork, create a feature branch, add tests, and open a PR.
- Keep changes limited to a single concern per PR.
- Update or add migrations in `src/ECommerce.Infrastructure/Migrations` and include migration commands in PR description if schema changes are required.


## License
Specify a license (e.g., MIT) — add a LICENSE file to the repo.

## Contact
Repo owner: Behtash Shirzad (BehtashShirzad)
