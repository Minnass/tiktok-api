using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiktokAPI.Models;
using TiktokAPI.Models.ApiResponse;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }
        [HttpPost("")]
        public  ActionResult AddFeedback([FromForm] FeedbackModel model)
        {
             this.feedbackService.CreateFeedback(model);
            return Ok(new ApiResponse("Success", 200));
        }
    }
}
