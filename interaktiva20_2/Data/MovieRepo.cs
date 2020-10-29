using interaktiva20_2.Infra;
using interaktiva20_2.Models.DTO;
using interaktiva20_2.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public async Task<MovieDetailsDto> GetMovieDetails(string imdbId)
        {
            return await apiClient.GetAsync<MovieDetailsDto>(omdbUrl + $"i={imdbId}&plot=full");
        }
        #endregion
        private Task<IEnumerable<CmdbMovieDto>> CallCmdbApi(string apiKey)
        {
            return apiClient.GetAsync<IEnumerable<CmdbMovieDto>>(cmdbUrl + apiKey);
        }

        public async Task<MovieViewModel> GetMovieViewModel()
        {
            var myTaskList = new List<Task>();
            var topList = GetTopRatedFiveList();
            //var movies = apiClient.GetAsync<IEnumerable<CountryDto>>(baseUrl + "countries");
            //var summary = apiClient.GetAsync<SummaryDTO>(baseUrl + "summary");

            myTaskList.Add(topList);
            await Task.WhenAll(myTaskList); // kör alla trådar, parallellt
            return null;
            /*SummaryDetailDto summaryDetail = summary.Result.Countries
                .Where(c => c.Country.Equals(country))
                .FirstOrDefault();
            return new SummaryViewModel(countries.Result, summaryDetail);*/
        }


        private async Task<MovieDetailsDto> GetDetailsForEachMovie(IEnumerable<CmdbMovieDto> topList)
        {
            List<MovieDetailsDto> myReturnList = null;
            foreach (var movie in topList)
            {
                var myMovieDetails = await GetMovieDetails(movie.ImdbId);
                myReturnList.Add(myMovieDetails);
            }
            return null;
        }



    }
}
