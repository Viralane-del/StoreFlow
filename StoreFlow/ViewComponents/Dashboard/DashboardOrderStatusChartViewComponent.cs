using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardOrderStatusChartViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardOrderStatusChartViewComponent(StoreContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke() 
        {
            var result = _context.Orders.GroupBy(o => o.Status).Select(g => new OrderStatusChartViewModel
            {
                Status = g.Key,
                Count = g.Count()
            }).ToList();
            return View(result);
        }
    }
}
