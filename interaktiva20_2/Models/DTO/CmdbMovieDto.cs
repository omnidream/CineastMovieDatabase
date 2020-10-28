using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Models.DTO
{
    public class CmdbMovieDto
    {
        public string ImdbId { get; set; }
        public int numberOfLikes { get; set; }
        public int numberOfDislikes { get; set; }
    }
}
