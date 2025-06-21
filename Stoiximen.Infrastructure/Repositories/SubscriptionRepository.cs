using Microsoft.EntityFrameworkCore;
using Stoiximen.Domain.Models;
using Stoiximen.Domain.Repositories;
using Stoiximen.Infrastructure.EF.Context;

namespace Stoiximen.Infrastructure.Repositories
{
    public class SubscriptionRepository : BaseRepository<Subscription, int>, ISubscriptionRepository
    {
        public SubscriptionRepository(StoiximenDbContext context) : base(context)
        {

        }
    }
}
