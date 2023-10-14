using Newtonsoft.Json;
using TiktokAPI.Entities;
using TiktokAPI.Models.Account;

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
        public UserInfomation?User { get; set; }
        [JsonProperty("userId")]
        public long? UserId { get; set; }
        [JsonProperty("videoId")]
        public long? VideoId { get; set; }
    }
    public class CommentRequestModel
    {
        [JsonProperty("videoId")]
        public long? VideoId { get; set; }
        [JsonProperty("userId")]
        public long? UserId { get; set; }
        [JsonProperty("text")]
        public string? Text { get; set; }
    }
}
