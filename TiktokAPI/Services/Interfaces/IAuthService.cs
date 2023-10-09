using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TiktokAPI.Models.Account;

namespace TiktokAPI.Services.Interfaces
{
    public interface IAuthService
    {
        bool SignUp(SignUpRequest request); 
        UserInfomation Login(string userName, string password, out JwtAuthResult jwtAuthResult);
        JwtAuthResult RefreshToken(string token,string refreshToken,DateTime now);
        public JwtAuthResult GenerateTokens(string UserName, Claim[] claims, DateTime now);
        UserInfomation getUserInfoFromJwt();

    }
}
