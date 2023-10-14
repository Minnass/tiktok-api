using Microsoft.AspNetCore.Mvc;
using TiktokAPI.Models.Post;

namespace TiktokAPI.Services.Interfaces
{
    public interface IPostService
    {
        VideoOverview GetVideo(long id);
        IList<VideoOverview> getAll(string? search);
        Task CreatePost(PostRequestModel file);
        void DeletePost(long ID);
    }
}
