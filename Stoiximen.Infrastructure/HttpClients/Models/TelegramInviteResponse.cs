using Newtonsoft.Json;

namespace Stoiximen.Infrastructure.HttpClients.Models
{
    public class TelegramInviteLinkResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("result")]
        public TelegramInviteLinkResult Result { get; set; }
    }

    public class TelegramInviteLinkResult
    {
        [JsonProperty("invite_link")]
        public string InviteLink { get; set; }

        [JsonProperty("creator")]
        public TelegramCreator Creator { get; set; }

        [JsonProperty("member_limit")]
        public int MemberLimit { get; set; }

        [JsonProperty("creates_join_request")]
        public bool CreatesJoinRequest { get; set; }

        [JsonProperty("is_primary")]
        public bool IsPrimary { get; set; }

        [JsonProperty("is_revoked")]
        public bool IsRevoked { get; set; }
    }

    public class TelegramCreator
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("is_bot")]
        public bool IsBot { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}