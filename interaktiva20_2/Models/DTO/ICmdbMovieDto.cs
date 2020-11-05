
namespace interaktiva20_2.Models.DTO
{
    public interface ICmdbMovieDto
    {
        public string ImdbId { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
    }
}
