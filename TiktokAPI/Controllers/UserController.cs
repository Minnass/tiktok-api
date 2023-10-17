using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("GetSuggestedUsers")]
        public ActionResult GetSuggestedUsers(SuggestedCollection model)
        {
            var result = this.userService.GetSuggestedUsers(model);
            return Ok(new ApiResponse("Success", 200, data: result));
        }
    }
}
