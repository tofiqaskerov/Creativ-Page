using CreativPage.Data;
using CreativPage.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreativPage.Areas.dashboard.Controllers
{
    [Area("dashboard")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var about = _context.AboutBanners.FirstOrDefault();
            return View(about);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var aboutBanner = _context.AboutBanners.FirstOrDefault();
            if(aboutBanner != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(AboutBanner aboutBanner)
        {
            
            _context.AboutBanners.Add(aboutBanner);
            _context.SaveChanges(); 
            return RedirectToAction("Index");
        }
        
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var about = _context.AboutBanners.FirstOrDefault(x => x.Id == id);
            if(about == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete(AboutBanner aboutBanner)
        {
            if(aboutBanner == null)
            {
                return RedirectToAction("Index");
            }
            _context.AboutBanners.Remove(aboutBanner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var about = _context.AboutBanners.FirstOrDefault(x => x.Id == id);
            if (id == null)
                return NotFound();

            if (about == null)
                return NotFound();

            return View(about);
        }
        [HttpPost]
        public IActionResult Edit(AboutBanner aboutBanner)
        {
            _context.AboutBanners.Update(aboutBanner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detail()
        {
            return View();
        }

    }
}
