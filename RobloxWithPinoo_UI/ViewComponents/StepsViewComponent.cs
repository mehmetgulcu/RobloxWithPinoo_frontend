using Microsoft.AspNetCore.Mvc;

namespace RobloxWithPinoo_UI.ViewComponents
{
    public class StepsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
