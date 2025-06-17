using Microsoft.Extensions.Configuration;
using Stoiximen.Application.Dtos;
using Stoiximen.Application.Mappers;
using Stoiximen.Application.Services.Subscription;

namespace Stoiximen.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public Task<string> ValidateTelegramHashAndGenerateToken(TelegramAuthRequest telegramData)
        {
            ValidateTelegramHash(telegramData);
            throw new NotImplementedException();
        }

        private string ValidateTelegramHash(TelegramAuthRequest telegramData)
        {
            var a = "asdfs";
            return "dummy_hash_validation_result";
        }

        private string GenerateToken(TelegramAuthRequest telegramData)
        {
            // Here you would implement the logic to generate a JWT token based on the telegramData
            // For now, we will return a dummy token
            return "dummy_token"; // Replace with actual token generation logic
        }
    }
}
