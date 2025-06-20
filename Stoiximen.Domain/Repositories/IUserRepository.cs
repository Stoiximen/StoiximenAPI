using Stoiximen.Domain.Models;

namespace Stoiximen.Domain.Repositories
{
    public interface IUserRepository
    {
        User? GetUserById(string id);
    }
}
