using TiktokAPI.Helper.Collection.Interfaces;

namespace TiktokAPI.Helper.Collection
{
    public static class Extentions
    {
        private static void ValidateParameters(int pageNumber, int pageSize)
        {
            if (pageNumber < 0)
            {
                throw new ArgumentException("The page number must be greater than or equal to zero");
            }
            if (pageSize < 0)
            {
                throw new ArgumentException("The page size must be greater than zero");
            }
        }
        private static (int count, int totalPages, IQueryable<T> pagedItems) GetPage<T>(IQueryable<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count();
            int totalPages = (int)Math.Ceiling((double)count / pageSize);
            if (totalPages == 0) totalPages = 1;
            if (pageNumber >= totalPages) { throw new PageNotFoundException(pageNumber, totalPages); }
            var pagedItems = source.Skip(pageNumber * pageSize).Take(pageSize);
            return (count, totalPages, pagedItems);
        }
        public static IPagedCollection<T> ToPagedList<T>(this IQueryable<T> source, int pageSize, int pageNumber = 0)
        {
            ValidateParameters(pageNumber, pageSize);
            (int count, int totalPages, IQueryable<T> pagesItems) = GetPage(source, pageSize, pageNumber);
            var pageItems = pagesItems.ToList();
            return new PagedList<T>(pageNumber, pageSize, count, totalPages, pageItems);
        }
    }
}
