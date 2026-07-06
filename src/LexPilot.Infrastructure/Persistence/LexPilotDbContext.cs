using LexPilot.Application.Common.Interfaces;
using LexPilot.Domain.Entities;
using LexPilot.Domain.Clients;
using Microsoft.EntityFrameworkCore;

namespace LexPilot.Infrastructure.Persistence;

public class LexPilotDbContext : DbContext, IApplicationDbContext
{
    public LexPilotDbContext(DbContextOptions<LexPilotDbContext> options) : base(options) {}

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<CaseFile> CaseFiles => Set<CaseFile>();
    public DbSet<DocumentFile> Documents => Set<DocumentFile>();
    public DbSet<MailAccount> MailAccounts => Set<MailAccount>();
    IQueryable<Client> IApplicationDbContext.Clients => Clients;

    public void AddClient(Client client)
    {
        Clients.Add(client);
    }

    public void RemoveClient(Client client)
    {
        Clients.Remove(client);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().HasKey(x => x.Id);
        modelBuilder.Entity<CaseFile>().HasKey(x => x.Id);
        modelBuilder.Entity<DocumentFile>().HasKey(x => x.Id);
        modelBuilder.Entity<MailAccount>().HasKey(x => x.Id);

        modelBuilder.Entity<Client>().HasData(new Client
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Prenom = "Client",
            Nom = "Demo",
            Email = "client.demo@example.com",
            Telephone = "0600000000",
            
        });
    }
}



