using CreativPage.Data;
using CreativPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreativPage.Areas.dashboard.Controllers
{
    [Area("dashboard")]
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;

        public PortfolioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var portfolios = _context.Portfolios.Include(x =>x.Category).ToList();
            
            return View(portfolios);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            var categories = _context.Categories.ToList();
            ViewData["categories"] = categories;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Portfolio portfolio, IFormFile NewPhoto, string category)
        {
            var fileExtation = Path.GetExtension(NewPhoto.FileName);
            if(fileExtation != ".jpg")
            {
                ViewBag.PhotoError = " Yalniz jpg formati qebul olunur";
                return View();
            }
            string myPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewPhoto.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img", myPhoto);
            using(var stream  = new FileStream(path, FileMode.Create))
            {
                NewPhoto.CopyTo(stream);
            }
            var cat = _context.Categories.FirstOrDefault(x =>x.CategoryName == category);
            portfolio.CategoryId = cat.Id;
            portfolio.PhotoURL = "Img/" + myPhoto;
            _context.Portfolios.Add(portfolio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int? id)  
        {
            var portfolios = _context.Portfolios.FirstOrDefault(x =>x.Id ==id);
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            return View(portfolios);
        }
        [HttpPost]
        public IActionResult Edit(Portfolio portfolio, IFormFile NewPhoto,string category )
        {
            var fileExtation = Path.GetExtension(NewPhoto.FileName);
            if (fileExtation != ".jpg")
            {
                ViewBag.PhotoError = " Yalniz jpg formati qebul olunur";
                return View();
            }
            string myPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewPhoto.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img", myPhoto);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                NewPhoto.CopyTo(stream);
            }
            var cat = _context.Categories.FirstOrDefault(x => x.CategoryName == category);
            portfolio.CategoryId = cat.Id;
            portfolio.PhotoURL = "Img/" + myPhoto;
            _context.Portfolios.Update(portfolio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var portfolio = _context.Portfolios.FirstOrDefault(x => x.Id == id);
            if(id == null)
                return NotFound();
            
            if(portfolio == null)
                return NotFound();
            
            return View(portfolio);
        }
        [HttpPost]
        public IActionResult Delete(Portfolio portfolio)
        {
           if(portfolio == null)
               return RedirectToAction("Index");
           
            _context.Portfolios.Remove(portfolio);
            _context.SaveChanges();
            return RedirectToAction("Index");

            
        
            
        }

          
         

    }
}
