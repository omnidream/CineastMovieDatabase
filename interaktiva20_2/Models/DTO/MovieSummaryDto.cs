namespace interaktiva20_2.Models.DTO
{
    public class MovieSummaryDto : MovieDetailsDto, IMovieSummaryDto, IMovieDetailsDto
    {
        public int MovieNumber { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public string ImdbId { get; set; }

        public MovieSummaryDto(ICmdbMovieDto myMovie, IMovieSummaryDto movieDetails, int movieNumber)
        {
            MovieNumber = movieNumber;
            Title = movieDetails.Title;
            Rated = movieDetails.Rated;
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
