using Newtonsoft.Json;

namespace TiktokAPI.Models
{
    public class SearchModel
    {
        [JsonProperty("searchId")]
        public long? SearchId { get; set; }
        [JsonProperty("userId")]
        public long? UserId { get; set; }
        [JsonProperty("keyWord")]
        public string? KeyWord { get; set; }

    }
}
