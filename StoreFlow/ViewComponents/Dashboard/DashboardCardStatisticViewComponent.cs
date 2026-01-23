using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardCardStatisticViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardCardStatisticViewComponent(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke() 
        {
            ViewBag.TotalCustomer = _context.Customers.Count();
            ViewBag.TotalCategory = _context.Categories.Count();
            ViewBag.TotalProduct = _context.Products.Count();
            ViewBag.TotalOrder = _context.Orders.Count();
            ViewBag.SumOrderProductCount = _context.Orders.Sum(o => o.OrderCount);
            ViewBag.AvgCustomerBalance = _context.Customers.Average(c => c.CustomerBalance).ToString("0.00");
            return View();
        }
    }
}
