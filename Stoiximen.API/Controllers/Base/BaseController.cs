using Microsoft.AspNetCore.Mvc;

namespace Stoiximen.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public abstract class BaseController : ControllerBase
    {
        public BaseController()
        {
        }
    }
}
