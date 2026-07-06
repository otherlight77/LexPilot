namespace LexPilot.Infrastructure.Mail;

public class OvhMailSettings
{
    public string EmailAddress { get; set; } = "";
    public string Password { get; set; } = "";
    public string ImapHost { get; set; } = "ssl0.ovh.net";
    public int ImapPort { get; set; } = 993;
    public string SmtpHost { get; set; } = "ssl0.ovh.net";
    public int SmtpPort { get; set; } = 465;
    public bool UseSsl { get; set; } = true;
}
