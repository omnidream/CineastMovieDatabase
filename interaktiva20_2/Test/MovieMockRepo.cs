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

        public async Task<IEnumerable<CmdbMovieDto>> GetTopRatedFiveList()
        {
            string testFile = "CMDbMockTopRated.json";
            var result = GetTestData<IEnumerable<CmdbMovieDto>>(testFile);
            await Task.Delay(0);
            return result;
        }

        public async Task<IEnumerable<CmdbMovieDto>> GetMostDislikedFiveList()
        {
            string testFile = "CMDbMockMostDisliked.json";
            var result = GetTestData<IEnumerable<CmdbMovieDto>>(testFile);
            await Task.Delay(0);
            return result;
        }

        public async Task<IEnumerable<CmdbMovieDto>> GetMostPopularFiveList()
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

        public async Task<IEnumerable<MovieViewModel>> GetMovieViewModel()
        {
            var topFiveList = await GetTopRatedFiveList();

            List<MovieViewModel> movieViewModels = new List<MovieViewModel>();

            foreach (var movie in topFiveList)
            {
                MovieDetailsDto myMovieDetails = await GetMovieDetails(movie.ImdbId);
                MovieViewModel myMovieViewModel = new MovieViewModel(movie, myMovieDetails);
                movieViewModels.Add(myMovieViewModel);
            }

            return movieViewModels;
        }
    }
}
