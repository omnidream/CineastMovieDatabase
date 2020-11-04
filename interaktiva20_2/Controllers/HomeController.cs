using System;
using System.Diagnostics;
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

            ViewBag.LikeButton = "<button class=\"btnLike\" data-btn-type=\"like\" data-imdbid=\"@movie.ImdbId\"></button>";
            try
            {
                var viewModel = await movieRepo.GetMovieListsViewModel();
                return View(viewModel);
            }
            //TODO: PRIO 3 Skapa en error-sida (UI) för våra try-cath på våra controllers actionmetoder
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
