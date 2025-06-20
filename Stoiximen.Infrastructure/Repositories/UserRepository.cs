using Stoiximen.Domain.Models;
using Stoiximen.Domain.Repositories;
using Stoiximen.Infrastructure.EF.Context;

namespace Stoiximen.Infrastructure.Repositories
{
    public class UserRepository : AsyncRepository<User>, IUserRepository
    {
        public UserRepository(StoiximenDbContext context) : base(context)
        {

        }

        public User? GetUserById(string id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }
    }

}
