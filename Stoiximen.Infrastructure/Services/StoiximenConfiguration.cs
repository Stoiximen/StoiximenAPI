using Microsoft.Extensions.Configuration;
using Stoiximen.Infrastructure.Interfaces;

namespace Stoiximen.Infrastructure.Services
{
    public class StoiximenConfiguration : IStoiximenConfiguration
    {
        public string TelegramBotToken { get; }
        public string TelegramChatId { get; }
        public string JwtSecretKey { get; }
        public string JwtSalt { get; }
        public string JwtIssuer { get; }
        public string JwtAudience { get; }
        public int JwtExpirationMinutes { get; }
        public string DbConnectionString { get; }
        public string TelegramUri { get; }
        public int RequestLimit { get; }
        public int HttpTimeoutInSeconds { get; }

        public StoiximenConfiguration(IConfiguration configuration)
        {
            TelegramBotToken = configuration["Security:TelegramBot:Token"] ?? throw new ArgumentNullException("TelegramBot:Token not configured");
            TelegramChatId = configuration["Security:TelegramBot:ChatId"] ?? string.Empty;
            JwtSecretKey = configuration["Security:Cryptography:AuthToken:Key"] ?? throw new ArgumentNullException("JWT Key not configured");
            JwtSalt = configuration["Security:Cryptography:AuthToken:Salt"] ?? throw new ArgumentNullException("JWT Salt not configured");
            JwtIssuer = configuration["Security:Cryptography:AuthToken:Issuer"] ?? throw new ArgumentNullException("JWT Issuer not configured");
            JwtAudience = configuration["Security:Cryptography:AuthToken:Audience"] ?? throw new ArgumentNullException("JWT Audience not configured");
            DbConnectionString = configuration["ConnectionStrings:StoiximenDb"] ?? throw new ArgumentNullException("Database connection string not configured");
            TelegramUri = configuration["ServicesEndpoints:TelegramApi"] ?? throw new ArgumentNullException("Telegram api URI string not configured");
            JwtExpirationMinutes = Int32.TryParse(configuration["Security:Cryptography:AuthToken:ExpirationMinutes"], out int jwtExpirationMinutes)
                ? jwtExpirationMinutes
                : throw new ArgumentNullException("Telegram Jwt expirations not configured");
            RequestLimit = Int32.TryParse(configuration["Security:RequestLimit"], out int requestLimit)
                ? requestLimit
                : throw new ArgumentNullException("RequestLimit not configured");
            HttpTimeoutInSeconds = Int32.TryParse(configuration["ServicesEndpoints:HttpTimeoutInSeconds"], out int httpTimeout)
                ? httpTimeout
                : throw new ArgumentNullException("Http timeout not configured");
        }
    }
}