using Stoiximen.Domain.Models;

namespace Stoiximen.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        IEnumerable<Subscription> GetAll();
    }
}
