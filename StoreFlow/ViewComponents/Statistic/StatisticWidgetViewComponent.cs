using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.Statistic
{
    public class StatisticWidgetViewComponent:ViewComponent
    {
        private readonly StoreContext _context;

        public StatisticWidgetViewComponent(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.categoryCount = _context.Categories.Count();
            ViewBag.productMaxPrice = _context.Products.Max(p => p.ProductPrice);
            ViewBag.productMinPrice = _context.Products.Min(p => p.ProductPrice);

            ViewBag.productMaxPriceProductName = _context.Products.Where(x=> x.ProductPrice ==(_context.Products.Max(y => y.ProductPrice)))
             .Select(z=> z.ProductName).FirstOrDefault();
            ViewBag.productMinPriceProductName = _context.Products.Where(x => x.ProductPrice == (_context.Products.Min(y => y.ProductPrice)))
                .Select(z => z.ProductName).FirstOrDefault();

            ViewBag.totalSumProductStock = _context.Products.Sum(p => p.ProductStock);
            ViewBag.avgProductStock = _context.Products.Average(p => p.ProductStock).ToString("0.00");
            ViewBag.avgProductPrice = _context.Products.Average(p => p.ProductPrice).ToString("0.00");

            ViewBag.biggerPriceThen1000ProductCount = _context.Products.Where(p => p.ProductPrice > 1000).Count();
            ViewBag.getIDIs4ProductName = _context.Products.Where(p => p.ProductId == 4)
                .Select(p => p.ProductName).FirstOrDefault();
            ViewBag.stockCountBigger50AndSmaller100ProductCount = _context.Products
                .Where(p => p.ProductStock > 50 && p.ProductStock < 100).Count();

            ViewBag.totalTodoCount = _context.Todos.Count();
            ViewBag.priorityTodoCount = _context.Todos.Where(t => t.Priority == "Birincil").Count();
            ViewBag.completedTodoCount = _context.Todos.Where(t => t.Status == true).Count();

            ViewBag.totalMessageCount = _context.Messages.Count();
            ViewBag.readMessageCount = _context.Messages.Where(m => m.IsRead == true).Count();
            ViewBag.unreadMessageCount = _context.Messages.Where(m => m.IsRead == false).Count();
            return View();
        }
    }
}
