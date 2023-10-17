namespace TiktokAPI.Helper.Collection
{
    public class PagedList<T> : PagedCollection<IReadOnlyList<T>, T>
    {
        public PagedList(int pageNumber, int pageSize, int totalCount, int totalPages, IReadOnlyList<T> items)
            : base(pageNumber, pageSize, totalCount, totalPages, items)
        { }

        /// <summary>
        /// List indexer
        /// </summary>
        /// <param name="index">Item index</param>
        /// <returns></returns>
        public T this[int index] => ((IReadOnlyList<T>)Items)[index];
    }
}
