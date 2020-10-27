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
        private ICMDbRepository cmdbRepository;
        public HomeController(ICMDbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
        }

        public async Task<IActionResult> Index()
        {
            var myModel = await cmdbRepository.GetToplist();
            return View(myModel);
        }
    }
}
