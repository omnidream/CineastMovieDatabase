using System;
using System.Threading.Tasks;
using interaktiva20_2.Data;
using interaktiva20_2.Models.DTO;
using interaktiva20_2.Models.ViewModels;
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

        //[Route ("")]
        public async Task<IActionResult> Index(string imdbId)
        {
            try
            {
                var viewModel = await movieRepo.GetMovieDetailViewModel("tt0245429");
                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "error");
            }
        }
    }
}
