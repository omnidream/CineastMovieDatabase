using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
