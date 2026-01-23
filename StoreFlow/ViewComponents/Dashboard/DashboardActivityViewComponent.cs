using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardActivityViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardActivityViewComponent(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var activities = _context.Activities.ToList();
            return View(activities);
        }
    }
}
