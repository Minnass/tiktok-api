using TiktokAPI.Models;

namespace TiktokAPI.Services.Interfaces
{
    public interface IFollowService
    {
        void FollowOrUnFollow(FollowRelationshipModel model);
  
        IList<long?> GetFollower(long userId);
        IList<long?>GetFollowingUser(long userId);
    }
}
