namespace Stoiximen.Infrastructure.HttpClients.Config
{
    public interface IHttpClientConfiguration
    {
        string BaseUrl { get; }
        TimeSpan Timeout { get; }
    }
}