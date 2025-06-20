using Stoiximen.Application.Dtos;

namespace Stoiximen.Application.Interfaces
{
    public interface IUserService 
    {
        public UserResource GetUserById(string id);
    }
}
