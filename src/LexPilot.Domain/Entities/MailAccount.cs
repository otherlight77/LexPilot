namespace LexPilot.Domain.Entities;

public class MailAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Provider { get; set; } = "OVH";
    public string EmailAddress { get; set; } = "";
    public string ImapHost { get; set; } = "ssl0.ovh.net";
    public int ImapPort { get; set; } = 993;
    public string SmtpHost { get; set; } = "ssl0.ovh.net";
    public int SmtpPort { get; set; } = 465;
    public bool UseSsl { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
