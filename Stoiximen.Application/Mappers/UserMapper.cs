using Stoiximen.Application.Dtos;
using Stoiximen.Domain.Models;

namespace Stoiximen.Application.Mappers
{
    public static class UserMapper
    {
        public static UserResource MapToUserResource(this User user)
        {
            if (user is null)
            {
                return new UserResource();
            }

            return new UserResource
            {
                Id = user.Id,
                Name = user.Name,
                SubscriptionStatus = user.SubscriptionStatus
            };
        }
    }
}
