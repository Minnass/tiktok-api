using TiktokAPI.Models.Account;
using TiktokAPI.Models.Collection;

namespace TiktokAPI.Services.Interfaces
{
    public interface IUserService
    {
        IList<UserInfomation> GetSuggestedUsers(SuggestedCollection model);
        IList<UserInfomation> GetUsers(string search);
    }
}
