using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardSidebarTodoViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardSidebarTodoViewComponent(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke() 
        {
            var values = _context.Todos.OrderBy(x => x.TodoId).ToList().TakeLast(18).ToList();
            return View(values);
        }
    }
}
