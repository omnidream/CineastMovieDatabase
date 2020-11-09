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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string searchString, int pageNum)
        {
            if (pageNum == 0)
                pageNum = 1;

            var viewModel = await movieRepo.GetSearchResultViewModel(searchString, pageNum);
            return View(viewModel);
        }

        public async Task<IActionResult> Index(int pageNum, string searchString)
        {
            var viewModel = await movieRepo.GetSearchResultViewModel(searchString, pageNum);
            return View(viewModel);
        }

    }
}