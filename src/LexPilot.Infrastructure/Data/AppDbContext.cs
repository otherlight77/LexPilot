using LexPilot.Domain.Clients;
using LexPilot.Domain.Dossiers;
using LexPilot.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LexPilot.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Dossier> Dossiers => Set<Dossier>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Client>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nom).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Prenom).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(180);
        });

        builder.Entity<Dossier>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Numero).HasMaxLength(80).IsRequired();
            entity.Property(x => x.Titre).HasMaxLength(250).IsRequired();
            entity.HasIndex(x => x.Numero).IsUnique();
            entity.HasOne(x => x.Client).WithMany(x => x.Dossiers).HasForeignKey(x => x.ClientId);
        });
    }
}
