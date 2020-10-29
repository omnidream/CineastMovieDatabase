using interaktiva20_2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Models.ViewModels
{
    public class MovieViewModel
    {
        //FÖR VARJE LISTTYP
        //Hämta från CMDB till en lista
        //Lägg i TASK-lista
        //Hämta från OMDB där ID stämmer i en foreach typ

        //Skicka retur IEnumerable-List med Lists tillbaka till View 
                        //=> MÅSTE vi det? Eller kan vi bara låta konstruktorn sätta värden till propparna så vi kommer åt dem?


        //Dessa proppar bör vi ha med eftersom det är dem vi har med i Index
        public int MovieNumber { get; set; } //Finns inte med från api men vi vill visa
        public string Title { get; set; }
        public string Poster { get; set; }
        public string ImdbRating { get; set; }
        public string Plot { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }


        public MovieViewModel(CmdbMovieDto myMovie, MovieDetailsDto movieDetails)
        {
            Title = movieDetails.Title;
            Poster = movieDetails.Poster;
            ImdbRating = movieDetails.ImdbRating;
            Plot = movieDetails.Plot;
            NumberOfLikes = myMovie.NumberOfLikes;
            NumberOfDislikes = myMovie.NumberOfDislikes;
        }


    }
}
