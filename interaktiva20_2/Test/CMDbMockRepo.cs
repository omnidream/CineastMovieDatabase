using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using interaktiva20_2.Data;
using interaktiva20_2.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;


namespace interaktiva20_2.Test
{
    public class CMDbMockRepo : ICMDbRepository
    {
        string myBasePath;

        public CMDbMockRepo(IWebHostEnvironment webHostEnv)
        {
            myBasePath = $"{ webHostEnv.ContentRootPath}\\Test\\Mockdata\\Cmdb\\";
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
    }
}
