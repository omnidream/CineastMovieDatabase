using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Models.DTO
{
    public class SearchResultDto
    {
        public List<MovieDetailsDto> Search { get; set; }

        public SearchResultDto()
        {
            
        }
    }
}
