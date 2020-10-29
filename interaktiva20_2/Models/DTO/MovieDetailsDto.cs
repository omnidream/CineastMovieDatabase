﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Models.DTO
{
    public class MovieDetailsDto
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Poster { get; set; }
        public string Awards { get; set; }
        public string ImdbRating { get; set; }
        public string imdbID { get; set; }
    }
}
