using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiktokAPI.Models;
using TiktokAPI.Models.ApiResponse;
using TiktokAPI.Services;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService likeService;
        public LikeController(ILikeService likeService)
        {
            this.likeService = likeService;
        }
        [HttpGet("{videoId}")]
        public ActionResult GetLikes(long videoId)
        {
            var result = this.likeService.GetLikes(videoId);
            return Ok(new ApiResponse("Success", 200, data: result));
        }
        [HttpPost("like")]
        public ActionResult LikeOrDislike(LikeModel model)
        {
            likeService.LikeOrDislike(model);
            return Ok(new ApiResponse("Success", 200));

        }
        //[HttpGet("{userId")]
        //public ActionResult GetLikedVideos(long userId)
        //{
        //    var result=this.likeService.GetLikedVideo(userId);
        //    return Ok(new ApiResponse("Success", 200, data: result));
        //}

    }
}
