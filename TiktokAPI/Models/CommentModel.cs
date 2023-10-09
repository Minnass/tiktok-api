using Newtonsoft.Json;
using TiktokAPI.Entities;

namespace TiktokAPI.Models
{
    public class CommentModel
    {
        [JsonProperty("commentId")]
        public long CommentId { get; set; }
        [JsonProperty("time")]
        public DateTime? Time { get; set; }
        [JsonProperty("text")]
        public string? Text { get; set; }
        [JsonProperty("user")]
        public User? User { get; set; }
        [JsonProperty("userId")]
        public long? UserId { get; set; }
        [JsonProperty("videoId")]
        public long? VideoId { get; set; }
    }
}
