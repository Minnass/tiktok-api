using Newtonsoft.Json;

namespace TiktokAPI.Models.Collection
{
    public class CoreCollectionModel
    {
        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; }
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }   
    }
}
