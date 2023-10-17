using Newtonsoft.Json;

namespace TiktokAPI.Models.Collection
{
    public class FollowingPaged:CoreCollectionModel
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }
    }
}
