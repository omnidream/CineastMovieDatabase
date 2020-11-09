﻿using System;
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
        public string SearchString { get; set; }
        public string MoviesOnly { get; set; } = "&type=movie";
        public string SeriesOnly { get; set; } = "&type=series";
    }
}
