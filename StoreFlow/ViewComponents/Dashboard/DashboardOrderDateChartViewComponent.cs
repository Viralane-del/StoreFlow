using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardOrderDateChartViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardOrderDateChartViewComponent(StoreContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var chart = _context.Orders.GroupBy(o => o.OrderDate.Date).Select(g => new
            {
                RawDate = g.Key,
                Count = g.Count()
            }).OrderBy(x => x.RawDate).ToList().Select(w => new OrderDateChartViewModel
            {
                Date = w.RawDate.ToString("yyyy-MM-dd"),
                Count = w.Count
            }).ToList();
            return View(chart);
        }
    }
}
