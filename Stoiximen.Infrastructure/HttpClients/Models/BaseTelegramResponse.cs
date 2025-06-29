using System.Text.Json.Serialization;

namespace Stoiximen.Infrastructure.HttpClients.Models
{
    public class BaseTelegramResponse<T> where T : class
    {
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("result")]
        public T Result { get; set; }
    }
}