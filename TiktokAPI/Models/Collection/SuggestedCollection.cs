using Newtonsoft.Json;

namespace TiktokAPI.Models.Collection
{
    public class SuggestedCollection:CoreCollectionModel
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }
    }
}
