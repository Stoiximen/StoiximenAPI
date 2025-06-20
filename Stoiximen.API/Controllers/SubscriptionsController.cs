using Microsoft.AspNetCore.Mvc;
using Stoiximen.Application.Dtos;
using Stoiximen.Application.Interfaces;

namespace Stoiximen.API.Controllers
{
    public class SubscriptionsController : BaseAuthenticatedController
    {
        private readonly ILogger<SubscriptionsController> _logger;
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService, ILogger<SubscriptionsController> logger)
        {
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _logger = logger;
        }

        [HttpGet]
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
