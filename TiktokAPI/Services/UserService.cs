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
        private readonly IWebHostEnvironment _hostEnvironment;
        public UserService(IUnitOfWork<TikTerDBContext> uow, IAuthService authService, IWebHostEnvironment webHostEnvironment)
        {
            this.uow = uow;
            this.authService = authService;
            this._hostEnvironment = webHostEnvironment;
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

        public async Task UploadUser(UserUploadModel model)
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
                var uploadPath = Path.Combine(_hostEnvironment.ContentRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Avatar.FileName;
                var filePath = Path.Combine(uploadPath, uniqueFileName);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Avatar.CopyToAsync(stream);
                }
                user.Avatar = Path.Combine("images", uniqueFileName);
            }
            user.Bio = model.Bio;
            user.DisplayedName=model.DisplayedName;
            this.uow.SaveChanges();
        }
    }
}
