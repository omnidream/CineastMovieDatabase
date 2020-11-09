using interaktiva20_2.Models.DTO;
using interaktiva20_2.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace interaktiva20_2.Data
{
    public interface IMovieRepo
    {
        Task<IEnumerable<CmdbMovieDto>> GetTopRatedList(int numberOfMovies);
        Task<IEnumerable<CmdbMovieDto>> GetMostPopularList(int numberOfMovies);
        List<CmdbMovieDto> GetNeverRatedMovies(int numberOfMovies);
        Task<MovieDetailsDto> GetMovieDetails(string imdbId);
        Task<IEnumerable<IMovieSummaryDto>> GetToplistWithDetails(IEnumerable<CmdbMovieDto> myToplist);
        Task<MovieViewModel> GetMovieListsViewModel();
        Task<MovieDetailViewModel> GetMovieDetailViewModel(string imdbId);
        Task<ISearchResultDto> GetSearchResult(string apiKey);
        Task<SearchResultViewModel> GetSearchResultViewModel(string apiKey, int pageNum);
    }
}
