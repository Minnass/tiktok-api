﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiktokAPI.Models;
using TiktokAPI.Models.Account;
using TiktokAPI.Models.ApiResponse;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
    private readonly IFollowService followService;
        public FollowController(IFollowService followService)
        {
            this.followService = followService;
        }
        [HttpPost("")]
        public ActionResult AddFollow([FromBody] FollowRelationshipModel request)
        {
            this.followService.FollowOrUnFollow(request);
            return Ok(new ApiResponse("Success", 200));
        }
        [HttpGet("GetFollower/{userId}")]
        public ActionResult GetFollower(long userId)
        {
            var result=this.followService.GetFollower(userId);
            return Ok(new ApiResponse("Success", 200, data: result));
        }
        [HttpGet("GetFollowing/{userId}")]
        public ActionResult GetFollowing(long userId)
        {
            var result = this.followService.GetFollowingUser(userId);
            return Ok(new ApiResponse("Success", 200, data: result));
        }
    }
}
