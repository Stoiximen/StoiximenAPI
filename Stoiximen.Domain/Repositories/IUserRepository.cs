
using Stoiximen.Domain.Models;

namespace Stoiximen.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string id);
    }
}
