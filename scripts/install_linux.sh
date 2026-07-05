#!/usr/bin/env bash
set -e

echo "=== LexPilot Sprint 1 - Installation Linux/VPS ==="

if ! command -v dotnet >/dev/null 2>&1; then
  echo "Installe .NET 8 SDK avant de continuer."
  exit 1
fi

if ! command -v docker >/dev/null 2>&1; then
  echo "Installe Docker avant de continuer."
  exit 1
fi

docker compose up -d postgres

dotnet restore
dotnet build

echo "Installation terminee."
echo "Lance ensuite : dotnet run --project src/LexPilot.Api"
