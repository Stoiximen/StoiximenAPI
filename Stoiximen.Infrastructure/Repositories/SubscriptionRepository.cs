namespace Stoiximen.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        public Task<IEnumerable<Subscription>> GetAll()
        {
            var subscriptions = Enumerable.Range(1, 5).Select(index => new Subscription
            {
                Id = index,
                Name = $"Subscription - {index}",
                Description = $"Description for subscription {index}",
                Price = 1000
            });

            return Task.FromResult(subscriptions);
        }
    }
}
