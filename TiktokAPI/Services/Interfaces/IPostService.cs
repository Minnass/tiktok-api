using Microsoft.AspNetCore.Mvc;
using TiktokAPI.Models.Post;

namespace TiktokAPI.Services.Interfaces
{
    public interface IPostService
    {
        VideoOverview GetVideo(long id);
        IList<VideoOverview> getAll(string? search);
        IList<VideoOverview> GetVidesByTagName(string name);
        IList<VideoOverview> GetVideosForUser(long userId);
        IList<VideoOverview> GetFollowingVideos(IList<long> ids);
        Task CreatePost(PostRequestModel file);
        void DeletePost(long ID);
    }
}
