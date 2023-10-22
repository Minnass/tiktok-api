using Newtonsoft.Json;
using TiktokAPI.Models.Account;

namespace TiktokAPI.Models
{
    public class FollowingRelationshipModel
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }
        [JsonProperty("followers")]
        public IList<UserInfomation> Followers { get; set; }
        [JsonProperty("followings")]
        public IList<UserInfomation> Followings { get; set; }
    }
}
