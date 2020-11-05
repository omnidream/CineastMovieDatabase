
namespace interaktiva20_2.Models.DTO
{
    public class CmdbMovieDto : ICmdbMovieDto
    {
        public string ImdbId { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
    }
}
