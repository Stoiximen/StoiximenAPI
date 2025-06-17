
using Stoiximen.Domain.Models.Subscription.Subscription;

namespace Stoiximen.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAll();
    }
}
