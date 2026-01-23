using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardSalesDataViewComponent : ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardSalesDataViewComponent(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var salesData = _context.Orders.OrderByDescending(y =>y.OrderId).Include(x =>x.Customer).Include(x =>x.Product).Take(5).ToList();
            return View(salesData);
        }
    }
}
