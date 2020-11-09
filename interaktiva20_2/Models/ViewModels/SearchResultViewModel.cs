using interaktiva20_2.Models.DTO;

namespace interaktiva20_2.Models.ViewModels
{
    public class SearchResultViewModel
    {
        public int TotalPages { get; set; }
        public ISearchResultDto SearchResult { get; set; }
        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
    }
}
