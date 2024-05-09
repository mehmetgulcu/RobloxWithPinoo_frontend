using Microsoft.AspNetCore.Mvc;

namespace RobloxWithPinoo_UI.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
