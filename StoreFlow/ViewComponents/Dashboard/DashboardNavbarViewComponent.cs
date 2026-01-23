using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardNavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
