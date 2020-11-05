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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string searchString, int pageNum)
        {
            if (pageNum == 0)
                pageNum = 1;

            string apiKey = $"&s={searchString}&plot=full&type=movie&page=";
            var searchResults = await movieRepo.GetSearchResult(apiKey, pageNum);
                                
            return View(searchResults);
        }
    }
}
/*
 * public async Task<IActionResult> Index(string searchString)
{
    var movies = from m in _context.Movie
                 select m;

    if (!String.IsNullOrEmpty(searchString))
    {
        movies = movies.Where(s => s.Title.Contains(searchString));
    }

    return View(await movies.ToListAsync());
}
*/