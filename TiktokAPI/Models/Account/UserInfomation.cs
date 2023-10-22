using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace TiktokAPI.Models.Account
{
    public class UserInfomation
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }
        [JsonProperty("userName")]
        public string? UserName { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("displayedName")]
        public string? DisplayedName { get; set; }

        [JsonProperty("bio")]
        public string? Bio    { get; set; }
        [JsonProperty("avatar")]
        public string? Avatar { get; set; }
        [JsonProperty("isDeleted")]
        public bool? IsDeleted { get; set; }
      
    }

    public class UserUploadModel
    {
     
        [JsonProperty("userName")]
        public string? UserName { get; set; }

        [JsonProperty("displayedName")]
        public string? DisplayedName { get; set; }

        [JsonProperty("bio")]
        public string? Bio { get; set; }
        [JsonProperty("avatar")]
        public  IFormFile? Avatar { get; set; }

    }


    public class SignUpRequest
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("displayedName")]
        public string DisplayedName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public class LoginRequest
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public class JwtAuthResult
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
        [JsonPropertyName("refreshToken")]
        public RefreshTokenModel RefreshToken { get; set; }
    }
    public class RefreshTokenModel
    {
        [JsonPropertyName("userID")]
        public long? UserID { get; set; }
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("tokenString")]
        public string TokenString { get; set; }
        [JsonPropertyName("expireAt")]
        public DateTime ExpireAt { get; set; }
    }
    public class JwtTokenConfig
    {
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }
        [JsonPropertyName("audience")]
        public string Audience { get; set; }
        [JsonPropertyName("accessTokenExpiration")]
        public int AccessTokenExpiration { get; set; }
        [JsonPropertyName("refreshTokenExpiration")]
        public int RefreshTokenExpiration { get; set; }
    }

    public class LoginResult
    {
        [JsonPropertyName("jwtResult")]
        public JwtAuthResult JwtResult { get; set; }
        [JsonPropertyName("userInfomation")]
        public UserInfomation  UserInformation{ get; set; }

}

}
