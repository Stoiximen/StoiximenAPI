using Stoiximen.Infrastructure.Http.Telegram;
using Stoiximen.Infrastructure.Interfaces;
namespace Stoiximen.Infrastructure.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly TelegramHttpClient _telegramHttpClient;

        public TelegramService(TelegramHttpClient telegramHttpClient)
        {
            _telegramHttpClient = telegramHttpClient ?? throw new ArgumentNullException(nameof(telegramHttpClient));
        }

        public async Task InviteUserToGroupChat(string userId)
        {
            var inviteLinkResponse = await _telegramHttpClient.CreateInviteLinkAsync();

            var message = $"You have been invited to join the group chat. Click here to join: {inviteLinkResponse.Result.InviteLink}";

            var sendMessageResponse = await _telegramHttpClient.SendMessageAsync(userId, message);
        }
    }
}
