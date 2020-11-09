using System.Threading.Tasks;

namespace interaktiva20_2.Infra
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(string endpoint);
    }
}
