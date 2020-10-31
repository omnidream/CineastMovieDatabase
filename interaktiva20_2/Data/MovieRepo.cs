using interaktiva20_2.Infra;
using interaktiva20_2.Models.DTO;
using interaktiva20_2.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace interaktiva20_2.Data
{
    public class MovieRepo : IMovieRepo
    {
        private string cmdbUrl;
        private string omdbUrl;
        private int numberOfMovies = 1;
        private int numberOfNeverRatedMovies = 1;
        Random rnd = new Random();
        List<CmdbMovieDto> myNeverRatedList;
        CmdbMovieDto myMovie = null;
        IApiClient apiClient;

        #region Constructor
        public MovieRepo(IConfiguration configuration, IApiClient apiClient)
        {
            cmdbUrl = configuration.GetValue<string>("CMDbApi:BaseUrl");
            omdbUrl = configuration.GetValue<string>("OMDbApi:BaseUrl");
            this.apiClient = apiClient;
        } 
        #endregion

        #region CMDbRepo
        public async Task<IEnumerable<CmdbMovieDto>> GetTopRatedList(int numberOfMovies)
        {
            return await CallCmdbApi($"toplist/?count={numberOfMovies}");
        }
        public async Task<IEnumerable<CmdbMovieDto>> GetMostPopularList(int numberOfMovies)
        {
            return await CallCmdbApi($"toplist/?type=popularity&count={numberOfMovies}");
        }
        //TODO: Ändra så att vi så att vi använder GET Test-anropet istället för att få fram mer relevanta filmer.
        public List<CmdbMovieDto> GetNeverRatedMovies(int numberOfMovies)
        {
            myNeverRatedList = new List<CmdbMovieDto>();
            string imdbId = GetRandomImdbId();
            while (MovieIsNotInCMDb(imdbId) && ListIsNotFull(numberOfMovies))
            {
                AddToNeverRatedList(imdbId);
                imdbId = GetRandomImdbId();
            }
            return myNeverRatedList;
        }
        //TODO: Gör så att cmdb-kollen kommer FÖRE kollen mot omdb
        private string GetRandomImdbId()
        {
            string returnImdbId;
            do
                returnImdbId = "tt" + GenerateNumberAsString();
            while (MovieHasNoPoster(returnImdbId));

            return returnImdbId;
        }

        private bool MovieHasNoPoster(string imdbId)
        {
            bool result = false;
            MovieDetailsDto myTempMovie = GetMovieDetails(imdbId).Result;
            if (myTempMovie.Poster == "N/A" || myTempMovie.Poster == null)
                result = true;
            return result;

        }

        private string GenerateNumberAsString()
        {
            int maxIdLength = 7;
            string tempId = rnd.Next(8000000, 8004663).ToString();
            while (tempId.Length < maxIdLength)
                tempId = tempId.Insert(0, "0");
            return tempId;
        }

        private bool ListIsNotFull(int numberOfMovies)
        {
            bool result = false;
            if (myNeverRatedList == null || myNeverRatedList.Count < numberOfMovies)
                result = true;
            return result;
        }

        private void AddToNeverRatedList(string imdbId)
        {
            myMovie = new CmdbMovieDto();
            myMovie.ImdbId = imdbId;
            myNeverRatedList.Add(myMovie);
        }

        private bool MovieIsNotInCMDb(string imdbId)
        {
            bool result = true;
            if (CallCmdbApi($"{imdbId}") == null)
                result = false;
            return result;
        }
        #endregion

        #region OMDbRepo
        //TODO: Skapa kontroll så att data faktiskt existerar.
        public async Task<MovieDetailsDto> GetMovieDetails(string imdbId)
        {
            return await apiClient.GetAsync<MovieDetailsDto>(omdbUrl + $"i={imdbId}&plot=full&year=2010");
        }
        #endregion
        private Task<IEnumerable<CmdbMovieDto>> CallCmdbApi(string apiKey)
        {
            return apiClient.GetAsync<IEnumerable<CmdbMovieDto>>(cmdbUrl + apiKey);
        }

        public async Task<IEnumerable<MovieSummaryDto>> GetToplist(IEnumerable<CmdbMovieDto> myToplist)
        {
            int movieNumber = 0;
            List<MovieSummaryDto> movieSummaries = new List<MovieSummaryDto>();

            foreach (var movie in myToplist)
            {
                movieNumber++;
                MovieDetailsDto myMovieDetails = await GetMovieDetails(movie.ImdbId);
                MovieSummaryDto myMovieSummary = new MovieSummaryDto(movie, myMovieDetails, movieNumber);
                movieSummaries.Add(myMovieSummary);
            }
            return movieSummaries;
        }

        public async Task<MovieViewModel> GetMovieListsViewModel()
        {
            var taskList = new List<Task>();

            var topRatedMovies = GetToplist(GetTopRatedList(numberOfMovies).Result);
            var mostPopularMovies = GetToplist(GetMostPopularList(numberOfMovies).Result);
            var neverRatedMovies = GetToplist(GetNeverRatedMovies(numberOfNeverRatedMovies));

            taskList.Add(topRatedMovies);
            taskList.Add(mostPopularMovies);
            taskList.Add(neverRatedMovies);
            await Task.WhenAll(taskList);

            return new MovieViewModel(topRatedMovies, mostPopularMovies, neverRatedMovies);
        }
    }
}
