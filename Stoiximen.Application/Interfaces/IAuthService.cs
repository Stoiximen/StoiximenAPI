using Stoiximen.Application.Dtos;

namespace Stoiximen.Application.Services.Subscription
{
    public interface IAuthService
    {
        Task<string> ValidateTelegramHashAndGenerateToken(TelegramAuthRequest telegramData);
    }
}
