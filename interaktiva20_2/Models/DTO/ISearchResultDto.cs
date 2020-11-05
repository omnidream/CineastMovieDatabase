using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Models.DTO
{
    public interface ISearchResultDto
    {
        public string Response { get; set; }
        public string Error { get; set; }
        public List<MovieDetailsDto> Search { get; set; }

    }
}
