using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LexPilot.Infrastructure.Mail;

public class OvhMailService
{
    private readonly OvhMailSettings _settings;

    public OvhMailService(IOptions<OvhMailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<object> TestConnectionAsync()
    {
        using var client = new ImapClient();
        await client.ConnectAsync(_settings.ImapHost, _settings.ImapPort, _settings.UseSsl);
        await client.AuthenticateAsync(_settings.EmailAddress, _settings.Password);
        await client.DisconnectAsync(true);

        return new { ok = true, provider = "OVH", email = _settings.EmailAddress };
    }

    public async Task<IReadOnlyList<object>> GetLastInboxMessagesAsync(int max = 10)
    {
        var result = new List<object>();

        using var client = new ImapClient();
        await client.ConnectAsync(_settings.ImapHost, _settings.ImapPort, _settings.UseSsl);
        await client.AuthenticateAsync(_settings.EmailAddress, _settings.Password);

        var inbox = client.Inbox;
        await inbox.OpenAsync(MailKit.FolderAccess.ReadOnly);

        var uids = await inbox.SearchAsync(SearchQuery.All);
        foreach (var uid in uids.Reverse().Take(max))
        {
            var message = await inbox.GetMessageAsync(uid);
            result.Add(new
            {
                subject = message.Subject,
                from = message.From.ToString(),
                date = message.Date.DateTime,
                snippet = message.TextBody?.Substring(0, Math.Min(message.TextBody.Length, 250))
            });
        }

        await client.DisconnectAsync(true);
        return result;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_settings.EmailAddress));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, _settings.UseSsl);
        await smtp.AuthenticateAsync(_settings.EmailAddress, _settings.Password);
        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}
