namespace LexPilot.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime DateCreation { get; set; } = DateTime.UtcNow;

    public DateTime? DateModification { get; set; }
}
