
using Stoiximen.Domain.Models;

namespace Stoiximen.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAll();
    }
}
