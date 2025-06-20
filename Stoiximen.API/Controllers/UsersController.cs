using Microsoft.AspNetCore.Mvc;
using Stoiximen.Application.Dtos;
using Stoiximen.Application.Interfaces;

namespace Stoiximen.API.Controllers
{
    public class UsersController : BaseAuthenticatedController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger;
        }

        [HttpGet]
        [Route("me")]
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var id = GetUserIdFromToken();

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID is not available in the token.");
            }

            var response = await _userService.GetUserById(id);

            if (response == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(response);
        }

    }
}
