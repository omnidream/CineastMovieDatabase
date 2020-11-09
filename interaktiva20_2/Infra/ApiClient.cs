using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace interaktiva20_2.Infra
{
    public class ApiClient : IApiClient
    {
        public async Task<T> GetAsync<T>(string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(data);
                return result;
            }
        }
    }
}
