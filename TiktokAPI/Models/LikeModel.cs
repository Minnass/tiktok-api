using Newtonsoft.Json;

namespace TiktokAPI.Models
{
    public class LikeModel
    {
        [JsonProperty("likeId")]
        public long LikeID { get; set; }
    
        [JsonProperty("userId")]
        public long? UserId { get; set; }
        [JsonProperty("videoId")]
        public long? VideoId { get; set; }

    }
}
