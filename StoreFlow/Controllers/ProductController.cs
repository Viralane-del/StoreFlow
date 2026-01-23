using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;

namespace StoreFlow.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreContext _context;

        public ProductController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult ProductList()
        {
            var products = _context.Products.Include(x => x.Category).ToList();
            return View(products);
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {

            ViewBag.Categories = _context.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {

            ViewBag.Categories = _context.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            }).ToList();
            var product = _context.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {

            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("ProductList");

        }
        public IActionResult First5ProductList()
        {
            var products = _context.Products.Include(x => x.Category).Take(5).ToList();
            return View(products);
        }
        public IActionResult Skip4ProductList()
        {
            var products = _context.Products.Include(x => x.Category).Skip(4).Take(10).ToList();
            return View(products);
        }
        [HttpGet]
        public IActionResult CreateProductWithAttach()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProductWithAttach(Product product)
        {
            var category = new Category { CategoryId = 1 };
            _context.Categories.Attach(category);

            var productValue = new Product
            {
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductStock = product.ProductStock,
                Category = category
            };
            _context.Products.Add(productValue);
            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }
        public IActionResult ProductCount()
        {
            var value = _context.Products.LongCount();
            var lastValue = _context.Products.OrderBy(x => x.ProductId).Last();
            ViewBag.v2 = lastValue.ProductName;
            ViewBag.v = value;
            return View();
        }
        public IActionResult ProductListWithCategory()
        {
            var result = from c in _context.Categories
                         join p in _context.Products on
                         c.CategoryId equals p.CategoryId
                         select new ProductWithCategoryViewModel
                         {
                             ProductName = p.ProductName,
                             ProductStock = p.ProductStock,
                             CategoryName = c.CategoryName,

                         };
            return View(result.ToList());
        }
    }
}
