using Stoiximen.Domain.Models;
using Stoiximen.Domain.Repositories;
using Stoiximen.Infrastructure.EF.Context;

namespace Stoiximen.Infrastructure.Repositories
{
    public class SubscriptionRepository : AsyncRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(StoiximenDbContext context) : base(context)
        {

        }

        public IEnumerable<Subscription> GetAll()
        {
            return _context.Subscriptions;
        }
    }
}
