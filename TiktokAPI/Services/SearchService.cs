using Microsoft.EntityFrameworkCore;
using TiktokAPI.Core.Interfaces;
using TiktokAPI.Entities;
using TiktokAPI.Models;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Services
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork<TikTerDBContext> uow;
        public SearchService(IUnitOfWork<TikTerDBContext> uow)
        {
            this.uow = uow;
        }

        public int Add(SearchModel model)
        {
            var existedItem = this.uow.GetRepository<Search>().Queryable()
                .AsNoTracking()
                .Where(x => x.UserId == model.UserId && x.KeyWord == model.KeyWord)
                .FirstOrDefault();
            if (existedItem != null)
            {
                throw new Exception("Item already existed");
            }
            var newSearch = new Search
            {
                KeyWord = model.KeyWord,
                UserId = model.UserId,
            };
            this.uow.GetRepository<Search>().Insert(newSearch);


            this.uow.SaveChanges();
            return newSearch.SearchId;
        }

        public void Delete(long searchId)
        {
            var existedItem = this.uow.GetRepository<Search>().Queryable()
               .AsNoTracking()
               .Where(x => searchId==x.SearchId)
               .FirstOrDefault();
            if (existedItem == null)
            {
                throw new Exception("Item not existed");
            }
            this.uow.GetRepository<Search>().Delete(existedItem);
            this.uow.SaveChanges();
        }

        public IList<SearchModel> GetKeywordByUser(long userId)
        {
            var result = this.uow.GetRepository<Search>()
                .Queryable()
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x=>new SearchModel
                {
                    KeyWord=x.KeyWord,
                    SearchId=x.SearchId,
                    UserId=x.UserId
                })
                .ToList();
            return result;
        }
    }
}
