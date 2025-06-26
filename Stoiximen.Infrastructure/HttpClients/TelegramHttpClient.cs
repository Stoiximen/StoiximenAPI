using Microsoft.Extensions.Logging;
using Stoiximen.Infrastructure.HttpClients.Base;
using Stoiximen.Infrastructure.HttpClients.Models;

namespace Stoiximen.Infrastructure.Http.Telegram
{
    public class TelegramHttpClient : BaseHttpClient
    {
        public TelegramHttpClient(IHttpClientFactory httpClientFactory, TelegramHttpClientConfiguration config, ILogger<TelegramHttpClient> logger)
            : base(httpClientFactory, config, "TelegramBotClient", logger)
        {
        }

        public async Task<TelegramInviteLinkResponse> CreateInviteLinkAsync(string chatId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<TelegramInviteLinkResponse>(chatId, cancellationToken);
        }

        public async Task<string> SendMessageAsync(string chatId, string message, CancellationToken cancellationToken = default)
        {
            return await PostAsync<string>(chatId, message, cancellationToken);
        }
    }
}