using System.Text.Json.Serialization;

namespace Stoiximen.Infrastructure.HttpClients.Models
{
    public class TelegramInviteLinkResponse : BaseTelegramResponse<TelegramInviteLinkResult>
    {
    }

    public class TelegramInviteLinkResult
    {
        [JsonPropertyName("invite_link")]
        public string InviteLink { get; set; }

        [JsonPropertyName("creator")]
        public TelegramCreator Creator { get; set; }

        [JsonPropertyName("member_limit")]
        public int MemberLimit { get; set; }

        [JsonPropertyName("creates_join_request")]
        public bool CreatesJoinRequest { get; set; }

        [JsonPropertyName("is_primary")]
        public bool IsPrimary { get; set; }

        [JsonPropertyName("is_revoked")]
        public bool IsRevoked { get; set; }
    }

    public class TelegramCreator
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
}