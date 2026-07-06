Write-Host 'Test API LexPilot...' -ForegroundColor Cyan
Invoke-RestMethod http://localhost:5128/api/health
Invoke-RestMethod http://localhost:5128/api/dashboard
