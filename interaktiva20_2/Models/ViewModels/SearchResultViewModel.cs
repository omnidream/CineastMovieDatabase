using System;
using interaktiva20_2.Models.DTO;
using System.Collections.Generic;

namespace interaktiva20_2.Models.ViewModels
{
    public class SearchResultViewModel : ISearchResultDto
    {
        public string Response { get; set; }
        public string Error { get; set; }
        public int totalResults { get; set; }
        public List<MovieDetailsDto> Search { get; set; }
        public int TotalPages { get; set; }

        public SearchResultViewModel()
        {
            TotalPages = RoundNumberOfPages();
        }

        private int RoundNumberOfPages()
        {
            int resultsPerPage = 10;
            double numberOfPages = totalResults / resultsPerPage;
            return (int)Math.Ceiling(numberOfPages);
        }
    }
}
