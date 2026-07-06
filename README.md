# LexPilot Enterprise V0.1

Socle initial cree automatiquement.

## Demarrer Docker

`powershell
cd C:\Microward\LexPilot\docker
docker compose up -d
`

## Demarrer API

`powershell
cd C:\Microward\LexPilot\src\LexPilot.Api
dotnet run
`

Swagger :
http://localhost:5128/swagger

Health :
http://localhost:5128/api/health

Dashboard :
http://localhost:5128/api/dashboard

## Messagerie OVH

Modifier :

C:\Microward\LexPilot\src\LexPilot.Api\appsettings.json

Remplacer :
- adresse@avocatpilot.fr
- MOT_DE_PASSE_OVH_ICI

Endpoints :
- GET /api/mail/test-ovh
- GET /api/mail/inbox
- POST /api/mail/send-test?to=adresse@test.fr
