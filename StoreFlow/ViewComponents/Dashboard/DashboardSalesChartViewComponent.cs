using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardSalesChartViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardSalesChartViewComponent(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var chart = _context.Customers.GroupBy(x => x.CustomerCity).Select(g => new CustomerCityChartViewModel 
            {
                City = g.Key,
                Count = g.Count()
            }).ToList();    
            return View(chart);
        }
    }
}
