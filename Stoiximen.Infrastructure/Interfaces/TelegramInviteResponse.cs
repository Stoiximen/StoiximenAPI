using System.Text.Json.Serialization;

namespace Stoiximen.Infrastructure.Models
{
    public class TelegramInviteLinkResponse
    {
        public bool Ok { get; set; }

        public TelegramInviteLinkResult Result { get; set; }
    }

    public class TelegramInviteLinkResult
    {
        public string InviteLink { get; set; }

        public TelegramUser Creator { get; set; }

        public int MemberLimit { get; set; }

        public bool CreatesJoinRequest { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsRevoked { get; set; }
    }

    public class TelegramUser
    {
        public long Id { get; set; }

        public bool IsBot { get; set; }

        public string FirstName { get; set; }

        public string Username { get; set; }
    }
}