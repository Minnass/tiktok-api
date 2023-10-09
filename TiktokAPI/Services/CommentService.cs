using TiktokAPI.Models;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Services
{
    public class CommentService : ICommentService
    {
        public void AddComment(CommentModel comment)
        {
            throw new NotImplementedException();
        }

        public void Delete(long commentId)
        {
            throw new NotImplementedException();
        }

        public IList<CommentModel> GetAll(long videoId)
        {
            throw new NotImplementedException();
        }
    }
}
