using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardSettingsSidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
