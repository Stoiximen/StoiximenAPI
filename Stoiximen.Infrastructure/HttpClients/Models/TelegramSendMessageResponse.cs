using System.Text.Json.Serialization;

namespace Stoiximen.Infrastructure.HttpClients.Models
{
    public class TelegramSendMessageResponse : BaseTelegramResponse<MessageResult>
    {

    }

    public class MessageResult
    {
        [JsonPropertyName("message_id")]
        public long MessageId { get; set; }

        [JsonPropertyName("from")]
        public BotInfo From { get; set; }

        [JsonPropertyName("chat")]
        public ChatInfo Chat { get; set; }

        [JsonPropertyName("date")]
        public long Date { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class BotInfo
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("is_bot")]
        public bool IsBot { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }

    public class ChatInfo
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}