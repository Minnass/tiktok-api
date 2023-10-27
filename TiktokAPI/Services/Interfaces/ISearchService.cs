using TiktokAPI.Models;

namespace TiktokAPI.Services.Interfaces
{
    public interface ISearchService
    {
        IList<SearchModel> GetKeywordByUser(long userId);
        void Delete(long searchId);
        long Add(SearchModel model);
    }
}
