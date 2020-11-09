using System.Collections.Generic;

namespace interaktiva20_2.Models.DTO
{
    public class SearchResultDto : ISearchResultDto
    {
        public string Response { get; set; }
        public string Error { get; set; }
        public int totalResults { get; set; }
        public List<MovieDetailsDto> Search { get; set; }

    }
}
