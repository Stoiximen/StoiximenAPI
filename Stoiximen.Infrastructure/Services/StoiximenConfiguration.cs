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

        public StoiximenConfiguration(IConfiguration configuration)
        {
            TelegramBotToken = configuration["Security:TelegramBot:Token"] ?? throw new ArgumentNullException("TelegramBot:Token not configured");
            TelegramChatId = configuration["Security:TelegramBot:ChatId"] ?? string.Empty;
            JwtSecretKey = configuration["Security:Cryptography:AuthToken:Key"] ?? throw new ArgumentNullException("JWT Key not configured");
            JwtSalt = configuration["Security:Cryptography:AuthToken:Salt"] ?? throw new ArgumentNullException("JWT Salt not configured");
            JwtIssuer = configuration["Security:Cryptography:AuthToken:Issuer"] ?? throw new ArgumentNullException("JWT Issuer not configured");
            JwtAudience = configuration["Security:Cryptography:AuthToken:Audience"] ?? throw new ArgumentNullException("JWT Audience not configured");
            JwtExpirationMinutes = int.Parse(configuration["Security:Cryptography:AuthToken:ExpirationMinutes"] ?? "60");
            DbConnectionString = configuration["ConnectionStrings:StoiximenDb"] ?? throw new ArgumentNullException("Database connection string not configured");
        }
    }
}