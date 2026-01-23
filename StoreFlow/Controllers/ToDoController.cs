using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Entities;

namespace StoreFlow.Controllers
{
    public class ToDoController : Controller
    {
        private readonly StoreContext _context;

        public ToDoController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult ToDoList()
        {
            _context.Todos.ToList();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateTodo() 
        {
            var todos = new List<Todo>
            {
                new Todo { Description = "Mail Gönder", Status = true, Priority = "İkincil" },
                new Todo { Description = "Satışlar ile ilgili rapor hazırla", Status = true, Priority = "Birincil" },
                new Todo { Description = "Toplantıya Katıl", Status = true, Priority = "Birincil" }
            };
           await  _context.Todos.AddRangeAsync(todos);
           await _context.SaveChangesAsync();
           return View();
        }
        public IActionResult ToDoAgreagatePriority() 
        {
            var priorityFirstlyTodo = _context.Todos.Where(x => x.Priority == "Birincil")
                .Select(d => d.Description).ToList();
            string result = priorityFirstlyTodo.Aggregate(string.Empty,(acc, desc) => acc + $"<li>{desc}</li>");
            ViewBag.results = result;
            return View();
        }
        public IActionResult InCompleteTask() //Prepend de Gün başında olarak kullanılıyor
        {
            var values = _context.Todos.Where(x => !x.Status).Select(x => x.Description)
             .ToList().Append("Gün sonunda tüm yapılacakları kontrol etmeyi unutmayınız, iyi günler dileriz.")
             .ToList();
            return View(values);
        }
        public IActionResult TodoChunk() 
        {
            var values = _context.Todos
                .Where(x => !x.Status)
                .ToList()
                .Chunk(2)
                .ToList();
            return View(values);
        }
        public IActionResult TodoConcat() 
        {
            var values = _context.Todos
                .Where(x => x.Priority == "Birincil")
                .ToList()
                .Concat(_context.Todos.Where(x => x.Priority == "İkincil")
                .ToList());
            return View(values);
        }
        public IActionResult TodoUnion() 
        {
            var values = _context.Todos.Where(x => x.Priority == "Birincil").ToList();
            var values2 = _context.Todos.Where(x => x.Priority == "İkincil").ToList();
            var result = values.UnionBy(values2,x=>x.Description).ToList();
            return View(result);
        }
    }
}
