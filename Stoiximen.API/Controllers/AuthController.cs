using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stoiximen.Application.Dtos;
using Stoiximen.Application.Interfaces;

namespace Stoiximen.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateToken([FromBody] TelegramAuthRequest request)
        {
            var response = await _authService.ValidateTelegramHashAndGenerateToken(request);

            return Ok(response);
        }
    }

}
