using System.Text.RegularExpressions;
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

            var cleanedSearchString = CleanFromSpecialChars(searchString);
            var singleSpaceString = CleanFromMultipleSpaces(cleanedSearchString);

            string apiKey = $"&s={singleSpaceString}&plot=full&type=movie&page=";
            var searchResults = await movieRepo.GetSearchResult(apiKey, pageNum);
                                
            return View(searchResults);
        }


        //TODO: PRIO 3 Se över möjlighet att flytta ut metoderna så vi håller controllern mer clean
        private string CleanFromSpecialChars(string searchString)
        {
            var cleanedSearchString = Regex.Replace(searchString, @"[^0-9a-öA-Ö' ]+", "");
            return cleanedSearchString;
        }

        private string CleanFromMultipleSpaces(string cleanedSearchString)
        {
            string singleSpacesString = Regex.Replace(cleanedSearchString, " {2,}", " ");
            return singleSpacesString;
        }
    }
}