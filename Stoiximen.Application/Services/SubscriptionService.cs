using Stoiximen.Application.Dtos;
using Stoiximen.Application.Interfaces;
using Stoiximen.Application.Mappers;
using Stoiximen.Domain.Repositories;
using Stoiximen.Infrastructure.Interfaces;
using Stoiximen.Infrastructure.Models;

namespace Stoiximen.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ITelegramService _telegramService;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository,
            ITelegramService telegramService)
        {
            _subscriptionRepository = subscriptionRepository;
            _telegramService = telegramService;
        }

        public async Task<GetSubscriptionsResponse> GetSubscriptions()
        {
            var subscriptions = await _subscriptionRepository.GetAllAsync();
            return subscriptions.ToList().MapToSubscriptionResponse();
        }

        public async Task<TelegramInviteLinkResponse> Subscribe(int subscriptionId, string userId)
        {
            var subscriptions = await _subscriptionRepository.GetByIdAsync(subscriptionId);

            if (subscriptions == null)
            {
                throw new ArgumentNullException($"Subscription with ID {subscriptionId} not found.");
            }

            return await _telegramService.InviteUserToGroupChat(userId);
        }
    }
}
