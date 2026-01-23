using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardSidebarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
