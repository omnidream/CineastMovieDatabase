using interaktiva20_2.Infra;
using interaktiva20_2.Models.DTO;
using interaktiva20_2.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var randomMovies = GetAListOfRandomMovies();
            //myNeverRatedList = GetMoviesWithDetailsFromList(randomMovies, numberOfMovies);
            return myNeverRatedList;
        }

        private List<CmdbMovieDto> GetMoviesWithDetailsFromList(IEnumerable<MovieDetailsDto> randomMovies, int numberOfMovies)
        {
            int iterations = numberOfMovies;
            List<CmdbMovieDto> returnList = new List<CmdbMovieDto>();
            foreach (var movie in randomMovies)
            {
                if (MovieisValid(iterations, movie, myMovie))
                {
                    CmdbMovieDto myMovie = new CmdbMovieDto();
                    myMovie.ImdbId = movie.imdbID;
                    returnList.Add(myMovie);
                    iterations--;
                }
            }
            return returnList;
        }

        private bool MovieisValid(int iterations, MovieDetailsDto movie, CmdbMovieDto myMovie)
        {
            bool result = false;
            if (MovieHasPoster(movie) && MovieHasPlot(movie) && iterations > 0 && MovieIsInCMDb(movie.imdbID) == false)
                result = true;
            return result;
        }

        private async Task<MovieDetailsDto> GetAListOfRandomMovies()
        {
            return await apiClient.GetAsync<MovieDetailsDto>(omdbUrl + $"{GetSearchWord()}&plot=full&type=movie");
        }

        private string GetSearchWord()
        {
            int numberOfChars = 2;
            string mySearchWord = "s=the ";

            for (int i = 0; i < numberOfChars; i++)
            {
                string myChars = "abcdefghijklmnop";
                mySearchWord = mySearchWord + myChars[(rnd.Next(0, 15))].ToString();
            }
            return mySearchWord;
        }

        private bool MovieHasPoster(MovieDetailsDto movie)
        {
            bool result = false;
            if (movie.Poster != "N/A" || movie.Poster != null)
                result = true;
            return result;
        }

        private bool MovieHasPlot(MovieDetailsDto movie)
        {
            bool result = false;
            if (movie.Plot != "N/A" || movie.Poster != null)
                result = true;
            return result;
        }

        private bool MovieIsInCMDb(string imdbId)
        {
            bool result = false;
            if (CallCmdbApi($"{imdbId}") != null)
                result = true;
            return result;
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
