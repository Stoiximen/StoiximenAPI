using Microsoft.AspNetCore.Mvc;
using Stoiximen.Application.Dtos;
using Stoiximen.Application.Services.Subscription;

namespace Stoiximen.API.Controllers
{
    public class SubscriptionController : BaseAuthenticatedController
    {
        private readonly ILogger<SubscriptionController> _logger;
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService, ILogger<SubscriptionController> logger)
        {
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _logger = logger;
        }

        [HttpGet]
        [Route("subscriptions")]
        [ProducesResponseType(typeof(List<SubscriptionResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var response = await _subscriptionService.GetSubscriptions();

            return Ok(response);
        }
    }
}
