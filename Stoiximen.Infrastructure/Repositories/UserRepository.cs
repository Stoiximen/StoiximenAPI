using Stoiximen.Domain.Models;
using Stoiximen.Domain.Repositories;

namespace Stoiximen.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetUserById(string id)
        {
            var users = new List<User>
            {
                new User
                {
                    Id = "7750369537",
                    Name = "Tony",
                    SubscriptionStatus = 0 // Simulating subscription status
                },
                new User
                {
                    Id = "1234567890",
                    Name = "John",
                    SubscriptionStatus = 1 // Simulating subscription status
                }
            };

            return Task.FromResult(users.FirstOrDefault(user => user.Id == id));
        }
    }

}
