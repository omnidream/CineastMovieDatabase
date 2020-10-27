using interaktiva20_2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Data
{
    public class CMDbRepository : ICMDbRepository
    {
        public Task<IEnumerable<MovieDetailsDto>> GetMovieDetails()
        {
            throw new NotImplementedException(); //Ta bort sen
        }
    }
}
