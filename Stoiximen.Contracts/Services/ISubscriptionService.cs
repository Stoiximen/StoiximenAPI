namespace Stoiximen.Contracts.Services
{
    public interface ISubscriptionService
    {
        Task<GetSubscriptionsResponse> GetSubscriptions(GetSubscriptionsQuery subscriptionsQuery);
    }
}
