using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardProjectViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardProjectViewComponent(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke() 
        {
            var values = _context.Products.OrderBy(x => x.ProductId).ToList().SkipLast(5).TakeLast(5).ToList();
            return View(values);
        }
    }
}
