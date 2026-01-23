using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardNavbarOnTodoViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardNavbarOnTodoViewComponent(StoreContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Todos.Where(x => x.Status == false).OrderByDescending(x => x.TodoId).Take(5).ToList();
            ViewBag.todoCount = _context.Todos.Count();
            return View(values);
        }
    }
}
