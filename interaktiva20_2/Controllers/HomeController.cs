using System.Threading.Tasks;
using interaktiva20_2.Data;
using interaktiva20_2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace interaktiva20_2.Controllers
{
    public class HomeController : Controller
    {
        private IMovieRepo movieRepo;
        private int numberOfMovies = 1;
        private int numberOfNeverRatedMovies = 1;


        public HomeController(IMovieRepo movieRepo)
        {
            this.movieRepo = movieRepo;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new MovieViewModel
            {
                TopRatedMovies = await movieRepo.GetToplist(movieRepo.GetTopRatedList(numberOfMovies).Result),
                MostPopularMovies = await movieRepo.GetToplist(movieRepo.GetMostPopularList(numberOfMovies).Result),
                NeverRatedMovies = await movieRepo.GetToplist(movieRepo.GetNeverRatedMovies(numberOfNeverRatedMovies))
            };

            return View(viewModel);
        }
    }
}
