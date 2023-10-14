using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiktokAPI.Models;
using TiktokAPI.Models.ApiResponse;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        [HttpPost("")]
        public ActionResult AddComment(CommentRequestModel model)
        {
            this.commentService.AddComment(model);
            return Ok(new ApiResponse("Success", 200));
        }
        [HttpGet("{videoId}")]
        public ActionResult GetAllComment(long videoId)
        {
            var result = this.commentService.GetAll(videoId);
            return Ok(new ApiResponse("Success", 200, data: result));   
        }
        [HttpDelete("{commentId}")]
        public ActionResult Delete(long commentId)
        {
            this.commentService.Delete(commentId);
            return Ok(new ApiResponse("Success", 200));
        }
    }
}
