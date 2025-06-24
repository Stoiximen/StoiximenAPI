using Stoiximen.Application.Dtos;

namespace Stoiximen.Application.Interfaces
{
    public interface ISubscriptionService
    {
        Task<GetSubscriptionsResponse> GetSubscriptions();
        Task<SubscribeResponse> Subscribe(int subscriptionId, string userId);
    }
}
