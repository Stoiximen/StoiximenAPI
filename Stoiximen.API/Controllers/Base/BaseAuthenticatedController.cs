using Microsoft.AspNetCore.Authorization;

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
            return User?.Claims?.FirstOrDefault(c => c.Type == "telegram_id")?.Value;
        }
    }
}
