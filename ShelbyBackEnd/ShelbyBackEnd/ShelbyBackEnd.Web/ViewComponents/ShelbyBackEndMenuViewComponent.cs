using Microsoft.AspNetCore.Mvc;
using ShelbyBackEnd.Services.Contract;

namespace ShelbyBackEnd.Web.ViewComponents
{
    public class ShelbyBackEndMenuViewComponent : ViewComponent
    {
        private readonly IBackEndMenuService _backEndMenuService;
        public ShelbyBackEndMenuViewComponent(IBackEndMenuService backEndMenuService)
        {
            _backEndMenuService = backEndMenuService    ;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = (await _backEndMenuService.GetAllMenu())
                  .ToList();
            return View(menu);
        }
    }
}
