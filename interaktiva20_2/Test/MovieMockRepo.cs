using interaktiva20_2.Data;
using interaktiva20_2.Infra;
using interaktiva20_2.Models.DTO;
using interaktiva20_2.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace interaktiva20_2.Test
{
    public class MovieMockRepo : IMovieRepo
    {
        private string cmbdMockUrl;
        private string omdbUrl;
        private int numberOfMovies = 1;
        private int numberOfNeverRatedMovies = 0;
        
        IApiClient apiClient;

        #region Constructor
        public MovieMockRepo(IWebHostEnvironment webHostEnv, IConfiguration configuration, IApiClient apiClient)
        {
            cmbdMockUrl = $"{ webHostEnv.ContentRootPath}\\Test\\Mockdata\\Cmdb\\";
            omdbUrl = configuration.GetValue<string>("OMDbApi:BaseUrl");
            this.apiClient = apiClient;
        } 
        #endregion

        private T GetTestData<T>(string testFile)
        {
            string path = $"{cmbdMockUrl}{testFile}";
            string data = File.ReadAllText(path);
            var result = JsonConvert.DeserializeObject<T>(data);
            return result;
        }

        public async Task<IEnumerable<CmdbMovieDto>> GetTopRatedList(int numberOfMovies)
        {
            string testFile = "CMDbMockTopRated.json";
            var result = GetTestData<IEnumerable<CmdbMovieDto>>(testFile);
            await Task.Delay(0);
            return result;
        }

        public async Task<IEnumerable<CmdbMovieDto>> GetMostPopularList(int numberOfMovies)
        {
            string testFile = "CMDbMockMostPopular.json";
            var result = GetTestData<IEnumerable<CmdbMovieDto>>(testFile);
            await Task.Delay(0);
            return result;
        }

        public List<CmdbMovieDto> GetNeverRatedMovies(int numberOfMovies)
        {
            string testFile = "CMDbMockMostDisliked.json";
            var result = GetTestData<IEnumerable<CmdbMovieDto>>(testFile);
            return (List<CmdbMovieDto>)result;
        }

        public async Task<CmdbMovieDto> GetCmdbRatings(string imdbId)
        {
            string testFile = "CmdbSingleMovie.json";
            var myRatings = GetTestData<CmdbMovieDto>(testFile);
            await Task.Delay(0);

            return myRatings;
        }

        public async Task<MovieDetailsDto> GetMovieDetails(string imdbId)
        {
            return await apiClient.GetAsync<MovieDetailsDto>(omdbUrl + $"i={imdbId}&plot=full");
        }

        public async Task<IEnumerable<IMovieSummaryDto>> GetToplistWithDetails(IEnumerable<CmdbMovieDto> myToplist)
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

        public Task<SearchResultDto> GetSearchResult(string apiKey, int pageNum)
        {
            throw new NotImplementedException();
        }

        Task<ISearchResultDto> IMovieRepo.GetSearchResult(string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<SearchResultViewModel> GetSearchResultViewModel(string apiKey, int pageNum)
        {
            throw new NotImplementedException();
        }
    }
}
