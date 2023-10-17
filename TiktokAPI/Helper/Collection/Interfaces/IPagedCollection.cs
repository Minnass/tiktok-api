namespace TiktokAPI.Helper.Collection.Interfaces
{
    public interface IPagedCollection<out T>
    {
        int PageNumber { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        IReadOnlyCollection<T> Items { get; }
        public T this[int index] => Items.ElementAt(index);
    }
}
