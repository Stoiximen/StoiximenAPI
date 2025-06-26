using Newtonsoft.Json;
using Stoiximen.Infrastructure.HttpClients.Models;
using Stoiximen.Infrastructure.Interfaces;
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
            var inviteLinkResponse = await _httpClient.GetAsync($"https://api.telegram.org/bot{_config.TelegramBotToken}/createChatInviteLink?chat_id={_config.TelegramChatId}&member_limit=1");
            var responseBody = await inviteLinkResponse.Content.ReadAsStringAsync();
            var inviteResponse = JsonConvert.DeserializeObject<TelegramInviteLinkResponse>(responseBody);

            var str = $"https://api.telegram.org/bot{_config.TelegramBotToken}/sendMessage?chat_id={userId}&text=pata edw   {inviteResponse.Result.InviteLink}";

            var sendMessageResponse = await _httpClient.GetAsync(str);

            
            return inviteResponse;
        }
    }
}
