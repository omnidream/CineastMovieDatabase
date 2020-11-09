
namespace interaktiva20_2.Models.DTO
{
    public interface IMovieSummaryDto : ICmdbMovieDto, IMovieDetailsDto
    {
        public int MovieNumber { get; set; }
    }
}
