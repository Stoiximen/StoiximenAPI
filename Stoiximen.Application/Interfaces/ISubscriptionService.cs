using Stoiximen.Application.Dtos;

namespace Stoiximen.Application.Services.Subscription
{
    public interface ISubscriptionService 
    {
        public Task<GetSubscriptionsResponse> GetSubscriptions();
    }
}
