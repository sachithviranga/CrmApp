namespace CrmApp.Shared.DTO
{
    /// <summary>
    /// Generic container for paginated API responses.
    /// Holds items and pagination metadata to support client paging UIs.
    /// </summary>
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = [];
        public int TotalCount { get; set; }   
        public int Page { get; set; }         
        public int PageSize { get; set; }    
    }
}
