using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TiktokAPI.Models.Account;
using TiktokAPI.Models.ApiResponse;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;
        public AccountController(IAuthService authService)
        {
            this.authService = authService;
       
        }
        [AllowAnonymous]
        [HttpPost("SignUp")]
        public ActionResult SignUp([FromBody] SignUpRequest  request)
        {
            var successful = this.authService.SignUp(request);
            return Ok(new ApiResponse("Successful", 200, data: true));
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            JwtAuthResult jwtAuthResult;
            var authInfo = this.authService.Login(request.UserName, request.Password, out jwtAuthResult);
            return Ok(new ApiResponse("Success", 200, data: new LoginResult()
            {
                JwtResult = jwtAuthResult,
                UserInformation = authInfo
            })); ;
        }
        [HttpGet("info")]
        public ActionResult GetInfo()
        {
            return Ok(new ApiResponse("Success", 200, data: this.authService.getUserInfoFromJwt()));
        }

    }
}
