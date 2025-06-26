using Microsoft.AspNetCore.Authorization;
using Stoiximen.Infrastructure.Constants;

namespace Stoiximen.API.Controllers
{
    [Authorize]
    public abstract class BaseAuthenticatedController : BaseController
    {
        public BaseAuthenticatedController()
        {
        }

        protected string? GetUserIdFromToken()
        {
            return User?.Claims?.FirstOrDefault(c => c.Type == Claims.TelegramUserId)?.Value;
        }
    }
}
