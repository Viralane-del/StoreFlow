using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardThemeSettingsViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
