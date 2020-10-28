using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using interaktiva20_2.Data;
using Microsoft.AspNetCore.Mvc;

namespace interaktiva20_2.Controllers
{
    public class HomeController : Controller
    {
        private ICMDbRepository cmdbRepo;
        public HomeController(ICMDbRepository cmdbRepo)
        {
            this.cmdbRepo = cmdbRepo;
        }

        public async Task<IActionResult> Index()
        {
            var topFiveFromCmdb = await cmdbRepo.GetTopRatedFiveList(); //Fem bästa
            //var mostPopularFiveFromCmdb = await cmdbRepo.GetMostPopularFiveList();
            //var mostDislikedFiveFromCmdb = await cmdbRepo.GetMostDislikedFiveList();
            
            return View(topFiveFromCmdb);
        }
    }
}
