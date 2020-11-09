using System;
using interaktiva20_2.Models.DTO;
using System.Collections.Generic;
using interaktiva20_2.Data;

namespace interaktiva20_2.Models.ViewModels
{
    public class SearchResultViewModel
    {
        public int TotalPages { get; set; }
        public ISearchResultDto SearchResult { get; set; }
        public int CurrentPage { get; set; }
    }
}
