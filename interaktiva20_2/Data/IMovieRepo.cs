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

        Task<IEnumerable<CmdbMovieDto>> GetTopRatedFiveList();
        Task<IEnumerable<CmdbMovieDto>> GetMostDislikedFiveList();
        Task<IEnumerable<CmdbMovieDto>> GetMostPopularFiveList();
        Task<MovieDetailsDto> GetMovieDetails(string imdbId);
        Task<IEnumerable<MovieViewModel>> GetMovieViewModel();

    }
}
