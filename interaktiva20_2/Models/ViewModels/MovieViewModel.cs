using interaktiva20_2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Models.ViewModels
{
    public class MovieViewModel
    {

        public int MovieNumber { get; set; } = 0; //Finns inte med från api men vi vill visa
        public string Title { get; set; }
        public string Poster { get; set; }
        public string ImdbRating { get; set; }
        public string Plot { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }

        //TODO: Skapa kontroll så att data faktiskt existerar.
        public MovieViewModel(CmdbMovieDto myMovie, MovieDetailsDto movieDetails, int movieNumber)
        {
            MovieNumber = movieNumber;
            Title = movieDetails.Title;
            Poster = movieDetails.Poster;
            ImdbRating = movieDetails.ImdbRating;
            Plot = movieDetails.Plot;
            NumberOfLikes = myMovie.NumberOfLikes;
            NumberOfDislikes = myMovie.NumberOfDislikes;
        }


    }
}
