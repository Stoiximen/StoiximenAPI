namespace Stoiximen.Application.Services.Subscription
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IMediator _mediator;

        public SubscriptionService(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        public Task<GetSubscriptionsResponse> GetSubscriptions(GetSubscriptionsQuery query)
            => _mediator.Send(query);
    }
}
