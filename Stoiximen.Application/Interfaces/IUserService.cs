using Stoiximen.Application.Dtos;

namespace Stoiximen.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResource> GetUserById(string id);
    }
}
