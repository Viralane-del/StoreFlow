using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardMessageViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardMessageViewComponent(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke() 
        {
            var values = _context.Messages.Where(x => x.IsRead == false).ToList();
            return View(values);
        }
    }
}
