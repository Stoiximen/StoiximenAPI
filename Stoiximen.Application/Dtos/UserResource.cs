namespace Stoiximen.Application.Dtos
{
    public class UserResource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int SubscriptionStatus { get; set; }

        public UserResource()
        {

        }

        public UserResource(string id, string name, int subscriptionStatus)
        {
            Id = id;
            Name = name;
            SubscriptionStatus = subscriptionStatus;
        }
    }
}
