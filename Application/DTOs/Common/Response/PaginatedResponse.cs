namespace Fas7ny.Application.DTOs.Common.Response
{
    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; }
        public virtual int TotalCount { get; set; }
        public virtual int PageNumber { get; set; }
        public virtual int PageSize { get; set; }
        public virtual int TotalPages { get; }
        public virtual bool HasPreviousPage { get; }
        public virtual bool HasNextPage { get; }
    }
}
