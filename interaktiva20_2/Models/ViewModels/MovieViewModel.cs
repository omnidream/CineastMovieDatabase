using interaktiva20_2.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace interaktiva20_2.Models.ViewModels
{
    public class MovieViewModel
    {
        public IEnumerable<IMovieSummaryDto> TopRatedMovies { get; set; }
        public IEnumerable<IMovieSummaryDto> MostPopularMovies { get; set; }
        public IEnumerable<IMovieSummaryDto> NeverRatedMovies { get; set; }

        
        public MovieViewModel(Task<IEnumerable<IMovieSummaryDto>> topRatedMovies, Task<IEnumerable<IMovieSummaryDto>> mostPopularMovies, Task<IEnumerable<IMovieSummaryDto>> neverRatedMovies)
        {
            TopRatedMovies = topRatedMovies.Result;
            MostPopularMovies = mostPopularMovies.Result;
            NeverRatedMovies = neverRatedMovies.Result;
        }
    }
}
