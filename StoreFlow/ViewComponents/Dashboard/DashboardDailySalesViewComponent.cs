using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardDailySalesViewComponent : ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardDailySalesViewComponent(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var data = _context.Todos.GroupBy(t => t.Priority).Select(g => new ToDoPriorityChartViewModel 
            {
                Priority = g.Key.ToString(),
                Count = g.Count()
            }).ToList();
            return View(data);
        }
    }
}
