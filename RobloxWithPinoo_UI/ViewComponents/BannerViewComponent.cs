using Microsoft.AspNetCore.Mvc;

namespace RobloxWithPinoo_UI.ViewComponents
{
    public class BannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
