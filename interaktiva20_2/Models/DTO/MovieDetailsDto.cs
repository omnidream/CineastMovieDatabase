using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Models.DTO
{
    public class MovieDetailsDto
    {
        public string MovieTitel { get; set; }
        public int MovieYear { get; set; }
        public string MovieRuntime { get; set; }
        public string MovieGenre { get; set; }
        public string MovieDirector { get; set; }
        public string MovieActors { get; set; }
        public string MoviePlot { get; set; }
        public string MovieLanguage { get; set; }
        public string MovieCountry { get; set; }
        public string MovieAwards { get; set; }
        public string MovieImdbRating { get; set; }
    }
}
