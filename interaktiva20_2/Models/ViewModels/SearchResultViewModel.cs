using interaktiva20_2.Models.DTO;
using System.Collections.Generic;

namespace interaktiva20_2.Models.ViewModels
{
    public class SearchResultViewModel : ISearchResultDto
    {
        public string Response { get; set; }
        public string Error { get; set; }
        public string totalResults { get; set; }
        public List<MovieDetailsDto> Search { get; set; }
    }
}
