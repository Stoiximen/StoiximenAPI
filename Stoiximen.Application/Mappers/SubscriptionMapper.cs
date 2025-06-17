using Stoiximen.Application.Dtos;
using Stoiximen.Domain.Models.Subscription.Subscription;

namespace Stoiximen.Application.Mappers
{
    public static class SubscriptionMapper
    {
        public static GetSubscriptionsResponse MapToSubscriptionResponse(this List<Subscription> subscriptions)
        {
            if (subscriptions is null)
            {
                return new GetSubscriptionsResponse { Subscriptions = new List<SubscriptionResource>() };
            }

            var response = new GetSubscriptionsResponse()
            {
                Subscriptions = subscriptions.Select(sub => new SubscriptionResource
                {
                    Id = sub.Id,
                    Description = sub.Description,
                    Price = sub.Price,
                    Name = sub.Name
                }).ToList()
            };

            return response;
        }
    }
}
