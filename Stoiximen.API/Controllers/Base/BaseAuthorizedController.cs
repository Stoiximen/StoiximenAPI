using Microsoft.AspNetCore.Authorization;

namespace Stoiximen.API.Controllers
{
    [Authorize(Policy = "TelegramSignedIn")]
    public abstract class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {
        }
    }
}
