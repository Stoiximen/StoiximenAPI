namespace Stoiximen.Contracts.Resources.Subscription
{
    public class SubscriptionResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public SubscriptionResource()
        {

        }

        public SubscriptionResource(int id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
