using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TiktokAPI.Core.Interfaces;
using TiktokAPI.Entities;
using TiktokAPI.Models;
using TiktokAPI.Models.Account;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork<TikTerDBContext> uow;
        public CommentService(IUnitOfWork<TikTerDBContext> uow)
        {
            this.uow = uow;
        }
        public void AddComment(CommentRequestModel comment)
        {
            this.uow.GetRepository<Comment>().Insert(new Comment
            {
                IsDeleted=false,
                Time=DateTime.Now,
                UserId=comment.UserId,
                Text=comment.Text,
                VideoId=comment.VideoId,
            });
            this.uow.SaveChanges();
        }

        public void Delete(long commentId)
        {
            var existedComment = this.uow.GetRepository<Comment>().Queryable().Where(x => x.CommentId == commentId).FirstOrDefault();
            if (existedComment == null)
            {
                throw new Exception("Comment not found");
            }
            existedComment.IsDeleted = true;
            this.uow.SaveChanges();
        }

        public IList<CommentModel> GetAll(long videoId)
        {
                Expression<Func<Comment, bool>> predicate = x => x.IsDeleted == false && x.VideoId == videoId;  
            var result = this.uow.GetRepository<Comment>().Queryable()
                .AsNoTracking()
                .Include(x=>x.User)
                .Where(predicate)
                .Select(x=> new CommentModel
                {
                    CommentId = x.CommentId,
                    Text = x.Text,
                    Time  =x.Time,
                    User=new UserInfomation
                    {
                        Avatar=x.User.Avatar,
                        DisplayedName=x.User.DisplayedName,
                        UserId=x.User.UserId,
                        UserName=x.User.UserName
                    },
                    VideoId =x.VideoId,
                    UserId = x.UserId
                }).ToList();
            return result;
        }
    }
}
