using interaktiva20_2.Infra;
using interaktiva20_2.Models.DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace interaktiva20_2.Data
{
    public class MovieRepo : IMovieRepo
    {
        private string cmdbUrl;
        private string omdbUrl;
        IApiClient apiClient;

        public MovieRepo(IConfiguration configuration, IApiClient apiClient)
        {
            cmdbUrl = configuration.GetValue<string>("CMDbApi:BaseUrl");
            omdbUrl = configuration.GetValue<string>("OMDbApi:BaseUrl");
            this.apiClient = apiClient;
        }

        #region CMDbRepo
        public async Task<IEnumerable<CmdbMovieDto>> GetTopRatedFiveList()
        {
            return await CallCmdbApi("toplist/?count=5");
        }

        //TODO: Fixa en lista för "Not yet rated" istället
        public async Task<IEnumerable<CmdbMovieDto>> GetMostDislikedFiveList()
        {
            return await CallCmdbApi("toplist/?sort=asc&count=5");
        }

        public async Task<IEnumerable<CmdbMovieDto>> GetMostPopularFiveList()
        {
            return await CallCmdbApi("toplist/?type=popularity&count=5");
        }
        #endregion

        #region OMDbRepo
        //TODO: Skapa kontroll så att data faktiskt existerar.
        public async Task<MovieDetailsDto> GetMovieDetails(string imdbId)
        {
            return await apiClient.GetAsync<MovieDetailsDto>(omdbUrl + $"i={imdbId}&plot=full");
        }
        #endregion
        private Task<IEnumerable<CmdbMovieDto>> CallCmdbApi(string apiKey)
        {
            return apiClient.GetAsync<IEnumerable<CmdbMovieDto>>(cmdbUrl + apiKey);
        }

        public async Task<IEnumerable<MovieSummaryDto>> GetToplist(IEnumerable<CmdbMovieDto> myToplist)
        {
            List<MovieSummaryDto> movieSummaries = new List<MovieSummaryDto>();
            int movieNumber = 0;

            foreach (var movie in myToplist)
            {
                movieNumber++;
                MovieDetailsDto myMovieDetails = await GetMovieDetails(movie.ImdbId);
                MovieSummaryDto myMovieSummary = new MovieSummaryDto(movie, myMovieDetails, movieNumber);
                movieSummaries.Add(myMovieSummary);
            }
            return movieSummaries;
        }
    }
}
