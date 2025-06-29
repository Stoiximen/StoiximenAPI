﻿namespace Stoiximen.Infrastructure.Interfaces
{
    public interface IStoiximenConfiguration
    {
        // Telegram
        string TelegramBotToken { get; }
        string TelegramChatId { get; }

        // JWT
        string JwtSecretKey { get; }
        string JwtSalt { get; }
        string JwtIssuer { get; }
        string JwtAudience { get; }
        int JwtExpirationMinutes { get; }

        // DB
        string DbConnectionString { get; }

        // Services Endpoints
        string TelegramUri { get; }

        int RequestLimit { get; }
        int HttpTimeoutInSeconds { get; }
    }
}
