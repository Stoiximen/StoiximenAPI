using Stoiximen.Application.Dtos;
using Stoiximen.Application.Interfaces;
using Stoiximen.Application.Mappers;
using Stoiximen.Domain.Repositories;

namespace Stoiximen.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserResource GetUserById(string id)
        {
            var user = _userRepository.GetUserById(id);
            return user.MapToUserResource();
        }
    }
}
