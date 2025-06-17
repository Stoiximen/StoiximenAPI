using Microsoft.AspNetCore.Authorization;

namespace Stoiximen.API.Controllers
{
    [Authorize(Policy = "AuthenticatedUser")]
    public abstract class BaseAuthenticatedController : BaseController
    {
        public BaseAuthenticatedController()
        {
        }
    }
}
