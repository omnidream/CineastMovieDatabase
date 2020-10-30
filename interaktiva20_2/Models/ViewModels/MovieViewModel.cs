using interaktiva20_2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Models.ViewModels
{
    public class MovieViewModel
    {
        public IEnumerable<MovieSummaryDto> TopRatedMovies { get; set; }
        public IEnumerable<MovieSummaryDto> MostPopularMovies { get; set; }
        public IEnumerable<MovieSummaryDto> MostDislikedMovies { get; set; }

        
        public MovieViewModel()
        {
            TopRatedMovies = new List<MovieSummaryDto>();
            MostPopularMovies = new List<MovieSummaryDto>();
            MostDislikedMovies = new List<MovieSummaryDto>();
        }
    }
}
