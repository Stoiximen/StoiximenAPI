using Stoiximen.Application.Dtos;

namespace Stoiximen.Application.Interfaces
{
    public interface ISubscriptionService 
    {
        public Task<GetSubscriptionsResponse> GetSubscriptions();
    }
}
