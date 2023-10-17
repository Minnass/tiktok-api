using Newtonsoft.Json;

namespace TiktokAPI.Models
{
    public class FollowRelationshipModel
    {
        [JsonProperty("followId")]
        public long Id { get; set; }
        [JsonProperty("followerId")]
        public long FollowerId { get; set; }
        [JsonProperty("followedId")]
        public long FollowedId { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }
    }
}
