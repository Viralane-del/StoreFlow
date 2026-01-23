using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardInboxViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardInboxViewComponent(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke() 
        {
            var values = _context.Messages.OrderBy(x => x.MessageId).ToList().TakeLast(5).ToList();
            return View(values);
        }
    }
}
