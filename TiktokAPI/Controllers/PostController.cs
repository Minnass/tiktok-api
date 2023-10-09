using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiktokAPI.Models.Account;
using TiktokAPI.Models.ApiResponse;
using TiktokAPI.Models.Post;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] PostRequestModel model)
        {
            await this.postService.CreatePost(model);
            return Ok(new ApiResponse("Success", 200));
          
        }
        [AllowAnonymous]
        [HttpGet("{search}")]
        public ActionResult GetAll(string? search)
        {
            var result = this.postService.getAll(search);
            return Ok(new ApiResponse("Success", 200, data: result));
        }
        //[HttpGet("{videoID}")]
        //public ActionResult GetVideo(long videoID)
        //{
        //    var result = this.postService.GetVideo(videoID);
        //    return Ok(new ApiResponse("Success", 200, data: result));
        //}
        //[HttpDelete("{videoID}")]
        //public ActionResult DeleteVideo(long videoID)
        //{
        //    this.postService.DeletePost(videoID);
        //    return Ok(new ApiResponse("Success", 200));
        //}

    }
}