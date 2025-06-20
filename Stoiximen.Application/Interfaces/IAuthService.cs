using Stoiximen.Application.Dtos;

namespace Stoiximen.Application.Services
{
    public interface IAuthService
    {
        Task<string> ValidateTelegramHashAndGenerateToken(TelegramAuthRequest telegramData);
    }
}
