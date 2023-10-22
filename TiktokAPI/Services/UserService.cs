using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TiktokAPI.Core.Interfaces;
using TiktokAPI.Entities;
using TiktokAPI.Models.Account;
using TiktokAPI.Models.Collection;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<TikTerDBContext> uow;
        private readonly IAuthService authService;
        public UserService(IUnitOfWork<TikTerDBContext> uow, IAuthService authService)
        {
            this.uow = uow;
            this.authService = authService;
        }
        public IList<UserInfomation> GetSuggestedUsers(SuggestedCollection model)
        {
            var queryable = this.uow.GetRepository<User>().Queryable()
                 .AsNoTracking().
                 Where(x => x.IsDeleted == false && x.UserId != model.UserId)
                .Select(x => new UserInfomation
                {
                    Avatar = x.Avatar,
                    DisplayedName = x.DisplayedName,
                    UserId = x.UserId,
                    UserName = x.UserName
                }).ToList();
            var result = queryable.Take(model.PageSize * model.PageNumber).ToList();
            return result;
        }

        public IList<UserInfomation> GetUsers(string search)
        {
            Expression<Func<User, bool>> predicate = x => x.IsDeleted == false && (x.UserName.Contains(search) || x.DisplayedName.Contains(search));
            var result = this.uow.GetRepository<User>().Queryable()
                 .AsNoTracking()
                 .Where(predicate)
                  .Select(x => new UserInfomation
                  {
                      Avatar = x.Avatar,
                      DisplayedName = x.DisplayedName,
                      UserId = x.UserId,
                      UserName = x.UserName,
                      Bio = x.Bio
                  }).ToList();
            return result;
        }
        public UserInfomation GetUserInfomation(string userName )
        {
            var result=this.uow.GetRepository<User>().Queryable()
                .AsNoTracking()
                .Where(x => x.UserName == userName && x.IsDeleted == false)
                .Select(x => new UserInfomation()
                {
                    Email = x.Email,
                    Avatar = x.Avatar,
                    Bio = x.Bio,
                    DisplayedName = x.DisplayedName,
                    IsDeleted = x.IsDeleted,
                    UserId = x.UserId,
                    UserName = x.UserName
                }).FirstOrDefault();
            if (result == null)
            {
                throw new Exception("user not found!");
            }
            return result;
        }

        public void UploadUser(UserUploadModel model)
        {
            var userId = this.authService.getUserInfoFromJwt().UserId;
            Expression<Func<User, bool>> predicate = x => x.IsDeleted == false && x.UserId == userId;
            var user = this.uow.GetRepository<User>()
                .Queryable()
                .Where(predicate).FirstOrDefault(); 
            if(user==null)
            {
                throw new Exception("User not found!");
            }
            if(model.Avatar!=null)
            {

            }
            user.Bio = model.Bio;
            user.DisplayedName=model.DisplayedName;
            uow.SaveChanges();
        }
    }
}
