# Architecture LexPilot

Sprint 1 pose une Clean Architecture simple :

- LexPilot.Domain : entites metier
- LexPilot.Application : DTO et cas d'usage
- LexPilot.Infrastructure : EF Core, PostgreSQL, Identity
- LexPilot.Api : endpoints REST, Swagger, JWT

Les prochains sprints ajouteront :

- Clients complet
- Dossiers complet
- Documents
- Agenda
- IA documentaire
