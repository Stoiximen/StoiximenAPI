using Stoiximen.Application.Dtos;
using Stoiximen.Application.Interfaces;
using Stoiximen.Application.Mappers;
using Stoiximen.Domain.Exceptions;
using Stoiximen.Domain.Models;
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

        public async Task<UserResource> GetUserById(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new EntityNotFoundException(nameof(User), id);
            }

            return user.MapToUserResource();
        }
    }
}
