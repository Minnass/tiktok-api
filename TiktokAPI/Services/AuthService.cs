using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TiktokAPI.Core.Implementations;
using TiktokAPI.Core.Interfaces;
using TiktokAPI.Entities;
using TiktokAPI.Models.Account;
using TiktokAPI.Services.Interfaces;


namespace TiktokAPI.Services
{
    public class AuthService : IAuthService
    {

        private readonly JwtTokenConfig jwtTokenConfig;
        private readonly IUnitOfWork<TikTerDBContext> uow;
        private readonly IHttpContextAccessor context;
        private readonly byte[] secret;
        public AuthService(JwtTokenConfig jwtTokenConfig, IUnitOfWork<TikTerDBContext> uow, IHttpContextAccessor context)
        {
            this.context = context;
            this.jwtTokenConfig = jwtTokenConfig;
            this.uow = uow;
            this.secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
        }
  
        public JwtAuthResult GenerateTokens(string UserName, Claim[] claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                 jwtTokenConfig.Issuer,
                 shouldAddAudienceClaim ? jwtTokenConfig.Audience : string.Empty,
                 claims,
                 expires: now.AddMinutes(jwtTokenConfig.AccessTokenExpiration),
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256)
                );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToke = new RefreshTokenModel
            {
                UserName = UserName,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = now.AddMinutes(jwtTokenConfig.RefreshTokenExpiration)
            };
            return new JwtAuthResult() { AccessToken = accessToken, RefreshToken = refreshToke };
        }
        public  static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public UserInfomation Login(string userName, string password, out JwtAuthResult jwtAuthResult)
        {
            jwtAuthResult = null;
            var user = uow.GetRepository<User>().Queryable().Where(x => x.UserName == userName).FirstOrDefault();
            if (user != null && password.Trim() != user.HashedPassword)
                throw new Exception("Khong tim thay");
            if (user == null)
                throw new Exception("Khong tim thay");
            var result = new UserInfomation()
            {
                UserName = userName,
                UserId = user.UserId,
                Avatar = user.Avatar,
                Bio = user.Bio,
                DisplayedName = user.DisplayedName,
                Email = user.Email,
                IsDeleted = user.IsDeleted,
            };
            var claims = new List<Claim>()
              {
                  new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                  new Claim(ClaimTypes.Name, user.UserName),
                  new Claim(ClaimTypes.Email,user.Email)
              };

            jwtAuthResult = GenerateTokens(user.UserName, claims.ToArray(), DateTime.Now);
            if (!user.RefreshTokens.Where(x=>x.IsUsed==false&&x.IsDeleted==false).Any())
            {
                user.RefreshTokens.Add(new RefreshToken
                {
                    UserId = user.UserId,
                    Token = jwtAuthResult.RefreshToken.TokenString,
                    ExpiredDate = jwtAuthResult.RefreshToken.ExpireAt,
                    IssuedDate = DateTime.Now,
                    IsDeleted = false,
                    IsUsed = false
                });
                uow.GetRepository<User>().Update(user);
                uow.SaveChanges();
            }
            return result;
        }

        public JwtAuthResult RefreshToken(string token, string refreshToken, DateTime now)
        {
            throw new NotImplementedException();
        }

        public bool SignUp(SignUpRequest request)
        {
            var user = uow.GetRepository<User>().Queryable().Where(x => x.Email == request.Email).FirstOrDefault();
            if (user != null)
            {
                throw new Exception("User ton tai");
            }
            if (user != null && (user.UserName == request.UserName))
            {
                throw new Exception("User da ton tai");
            }
            var newUser = new User()
            {
                UserName = request.UserName,
                DisplayedName = request.DisplayedName,
                HashedPassword = request.Password,
                Email = request.Email,
                IsDeleted=false
            };
            uow.GetRepository<User>().Insert(newUser);
            uow.SaveChanges();
            return true;
        }

        public UserInfomation getUserInfoFromJwt()
        {
            var userIdClaim = context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out var userId))
            {
                throw new Exception("Invalid or missing UserId in the JWT token.");
            }
            var user = uow.GetRepository<User>().Queryable().Where(x =>x.UserId==long.Parse(userIdClaim.Value)).FirstOrDefault();
            return new UserInfomation()
            {
                Email = user.Email,
                Avatar = user.Avatar,
                Bio = user.Bio,
                DisplayedName = user.DisplayedName,
                IsDeleted = user.IsDeleted,
                UserId = user.UserId,
                UserName = user.UserName
            };
        }
    }
}
