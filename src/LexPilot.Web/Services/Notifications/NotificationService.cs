namespace LexPilot.Web.Services.Notifications;

public sealed class NotificationService
{
    public event Action? OnChange;

    public string? Message { get; private set; }
    public string Type { get; private set; } = "info";

    public void Success(string message)
    {
        Message = message;
        Type = "success";
        OnChange?.Invoke();
    }

    public void Error(string message)
    {
        Message = message;
        Type = "error";
        OnChange?.Invoke();
    }

    public void Clear()
    {
        Message = null;
        Type = "info";
        OnChange?.Invoke();
    }
}
