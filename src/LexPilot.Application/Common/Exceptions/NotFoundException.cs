namespace LexPilot.Application.Common.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"{name} avec l'identifiant '{key}' est introuvable.")
    {
    }
}
