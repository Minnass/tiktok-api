using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TiktokAPI.Entities;
using TiktokAPI.Models;
using TiktokAPI.Models.ApiResponse;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService userService;
        public SearchController(ISearchService searchService)
        {
            this.userService = searchService;
        }
        [HttpGet("{userId}")]
        public ActionResult GetSearchedByUser(long userId)
        {
            var result = userService.GetKeywordByUser(userId);
            return Ok(new ApiResponse("Success", 200, data: result));
        }
        [HttpDelete("{searchId}")]
        public ActionResult Delete(long searchId)
        {
            userService.Delete(searchId);
            return Ok(new ApiResponse("Success", 200));
        }
        [HttpPost("")]
        public ActionResult Add(SearchModel model)
        {
            var result=userService.Add(model);
            return Ok(new ApiResponse("Success", 200,data: result));
        }
    }
}
