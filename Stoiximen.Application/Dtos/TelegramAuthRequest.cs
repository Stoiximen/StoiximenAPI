using System.ComponentModel.DataAnnotations;

namespace Stoiximen.Application.Dtos
{
    public class TelegramAuthRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string AuthDate { get; set; }
        [Required]
        public string Hash { get; set; }
        public string? Username { get; set; }
        public string? PhotoUrl { get; set; }
    }
}