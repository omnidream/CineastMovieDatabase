namespace interaktiva20_2.Models.DTO
{
    public class MovieSummaryDto
    {
        public int MovieNumber { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
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

        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }

        public MovieSummaryDto(CmdbMovieDto myMovie, MovieDetailsDto movieDetails, int movieNumber)
        {
            MovieNumber = movieNumber;
            Title = movieDetails.Title;
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
