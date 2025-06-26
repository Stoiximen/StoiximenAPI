using Microsoft.Extensions.Logging;
using Stoiximen.Infrastructure.HttpClients.Base;
using Stoiximen.Infrastructure.HttpClients.Models;
using Stoiximen.Infrastructure.Interfaces;

namespace Stoiximen.Infrastructure.Http.Telegram
{
    public class TelegramHttpClient : BaseHttpClient
    {
        private readonly IStoiximenConfiguration _stoiximenConfiguration;

        public TelegramHttpClient(
            IHttpClientFactory httpClientFactory,
            IStoiximenConfiguration stoiximenConfiguration,
            TelegramHttpClientConfiguration config,
            ILogger<TelegramHttpClient> logger)
            : base(httpClientFactory, config, "TelegramBotClient", logger)
        {
            _stoiximenConfiguration = stoiximenConfiguration ?? throw new ArgumentNullException(nameof(stoiximenConfiguration));
        }

        public async Task<TelegramInviteLinkResponse> CreateInviteLinkAsync(CancellationToken cancellationToken = default)
        {
            var endpoint = $"bot{Uri.EscapeDataString(_stoiximenConfiguration.TelegramBotToken)}/createChatInviteLink?chat_id={Uri.EscapeDataString(_stoiximenConfiguration.TelegramChatId)}&member_limit=1";
            return await GetAsync<TelegramInviteLinkResponse>(endpoint, cancellationToken);
        }

        public async Task<MessageResult> SendMessageAsync(string chatId, string message, CancellationToken cancellationToken = default)
        {
            var endpoint = $"bot{Uri.EscapeDataString(_stoiximenConfiguration.TelegramBotToken)}/sendMessage?chat_id={Uri.EscapeDataString(chatId)}&text={Uri.EscapeDataString(message)}";
            return await GetAsync<MessageResult>(endpoint, cancellationToken);
        }
    }
}