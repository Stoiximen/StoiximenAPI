namespace Stoiximen.Application.Dtos
{
    public class TelegramAuthRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AuthDate { get; set; }
        public string Hash { get; set; }
    }
}