# MiniBank Ledger API

A backend financial ledger API built with **.NET 10** focusing on **Clean Architecture** and **CQRS** as an architectural foundation for a production-grade system.

This repository represents the **architectural backbone**: clear separation of layers, predictable dependencies, and a scalable structure ready for further production features.

## Tech Stack
- .NET 10 / ASP.NET Core Web API
- Clean Architecture (Domain / Application / Infrastructure / API)
- CQRS with MediatR
- EF Core + SQLite
- EF Core Migrations
- GitHub Actions (CI)
- Tests (xUnit)

## Architecture
The solution is organized into strict layers:

- **Domain** — core business entities, value objects, domain rules
- **Application** — use-cases, CQRS (Commands/Queries), contracts
- **Infrastructure** — persistence implementation (EF Core, SQLite), migrations
- **API** — REST endpoints, request/response models, configuration

Dependency direction: **API → Infrastructure → Application → Domain**  
(Domain has no dependencies on outer layers.)

## Features (Current)
- Layered backend architecture with isolated concerns
- Transaction endpoints
- Basic reporting endpoints (summary/financial overview)
- SQLite persistence with EF Core migrations
- CQRS pipeline via MediatR
- CI build pipeline on GitHub Actions

## Getting Started (Local)
> Commands may vary depending on your solution structure.

```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project MiniBank.Ledger.Api
```
If migrations are required (example):
```bash
dotnet ef database update --project MiniBank.Ledger.Infrastructure --startup-project MiniBank.Ledger.Api
