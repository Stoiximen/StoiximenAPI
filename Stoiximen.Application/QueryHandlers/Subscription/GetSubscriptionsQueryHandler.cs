namespace Stoiximen.Application.QueryHandlers.Subscription
{
    public class GetSubscriptionsQueryHandler : IRequestHandler<GetSubscriptionsQuery, GetSubscriptionsResponse>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public GetSubscriptionsQueryHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<GetSubscriptionsResponse> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await _subscriptionRepository.GetAll();

            return subscriptions.ToList().MapToSubscriptionResponse();
        }
    }
}
