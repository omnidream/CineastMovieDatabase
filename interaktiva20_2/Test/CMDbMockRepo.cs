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
            myBasePath = $"{ webHostEnv.ContentRootPath}\\Test\\Mockdata\\";
        }

        private T GetTestData<T>(string testFile)
        {
            string path = $"{myBasePath}{testFile}";
            string data = File.ReadAllText(path);
            var result = JsonConvert.DeserializeObject<T>(data);
            return result;
        }

        public async Task<IEnumerable<TopListDto>> GetToplist()
        {
            string testFile = "CMDbMock.json";
            var result = GetTestData<IEnumerable<TopListDto>>(testFile);
            await Task.Delay(0);
            return result;
        }
    }
}
