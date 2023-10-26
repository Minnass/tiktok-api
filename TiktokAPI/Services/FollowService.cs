using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TiktokAPI.Core.Interfaces;
using TiktokAPI.Entities;
using TiktokAPI.Helper.Collection;
using TiktokAPI.Helper.Collection.Interfaces;
using TiktokAPI.Models;
using TiktokAPI.Models.Account;
using TiktokAPI.Models.Collection;
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
            Expression<Func<FollowRelationship, bool>> predicate = x => x.IsDeleted == false
            && x.Followeduser == userId;
            var result = this.uow.GetRepository<FollowRelationship>()
                .Queryable()
            .AsNoTracking()
            .Where(predicate).Select(x => x.FollowerUser)
            .ToList();
            return result;
        }

        public IList<UserInfomation> GetFollowingUser(long userId)
        {
            Expression<Func<FollowRelationship, bool>> predicate = x => x.IsDeleted == false
           && x.FollowerUser == userId;
            var result = this.uow.GetRepository<FollowRelationship>()
            .Queryable()
            .Include(x=>x.FolloweduserNavigation)
        .AsNoTracking()
        .Where(predicate).Select(x => new UserInfomation
        {
            Avatar=x.FolloweduserNavigation.Avatar,
            DisplayedName=x.FolloweduserNavigation.DisplayedName,
            UserName= x.FolloweduserNavigation.UserName,
            UserId= x.FolloweduserNavigation.UserId
        })
        .ToList();
            return result;
        }

        public IList<UserInfomation> GetFollowingUserForPaged(FollowingPaged model)
        {
            Expression<Func<FollowRelationship, bool>> predicate = x => x.IsDeleted == false
            && x.FollowerUser == model.UserId;
            var queryable = this.uow.GetRepository<FollowRelationship>().Queryable()
                .AsNoTracking()
                .Include(x => x.FolloweduserNavigation)
                .Where(predicate)
                .Select(x => new UserInfomation
                {
                    Avatar = x.FolloweduserNavigation.Avatar,
                    DisplayedName = x.FolloweduserNavigation.DisplayedName,
                    UserId = x.FolloweduserNavigation.UserId,
                    UserName = x.FolloweduserNavigation.UserName
                });
            var result = queryable.Take(model.PageSize * model.PageNumber).ToList();
            return result;
        }

        public FollowingRelationshipModel GetFollwersAndFollowings(long userId)
        {
            Expression<Func<FollowRelationship, bool>> followersPredicate = x => x.IsDeleted == false && x.Followeduser == userId;
            var followers = this.uow.GetRepository<FollowRelationship>().Queryable()
                 .AsNoTracking()
                 .Include(x => x.FollowerUserNavigation)
                 .Where(followersPredicate)
                 .Select(x => new UserInfomation
                 {
                     Avatar=x.FollowerUserNavigation.Avatar,
                     Bio=x.FollowerUserNavigation.Bio,
                     DisplayedName=x.FollowerUserNavigation.DisplayedName,
                     UserName=x.FollowerUserNavigation.UserName ,
                     UserId=x.FollowerUserNavigation.UserId 
                 }).ToList();
            Expression<Func<FollowRelationship, bool>> followingPredicate = x => x.IsDeleted == false && x.FollowerUser == userId;
            var followings = this.uow.GetRepository<FollowRelationship>().Queryable()
                 .AsNoTracking()
                 .Include(x => x.FolloweduserNavigation)
                 .Where(followingPredicate)
                 .Select(x => new UserInfomation
                 {
                     Avatar = x.FolloweduserNavigation.Avatar,
                     Bio = x.FolloweduserNavigation.Bio,
                     DisplayedName = x.FolloweduserNavigation.DisplayedName,
                     UserName = x.FolloweduserNavigation.UserName,
                     UserId = x.FolloweduserNavigation.UserId
                 }).ToList();
            var result = new FollowingRelationshipModel
            {
               UserId=userId,
               Followers=followers,
               Followings=followings
            };
            return result;
        }
    }
}
