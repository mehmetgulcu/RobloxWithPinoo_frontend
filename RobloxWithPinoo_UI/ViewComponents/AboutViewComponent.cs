using Microsoft.AspNetCore.Mvc;

namespace RobloxWithPinoo_UI.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
