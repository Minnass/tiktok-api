﻿using Microsoft.EntityFrameworkCore;
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
        public UserService(IUnitOfWork<TikTerDBContext> uow)
        {
            this.uow = uow;
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
    }
}
