using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiktokAPI.Entities;
using TiktokAPI.Models.Account;
using TiktokAPI.Models.ApiResponse;
using TiktokAPI.Models.Collection;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("{search}")]
        public ActionResult GetUsers(string search)
        {
            var result = this.userService.GetUsers(search);
            return Ok(new ApiResponse("Success", 200, data: result));
        }
        [HttpPost("GetSuggestedUsers")]
        public ActionResult GetSuggestedUsers(SuggestedCollection model)
        {
            var result = this.userService.GetSuggestedUsers(model);
            return Ok(new ApiResponse("Success", 200, data: result));
        }
        [AllowAnonymous]
        [HttpGet("getUserInfo/{userName}")]
        public ActionResult GetUserInfo(string userName)
        {
            var result = this.userService.GetUserInfomation(userName);
            return Ok(new ApiResponse("Success", 200, data: result));
        }
        [HttpPost("Upload")]
        public ActionResult UploadUserInfo([FromForm] UserUploadModel model)
        {
            this.userService.UploadUser(model);
            return Ok(new ApiResponse("Success", 200));
        }
    }
}
