namespace Stoiximen.Domain.Models
{
    public class Subscription : SoftDeletableEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
