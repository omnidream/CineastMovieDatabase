using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Models.DTO
{
    public interface IMovieSummaryDto : ICmdbMovieDto, IMovieDetailsDto
    {
        public int MovieNumber { get; set; }
    }
}
