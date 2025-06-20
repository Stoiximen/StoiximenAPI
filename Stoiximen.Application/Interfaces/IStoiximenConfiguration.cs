using Stoiximen.Application.Dtos;

namespace Stoiximen.Application.Services.Subscription
{
    public interface IStoiximenConfiguration
    {
        public Task<GetSubscriptionsResponse> GetSubscriptions();
    }
}
