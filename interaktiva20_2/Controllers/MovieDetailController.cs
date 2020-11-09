using System.Threading.Tasks;
using interaktiva20_2.Data;
using Microsoft.AspNetCore.Mvc;

namespace interaktiva20_2.Controllers
{
    public class MovieDetailController : Controller
    {
        private IMovieRepo movieRepo;

        public MovieDetailController(IMovieRepo movieRepo)
        {
            this.movieRepo = movieRepo;
        }

        [Route ("details")]

        public async Task<IActionResult> Index(string imdbId)
        {
            var viewModel = await movieRepo.GetMovieDetailViewModel(imdbId);
            return View(viewModel);
        }
    }
}
