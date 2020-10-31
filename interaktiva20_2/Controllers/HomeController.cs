using System.Threading.Tasks;
using interaktiva20_2.Data;
using interaktiva20_2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace interaktiva20_2.Controllers
{
    public class HomeController : Controller
    {
        private IMovieRepo movieRepo;

        public HomeController(IMovieRepo movieRepo)
        {
            this.movieRepo = movieRepo;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await movieRepo.GetMovieViewModel();
            return View(viewModel);
        }
    }
}
