using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardChartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
