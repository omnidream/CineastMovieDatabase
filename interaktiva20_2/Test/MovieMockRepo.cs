using interaktiva20_2.Data;
using interaktiva20_2.Infra;
using interaktiva20_2.Models.DTO;
using interaktiva20_2.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace interaktiva20_2.Test
{
    public class MovieMockRepo : IMovieRepo
    {
        string myBasePath;
        private string omdbUrl;
        private int numberOfMovies = 1;
        private int numberOfNeverRatedMovies = 1;
        IApiClient apiClient;

        public MovieMockRepo(IWebHostEnvironment webHostEnv, IConfiguration configuration, IApiClient apiClient)
        {
            myBasePath = $"{ webHostEnv.ContentRootPath}\\Test\\Mockdata\\Cmdb\\";
            omdbUrl = configuration.GetValue<string>("OMDbApi:BaseUrl");
            this.apiClient = apiClient;
        }

        private T GetTestData<T>(string testFile)
        {
            string path = $"{myBasePath}{testFile}";
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

        public List<CmdbMovieDto> GetNeverRatedMovies(int numberOfMovies)
        {
            string testFile = "CMDbMockMostDisliked.json";
            var result = GetTestData<IEnumerable<CmdbMovieDto>>(testFile);
            return (List<CmdbMovieDto>)result;
        }

        public async Task<IEnumerable<CmdbMovieDto>> GetMostPopularList(int numberOfMovies)
        {
            string testFile = "CMDbMockMostPopular.json";
            var result = GetTestData<IEnumerable<CmdbMovieDto>>(testFile);
            await Task.Delay(0);
            return result;
        }

        public async Task<MovieDetailsDto> GetMovieDetails(string imdbId)
        {
            return await apiClient.GetAsync<MovieDetailsDto>(omdbUrl + $"i={imdbId}&plot=full");
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
    }
}
