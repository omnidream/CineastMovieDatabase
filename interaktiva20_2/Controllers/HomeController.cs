using System;
using System.Threading.Tasks;
using interaktiva20_2.Data;
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
            try
            {
                var viewModel = await movieRepo.GetMovieListsViewModel();
                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
