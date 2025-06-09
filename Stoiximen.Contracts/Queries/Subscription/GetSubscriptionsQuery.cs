namespace Stoiximen.Contracts.Queries.Subscription
{
    public class GetSubscriptionsQuery : RequestBase<GetSubscriptionsResponse>
    {
    }

    public class GetSubscriptionsResponse : ResponseBase
    {
        public List<SubscriptionResource> Subscriptions { get; set; }

        public GetSubscriptionsResponse()
        {
            Subscriptions = new List<SubscriptionResource>();
        }
    }
}
