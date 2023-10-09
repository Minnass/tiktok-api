using TiktokAPI.Models;

namespace TiktokAPI.Services.Interfaces
{
    public interface ICommentService
    {
        IList<CommentModel> GetAll(long videoId);
        void Delete(long commentId);
        void AddComment(CommentModel comment);
    }
}
