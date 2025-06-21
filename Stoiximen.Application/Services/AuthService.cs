using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Stoiximen.Application.Dtos;
using Stoiximen.Application.Interfaces;
using Stoiximen.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Stoiximen.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IStoiximenConfiguration _config;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IStoiximenConfiguration config, ILogger<AuthService> logger)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<string> ValidateTelegramHashAndGenerateToken(TelegramAuthRequest telegramData)
        {
            _logger.LogInformation("Authentication attempt for Telegram user {TelegramId}", telegramData.Id);

            if (IsTelegramHashValid(telegramData))
            {
                _logger.LogInformation("Hash validation successful for user {TelegramId}", telegramData.Id);

                var token = GenerateJwtToken(telegramData);

                _logger.LogInformation("JWT token generated for user {TelegramId}", telegramData.Id);

                return Task.FromResult(token);
            }
            else
            {
                throw new UnauthorizedAccessException($"Invalid Telegram authentication data for {telegramData.Id}.");
            }
        }

        private bool IsTelegramHashValid(TelegramAuthRequest telegramData)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            telegramData.GetType().GetProperties().ToList().ForEach(prop =>
            {
                var value = prop.GetValue(telegramData, null);
                if (value != null)
                {
                    var key = GetKey(prop.Name);
                    if (key == "hash")
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

        private static byte[] CreateSecretKey(string botToken)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(botToken));
        }

        private string ComputeHash(string authDataString)
        {
            // Create secret key from bot token
            var secretKey = CreateSecretKey(_config.TelegramBotToken);

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

        private string GenerateJwtToken(TelegramAuthRequest telegramData)
        {
            var saltedKey = _config.JwtSecretKey + _config.JwtSalt;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(saltedKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Create claims for the user
            var claims = new[]
            {
                new Claim("telegram_id", telegramData.Id),
                new Claim("first_name", telegramData.FirstName),
                new Claim("last_name", telegramData.LastName),
                new Claim("auth_date", telegramData.AuthDate),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            //Create the token
            var token = new JwtSecurityToken(
                issuer: _config.JwtIssuer,
                audience: _config.JwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_config.JwtExpirationMinutes),
                signingCredentials: credentials
            );

            // Return the serialized token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
