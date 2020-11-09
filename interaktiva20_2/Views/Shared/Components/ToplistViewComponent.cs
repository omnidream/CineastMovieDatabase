using interaktiva20_2.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace interaktiva20_2.Views.Shared.Components
{
    public class ToplistViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<interaktiva20_2.Models.DTO.IMovieSummaryDto> viewModel)
        {
            return await Task.FromResult((IViewComponentResult)View("Toplist", viewModel));
        }
    }
}
