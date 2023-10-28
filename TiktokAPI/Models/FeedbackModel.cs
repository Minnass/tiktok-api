using Newtonsoft.Json;

namespace TiktokAPI.Models
{
    public class FeedbackModel
    {
        [JsonProperty("feedbackId")]
        public long? FeedbackId { get; set; }
        [JsonProperty("userId")]
        public string? UserId { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("problem")]
        public string? Problem { get; set; }
        [JsonProperty("image")]
        public IFormFile? Image { get; set; }    

    }
}
