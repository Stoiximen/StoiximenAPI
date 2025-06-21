namespace Stoiximen.Domain.Models
{
    public class User : SoftDeletableEntity, IEntity<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int SubscriptionStatus { get; set; }
    }
}
