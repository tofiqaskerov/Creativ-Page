using CreativPage.Data;
using CreativPage.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreativPage.Areas.dashboard.Controllers
{
    [Area("dashboard")]
    public class BannerController : Controller
    {

        private readonly AppDbContext _context;

        public BannerController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var banner = _context.Banners.FirstOrDefault();
            return View(banner);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var banner = _context.Banners.FirstOrDefault();
            if(banner != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Banner banner)
        {
            
           
            _context.Banners.Add(banner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var banner = _context.Banners.FirstOrDefault(x => x.Id == id);
            if(banner == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Banner banner)
        {
            if (banner == null)
            {
                return RedirectToAction("Index");
            }
            _context.Banners.Remove(banner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
           
            if (id == null)
                return NotFound();

            var banner = _context.Banners.FirstOrDefault(x => x.Id == id);
            if (banner == null)
                 return NotFound();
            
            return View(banner); 
        }
        [HttpPost]
        public IActionResult Edit(Banner banner)
        {
            _context.Banners.Update(banner);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));  // -----   nameof(Index)   bu cur yazilada biler: "Index"   
        }




    }
}
