using Microsoft.Extensions.Configuration;
using Stoiximen.Application.Dtos;
using Stoiximen.Application.Services.Subscription;
using System.Security.Cryptography;
using System.Text;

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
            if (IsTelegramHasValid(telegramData))
            {
                var token = GenerateToken(telegramData);
                return Task.FromResult(token);
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid Telegram authentication data.");
            }
        }

        private bool IsTelegramHasValid(TelegramAuthRequest telegramData)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            telegramData.GetType().GetProperties().ToList().ForEach(prop =>
            {
                var value = prop.GetValue(telegramData, null);
                if (value != null)
                {
                    var key = GetKey(prop.Name);
                    if(key == "hash")
                    {
                        // Skip hash property as it will be computed
                        return;
                    }
                    dict.Add(key, $"{value}");
                }
            });

            var dataCheckArray = dict.OrderBy(kvp => kvp.Key)
                                    .Select(kvp => $"{kvp.Key}={kvp.Value}")
                                    .ToArray();

            string dataCheckString = string.Join("\n", dataCheckArray);

            string computedHash = ComputeHash(dataCheckString);

            return telegramData.Hash.Equals(computedHash, StringComparison.OrdinalIgnoreCase);
        }

        private string GenerateToken(TelegramAuthRequest telegramData)
        {
            // Here you would implement the logic to generate a JWT token based on the telegramData
            // For now, we will return a dummy token
            return "dummy_token"; // Replace with actual token generation logic
        }

        private static byte[] CreateSecretKey(string botToken)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(botToken));
        }

        private string ComputeHash(string authDataString)
        {
            // Create secret key from bot token
            var secretKey = CreateSecretKey(_config.GetSection("TelegramBot:BotToken").Value ?? throw new ArgumentNullException(nameof(_config))); // Bot's token

            // Compute HMAC-SHA256
            using var hmac = new HMACSHA256(secretKey);
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(authDataString));

            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }

        private string GetKey(string propertyName)
        {
            return propertyName switch
            {
                "Id" => "id",
                "FirstName" => "first_name",
                "LastName" => "last_name",
                "AuthDate" => "auth_date",
                "Hash" => "hash",
                _ => throw new ArgumentException($"Unknown property: {propertyName}")
            };

        }
    }
}
