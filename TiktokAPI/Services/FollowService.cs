using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TiktokAPI.Core.Interfaces;
using TiktokAPI.Entities;
using TiktokAPI.Models;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Services
{
    public class FollowService : IFollowService
    {
        private readonly IUnitOfWork<TikTerDBContext> uow;
        public FollowService(IUnitOfWork<TikTerDBContext> uow)
        {
            this.uow = uow;
        }

        public void FollowOrUnFollow(FollowRelationshipModel model)
        {
            Expression<Func<FollowRelationship, bool>> predicate = x => x.IsDeleted == false
            && x.FollowerUser == model.FollowerId
            && x.Followeduser == model.FollowedId;

            var existedFollow = this.uow.GetRepository<FollowRelationship>().Queryable()
            .AsNoTracking()
            .Where(predicate)
            .FirstOrDefault();
            if (existedFollow != null)
            {
                existedFollow.IsDeleted = true;
                this.uow.GetRepository<FollowRelationship>().Delete(existedFollow);
            }
            else
            {
                uow.GetRepository<FollowRelationship>().Insert(
                    new FollowRelationship
                    {
                        FollowerUser = model.FollowerId,
                        Followeduser = model.FollowedId,
                        Time = DateTime.Now,
                        IsDeleted = false
                    });
            }
            uow.SaveChanges();
        }

        public IList<long?> GetFollower(long userId)
        {
            Expression<Func<FollowRelationship,bool>>predicate=x=>x.IsDeleted==false
            &&x.Followeduser==userId;
            var result = this.uow.GetRepository<FollowRelationship>()
                .Queryable()
            .AsNoTracking()
            .Where(predicate).Select(x=>x.FollowerUser)
            .ToList();
            return result;
        }

        public IList<long?> GetFollowingUser(long userId)
        {
            Expression<Func<FollowRelationship, bool>> predicate = x => x.IsDeleted == false
           && x.FollowerUser == userId;
            var result = this.uow.GetRepository<FollowRelationship>()
            .Queryable()
        .AsNoTracking()
        .Where(predicate).Select(x => x.Followeduser)
        .ToList();
            return result;
        }
    }
}
