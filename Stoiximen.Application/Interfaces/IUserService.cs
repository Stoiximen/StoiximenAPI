using Stoiximen.Application.Dtos;

namespace Stoiximen.Application.Interfaces
{
    public interface IUserService 
    {
        public Task<UserResource> GetUserById(string id);
    }
}
