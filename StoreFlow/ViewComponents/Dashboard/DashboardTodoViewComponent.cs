using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.Dashboard
{
    public class DashboardTodoViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public DashboardTodoViewComponent(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var todos = _context.Todos.OrderByDescending(x =>x.TodoId).Take(6).ToList();
            return View(todos);
        }
    }
}
