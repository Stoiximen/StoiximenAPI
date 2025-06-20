using Stoiximen.Application.Dtos;

namespace Stoiximen.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> ValidateTelegramHashAndGenerateToken(TelegramAuthRequest telegramData);
    }
}
