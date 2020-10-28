using interaktiva20_2.Infra;
using interaktiva20_2.Models.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Data
{
    public class OMDbRepository : IOMDbRepository
    {
        private string baseUrl;
        IApiClient apiClient;

        public OMDbRepository(IConfiguration configuration, IApiClient apiClient)
        {
            baseUrl = configuration.GetValue<string>("OMDbApi:BaseUrl");
            this.apiClient = apiClient;
        }

        public async Task<MovieDetailsDto> GetMovieDetails(string imdbId)
        {
            return await apiClient.GetAsync<MovieDetailsDto>(baseUrl + $"i={imdbId}&plot=full");
        }
    }
}
