using System.Runtime.Serialization;
using System.Security.Permissions;

namespace TiktokAPI.Helper.Collection
{
    [Serializable]
    public sealed class PageNotFoundException : Exception
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        public PageNotFoundException(int pageNumber, int totalPages) : base($"Not found the page {pageNumber} , the range of pages is 0 to {totalPages - 1}.")
        {
            PageNumber = pageNumber;
            TotalPages = totalPages;
        }
        private PageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            PageNumber = info.GetInt32(nameof(PageNumber));
            TotalPages = info.GetInt32(nameof(TotalPages));
        }
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(PageNumber), PageNumber);
            info.AddValue(nameof(TotalPages), TotalPages);
        }
    }
}
