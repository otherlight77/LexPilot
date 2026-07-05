# LexPilot AI - Sprint 1

Socle technique initial pour le logiciel avocat LexPilot AI.

## Contenu

- .NET 8 Web API
- Clean Architecture simplifiee
- PostgreSQL
- Entity Framework Core
- ASP.NET Identity
- JWT
- Swagger
- Serilog
- Docker Compose
- Scripts Windows/Linux

## Demarrage rapide Windows

```powershell
cd C:\Microward\LexPilot
Set-ExecutionPolicy -Scope Process Bypass
.\scripts\install_windows.ps1
```

## Demarrage rapide Linux / VPS OVH

```bash
cd /opt/microward/LexPilot
chmod +x scripts/install_linux.sh
./scripts/install_linux.sh
```

## Lancer en local

```bash
docker compose up -d postgres
dotnet restore
dotnet build
dotnet run --project src/LexPilot.Api
```

Swagger : http://localhost:5128/swagger

## Connexion PostgreSQL

- Host: localhost
- Port: 5432
- Database: lexpilot
- User: lexpilot
- Password: lexpilot_password
