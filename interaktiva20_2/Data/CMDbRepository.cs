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
        public async Task<IEnumerable<TopListDto>> GetToplist()
        {
            return await apiClient.GetAsync<IEnumerable<TopListDto>>(baseUrl + "toplist/?count=5");
        }
    }
}
 