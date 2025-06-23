using Stoiximen.Application.Dtos;
using Stoiximen.Infrastructure.Models;

namespace Stoiximen.Application.Interfaces
{
    public interface ISubscriptionService
    {
        Task<GetSubscriptionsResponse> GetSubscriptions();
        Task<TelegramInviteLinkResponse> Subscribe(int subscriptionId, string userId);
    }
}
