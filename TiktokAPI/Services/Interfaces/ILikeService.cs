using TiktokAPI.Models;

namespace TiktokAPI.Services.Interfaces
{
    public interface ILikeService
    {
        int GetLikes(long videoID);
        void LikeOrDislike(LikeModel model);
        IList<long?> GetLikedVideo(long userId);
    }
}
