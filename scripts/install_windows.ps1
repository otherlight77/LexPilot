Write-Host "=== LexPilot Sprint 1 - Installation Windows ===" -ForegroundColor Cyan

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
  Write-Host "Installe .NET 8 SDK avant de continuer." -ForegroundColor Red
  exit 1
}

if (-not (Get-Command docker -ErrorAction SilentlyContinue)) {
  Write-Host "Installe Docker Desktop avant de continuer." -ForegroundColor Red
  exit 1
}

docker compose up -d postgres

dotnet restore
dotnet build

Write-Host "Installation terminee." -ForegroundColor Green
Write-Host "Lance ensuite : dotnet run --project src/LexPilot.Api" -ForegroundColor Yellow
