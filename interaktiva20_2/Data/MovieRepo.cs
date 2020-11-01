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
        private int numberOfNeverRatedMovies = 3;
        Random rnd = new Random();
        List<CmdbMovieDto> myNeverRatedList;
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
            return await CallCmdbApi<IEnumerable<CmdbMovieDto>>($"toplist/?count={numberOfMovies}");
        }

        public async Task<IEnumerable<CmdbMovieDto>> GetMostPopularList(int numberOfMovies)
        {
            return await CallCmdbApi<IEnumerable<CmdbMovieDto>>($"toplist/?type=popularity&count={numberOfMovies}");
        }
        public List<CmdbMovieDto> GetNeverRatedMovies(int numberOfMovies)
        {
            var randomMovies = GetAListOfRandomMovies().Result;
            myNeverRatedList = GetMoviesWithDetailsFromList(randomMovies, numberOfMovies);
            return myNeverRatedList;
        }

        private List<CmdbMovieDto> GetMoviesWithDetailsFromList(SearchResultDto randomMovies, int numberOfMovies)
        {
            int iterations = numberOfMovies;
            List<CmdbMovieDto> returnList = new List<CmdbMovieDto>();
            foreach (var movie in randomMovies.Search)
            {
                if (MovieIsValid(iterations, movie))
                {
                    CmdbMovieDto myMovie = new CmdbMovieDto();
                    myMovie.ImdbId = movie.imdbID;
                    returnList.Add(myMovie);
                    iterations--;
                }
            }
            return returnList;
        }

        private bool MovieIsValid(int iterations, MovieDetailsDto movie)
        {
            bool result = false;
            if (MovieHasPoster(movie) && iterations > 0 && MovieIsInCMDb(movie.imdbID) == false)
                result = true;
            return result;
        }
        private async Task<SearchResultDto> GetAListOfRandomMovies()
        {
            SearchResultDto mySearchObject = new SearchResultDto();
            do
            {
                mySearchObject = await apiClient.GetAsync<SearchResultDto>(omdbUrl + $"s=the a&plot=full&type=movie&page={GeneratePageNo()}");
            } while (mySearchObject.Search == null);

            return mySearchObject;
        }

        private int GeneratePageNo()
        {
            return rnd.Next(1, 30);
        }

        private bool MovieHasPoster(MovieDetailsDto movie)
        {
            bool result = true;
            if (movie.Poster == "N/A" || movie.Poster == null)
                result = false;
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
            bool result = true;
            if (CallCmdbApi<CmdbMovieDto>($"{imdbId}").Result == null)
                result = false;
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
        private Task<T> CallCmdbApi<T>(string apiKey)
        {
            return apiClient.GetAsync<T>(cmdbUrl + apiKey);
        }

        public async Task<IEnumerable<IMovieSummaryDto>> GetToplistWithDetails(IEnumerable<CmdbMovieDto> myToplist)
        {
            int movieNumber = 0;
            List<IMovieSummaryDto> movieSummaries = new List<IMovieSummaryDto>();

            foreach (var movie in myToplist)
            {
                movieNumber++;
                MovieDetailsDto myMovieDetails = await GetMovieDetails(movie.ImdbId);
                MovieSummaryDto myMovieSummary = new MovieSummaryDto(movie, myMovieDetails, movieNumber);
                movieSummaries.Add(myMovieSummary);
            }
            return movieSummaries;
        }

        #region GetViewModels
        public async Task<MovieViewModel> GetMovieListsViewModel()
        {
            var taskList = new List<Task>();

            var topRatedMovies = GetToplistWithDetails(GetTopRatedList(numberOfMovies).Result);
            var mostPopularMovies = GetToplistWithDetails(GetMostPopularList(numberOfMovies).Result);
            var neverRatedMovies = GetToplistWithDetails(GetNeverRatedMovies(numberOfNeverRatedMovies));

            taskList.Add(topRatedMovies);
            taskList.Add(mostPopularMovies);
            //taskList.Add(neverRatedMovies);
            await Task.WhenAll(taskList);

            return new MovieViewModel(topRatedMovies, mostPopularMovies/*, neverRatedMovies*/);
        }

        public async Task<MovieDetailViewModel> GetMovieDetailViewModel(string imdbId)
        {
            var taskList = new List<Task>();

            var movie = GetMovieDetails(imdbId);
            //var ratings = GetCmdbRatings(imdbId);

            taskList.Add(movie);
            //taskList.Add(ratings);
            await Task.WhenAll(taskList);

            return new MovieDetailViewModel(movie.Result);
        } 
        #endregion
    }
}
