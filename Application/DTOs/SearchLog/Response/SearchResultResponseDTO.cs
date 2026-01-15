namespace Fas7ny.Application.DTOs.SearchLog.Response
{
    public class SearchResultResponseDTO
    {
        public List<object> Results { get; set; }
        public int TotalCount { get; set; }
        public string SearchCategory { get; set; }
        public string SearchTerm { get; set; }
    }

}
