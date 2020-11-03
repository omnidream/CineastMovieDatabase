using System;
using System.Threading.Tasks;
using interaktiva20_2.Data;
using Microsoft.AspNetCore.Mvc;

namespace interaktiva20_2.Controllers
{
    public class SearchResultController : Controller
    {
        private IMovieRepo movieRepo;

        public SearchResultController(IMovieRepo movieRepo)
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
                return RedirectToAction("index", "error");
            }
        }
    }
}
