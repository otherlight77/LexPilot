namespace LexPilot.Domain.Entities;

public class CaseFile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ClientId { get; set; }
    public string Reference { get; set; } = "";
    public string Title { get; set; } = "";
    public string Status { get; set; } = "Ouvert";
    public string? Description { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
