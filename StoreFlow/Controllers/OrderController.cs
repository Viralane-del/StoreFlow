using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;

namespace StoreFlow.Controllers
{
    public class OrderController : Controller
    {
        private readonly StoreContext _context;

        public OrderController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult AllStockSmallerThen5()
        {
            bool orderStockSmallerCount = _context.Orders.All(o => o.OrderCount <= 5);
            if (orderStockSmallerCount)
            {
                ViewBag.message = "Tüm siparişlerin sayısı 5'ten küçüktür!";
            }
            else
            {
                ViewBag.message = "Bazı siparişlerin sayısı 5'den daha büyüktür.";
            }
            return View();
        }
        public IActionResult OrderListByStatus(string status = "")
        {
            var values = _context.Orders.Where(o => o.Status.Contains(status)).Include(x => x.Customer).Include(x => x.Product).ToList();
            if (!values.Any())
            {
                ViewBag.message = $"{status} Belirtilen duruma sahip sipariş bulunamadı.";
            }
            return View(values);
        }
        public IActionResult OrderListSearch(string name, string filterType)
        {
            if (filterType == "start")
            {
                var values = _context.Orders.Where(x => x.Status.StartsWith(name)).ToList();
                return View(values);
            }
            else if (filterType == "end")
            {
                var values = _context.Orders.Where(x => x.Status.EndsWith(name)).ToList();

            }
            var allValues = _context.Orders.ToList();
            return View(allValues);
        }
        public async Task<IActionResult> OrderList()
        {
            var values = await _context.Orders.Include(x => x.Product).Include(x => x.Customer).ToListAsync();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var products = await _context.Orders
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = p.Product.ProductName
                }).ToListAsync();
            ViewBag.products = products;

            var customers = await _context.Orders
               .Select(c => new SelectListItem
               {
                   Value = c.CustomerId.ToString(),
                   Text = c.Customer.CustomerName + " " + c.Customer.CustomerSurname
               }).ToListAsync();
            ViewBag.customers = customers;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            order.Status = "Sipariş Alındı";
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderList");
        }
        public async Task<IActionResult> DeleteOrder(int id) 
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateOrder(int id) 
        {
            var products = await _context.Orders
               .Select(p => new SelectListItem
               {
                   Value = p.ProductId.ToString(),
                   Text = p.Product.ProductName
               }).ToListAsync();
            ViewBag.products = products;

            var customers = await _context.Orders
               .Select(c => new SelectListItem
               {
                   Value = c.CustomerId.ToString(),
                   Text = c.Customer.CustomerName + " " + c.Customer.CustomerSurname
               }).ToListAsync();
            ViewBag.customers = customers;
            var order = await _context.Orders.FindAsync(id);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(Order order) 
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderList");
        }
        public IActionResult OrderListWithCustomerGroup() 
        {
            var customers = _context.Customers.ToList();
            var orders = _context.Orders.ToList();

            var result = from customer in customers
                         join order in orders
                         on customer.CustomerId equals order.CustomerId
                         into orderGroup
                         select new CustomerOrderViewModel
                         {
                            CustomerName = customer.CustomerName + " " + customer.CustomerSurname,
                            Orders = orderGroup.ToList()
                         };           
            return View(result.ToList());
        }
    }
}
