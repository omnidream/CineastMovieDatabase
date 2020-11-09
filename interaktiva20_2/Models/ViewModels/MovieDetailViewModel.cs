using interaktiva20_2.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace interaktiva20_2.Models.ViewModels
{
    public class MovieDetailViewModel
    {
        public string Title { get; set; }
        public string Poster { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public string imdbID { get; set; }

        [Display(Name = "Year:")]
        public string Year { get; set; }
        [Display(Name = "Runtime:")]
        public string Runtime { get; set; }
        [Display(Name = "Genre:")]
        public string Genre { get; set; }
        [Display(Name = "Director:")]
        public string Director { get; set; }
        [Display(Name = "Actors:")]
        public string Actors { get; set; }
        [Display(Name = "Story:")]
        public string Plot { get; set; }
        [Display(Name = "Language:")]
        public string Language { get; set; }
        [Display(Name = "Country:")]
        public string Country { get; set; }
        [Display(Name = "Awards:")]
        public string Awards { get; set; }
        [Display(Name = "IMDb Rating:")]
        public string ImdbRating { get; set; }

        public MovieDetailViewModel(IMovieDetailsDto movieDetails, ICmdbMovieDto myMovie)
        {
            Title = movieDetails.Title;
            imdbID = movieDetails.imdbID;
            Year = movieDetails.Year;
            Runtime = movieDetails.Runtime;
            Genre = movieDetails.Genre;
            Director = movieDetails.Director;
            Actors = movieDetails.Actors;
            Plot = movieDetails.Plot;
            Language = movieDetails.Language;
            Country = movieDetails.Country;
            Poster = movieDetails.Poster;
            Awards = movieDetails.Awards;
            ImdbRating = movieDetails.ImdbRating;
            NumberOfLikes = myMovie.NumberOfLikes;
            NumberOfDislikes = myMovie.NumberOfDislikes;
        }
    }
}
