using Stoiximen.Infrastructure.Interfaces;
using Stoiximen.Infrastructure.Models;
using System.Text.Json;

namespace Stoiximen.Infrastructure.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly HttpClient _httpClient;
        private readonly IStoiximenConfiguration _config;

        public TelegramService(IHttpClientFactory httpClientFactory, IStoiximenConfiguration stoiximenConfiguration)
        {
            _httpClient = httpClientFactory is null
            ? throw new ArgumentNullException(nameof(httpClientFactory))
            : httpClientFactory.CreateClient("TelegramBotClient");
            _config = stoiximenConfiguration;
        }

        public async Task<TelegramInviteLinkResponse> InviteUserToGroupChat(string userId)
        {
            var response = await _httpClient.GetAsync("https://api.telegram.org/bot7061221080:AAEm3Jv3ki21qoLDS081yDc5QIroM4v4NU0/createChatInviteLink?chat_id=-1002824203400&member_limit=1");
            var responseBody = await response.Content.ReadAsStringAsync();

            var inviteResponse = JsonSerializer.Deserialize<TelegramInviteLinkResponse>(responseBody);
            return inviteResponse;
        }
    }
}
