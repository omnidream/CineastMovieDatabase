using interaktiva20_2.Infra;
using interaktiva20_2.Models.DTO;
using interaktiva20_2.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace interaktiva20_2.Data
{
    public class MovieRepo : IMovieRepo
    {
        private string cmdbUrl;
        private string omdbUrl;
        private int numberOfMovies = 4;
        private int numberOfNeverRatedMovies = 1;
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
            var apiKey = $"s=the a&plot=full&type=movie&page={GeneratePageNo()}";

            var randomMovies = GetSearchResult(apiKey).Result;
            myNeverRatedList = GetMoviesWithDetailsFromList(randomMovies, numberOfMovies);
            return myNeverRatedList;
        }

        public async Task<CmdbMovieDto> GetCmdbRatings(string imdbId)
        {
            var myRatings = await CallCmdbApi<CmdbMovieDto>($"movie/{imdbId}");

            if (myRatings == null)
                myRatings = GiveRatingWhenNull(myRatings);
            return myRatings;
        }

        private CmdbMovieDto GiveRatingWhenNull(CmdbMovieDto myRatings)
        {
            myRatings = new CmdbMovieDto();
            myRatings.NumberOfLikes = 0;
            myRatings.NumberOfDislikes = 0;

            return myRatings;
        }

        private List<CmdbMovieDto> GetMoviesWithDetailsFromList(ISearchResultDto randomMovies, int numberOfMovies)
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

        private int GeneratePageNo()
        {
            return rnd.Next(1, 50);
        }
        private bool MovieHasPoster(MovieDetailsDto movie)
        {
            bool result = true;
            if (movie.Poster == "N/A" || movie.Poster == null)
                result = false;
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
        public async Task<MovieDetailsDto> GetMovieDetails(string imdbId)
        {
            return await apiClient.GetAsync<MovieDetailsDto>(omdbUrl + $"i={imdbId}&plot=full");
        }

        public async Task<ISearchResultDto> GetSearchResult(string apiKey)
        {
            SearchResultDto mySearchObject = new SearchResultDto();
            mySearchObject = await apiClient.GetAsync<SearchResultDto>(omdbUrl + apiKey);

            return mySearchObject;
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
                myMovieDetails = AddPosterIfNoPoster(myMovieDetails);
                MovieSummaryDto myMovieSummary = new MovieSummaryDto(movie, myMovieDetails, movieNumber);
                movieSummaries.Add(myMovieSummary);
            }
            return movieSummaries;
        }

        public MovieDetailsDto AddPosterIfNoPoster(MovieDetailsDto myMovieDetails)
        {
            if (MovieHasPoster(myMovieDetails) == false)
                myMovieDetails.Poster = "/assets/images/no-poster.png";
            return myMovieDetails;
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
            taskList.Add(neverRatedMovies);
            await Task.WhenAll(taskList);

            return new MovieViewModel(topRatedMovies, mostPopularMovies, neverRatedMovies);
        }

        public async Task<MovieDetailViewModel> GetMovieDetailViewModel(string imdbId)
        {
            var taskList = new List<Task>();

            var movie = GetMovieDetails(imdbId);
            var ratings = GetCmdbRatings(imdbId);

            taskList.Add(movie);
            taskList.Add(ratings);
            await Task.WhenAll(taskList);

            return new MovieDetailViewModel(movie.Result, ratings.Result);
        }
        
        public async Task<SearchResultViewModel> GetSearchResultViewModel(string searchString, int pageNum)
        {
            string apiKey = GetApiKey(searchString, pageNum);
            SearchResultViewModel searchResultVM = new SearchResultViewModel();
            searchResultVM.SearchResult = await GetSearchResult(apiKey);
            searchResultVM.TotalPages = RoundNumberOfPages(searchResultVM.SearchResult.totalResults);
            searchResultVM.CurrentPage = pageNum;
            searchResultVM.SearchString = searchString;

            return searchResultVM;
        }

        private string GetApiKey(string searchString, int pageNum)
        {
            var cleanedSearchString = CleanFromSpecialChars(searchString);
            var singleSpaceString = CleanFromMultipleSpaces(cleanedSearchString);
            string apiKey = $"&s={singleSpaceString}&plot=full&page={pageNum}";

            return apiKey;
        }
        private string CleanFromSpecialChars(string searchString)
        {
            var cleanedSearchString = Regex.Replace(searchString, @"[^0-9a-öA-Ö'&= ]+", "");
            return cleanedSearchString;
        }

        private string CleanFromMultipleSpaces(string cleanedSearchString)
        {
            string singleSpacesString = Regex.Replace(cleanedSearchString, " {2,}", " ");
            return singleSpacesString;
        }

        private int RoundNumberOfPages(int totalResults)
        {
            double resultsPerPage = 10;
            double numberOfPages = ((double)totalResults / resultsPerPage);
            int roundedNum = (int)Math.Ceiling(numberOfPages);

            return roundedNum;
        }

        #endregion


    }
}
