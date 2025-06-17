using Stoiximen.Application.Dtos;
using Stoiximen.Application.Mappers;
using Stoiximen.Domain.Repositories;

namespace Stoiximen.Application.Services.Subscription
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<GetSubscriptionsResponse> GetSubscriptions()
        {
            var subscriptions = await _subscriptionRepository.GetAll();
            return subscriptions.ToList().MapToSubscriptionResponse();
        }
    }
}
