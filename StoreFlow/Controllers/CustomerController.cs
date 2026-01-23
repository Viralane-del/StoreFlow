using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;


namespace StoreFlow.Controllers
{
    public class CustomerController : Controller
    {
        private readonly StoreContext _context;

        public CustomerController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult CustomerListOrderByCustomerName()
        {
            var Customers = _context.Customers.OrderBy(x => x.CustomerName + " " + x.CustomerSurname).ToList();
            return View(Customers);
        }
        public IActionResult CustomerListOrderByDescBalance() 
        {
            var customers = _context.Customers.OrderByDescending(x => x.CustomerBalance).ToList();
            return View(customers);
        }
        public IActionResult CustomerGetByCity(string city)
        {
            var customers = _context.Customers.Any(x => x.CustomerCity == city);
            if (customers) 
            {
                ViewBag.message = $"{city} şehrinde en az 1 tane müşteri var";
            }
            else 
            {
                ViewBag.message = $"{city} şehrinde hiç müşteri yok";
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCustomer(Customer Customer)
        {
            _context.Customers.Add(Customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerList");

        }
        public IActionResult DeleteCustomer(int id)
        {
            var Customer = _context.Customers.Find(id);
            if (Customer != null)
            {
                _context.Customers.Remove(Customer);
                _context.SaveChanges();
            }
            return RedirectToAction("CustomerList");
        }
        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            var value = _context.Customers.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateCustomer(Customer Customer)
        {
            _context.Customers.Update(Customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerList");
        }
        public IActionResult CustomerListByCity() 
        {
            var groupedCustomers = _context.Customers
                .ToList()
                .GroupBy(c => c.CustomerCity)
                .ToList();
            return View(groupedCustomers);
        }
        public IActionResult CustomerByCityCount() 
        {
            var query = from c in _context.Customers
                group c by c.CustomerCity  into cityGroup
                select new CustomerCityGroup 
                {
                    City = cityGroup.Key,
                    CustomerCount = cityGroup.Count()
                };
            var model = query.ToList();
            return View(model);
        }
        public IActionResult CustomerCityList() 
        {
            var values = _context.Customers.Select(x =>x.CustomerCity).Distinct().ToList();
            return View(values);
        }
        public IActionResult ParallelCustomer() 
        {
            var customer = _context.Customers.ToList();
            var result = customer.AsParallel().Where(c =>c.CustomerCity.StartsWith("A",StringComparison.OrdinalIgnoreCase));
            return View(result);
        }
        public IActionResult CustomerListExceptCityIstanbul() 
        {
            var allCustomers = _context.Customers.ToList();
            var customersListInIstanbul = _context.Customers
                .Where(x => x.CustomerCity == "Istanbul").Select(c => c.CustomerCity)
                .ToList();
            var result = allCustomers.ExceptBy(customersListInIstanbul,c => c.CustomerCity).ToList();
            return View(result);
        }
        public IActionResult CustomerListWithDefaultIfEmpty() 
        {
            var customers = _context.Customers.Where(x => x.CustomerCity == "Ankara").ToList().DefaultIfEmpty(new Customer 
            {
                CustomerId = 0,
                CustomerName = "Böyle bir müşteri yok",
                CustomerSurname = "------",
                CustomerCity = "Ankara",
            }).ToList();
            return View(customers);
        }
        public IActionResult CustomerIntersectByCity() 
        {
            var cityValues1 = _context.Customers.Where(x => x.CustomerCity == "İstanbul").Select(c => c.CustomerName + " " + c.CustomerSurname).ToList();
            var cityValues2 = _context.Customers.Where(x => x.CustomerCity == "Ankara").Select(c => c.CustomerName + " " + c.CustomerSurname).ToList();
            var result = cityValues1.Intersect(cityValues2).ToList();
            return View(result);
        }
        public IActionResult CustomerCastExample() 
        {
            var customers = _context.Customers.ToList();
            ViewBag.result = customers;
            return View();
        }
        public IActionResult CustomerListWithIndex() 
        {
            var customers = _context.Customers.ToList().Select((c, index) => new
            {
                SiraNo = index + 1,
                c.CustomerName,
                c.CustomerSurname,
                c.CustomerCity
            }).ToList();
            return View(customers);
        }
    }
}
