using Newtonsoft.Json;
using TiktokAPI.Entities;
using TiktokAPI.Models.Account;

namespace TiktokAPI.Models.Post
{
    public class PostRequestModel
    {
        [JsonProperty("caption")]
        public string? Caption { get; set; }
        [JsonProperty("videoFile")]
        public IFormFile VideoFile { get; set; }
        [JsonProperty("hashTag")]
        public string HashTag { get; set; }

    }
    public class VideoModel
    {
        [JsonProperty("videoId")]
        public long VideoId { get; set; }
        [JsonProperty("videoURL")]
        public string VideoUrl { get; set; } = null!;
        [JsonProperty("caption")]
        public string? Caption { get; set; }
        [JsonProperty("uploadDate")]
        public DateTime? UploadDate { get; set; }
        [JsonProperty("user")]
        public virtual User User { get; set; } = null!;
        [JsonProperty("comments")]
        public virtual ICollection<Comment> Comments { get; set; }
        [JsonProperty("hashTag")]
        public virtual ICollection<HashtagVideo> HashtagVideos { get; set; }
        [JsonProperty("like")]
        public int Like { get; set; }
    }
    public class VideoOverview
    {
        [JsonProperty("videoId")]
        public long VideoId { get; set; }
        [JsonProperty("videoURL")]
        public string VideoUrl { get; set; } = null!;
        [JsonProperty("caption")]
        public string? Caption { get; set; }
        [JsonProperty("uploadDate")]
        public DateTime? UploadDate { get; set; }
        [JsonProperty("like")]
        public int Like { get; set; }
        [JsonProperty("comment")]
        public int Comment { get; set; }
        [JsonProperty("hashTag")]
        public List<HasTagModel> HasTag { get; set; }
        [JsonProperty("user")]
        public UserInfomation User { get; set; }
    }
}
