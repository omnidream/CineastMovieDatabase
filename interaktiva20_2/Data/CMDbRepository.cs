using interaktiva20_2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using interaktiva20_2.Infra;
using Microsoft.Extensions.Configuration;

namespace interaktiva20_2.Data
{
    public class CMDbRepository : ICMDbRepository
    {
        private string baseUrl;
        IApiClient apiClient;

        public CMDbRepository(IConfiguration configuration, IApiClient apiClient)
        {
            baseUrl = configuration.GetValue<string>("CMDbApi:BaseUrl");
            this.apiClient = apiClient;
        }
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

        private Task<IEnumerable<CmdbMovieDto>> CallCmdbApi(string apiKey)
        {
            return apiClient.GetAsync<IEnumerable<CmdbMovieDto>>(baseUrl + apiKey);
        }


    }
}
 