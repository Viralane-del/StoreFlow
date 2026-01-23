using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardNavbarOnMessageViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardNavbarOnMessageViewComponent(StoreContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Messages.Where(x=>x.IsRead == false).OrderByDescending(x =>x.MessageId).Take(3).ToList();
            ViewBag.messageCount = _context.Messages.Where(x => x.IsRead == false).Count();
            return View(values);
        }
    }
}
