using LexPilot.AI.Configuration;
using Microsoft.Extensions.Options;

namespace LexPilot.AI.Providers;

public interface IAIProviderFactory
{
    IAIProvider GetProvider();
}
