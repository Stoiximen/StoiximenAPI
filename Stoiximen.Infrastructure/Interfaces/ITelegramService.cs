using Stoiximen.Infrastructure.Models;

namespace Stoiximen.Infrastructure.Interfaces
{
    public interface ITelegramService
    {
        Task<TelegramInviteLinkResponse> InviteUserToGroupChat(string userId);
    }
}
