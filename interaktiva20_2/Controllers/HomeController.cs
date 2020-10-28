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
        public HomeController(ICMDbRepository cmdbRepo, IOMDbRepository omdbRepo)
        {
            this.cmdbRepo = cmdbRepo;
        }

        public async Task<IActionResult> Index()
        {
            var topFiveFromCmdb = await cmdbRepo.GetToplist(); //Fem bästa
            
            return View(topFiveFromCmdb);
        }
    }
}
