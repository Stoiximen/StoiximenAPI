using Stoiximen.Application.Dtos;
using Stoiximen.Application.Interfaces;
using Stoiximen.Application.Mappers;
using Stoiximen.Domain.Exceptions;
using Stoiximen.Domain.Models;
using Stoiximen.Domain.Repositories;
using Stoiximen.Infrastructure.HttpClients.Models;
using Stoiximen.Infrastructure.Interfaces;

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

        public async Task<SubscribeResponse> Subscribe(int subscriptionId, string userId)
        {
            var subscriptions = await _subscriptionRepository.GetByIdAsync(subscriptionId);

            if (subscriptions == null)
            {
                throw new EntityNotFoundException(nameof(Subscription), subscriptionId);
            }

            TelegramInviteLinkResponse response = await _telegramService.InviteUserToGroupChat(userId);

            return response.MapToSubscribeResponse();
        }
    }
}
