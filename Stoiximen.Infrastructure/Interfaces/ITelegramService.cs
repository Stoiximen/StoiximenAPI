namespace Stoiximen.Infrastructure.Interfaces
{
    public interface ITelegramService
    {
        Task InviteUserToGroupChat(string userId);
    }
}
