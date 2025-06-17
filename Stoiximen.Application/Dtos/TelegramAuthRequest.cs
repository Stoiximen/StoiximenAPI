namespace Stoiximen.Application.Dtos
{
    public class TelegramAuthRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public TelegramAuthRequest()
        {

        }

        public TelegramAuthRequest(int id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
