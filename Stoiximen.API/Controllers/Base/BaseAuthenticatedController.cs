using Microsoft.AspNetCore.Authorization;

namespace Stoiximen.API.Controllers
{
    [Authorize]
    public abstract class BaseAuthenticatedController : BaseController
    {
        public BaseAuthenticatedController()
        {
        }
    }
}
