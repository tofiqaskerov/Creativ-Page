using CreativPage.Data;
using CreativPage.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreativPage.Areas.dashboard.Controllers
{
    [Area("dashboard")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var service = _context.Services.ToList();
            return View(service);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Service services  )
        {
            _context.Services.Add(services);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Edit( int id)
        {
            var service = _context.Services.FirstOrDefault(x => x.Id == id);
            return View(service);
        }

        [HttpPost]
        public IActionResult Edit(Service services)
        {
            _context.Services.Update(services);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var service = _context.Services.FirstOrDefault(x =>x.Id == id);
            return View(service);
        }
        [HttpPost]
        public IActionResult Delete( Service services)
        {
            _context.Services.Remove(services);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
