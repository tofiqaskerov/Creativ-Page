using CreativPage.Data;
using CreativPage.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreativPage.Areas.dashboard.Controllers
{
    [Area("dashboard")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            var selectCategory = _context.Categories.FirstOrDefault(x =>x.CategoryName == category.CategoryName);
            if(selectCategory != null)
            {
                ViewBag.CategoryExist = "Category Movcuddur";
                return View();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var category = _context.Categories.FirstOrDefault(x =>x.Id == id);
            if(id == null )
                return NotFound();
            if(category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                var portfolio = _context.Portfolios.Where(x => x.CategoryId == category.Id).ToList();
                if(portfolio != null)
                {
                    ViewBag.ExistCategory = "Bu kaqteqoriyada asagidaki portfolio olduguna gore sile bilmezsiniz";
                    ViewData["portfolios"] = portfolio;
                }
                else
                {
                    ViewBag.CategoryError = "Bilinmeyen sebebe gore error cixdi";
                }
               

            }
            return View();  
        }
    }
}
