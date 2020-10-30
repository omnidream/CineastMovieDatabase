using interaktiva20_2.Models.DTO;
using interaktiva20_2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Data
{
    public interface IMovieRepo
    {

        Task<IEnumerable<CmdbMovieDto>> GetTopRatedList(int numberOfMovies);
        Task<IEnumerable<CmdbMovieDto>> GetMostPopularList(int numberOfMovies);
        Task<IEnumerable<CmdbMovieDto>> GetNeverRatedMovies(int numberOfMovies, string imdbId);
        Task<MovieDetailsDto> GetMovieDetails(string imdbId);
        Task<IEnumerable<MovieSummaryDto>> GetToplist(IEnumerable<CmdbMovieDto> myToplist);
    }
}
