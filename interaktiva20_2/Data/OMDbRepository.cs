using interaktiva20_2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Data
{
    public class OMDbRepository : IOMDbRepository
    {
        public Task<MovieDetailsDto> GetMovieDetails()
        {
            throw new NotImplementedException();
        }
    }
}
