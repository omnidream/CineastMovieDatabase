using interaktiva20_2.Data;
using interaktiva20_2.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace interaktiva20_2.Test
{
    public class OMDbMockRepository : IOMDbRepository
    {
        string myBasePath;

        public OMDbMockRepository(IWebHostEnvironment webHostEnv)
        {
            myBasePath = $"{ webHostEnv.ContentRootPath}\\Test\\Mockdata\\Omdb\\";
        }

        public async Task<MovieDetailsDto> GetMovieDetails()
        {
            string testFile = "OMDbMockAllRelevantMovies.json";
            var result = GetTestData<MovieDetailsDto>(testFile);
            await Task.Delay(0);
            return result;
        }

        private T GetTestData<T>(string testFile)
        {
            string path = $"{myBasePath}{testFile}";
            string data = File.ReadAllText(path);
            var result = JsonConvert.DeserializeObject<T>(data);
            return result;
        }
    }
}
