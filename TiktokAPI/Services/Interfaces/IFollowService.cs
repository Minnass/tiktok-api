﻿using TiktokAPI.Helper.Collection.Interfaces;
using TiktokAPI.Models;
using TiktokAPI.Models.Account;
using TiktokAPI.Models.Collection;

namespace TiktokAPI.Services.Interfaces
{
    public interface IFollowService
    {
        void FollowOrUnFollow(FollowRelationshipModel model);
  
        IList<long?> GetFollower(long userId);
        IList<long?>GetFollowingUser(long userId);
        IList<UserInfomation>GetFollowingUserForPaged(FollowingPaged userId);
    }
}
