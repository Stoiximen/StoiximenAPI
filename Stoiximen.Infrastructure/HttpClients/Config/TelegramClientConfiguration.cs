using Stoiximen.Infrastructure.HttpClients.Config;
using Stoiximen.Infrastructure.Interfaces;

namespace Stoiximen.Infrastructure.Http
{
    public class TelegramHttpClientConfiguration : IHttpClientConfiguration
    {
        public string BaseUrl { get; private set; }

        public TimeSpan Timeout { get; private set; }

        public TelegramHttpClientConfiguration(IStoiximenConfiguration configuration)
        {
            BaseUrl = configuration.TelegramUri;
            Timeout = TimeSpan.FromSeconds(configuration.HttpTimeoutInSeconds);
        }
    }
}