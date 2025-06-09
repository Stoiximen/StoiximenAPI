namespace Stoiximen.API.Controllers
{
    [ApiController]
    [Route("[controller]")] //api/[controller]  ??
    public class SubscriptionController : ControllerBase // create base controller for authenticated users
    {
        private readonly ILogger<SubscriptionController> _logger;
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService, ILogger<SubscriptionController> logger)
        {
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _logger = logger;
        }

        [Produces("application/json")] // move to base controller
        [HttpGet(Name = "GetSubscriptions")]
        [ProducesResponseType(typeof(List<SubscriptionResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var subscriptionQuery = new GetSubscriptionsQuery(); //oti xreiastei edw...

            var response = await _subscriptionService.GetSubscriptions(subscriptionQuery);

            return Ok(response);
        }
    }
}
