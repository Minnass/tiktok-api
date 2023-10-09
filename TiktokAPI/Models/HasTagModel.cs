using Newtonsoft.Json;

namespace TiktokAPI.Models
{
    public class HasTagModel
    {
        [JsonProperty("hasTagId")]
        public long HasTagId { get; set; }
        [JsonProperty("hasTagName")]
        public string HasTagName { get; set; }  
    }
}
