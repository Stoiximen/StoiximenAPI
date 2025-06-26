using Newtonsoft.Json;

namespace Stoiximen.Infrastructure.HttpClients.Models
{
    public class BaseTelegramResponse<T> where T : class
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }
    }
}