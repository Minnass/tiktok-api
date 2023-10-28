using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TiktokAPI.Core.Interfaces;
using TiktokAPI.Entities;
using TiktokAPI.Models;
using TiktokAPI.Models.Post;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork<TikTerDBContext> uow;
        public LikeService(IUnitOfWork<TikTerDBContext> uow)
        {
            this.uow = uow;
        }

        public IList<long?> GetLikedVideo(long userId)
        {
            Expression<Func<Like, bool>> predicate = x => x.UserId == userId&&x.IsDislike==false;
            var result=uow.GetRepository<Like>().Queryable().AsNoTracking().Where(predicate).Select(x=>x.VideoId).ToList();
            return result;
        }

        public int GetLikes(long videoID)
        {
            Expression<Func<Like, bool>> predicate = x => x.IsDislike == false && x.VideoId == videoID;
            var result=uow.GetRepository<Like>().Queryable().AsNoTracking().Where(predicate).Count(); 
            return result;
        }

        public void LikeOrDislike(LikeModel model)
        {
            Expression<Func<Like, bool>> predicae = x => x.VideoId == model.VideoId && x.UserId ==model.UserId;
            var item=uow.GetRepository<Like>().Queryable().Where(predicae).FirstOrDefault();
            if (item != null)
            {
                item.IsDislike=!item.IsDislike;
            }
            else
            {
                var newLike = new Like
                {
                    IsDislike = false,
                    UserId = model.UserId,
                    VideoId = model.VideoId
                };
                uow.GetRepository<Like>().Insert(newLike);
            }
            uow.SaveChanges();
        }
    }
}
