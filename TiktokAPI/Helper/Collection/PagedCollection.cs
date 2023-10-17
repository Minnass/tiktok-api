using TiktokAPI.Helper.Collection.Interfaces;

namespace TiktokAPI.Helper.Collection
{
    public class PagedCollection<TCollection, T> : IPagedCollection<T> where TCollection : IReadOnlyCollection<T>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => PageNumber > 0;
        public bool HasNextPage => PageNumber < TotalPages - 1;

        public IReadOnlyCollection<T> Items { get; }

        /// <summary>
        /// Construct a paged collection
        /// </summary>
        /// <param name="pageNumber">The current page number</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="totalCount">Total amount of items in the full collection</param>
        /// <param name="totalPages">Total pages of the collection</param>
        /// <param name="items">The items of the current page.</param>
        public PagedCollection(int pageNumber, int pageSize, int totalCount, int totalPages, TCollection items)
        {
            pageNumber = PageNumber;
            pageSize = PageSize;
            totalCount = TotalCount;
            totalPages = TotalPages;
            Items = items;
        }



    }
}
